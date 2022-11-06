using ContactList.Core.Interfaces;
using ContactList.Application;
using ContactList.Infrastructure;
using ContactList.Infrastructure.Persistance;
using ContactList.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using ContactList.Api;
using ContactList.Application.Contracts;
using ContactList.Api.Services;
using System.Reflection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7139", "http://localhost:8000", "http://localhost:5000", "https://localhost:")
                                .AllowAnyHeader()
                                .AllowAnyOrigin()
                                .AllowAnyMethod();
        });
});

// Add services to the container.

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactListConnectionString")));

//builder.Services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
//builder.Services.AddTransient<IContactRepositoryAsync, ContactRepositoryAsync>();

builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
//builder.Services.AddScoped<IRazorRenderService, RazorRenderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();



app.Run();



