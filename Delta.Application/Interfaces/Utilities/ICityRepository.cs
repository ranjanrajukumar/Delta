using Delta.Application.DTOs.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Utilities
{
    public interface ICityRepository
    {
        // Get all cities
        Task<IEnumerable<CityDto>> GetAllAsync();

        // Get city by Id
        Task<CityDto?> GetByIdAsync(int cityId);

        // Insert city
        Task<int> AddAsync(CityDto cityDto);

        // Update city
        Task<bool> UpdateAsync(CityDto cityDto);

        // Soft delete city
        Task<bool> DeleteAsync(int cityId);
    }
}
