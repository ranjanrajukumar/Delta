using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.DTOs.Utilities
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
