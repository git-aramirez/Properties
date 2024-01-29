using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Properties.Infraestructure.Configuration;
using Properties.Api.IServices;
using Properties.Core.Services;
using Properties.Domain;
using Properties.Domain.IRepositories;
using Properties.Domain.Repositories;
using Properties.Infraestructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name="Authorization",
        Type=SecuritySchemeType.Http,
        Scheme="basic",
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description="Basic Auth Header"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id="basic"
                }
            },
            new string[]{ }
        }
    });
});

builder.Services.AddDbContext<PropertiesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
builder.Services.AddScoped<IPropertyTraceService, PropertyTraceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

builder.Services.AddAuthentication("BasicAuthentication").AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions,
    Properties.Api.Security.BasicAuthHandler>("BasicAuthentication", null);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropertiesDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
