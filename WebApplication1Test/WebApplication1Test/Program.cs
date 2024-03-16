using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebApplication1Test.DB;
using WebApplication1Test.Models.EntityFramework;
using WebApplication1Test.Repository;
using MySqlConnector;
using Microsoft.AspNetCore.DataProtection;
using WebApplication1Test.Extensions;

// Indicate if production enviroment info should be enabled regardless of environment.
bool enableInProduction = false;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

if (env.IsProdEnv())
{
    // https://aws.amazon.com/blogs/developer/aws-ssm-asp-net-core-data-protection-provider/
    // Perisist the antiforgery token to the system manager in AWS so all the machines use the same antiforgery token and if the user is moved to another machine they will not get antiforgery errors.
    builder.Services.AddDataProtection()
        .SetApplicationName("WebApplication1Test")
        .PersistKeysToAWSSystemsManager("/Prod/WebApplication1Test/DataProtection");
}
else if (env.IsQAEnv())
{
    builder.Services.AddDataProtection()
    .SetApplicationName("WebApplication1Test")
    .PersistKeysToAWSSystemsManager("/QA/WebApplication1Test/DataProtection");
}
else
{
    builder.Services.AddDataProtection().SetApplicationName("WebApplication1Test");
    builder.Configuration.AddUserSecrets<Program>();
}

var connectionString = builder.Configuration.GetConnectionString("mysqldb");
builder.Services.AddMySqlDataSource(connectionString ?? throw new InvalidOperationException("Connection string 'mysqldb' not found."));

//Add DbContext please make sure the version is he same as scaffold-DbContext command
var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
builder.Services.AddDbContext<NewTestingContext>(options => options.UseMySql(connectionString, serverVersion));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, serverVersion));

//TO DO: // Add the database developer exception filter only in Development environment
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Identity Framework.
// TODO: Change this to true once go live.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(30);
});
builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression();
// Add antiforgery services
builder.Services.AddAntiforgery(options =>
{
    // Configure antiforgery options here
    options.HeaderName = "X-CSRF-TOKEN"; // Customize the header name for token validation
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    // Unique usernames are request but not email addresses.
    options.User.RequireUniqueEmail = true;
});

//register repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProdEnv())
{
    //It's important to note that app.UseDeveloperExceptionPage() should only be used in the development environment.
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}
else
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
