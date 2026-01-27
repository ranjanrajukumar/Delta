using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vidya.API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 🔹 1. Read Token from Header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                context.Items["Token"] = token;
            }

            // 🔹 2. Read User Info from Claims (JWT)
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = context.User.FindFirst(ClaimTypes.Name)?.Value;

                context.Items["UserId"] = userId;
                context.Items["Username"] = username;
            }

            await _next(context);
        }
    }
}
