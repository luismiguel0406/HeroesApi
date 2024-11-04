
namespace HeroesApi.Middlewares
{
    public  class ApplicationJsonMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            if (!context.Request.HasJsonContentType())
            {
                context.Request.Headers.ContentType = "application/json";              
            }

            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Append("Authorization-test", Guid.NewGuid().ToString());
                return Task.CompletedTask;
            }, context);


            await _next(context);
        }

       
    }
    public static class ApplicationJsonMiddlewareExtension
    {
        public static IApplicationBuilder UseMyApplicationJsonMiddleware(this IApplicationBuilder builder) {

          return  builder.UseMiddleware<ApplicationJsonMiddleware>();
        }
    }
}
