using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WineshopManagerStarterKit.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Database configuration: try MySQL (Pomelo) first, then SQL Server, then MariaDB (Oracle provider)
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Port=3306;Database=wineshop_db;User=root;Password=;Convert Zero Datetime=true;";
var sqlServerConnection = builder.Configuration.GetConnectionString("SqlServerConnection")
    ?? "Server=localhost;Database=WineShopDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
var mariaDbConnection = builder.Configuration.GetConnectionString("MariaDbConnection")
    ?? "Server=localhost;Port=3306;Database=wineshop_db;User=root;Password=;";

string dbProvider = "mysql";

// Test if MySQL is reachable (for Pomelo)
try
{
    var testMySqlConnectionString = mySqlConnection.Replace("Database=wineshop_db", "Database=mysql");
    using var testConnection = new MySqlConnector.MySqlConnection(testMySqlConnectionString);
    testConnection.Open();
    testConnection.Close();
    Console.WriteLine("MySQL connection successful. Using Pomelo/MySQL.");
}
catch
{
    // MySQL (Pomelo) not available, try SQL Server
    try
    {
        var testSqlServerConnectionString = sqlServerConnection.Replace("Database=WineShopDb", "Database=master");
        using var testSqlConnection = new Microsoft.Data.SqlClient.SqlConnection(testSqlServerConnectionString);
        testSqlConnection.Open();
        testSqlConnection.Close();
        dbProvider = "sqlserver";
        Console.WriteLine("SQL Server connection successful. Using SQL Server.");
    }
    catch
    {
        // SQL Server not available either, fall back to MariaDB (Oracle provider)
        dbProvider = "mariadb";
        Console.WriteLine("MySQL and SQL Server not available. Falling back to MariaDB (Oracle provider).");
    }
}

switch (dbProvider)
{
    case "mysql":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(mySqlConnection!, ServerVersion.AutoDetect(mySqlConnection!)));
        break;
    case "sqlserver":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(sqlServerConnection));
        break;
    case "mariadb":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySQL(mariaDbConnection));
        break;
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configure JWT authentication
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? "ThisIsDefaultDevelopmentKeyThatShouldBeChanged123!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"]?? "WineshopManager";
var jwtAudience = builder.Configuration["Jwt:Audience"]?? "WineshopManagerUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


var app = builder.Build();

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    DbSeeder.Seed(context);

    // Seed Identity roles and admin user
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager);

    Console.WriteLine("Database initialized and seeded successfully.");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
