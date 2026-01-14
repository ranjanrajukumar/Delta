using Delta.Application.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delta.API.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await _menuService.GetMenuTreeAsync();
            return Ok(menus);
        }
    }
}
