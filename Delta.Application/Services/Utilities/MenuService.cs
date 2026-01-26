using Delta.Application.DTOs.Menu;
using Delta.Application.Interfaces.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delta.Application.Services.Utilities
{
    public class MenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuFormRightsRepository _menuFormRightsRepository;

        public MenuService(
            IMenuRepository menuRepository,
            IMenuFormRightsRepository menuFormRightsRepository)
        {
            _menuRepository = menuRepository;
            _menuFormRightsRepository = menuFormRightsRepository;
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            var menus = await _menuRepository.GetAllAsync();

            var lookup = menus.ToLookup(m => m.ParentID);

            List<MenuDto> BuildTree(int? parentId)
            {
                return lookup[parentId]
                    .OrderBy(m => m.MenuOrder)
                    .Select(m =>
                    {
                        var menuDto = new MenuDto
                        {
                            MenuID = m.MenuID,
                            MenuTitle = m.MenuTitle,
                            MenuUrl = m.MenuUrl,
                            IconClass = m.IconClass,
                            MenuOrder = m.MenuOrder,
                            Children = BuildTree(m.MenuID)
                        };

                        // 🔥 Only leaf menus have actions
                        if (!string.IsNullOrEmpty(m.MenuUrl))
                        {
                            var rights = _menuFormRightsRepository
                                .GetByMenuIdAsync(m.MenuID)
                                .Result; // safe here because already async boundary

                            // ✅ Form buttons (Tab = 0)
                            menuDto.FormButtons = rights
                                .Where(r => r.Tab == 0)
                                .Select(r => new ButtonDto
                                {
                                    IdCode = r.IdCode,
                                    ButtonId = r.ButtonId,
                                    ButtonText = r.ButtonText
                                })
                                .ToList();

                            // ✅ Reports (Tab = 2)
                            menuDto.Reports = rights
                                .Where(r => r.Tab == 2)
                                .Select(r => new ButtonDto
                                {
                                    IdCode = r.IdCode,
                                    ButtonId = r.ButtonId,
                                    ButtonText = r.ButtonText
                                })
                                .ToList();

                            // ✅ Tabs (Tab = 1) → grouped by DisplayName
                            menuDto.Tabs = rights
                                .Where(r => r.Tab == 1)
                                .GroupBy(r => r.DisplayName)
                                .Select(g => new TabDto
                                {
                                    TabName = g.Key,
                                    Buttons = g.Select(b => new ButtonDto
                                    {
                                        IdCode = b.IdCode,
                                        ButtonId = b.ButtonId,
                                        ButtonText = b.ButtonText
                                    }).ToList()
                                })
                                .ToList();
                        }

                        return menuDto;
                    })
                    .ToList();
            }

            return BuildTree(null);
        }
    }
}
