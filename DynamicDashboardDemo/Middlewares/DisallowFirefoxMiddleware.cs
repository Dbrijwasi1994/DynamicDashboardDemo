using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Middlewares
{
    public class DisallowFirefoxMiddleware
    {
        private RequestDelegate nextDelegate;
        public DisallowFirefoxMiddleware(RequestDelegate next) => nextDelegate = next;

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Items["Firefox"] as bool? == true)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }
    }
}
