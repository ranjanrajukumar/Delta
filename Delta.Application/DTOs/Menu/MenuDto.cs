using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Menu
{
    public class MenuDto
    {
        public int MenuID { get; set; }
        public string MenuTitle { get; set; } = null!;
        public string? MenuUrl { get; set; }
        public string? IconClass { get; set; }
        public int MenuOrder { get; set; }

        public List<MenuDto> Children { get; set; } = new();
    }
}
