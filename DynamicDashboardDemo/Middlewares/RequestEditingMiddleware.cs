using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Middlewares
{
    public class RequestEditingMiddleware
    {
        private RequestDelegate nextDelegate;
        public RequestEditingMiddleware(RequestDelegate next) => nextDelegate = next;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["Firefox"] = httpContext.Request.Headers["User-Agent"].Any(v => v.Contains("Firefox"));
            await nextDelegate.Invoke(httpContext);
        }
    }
}
