using Delta.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Interfaces.Common
{
    public interface ICommonSearchService
    {
        Task<CommonSearchResponseDto> SearchAsync(
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string? otherCondition,
            string? sortBy);
    }
}
