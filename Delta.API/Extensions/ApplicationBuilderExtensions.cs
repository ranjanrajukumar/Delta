using Vidya.API.Middleware;

namespace Delta.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUserContext(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserContextMiddleware>();
        }
    }

}
