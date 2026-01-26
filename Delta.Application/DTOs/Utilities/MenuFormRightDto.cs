using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Utilities
{
    public class MenuFormRightDto
    {
        public int IdCode { get; set; }        
        public int MenuId { get; set; }      
        public string ButtonId { get; set; }     
        public string ButtonText { get; set; }    
        public string DisplayName { get; set; }   
        public int Tab { get; set; }
    }
}
