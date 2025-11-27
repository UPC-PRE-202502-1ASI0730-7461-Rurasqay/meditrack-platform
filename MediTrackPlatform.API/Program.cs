using MediTrackPlatform.API.Devices.Infrastructure.Interfaces.ASP.Configuration;
using MediTrackPlatform.API.Relatives.Infrastructure.Interfaces.ASP.Configuration;
using MediTrackPlatform.API.Shared.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using MediTrackPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using MediTrackPlatform.API.Organization.Infrastructure.Interfaces.ASP.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    // Use full type name for schema IDs to avoid conflicts between types with the same name in different namespaces
    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

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
            // Try to get the connection string from Azure App Service connection strings
            connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"DefaultConnection from configuration before processing: {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 50))}...");
            
            // If the connection string is the template pattern, replace it with the actual connection string
            if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("#{") && connectionString.EndsWith("}#"))
            {
                // Extract environment variable name between #{ and }#
                var envVarName = connectionString.Substring(2, connectionString.Length - 4);
                Console.WriteLine($"Found placeholder pattern, extracting environment variable: {envVarName}");
                
                // Get the actual connection string from environment variables
                var actualConnectionString = Environment.GetEnvironmentVariable(envVarName);
                if (!string.IsNullOrEmpty(actualConnectionString))
                {
                    connectionString = actualConnectionString;
                    Console.WriteLine($"Using connection string from environment variable: {envVarName}");
                }
                else 
                {
                    Console.WriteLine($"Environment variable {envVarName} not found, trying to get connection string directly");
                    // Try to get the connection string directly using the name from the placeholder
                    connectionString = builder.Configuration.GetConnectionString(envVarName);
                    
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new Exception($"Could not find connection string with name '{envVarName}' in Configuration or Environment Variables.");
                    }
                }
            }
            
            // If still not found, try with the explicit AZURE_MYSQL_CONNECTIONSTRING name
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = builder.Configuration.GetConnectionString("AZURE_MYSQL_CONNECTIONSTRING");
                Console.WriteLine($"Using connection string from AZURE_MYSQL_CONNECTIONSTRING configuration: {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))}...");
            }
            
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Database connection string is not set in the configuration.");

            // Debug: Print the connection string before cleaning
            Console.WriteLine("Original connection string format:");
            for (int i = 0; i < Math.Min(connectionString.Length, 50); i++) {
                Console.Write($"[{i}]={connectionString[i]} (ASCII: {(int)connectionString[i]}) ");
            }
            Console.WriteLine();
            
            // Fix common connection string format issues
            
            // 1. Handle quotes and trim whitespace
            connectionString = connectionString.Trim();
            
            // Remove surrounding quotes if present
            if (connectionString.StartsWith("'") && connectionString.EndsWith("'"))
            {
                connectionString = connectionString.Trim('\'');
            }
            if (connectionString.StartsWith("\"") && connectionString.EndsWith("\""))
            {
                connectionString = connectionString.Trim('"');
            }

            // 2. Fix MySQL parameter names to match expected format
            if (connectionString.Contains("SSL Mode") && !connectionString.Contains("SslMode"))
            {
                connectionString = connectionString.Replace("SSL Mode", "SslMode");
            }
            if (connectionString.Contains("User Id") && !connectionString.Contains("User ID") && !connectionString.Contains("Uid"))
            {
                connectionString = connectionString.Replace("User Id", "Uid");
            }
            
            // 3. Normalize the connection string to MySQL format (key=value;key=value)
            // Remove any spaces around = and ; characters
            connectionString = System.Text.RegularExpressions.Regex.Replace(connectionString, @"\s*=\s*", "=");
            connectionString = System.Text.RegularExpressions.Regex.Replace(connectionString, @"\s*;\s*", ";");
            
            // 4. Handle quoted values correctly
            if (connectionString.Contains("'") || connectionString.Contains("\""))
            {
                // Split by semicolons to get key-value pairs
                var pairs = connectionString.Split(';');
                for (var i = 0; i < pairs.Length; i++)
                {
                    if (!string.IsNullOrEmpty(pairs[i]))
                    {
                        var parts = pairs[i].Split('=', 2);
                        if (parts.Length == 2)
                        {
                            // Trim quotes from values but not from keys
                            var key = parts[0];
                            var value = parts[1].Trim('\'', '"');
                            pairs[i] = $"{key}={value}";
                        }
                    }
                }
                connectionString = string.Join(";", pairs);
            }
            
            // Debug: Print the cleaned connection string
            Console.WriteLine("Normalized connection string format:");
            for (int i = 0; i < Math.Min(connectionString.Length, 50); i++) {
                Console.Write($"[{i}]={connectionString[i]} (ASCII: {(int)connectionString[i]}) ");
            }
            Console.WriteLine();
            
            Console.WriteLine($"Final connection string keys: {string.Join(", ", connectionString.Split(';').Select(p => p.Split('=')[0]))}");
            
            // Use the connection string with MySQL
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
        catch (ArgumentException argEx)
        {
            // Provide more details about the connection string format error
            Console.WriteLine($"Connection string format error: {argEx.Message}");
            if (connectionString != null)
            {
                Console.WriteLine($"Connection string length: {connectionString.Length}");
                Console.WriteLine($"Connection string ASCII values:");
                for (int i = 0; i < Math.Min(connectionString.Length, 50); i++) {
                    Console.Write($"[{i}]={connectionString[i]} (ASCII: {(int)connectionString[i]}) ");
                }
                Console.WriteLine();
            }
            
            throw new Exception($"Invalid connection string format: {argEx.Message}", argEx);
        }
    });

// Add Open API Configuration

// Add context-specific services
builder.AddSharedContextServices();
builder.AddDevicesContextServices();
builder.AddOrganizationContextServices();
builder.AddRelativesContextServices();

// Mediator Configuration
builder.AddCortexConfigurationServices();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    // Diagnostic logging: Print EF model entity types and their table names
    Console.WriteLine("EF Model Entity Types and Table Names:");
    var entityTypes = context.Model.GetEntityTypes();
    foreach (var entityType in entityTypes)
    {
        var tableName = entityType.GetTableName();
        Console.WriteLine($"Entity: {entityType.ClrType.Name}, Table: {tableName}");
    }
    
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

