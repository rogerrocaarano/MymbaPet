using c18_98_m_csharp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using c18_98_m_csharp.Models.Identity;
using c18_98_m_csharp.Services.MailKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using c18_98_m_csharp.Services.DbSeeder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Builder configuration sources
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables("DOTNET_")
    .AddEnvironmentVariables("MYMBAAPP_");

// Add services to the container.
// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Email
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailSender, MailService>();

// Identity
builder.Services.AddDefaultIdentity<AppUser>(
        options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Database seeder
builder.Services.Configure<DbSeederSettings>(builder.Configuration.GetSection("DbSeederSettings"));
builder.Services.AddScoped<IDbSeeder, DbSeeder>();

// Domain services
builder.Services.AddScoped<PetsManager>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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
app.MapRazorPages();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
    await seeder.MigrateDatabase();
    await seeder.SeedRoles();
    await seeder.AddRoleToAdminUser();
}

app.Run();