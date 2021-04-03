using Evolutional.Project.Admin.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SimpleInjector;
using System.Net;
using System.Threading.Tasks;
namespace Evolutional.Project.Admin.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower().EndsWith("login") || context.Request.Path.Value.ToLower().EndsWith("filter"))
            {
                await _next.Invoke(context);
                return;
            }

            string authorizationToken = context.Request.Headers["Authorization"];
            authorizationToken = FormattedAuthorizetionToken(authorizationToken);

            if (string.IsNullOrEmpty(authorizationToken))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            if (!TokenValidator.Validate(authorizationToken))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Usuário não autenticado.");
                return;
            }

            await _next.Invoke(context);
        }

        private static string FormattedAuthorizetionToken(string authorizationToken) => authorizationToken?.Replace("Bearer", string.Empty)?.Trim();
    }
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
