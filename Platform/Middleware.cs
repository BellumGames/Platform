/*namespace Platform
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate next;
        public QueryStringMiddleWare(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                await context.Response.WriteAsync("Class-based Middleware \n");
            }
            await next(context);

            if (next != null)//poate sau poate sa nu fie terminal
            {
                await next(context);
            }
        }
    }
}*/

using Microsoft.Extensions.Options;
namespace Platform
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate? next;
        // ...statements omitted for brevity... 
    }
    public class LocationMiddleware
    {
        private RequestDelegate next;
        private MessageOptions options;
        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> opts)
        {
            next = nextDelegate;
            options = opts.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response
                .WriteAsync($"{options.CityName}, {options.CountryName}");
            }
            else
            {
                await next(context);
            }
        }
    }
}