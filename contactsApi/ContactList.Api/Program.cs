using ContactList.Core.Interfaces;
using ContactList.Infrastructure;
using ContactList.Infrastructure.Data;
using ContactList.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactListConnectionString")));

builder.Services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
builder.Services.AddTransient<IContactRepositoryAsync, ContactRepositoryAsync>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IRazorRenderService, RazorRenderService>();

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

//app.UseHttpsRedirection();
//app.UseRouting();

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



