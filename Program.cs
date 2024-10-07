using Microsoft.AspNetCore.Mvc;
using PokeApi.Interfaces;
using PokeApi.Middlewares;
using PokeApi.Services;
using System.Globalization;

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
builder.Services.AddScoped<IElement, ElementTeller>();
builder.Services.AddKeyedScoped<IElement, ElementTeller>("Teller");


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
/*
app.Use(async (context, next) =>
{
    var cultureQuery = context.Request.Query["culture"];
    if(!string.IsNullOrEmpty(cultureQuery))
    {
        var culture = new CultureInfo(cultureQuery!);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }

    await next(context);
});*/

app.MapGet("/test", () =>Results.Ok("Success")) ;

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
