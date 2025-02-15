using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using API;
using API.Middleware;
using API.SwaggerFilter;
using Application;
using Application.Contracts;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Infrastructure.Persistence;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkedJob API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  }
              },
              new string[] {}
        }
    });
    c.OperationFilter<AllowAnonymousOperationFilter>();
});

builder.Services.AddInfrastructureServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//        //ApplicationDbInitializer.SeedRoles(roleManager).Wait();
//        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
//        //string[] roleNames = { "Superadmin", "Admin", "User" };
//        await ApplicationDbInitializer.SeedRoles(roleManager);
//    }
//    catch (Exception ex)
//    {
//        // Handle exceptions if needed
//        Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
//    }
//}







app.UseMiddleware<TokenMiddleware>(); // Use custom middleware to append Bearer prefix

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Register the middleware

app.UseAuthentication();
app.UseAuthorization();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.MapControllers();

app.Run();
