using PaperTrade.API.Middleware;

namespace PaperTrade.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseUserSynchronization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserSynchronizationMiddleware>();
        }
    }
}