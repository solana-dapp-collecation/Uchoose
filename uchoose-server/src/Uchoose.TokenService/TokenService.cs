// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.EventLogService.Interfaces;
using Uchoose.TokenService.Interfaces;
using Uchoose.TokenService.Interfaces.Requests;
using Uchoose.TokenService.Interfaces.Responses;
using Uchoose.TokenService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.TokenService
{
    /// <inheritdoc cref="ITokenService"/>.
    internal sealed class TokenService :
        ITokenService,
        ITransientService
    {
        private readonly UserManager<UchooseUser> _userManager;
        private readonly SignInManager<UchooseUser> _signInManager;
        private readonly RoleManager<UchooseRole> _roleManager;
        private readonly IEventLogService _eventLogService;
        private readonly IStringLocalizer<TokenService> _localizer;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="TokenService"/>.
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/>.</param>
        /// <param name="signInManager"><see cref="SignInManager{TUser}"/></param>
        /// <param name="roleManager"><see cref="RoleManager{TRole}"/>.</param>
        /// <param name="eventLogService"><see cref="IEventLogService"/>.</param>
        /// <param name="jwtSettings"><see cref="JwtSettings"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public TokenService(
            UserManager<UchooseUser> userManager,
            SignInManager<UchooseUser> signInManager,
            RoleManager<UchooseRole> roleManager,
            IEventLogService eventLogService,
            IOptionsSnapshot<JwtSettings> jwtSettings,
            IStringLocalizer<TokenService> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _eventLogService = eventLogService;
            _localizer = localizer;
            _jwtSettings = jwtSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress, bool withRefreshToken = true)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (user == null)
            {
                throw new EntityNotFoundException<Guid, UchooseUser>(nameof(UchooseUser.Email), request.Email, _localizer);
            }

            if (!user.IsActive)
            {
                throw new IdentityException(_localizer["User Not Active. Please contact the administrator."], statusCode: HttpStatusCode.Unauthorized);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, _userManager.SupportsUserLockout && user.LockoutEnabled);
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    throw new LockedOutException(user.LockoutEnd, _localizer["User account locked out."]);
                }

                if (signInResult.IsNotAllowed)
                {
                    if (!user.EmailConfirmed)
                    {
                        throw new IdentityException(_localizer["E-Mail not confirmed."], statusCode: HttpStatusCode.Unauthorized);
                    }

                    if (!user.PhoneNumberConfirmed)
                    {
                        throw new IdentityException(_localizer["Phone Number not confirmed."], statusCode: HttpStatusCode.Unauthorized);
                    }

                    throw new IdentityException(_localizer["Not Allowed to authorize. Please contact the administrator."], statusCode: HttpStatusCode.Unauthorized);
                }

                throw new IdentityException(_localizer["Invalid Credentials."], statusCode: HttpStatusCode.Unauthorized);
            }

            if (withRefreshToken)
            {
                user.RefreshToken = GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
                await _userManager.UpdateAsync(user);
            }

            string token = await GenerateJwtAsync(user, ipAddress);
            var response = new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
            await _eventLogService.LogCustomEventAsync(new(user.Id, "JWT Tokens generated.", user.Email));
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        /// <inheritdoc/>
        public async Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            if (request is null)
            {
                throw new IdentityException(_localizer["Invalid Client Token."], statusCode: HttpStatusCode.Unauthorized);
            }

            var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
            string userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                throw new EntityNotFoundException<Guid, UchooseUser>(nameof(UchooseUser.Email), userEmail, _localizer);
            }

            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new IdentityException(_localizer["Invalid Client Token."], statusCode: HttpStatusCode.Unauthorized);
            }

            string token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
            await _userManager.UpdateAsync(user);
            var response = new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        /// <summary>
        /// Сгенерировать refresh токен.
        /// </summary>
        /// <returns>Возвращает refresh токен.</returns>
        private static string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Сгенерировать JWT.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Возвращает JWT.</returns>
        private async Task<string> GenerateJwtAsync(UchooseUser user, string ipAddress)
        {
            return GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
        }

        /// <summary>
        /// Получить коллекцию разрешений.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Возвращает коллекцию разрешений.</returns>
        private async Task<IEnumerable<Claim>> GetClaimsAsync(UchooseUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();
            foreach (string role in roles)
            {
                roleClaims.Add(new(ClaimTypes.Role, role));
                var thisRole = await _roleManager.FindByNameAsync(role);
                var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            return new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email),
                    new("fullName", $"{user.FirstName} {user.LastName}"),
                    new(ClaimTypes.Name, user.FirstName),
                    new(ClaimTypes.Surname, user.LastName),
                    new("ipAddress", ipAddress)
                }
                .Union(userClaims)
                .Union(roleClaims)
                .Union(permissionClaims);
        }

        /// <summary>
        /// Получить защищённый авторизационный токен.
        /// </summary>
        /// <param name="signingCredentials"><see cref="SigningCredentials"/>.</param>
        /// <param name="claims">Список разрешений.</param>
        /// <returns>Возвращает авторизационный токен.</returns>
        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidateIssuer ? _jwtSettings.Issuer : null,
                audience: _jwtSettings.ValidateAudience ? _jwtSettings.Audience : null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Получить <see cref="ClaimsPrincipal"/> из "протухшего" авторизационного токена.
        /// </summary>
        /// <param name="token">Авторизационный токен.</param>
        /// <returns>Возвращает <see cref="ClaimsPrincipal"/>.</returns>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidAudience = _jwtSettings.ValidateAudience ? _jwtSettings.Audience : null,
                ValidIssuer = _jwtSettings.ValidateIssuer ? _jwtSettings.Issuer : null,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new IdentityException(_localizer["Invalid Token."], statusCode: HttpStatusCode.Unauthorized);
            }

            return principal;
        }

        /// <summary>
        /// Получить <see cref="SigningCredentials"/>.
        /// </summary>
        /// <returns>Возвращает <see cref="SigningCredentials"/>.</returns>
        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            return new(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }
    }
}