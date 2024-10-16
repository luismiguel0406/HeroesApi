using HeroesApi.Interfaces;
using HeroesApi.Services;
using HeroesApi.Middlewares;
using HeroesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddControllers();

builder.Services.AddResponseCaching();

var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts")!.Split(",");
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedHosts).AllowAnyMethod().AllowAnyHeader();
    });
});

//DB
builder.Services.DbConecctionService(builder);

//DI
builder.Services.AddScoped<IHeroRepository, HeroesRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHsts();
app.UseHttpsRedirection();

app.UseMyApplicationJsonMiddleware();

app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Middleware inline
app.Use(async (context, next) =>
{
   var port = context.Connection.LocalPort;
    Console.WriteLine(port);
    await next(context);
});

app.MapGet("/", () =>Results.Ok($"Api Listening...")) ;

/*app.Run(context=>
{
    context.Response.StatusCode = 404;
    return Task.CompletedTask;
});

*/
/*app.Run(async (context) =>
{
    await context.Response.WriteAsync($"CurrentCulture.DisplayName: {CultureInfo.CurrentCulture.DisplayName}");
        
});*/

app.Run();
