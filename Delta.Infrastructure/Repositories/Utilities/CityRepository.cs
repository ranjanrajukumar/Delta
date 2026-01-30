using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using Delta.Infrastructure.Persistence.Dapper;
using System.Collections.Generic;
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
            const string query =
                "SELECT CityId, CityName FROM city WHERE delstatus = 0";

            return await _context.GetAllAsync<CityDto>(query, new { });
        }

        public async Task<CityDto?> GetByIdAsync(int cityId)
        {
            const string query =
                "SELECT CityId, CityName FROM city WHERE CityId = @CityId AND delstatus = 0";

            return await _context.GetAsync<CityDto>(query, new { CityId = cityId });
        }

        public async Task<int> AddAsync(CityDto cityDto)
        {
            const string query = @"
                INSERT INTO city (CityName, delstatus)
                VALUES (@CityName, 0);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _context.ExecuteScalarAsync<int>(query, cityDto);
        }

        public async Task<bool> UpdateAsync(CityDto cityDto)
        {
            const string query = @"
                UPDATE city
                SET CityName = @CityName
                WHERE CityId = @CityId AND delstatus = 0";

            var rows = await _context.EditDataAsync(query, cityDto);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int cityId)
        {
            const string query =
                "UPDATE city SET delstatus = 1 WHERE CityId = @CityId";

            var rows = await _context.EditDataAsync(query, new { CityId = cityId });
            return rows > 0;
        }
    }
}
