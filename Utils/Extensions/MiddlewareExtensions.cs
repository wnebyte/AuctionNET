using AuctionCore.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AuctionCore.Utils.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMySession(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }
}
