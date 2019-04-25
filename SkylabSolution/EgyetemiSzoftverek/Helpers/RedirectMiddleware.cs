using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EgyetemiSzoftverek.Helpers
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private Dictionary<string,string> RedirectUrls { get; set; }

        public RedirectMiddleware(RequestDelegate next, IOptions<Dictionary<string,string>> redirectUrlsOptions)
        {
            RedirectUrls = redirectUrlsOptions.Value;

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // if specific condition does not meet
            if (RedirectUrls.ContainsKey(context.Request.Path.ToString()))
            {
                context.Response.Redirect(RedirectUrls.GetValueOrDefault(context.Request.Path.ToString()));
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
