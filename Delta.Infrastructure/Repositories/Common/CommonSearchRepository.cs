using Delta.Application.DTOs.Common;
using Delta.Application.Interfaces.Common;
using Delta.Infrastructure.Persistence.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Delta.Infrastructure.Repositories.Common
{
    public class CommonSearchRepository : ICommonSearchRepository
    {
        private readonly IDapperContext _context;

        public CommonSearchRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<CommonSearchResponseDto> SearchAsync(
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string otherCondition,
            string sortBy)
        {
            // Split display columns
            var columns = displayColumns
                .Split(',')
                .Select(x => x.Trim())
                .ToList();

            // Call stored procedure using Dapper
            var result = await _context.GetAllAsync<dynamic>(
                "usp_CommonSearch",
                new
                {
                    TableName = tableName,
                    ColumnId = columnId,
                    DisplayColumns = displayColumns,
                    SearchTerm = searchTerm ?? string.Empty,
                    OtherCondition = otherCondition ?? string.Empty,
                    SortBy = sortBy ?? string.Empty
                }
            );

            var data = new List<CommonSearchRowDto>();

            foreach (var row in result)
            {
                var dict = (IDictionary<string, object>)row;

                var dto = new CommonSearchRowDto
                {
                    Id = Convert.ToInt32(dict["Id"])
                };

                foreach (var col in columns)
                {
                    dto.Columns[col] = dict[col]?.ToString();
                }

                data.Add(dto);
            }

            return new CommonSearchResponseDto
            {
                DisplayName = displayName,
                Headers = columns,
                Data = data
            };
        }
    }
}
