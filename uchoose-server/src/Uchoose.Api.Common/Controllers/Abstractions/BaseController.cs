// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uchoose.Api.Common.Attributes.Logging;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Abstractions
{
    /// <summary>
    /// Базовый контроллер.
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route(BasePath + "/[controller]")]
    [LogContextProperties(LogContextProperties.UserId | LogContextProperties.UserEmail | LogContextProperties.UserIp)]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Базовый путь для роутинга.
        /// </summary>
        protected internal const string BasePath = "api/v{version:apiVersion}";

        /// <summary>
        /// Общий тэг для операций, связанных с расширенными атрибутами.
        /// </summary>
        protected internal const string ExtendedAttributesTag = "ExtendedAttributes";

        /// <summary>
        /// Общий тэг для операций, связанных с логами событий.
        /// </summary>
        protected internal const string EventLogsTag = "EventLogs";

        /// <summary>
        /// Общий тэг для операций, связанных с данными аудита.
        /// </summary>
        protected internal const string AuditTag = "Audit";

        private IMediator _mediatorInstance;
        private IMapper _mapperInstance;
        private IDateTimeService _dateTimeServiceInstance;

        /// <summary>
        /// <see cref="IMediator"/>.
        /// </summary>
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        /// <summary>
        /// <see cref="IMapper"/>.
        /// </summary>
        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService(typeof(IMapper)) as IMapper;

        /// <summary>
        /// <see cref="IMapper"/>.
        /// </summary>
        protected IDateTimeService DateTimeService => _dateTimeServiceInstance ??= HttpContext.RequestServices.GetService(typeof(IDateTimeService)) as IDateTimeService;

        /// <summary>
        /// Версия API из запроса.
        /// </summary>
        protected string ApiVersion => (HttpContext.GetRequestedApiVersion() ?? new ApiVersion(1, 0)).ToString();

        /// <summary>
        /// Экспортировать сущность с данными.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TQuery">Тип запроса, отправляемого в слой приложения.</typeparam>
        /// <typeparam name="TFilter">Тип фильтра для экспорта с пагинацией.</typeparam>
        /// <param name="filter">Фильтр для экспорта сущности с пагинацией в файл.</param>
        /// <response code="200">Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</response>
        public virtual async Task<IActionResult> ExportAsync<TEntityId, TEntity, TQuery, TFilter>([FromQuery] TFilter filter)
            where TEntity : class, IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
            where TQuery : class, IRequest<Result<string>>, IMapFromTo<TFilter, TQuery>, IPaginated, new()
            where TFilter : ExportPaginationFilter<TEntityId, TEntity>
        {
            var request = Mapper.Map<TQuery>(filter);
            var response = await Mediator.Send(request);

            if (filter.ReturnAsFileStream)
            {
                byte[] bytes = response.Data.FromBase64();
                var stream = new MemoryStream(bytes);
                return new FileStreamResult(stream, Utils.Constants.Exporting.MediaTypeNames.Application.Xlsx)
                {
                    FileDownloadName = $"{typeof(TEntity).GetGenericTypeName()}_ExportResult_{DateTimeService.NowUtc:yyyy-MM-dd_hh-mm-ss}.xlsx",
                    LastModified = DateTimeService.NowUtcOffset
                };
            }

            return Ok(response);
        }
    }
}