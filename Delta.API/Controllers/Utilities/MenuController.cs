using Delta.Application.Interfaces.Utilities;
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
        private readonly IUserContext _userContext;
        public MenuController(MenuService menuService, IUserContext userContext)
        {
            _menuService = menuService;
            _userContext = userContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var userId = _userContext.UserId;
            var username = _userContext.Username;
            var token = _userContext.Token;
            var menus = await _menuService.GetMenuTreeAsync();
            return Ok(menus);
        }
    }
}
