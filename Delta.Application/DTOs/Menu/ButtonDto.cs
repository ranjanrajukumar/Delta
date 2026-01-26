using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Menu
{
    public class ButtonDto
    {
        public int IdCode { get; set; }
        public string ButtonId { get; set; } = null!;
        public string ButtonText { get; set; } = null!;
    }
}
