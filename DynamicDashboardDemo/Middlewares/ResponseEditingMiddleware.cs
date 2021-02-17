using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Middlewares
{
    public class ResponseEditingMiddleware
    {
        private RequestDelegate nextDelegate;
        public ResponseEditingMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await nextDelegate.Invoke(httpContext);
            if (httpContext.Response.StatusCode == 401)
            {
                await httpContext.Response.WriteAsync("Mozilla Firefox browser is not authorized for running this application. Please use another browser in order to use the application.");
            }
            else if (httpContext.Response.StatusCode == 404)
            {
                await httpContext.Response.WriteAsync("No Response Generated");
            }
        }
    }
}
