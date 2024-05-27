using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.Data;

var builder = WebApplication.CreateBuilder(args);

// Get proper connection string from environment variable or appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException(
                           "Connection string 'DefaultConnection' not found."
                           );

if (connectionString == "GET_FROM_ENV")
{
    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                       throw new InvalidOperationException(
                           "Environment variable 'DB_CONNECTION_STRING' not found."
                           );
}

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
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