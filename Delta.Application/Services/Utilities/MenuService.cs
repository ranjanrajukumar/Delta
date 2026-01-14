using Delta.Application.DTOs.Menu;
using Delta.Application.Interfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Services.Utilities
{
    public class MenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            var menus = await _menuRepository.GetAllAsync();

            var lookup = menus.ToLookup(m => m.ParentID);

            List<MenuDto> BuildTree(int? parentId)
            {
                return lookup[parentId]
                    .Select(m => new MenuDto
                    {
                        MenuID = m.MenuID,
                        MenuTitle = m.MenuTitle,
                        MenuUrl = m.MenuUrl,
                        IconClass = m.IconClass,
                        MenuOrder = m.MenuOrder,
                        Children = BuildTree(m.MenuID)
                    })
                    .ToList();
            }

            return BuildTree(null);
        }
    }
}
