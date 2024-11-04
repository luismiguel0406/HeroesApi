using HeroesApi.Interfaces;
using HeroesApi.Services;
using HeroesApi.Middlewares;
using HeroesApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HeroesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Token:SecretKey")!.ToArray())),
            ValidIssuer = "http://localhost:5266",
            ValidAudience = "http://localhost:4200",
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,

        };
    });
    

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

//DI Token Generator
builder.Services.AddSingleton<TokenGenerator>();

//Factory MiddleWares
builder.Services.AddTransient<ApplicationJsonMiddleware>();

var app = builder.Build();

//app.Urls.Add("https://localhost:3000");

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

app.MapGet("/", () =>Results.Ok($"Api Listening...,{builder.Configuration.GetValue<string>("Token:SecretKey")}"));
app.MapPost("/GetToken", (Users user, TokenGenerator tokengenerator) =>
{
    return tokengenerator.GenerateToken(user, builder); 
});

app.Run();
