using Delta.Application.DTOs.Common;
using Delta.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Services.Common
{
    public class CommonSearchService : ICommonSearchService
    {
        private readonly ICommonSearchRepository _repository;

        public CommonSearchService(ICommonSearchRepository repository)
        {
            _repository = repository;
        }

        public Task<CommonSearchResponseDto> SearchAsync(
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string otherCondition,
            string sortBy)
        {
            return _repository.SearchAsync(
                tableName,
                columnId,
                displayColumns,
                displayName,
                searchTerm,
                otherCondition,
                sortBy);
        }
    }
}
