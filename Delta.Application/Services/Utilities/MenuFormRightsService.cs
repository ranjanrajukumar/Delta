using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delta.Application.Services.Utilities
{
    public class MenuFormRightsService : IMenuFormRightsService
    {
        private readonly IMenuFormRightsRepository _menuFormRightsRepository;

        public MenuFormRightsService(IMenuFormRightsRepository menuFormRightsRepository)
        {
            _menuFormRightsRepository = menuFormRightsRepository;
        }

        public async Task<List<MenuFormRightDto>> GetMenuRightsAsync(int menuId)
        {
            var rights = await _menuFormRightsRepository.GetByMenuIdAsync(menuId);

            return rights.Select(x => new MenuFormRightDto
            {
                IdCode = x.IdCode,
                ButtonId = x.ButtonId,
                ButtonText = x.ButtonText,
                DisplayName = x.DisplayName,
                Tab = x.Tab
            }).ToList();
        }
    }
}
