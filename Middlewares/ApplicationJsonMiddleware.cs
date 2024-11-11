
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
