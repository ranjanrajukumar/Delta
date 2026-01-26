using Delta.Application.Interfaces.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Delta.API.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuFormRightsController : ControllerBase
    {
        private readonly IMenuFormRightsService _menuFormRightsService;

        public MenuFormRightsController(IMenuFormRightsService menuFormRightsService)
        {
            _menuFormRightsService = menuFormRightsService;
        }

        /// <summary>
        /// Get Menu Form Rights by MenuId (Delta API)
        /// </summary>
        /// <param name="menuId">Example: STUDENT_ENTRY, PROFESSION</param>
        [HttpGet("{menuId}")]
        public async Task<IActionResult> GetMenuFormRights(int menuId)
        {
            //if (string.IsNullOrWhiteSpace(menuId))
            //    return BadRequest("MenuId is required.");

            var result = await _menuFormRightsService.GetMenuRightsAsync(menuId);
            return Ok(result);
        }
    }
}
