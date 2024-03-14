using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1Test.Models.EntityFramework;
// Replace with the namespace where ProductService is defined

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));//please make sure the version is he same as scaffold-DbContext command
var connectionString = builder.Configuration.GetConnectionString("mysqldb");
builder.Services.AddDbContext<NewTestingContext>(options => options.UseMySql(connectionString, serverVersion));
var app = builder.Build();

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
