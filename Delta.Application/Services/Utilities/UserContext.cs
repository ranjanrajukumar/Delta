using Delta.Application.Interfaces.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Application.Services.Utilities
{

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId =>
            _contextAccessor.HttpContext?.Items["UserId"]?.ToString();

        public string Username =>
            _contextAccessor.HttpContext?.Items["Username"]?.ToString();

        public string Token =>
            _contextAccessor.HttpContext?.Items["Token"]?.ToString();

    }
}
