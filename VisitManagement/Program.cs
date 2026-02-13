using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;
using VisitManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (connectionString?.Contains(".db") == true)
    {
        options.UseSqlite(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
});

// Add Email Service
builder.Services.AddScoped<IEmailService, EmailService>();

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Seed roles and default admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<ApplicationDbContext>();
        var configuration = services.GetRequiredService<IConfiguration>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var isSqlite = connectionString?.Contains(".db") == true;
        
        logger.LogInformation("Initializing database...");
        logger.LogInformation($"Using {(isSqlite ? "SQLite" : "SQL Server")} database");
        
        // Ensure database is created
        context.Database.EnsureCreated();
        
        logger.LogInformation("Database created successfully");
        
        // Create roles if they don't exist
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                logger.LogInformation($"Created role: {roleName}");
            }
        }
        
        // Check if admin user exists
        var adminEmail = "admin@visitmanagement.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            // Create admin user
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Administrator",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now
            };
            
            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            
            if (result.Succeeded)
            {
                // Add to Admin role
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogInformation("Created default admin user");
            }
        }
        else
        {
            // Ensure existing admin has Admin role
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
        
        logger.LogInformation("Database seeding completed successfully");
    }
    catch (Microsoft.Data.SqlClient.SqlException sqlEx)
    {
        logger.LogError(sqlEx, "SQL Server connection error occurred while seeding the database.");
        logger.LogError("==============================================================================");
        logger.LogError("DATABASE CONNECTION ERROR");
        logger.LogError("==============================================================================");
        logger.LogError("Could not connect to SQL Server. This usually means:");
        logger.LogError("1. SQL Server is not running");
        logger.LogError("2. The connection string has incorrect server name or credentials");
        logger.LogError("3. SQL Server is not configured to accept remote connections");
        logger.LogError("");
        logger.LogError("Current connection string (check appsettings.json or appsettings.Development.json):");
        var configuration = services.GetRequiredService<IConfiguration>();
        var connStr = configuration.GetConnectionString("DefaultConnection");
        // Mask password in log
        if (connStr != null)
        {
            var maskedConnStr = System.Text.RegularExpressions.Regex.Replace(
                connStr, 
                @"Password=([^;]*)", 
                "Password=****");
            logger.LogError($"  {maskedConnStr}");
        }
        logger.LogError("");
        logger.LogError("SOLUTIONS:");
        logger.LogError("----------");
        logger.LogError("Option 1 (Recommended for Development): Switch to SQLite");
        logger.LogError("  - Open appsettings.Development.json");
        logger.LogError("  - Change DefaultConnection to: \"Data Source=visitmanagement.db\"");
        logger.LogError("  - SQLite requires no installation and works immediately!");
        logger.LogError("");
        logger.LogError("Option 2: Fix SQL Server Connection");
        logger.LogError("  - Verify SQL Server is running (check Services or SQL Server Configuration Manager)");
        logger.LogError("  - Update connection string with correct server name and credentials");
        logger.LogError("  - See SQL_SERVER_SETUP.md for detailed setup instructions");
        logger.LogError("==============================================================================");
        throw; // Re-throw to stop the application
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
        logger.LogError("See the error details above for more information.");
        throw; // Re-throw to stop the application
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Visits}/{action=Index}/{id?}");

app.Run();
