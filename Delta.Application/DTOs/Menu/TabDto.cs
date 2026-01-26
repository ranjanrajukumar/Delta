using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Menu
{
    public class TabDto
    {
        public string TabName { get; set; } = null!;
        public List<ButtonDto> Buttons { get; set; } = new();
    }
}
