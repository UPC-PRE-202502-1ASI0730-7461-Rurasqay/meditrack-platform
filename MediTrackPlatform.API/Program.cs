using MediTrackPlatform.API.Shared.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Add Database Connection

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Verify Database Connection String
            if (connectionString is null)
                // Stop the application if the connection string is not set.
                throw new Exception("Database connection string is not set.");
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        string? connectionString = null;
        try
        {
            // Try different connection string sources and names
            
            // 1. Try standard DefaultConnection from configuration
            connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            // 2. Try Azure-specific connection string name if DefaultConnection not found
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = builder.Configuration.GetConnectionString("AZURE_MYSQL_CONNECTIONSTRING");
            }
            
            // 3. Try direct environment variable
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("AZURE_MYSQL_CONNECTIONSTRING");
            }
            
            // 4. Try template approach from appsettings.json if still not found
            if (string.IsNullOrEmpty(connectionString))
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                    
                var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionStringTemplate))
                    throw new Exception("Database connection string template is not set in the configuration.");
                    
                if (connectionStringTemplate.StartsWith("#{") && connectionStringTemplate.EndsWith("}#"))
                {
                    var envVarName = connectionStringTemplate.Substring(2, connectionStringTemplate.Length - 4);
                    connectionString = Environment.GetEnvironmentVariable(envVarName);
                    if (string.IsNullOrEmpty(connectionString))
                        throw new Exception($"Environment variable '{envVarName}' is not set or is empty.");
                }
                else
                {
                    connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
                }
            }
            
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Database connection string is not set in the configuration.");
            
            // Validate connection string format - remove quotes if they're present
            // Azure sometimes adds extra quotes around connection string values
            if (connectionString.StartsWith("'") && connectionString.EndsWith("'"))
            {
                connectionString = connectionString.Trim('\'');
            }
            
            // Check for other common format issues in Azure MySQL connection strings
            connectionString = connectionString.Replace("\"", ""); // Remove any double quotes
            
            // Ensure SSL Mode is properly formatted if present
            if (connectionString.Contains("SSL Mode") && !connectionString.Contains("SslMode"))
            {
                connectionString = connectionString.Replace("SSL Mode", "SslMode");
            }
            
            // Use the connection string with MySQL
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
        catch (ArgumentException argEx)
        {
            // Provide more details about the connection string format error
            Console.WriteLine($"Connection string format error: {argEx.Message}");
            Console.WriteLine($"Connection string (partial): {connectionString?.Substring(0, Math.Min(connectionString.Length, 20))}...");
            throw new Exception($"Invalid connection string format. Please check your Azure MySQL connection string format: {argEx.Message}", argEx);
        }
    });

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();