using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Delta.Infrastructure.Persistence.Dapper;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infrastructure.Repositories.Utilities
{
    public class CityRepository : ICityRepository
    {
        private readonly IDapperContext _context;

        public CityRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var sql = new StringBuilder();
            sql.Append("SELECT CityId, CityName ");
            sql.Append("FROM City ");
            sql.Append("WHERE DelStatus = 0;");

            return await _context.GetAllAsync<CityDto>(
                sql.ToString(),
                null
            );
        }

        public async Task<CityDto?> GetByIdAsync(int cityId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT CityId, CityName ");
            sql.Append("FROM City ");
            sql.Append("WHERE CityId = @CityId ");
            sql.Append("AND DelStatus = 0;");

            return await _context.GetAsync<CityDto>(
                sql.ToString(),
                new { CityId = cityId }
            );
        }

        public async Task<int> AddAsync(CityDto cityDto)
        {
            var sql = new StringBuilder();
            sql.Append("INSERT INTO City (CityName, DelStatus) ");
            sql.Append("VALUES (@CityName, 0); ");
            sql.Append("SELECT CAST(SCOPE_IDENTITY() AS INT);");

            return await _context.ExecuteScalarAsync<int>(
                sql.ToString(),
                cityDto
            );
        }

        public async Task<bool> UpdateAsync(CityDto cityDto)
        {
            var sql = new StringBuilder();
            sql.Append("UPDATE City ");
            sql.Append("SET CityName = @CityName ");
            sql.Append("WHERE CityId = @CityId ");
            sql.Append("AND DelStatus = 0;");

            var rows = await _context.EditDataAsync(
                sql.ToString(),
                cityDto
            );

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int cityId)
        {
            var sql = new StringBuilder();
            sql.Append("UPDATE City ");
            sql.Append("SET DelStatus = 1 ");
            sql.Append("WHERE CityId = @CityId;");

            var rows = await _context.EditDataAsync(
                sql.ToString(),
                new { CityId = cityId }
            );

            return rows > 0;
        }
    }
}
