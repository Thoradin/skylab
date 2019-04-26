using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EgyetemiSzoftverek.Helpers
{
    public class ServerCache : ActionFilterAttribute, IActionFilter, IResultFilter
    {
        private string CacheName { get; set; }

        //If The View cached, we load it from there
        public override void OnActionExecuting(ActionExecutingContext context)
        {
                CacheName = (string)context.RouteData.Values["Controller"] + (string)context.RouteData.Values["Action"];

            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            ViewResult cachedView = new ViewResult();

            if (cache.TryGetValue(CacheName, out cachedView))
            {
                context.Result = cachedView;
            }
        }

        //If the View wasn't cached, we save it to there from the results 
        public override void OnResultExecuted(ResultExecutedContext context)
        {
                CacheName = (string)context.RouteData.Values["Controller"] + (string)context.RouteData.Values["Action"];

            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            IActionResult result;

            if (!cache.TryGetValue(CacheName, out result))
            {
                result = context.Result;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                // Save data in cache.
                cache.Set(CacheName, result, cacheEntryOptions);
            }
        }

        //Not needed to implement
        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }
    }
}
