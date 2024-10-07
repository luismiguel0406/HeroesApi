
namespace PokeApi.Middlewares
{
    public  class ApplicationJsonMiddleware
    {
        private readonly RequestDelegate _next;

        public ApplicationJsonMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.HasJsonContentType())
            {
                context.Request.Headers.ContentType = "application/json";
                context.Request.Headers["token-test"] = Guid.NewGuid().ToString();
              
            }

            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Append("token-test", Guid.NewGuid().ToString().ToUpper());
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
