using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Utilities
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Category { get; set; }
        
        public string UserType { get; set; }
    }
}
