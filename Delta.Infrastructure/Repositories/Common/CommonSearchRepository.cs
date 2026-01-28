using Delta.Application.DTOs.Common;
using Delta.Application.Interfaces.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Delta.Infrastructure.Repositories.Common
{
    public class CommonSearchRepository : ICommonSearchRepository
    {
        private readonly string _connectionString;

        public CommonSearchRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
            var columns = displayColumns.Split(',')
                                        .Select(x => x.Trim())
                                        .ToList();

            var data = new List<CommonSearchRowDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("usp_CommonSearch", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TableName", tableName);
            cmd.Parameters.AddWithValue("@ColumnId", columnId);
            cmd.Parameters.AddWithValue("@DisplayColumns", displayColumns);
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm ?? "");
            cmd.Parameters.AddWithValue("@OtherCondition", otherCondition ?? "");
            cmd.Parameters.AddWithValue("@SortBy", sortBy ?? "");

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new CommonSearchRowDto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Columns = new Dictionary<string, string>()
                };

                foreach (var col in columns)
                    row.Columns[col] = reader[col]?.ToString();

                data.Add(row);
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
