using Microsoft.EntityFrameworkCore;
using BACK.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using BACK.Utils;
using BACK.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<EmpServices>();
builder.Services.AddScoped<PayrollServices>();
builder.Services.AddLogging();
var Connet = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PayContext>(item => item.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.38-mysql"), x => x.UseNetTopologySuite()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
