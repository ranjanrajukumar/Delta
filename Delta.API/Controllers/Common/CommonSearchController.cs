using Delta.Application.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace Delta.API.Controllers.Common
{
    [ApiController]
    [Route("api/common-search")]
    public class CommonSearchController : ControllerBase
    {
        private readonly ICommonSearchService _service;

        public CommonSearchController(ICommonSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string tableName,
            [FromQuery] string columnId,
            [FromQuery] string displayColumns,
            [FromQuery] string displayName,
            [FromQuery] string searchTerm = "",
            [FromQuery] string? otherCondition = null,
            [FromQuery] string? sortBy = null)
        {
            var result = await _service.SearchAsync(
                tableName,
                columnId,
                displayColumns,
                displayName,
                searchTerm,
                otherCondition,
                sortBy);

            return Ok(result);
        }
    }
}
