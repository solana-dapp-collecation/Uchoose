// ------------------------------------------------------------------------------------------------------
// <copyright file="ExcelService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Uchoose.ExcelService.Interfaces;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.ExcelService
{
    /// <inheritdoc cref="IExcelService"/>
    internal sealed class ExcelService : IExcelService, IScopedService
    {
        private readonly IStringLocalizer<ExcelService> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExcelService"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ExcelService(IStringLocalizer<ExcelService> localizer)
        {
            _localizer = localizer;
        }

        #region ExportAsync

        /// <inheritdoc/>
        public async Task<IResult<string>> ExportAsync<TEntityId, TEntity>(ExportRequest<TEntityId, TEntity> request)
                where TEntity : IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var p = new ExcelPackage();
            p.Workbook.Properties.Author = "Uchoose";
            p.Workbook.Worksheets.Add(request.SheetName);
            var ws = p.Workbook.Worksheets[0];
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            var headers = request.Mappers.Keys.Select(x => x).ToList();

            if (request.CheckProperties)
            {
                var entityProperties = IExportable<TEntityId, TEntity>.ExportableProperties();
                var errors = new List<string>();
                foreach (string header in headers)
                {
                    if (!entityProperties.ContainsKey(header))
                    {
                        errors.Add(string.Format(_localizer["Property '{0}' does not exist in exported entity!"], header));
                    }
                }

                if (errors.Count > 0)
                {
                    return await Result<string>.FailAsync(errors);
                }
            }

            var dataList = request.Data.ToList();
            int rowIndex = request.DataFirstRowNumber - 1;
            foreach (var item in dataList)
            {
                int colIndex = request.TitlesFirstColNumber;
                rowIndex++;

                IEnumerable<(string Header, (object Object, int Order) Value)> results = headers.Select(header => (header, request.Mappers[header](item)));

                foreach ((string header, var value) in results.OrderBy(x => x.Value.Order))
                {
                    var headerCell = ws.Cells[request.TitlesRowNumber, colIndex];
                    if (string.IsNullOrWhiteSpace(headerCell.Value?.ToString()))
                    {
                        var fill = headerCell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);

                        var border = headerCell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                                border.Left.Style =
                                    border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[request.TitlesRowNumber, colIndex].Value = header;
                    }

                    ws.Cells[rowIndex, colIndex++].Value = value.Object;
                }
            }

            using (var autoFilterCells = ws.Cells[request.TitlesRowNumber, request.TitlesFirstColNumber, dataList.Count + request.DataFirstRowNumber - 1, headers.Count + request.TitlesFirstColNumber - 1])
            {
                autoFilterCells.AutoFilter = true;
                autoFilterCells.AutoFitColumns();
            }

            byte[] byteArray = await p.GetAsByteArrayAsync();
            return await Result<string>.SuccessAsync(Convert.ToBase64String(byteArray), _localizer["Export Success"]);
        }

        #endregion ExportAsync

        #region ImportAsync

        /// <inheritdoc/>
        public async Task<IResult<IEnumerable<TEntity>>> ImportAsync<TEntityId, TEntity>(ImportRequest<TEntityId, TEntity> request)
                where TEntity : IEntity<TEntityId>, IImportable<TEntityId, TEntity>, new()
        {
            var result = new List<TEntity>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var p = new ExcelPackage();
            request.DataStream.Position = 0;
            await p.LoadAsync(request.DataStream);
            var ws = p.Workbook.Worksheets[request.SheetName];
            if (ws == null)
            {
                return await Result<IEnumerable<TEntity>>.FailAsync(string.Format(_localizer["Sheet with name '{0}' does not exist!"], request.SheetName));
            }

            int lastColumnNumber = request.TitlesLastColNumber ?? Math.Max(ws.Dimension.End.Column, ws.SelectedRange.End.Column);
            if (request.TitlesFirstColNumber > lastColumnNumber)
            {
                return await Result<IEnumerable<TEntity>>.FailAsync(string.Format(_localizer["Titles first column number ({0}) should be less than or equal to {1}!"], request.TitlesFirstColNumber, lastColumnNumber));
            }

            var dt = new DataTable();
            foreach (var firstRowCell in ws.Cells[request.TitlesRowNumber, request.TitlesFirstColNumber, request.TitlesRowNumber, lastColumnNumber])
            {
                dt.Columns.Add(firstRowCell.Text);
            }

            var headers = request.Mappers.Keys.Select(x => x).ToList();
            var entityProperties = IImportable<TEntityId, TEntity>.ImportableProperties();
            var errors = new List<string>();
            foreach (string header in headers)
            {
                if (!dt.Columns.Contains(header))
                {
                    errors.Add(string.Format(_localizer["Required header '{0}' does not exist in table with requested titles columns range!"], header));
                }

                if (request.CheckProperties && !entityProperties.ContainsKey(header))
                {
                    errors.Add(string.Format(_localizer["Property '{0}' does not exist in imported entity!"], header));
                }
            }

            if (errors.Count > 0)
            {
                return await Result<IEnumerable<TEntity>>.FailAsync(errors);
            }

            int lastRowNumber = request.DataLastRowNumber ?? Math.Max(ws.Dimension.End.Row, ws.SelectedRange.End.Row);

            for (int rowNum = request.DataFirstRowNumber; rowNum <= lastRowNumber; rowNum++)
            {
                try
                {
                    var wsRow = ws.Cells[rowNum, request.TitlesFirstColNumber, rowNum, lastColumnNumber];
                    var row = dt.Rows.Add();
                    var item = (TEntity)Activator.CreateInstance(typeof(TEntity));
                    bool rowIsEmpty = true;
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - request.TitlesFirstColNumber] = cell.Text;
                        if (rowIsEmpty && cell.Text.IsPresent())
                        {
                            rowIsEmpty = false;
                        }
                    }

                    if (!rowIsEmpty)
                    {
                        headers.ForEach(x => request.Mappers[x](row, item));
                        result.Add(item);
                    }
                }
                catch (Exception e)
                {
                    return await Result<IEnumerable<TEntity>>.FailAsync(result, _localizer[e.Message]);
                }
            }

            return await Result<IEnumerable<TEntity>>.SuccessAsync(result, _localizer["Import Success"]);
        }

        #endregion ImportAsync
    }
}