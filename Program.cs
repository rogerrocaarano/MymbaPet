using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using c18_98_m_csharp.Services.MailKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using c18_98_m_csharp.Services.DbSeeder;
using c18_98_m_csharp.Services.MedicalHistory;
using c18_98_m_csharp.Services.Pets;

var builder = WebApplication.CreateBuilder(args);

// Builder configuration sources
builder.Configuration
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables("DOTNET_")
.AddEnvironmentVariables("MYMBAAPP_");

// Add services to the container.
// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Email
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailSender, MailService>();

// Identity
builder.Services.AddDefaultIdentity<AppUser>(
options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Database seeder
builder.Services.Configure<DbSeederSettings>(builder.Configuration.GetSection("DbSeederSettings"));
builder.Services.AddScoped<IDbSeeder, DbSeeder>();

// Use case services
builder.Services.AddScoped<TutorPetsManager>();
builder.Services.AddScoped<PatientsManager>();
builder.Services.AddScoped<ClinicalEntryManager>();
builder.Services.AddScoped<ClinicalHistoryManager>();

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