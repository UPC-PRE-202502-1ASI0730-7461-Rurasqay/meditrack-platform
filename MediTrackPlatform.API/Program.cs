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
            Console.WriteLine($"DefaultConnection from configuration: {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))}...");
            
            // 2. Try Azure-specific connection string name if DefaultConnection not found
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = builder.Configuration.GetConnectionString("AZURE_MYSQL_CONNECTIONSTRING");
                Console.WriteLine($"AZURE_MYSQL_CONNECTIONSTRING from configuration: {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))}...");
            }
            
            // 3. Try direct environment variable
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("AZURE_MYSQL_CONNECTIONSTRING");
                Console.WriteLine($"AZURE_MYSQL_CONNECTIONSTRING from environment: {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))}...");
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
                    Console.WriteLine($"Connection string from template (env var {envVarName}): {connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))}...");
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

            // 2. Handle quoted parameter values correctly - only remove quotes around the entire string, not within key-value pairs
            
            // 3. Ensure MySQL parameter names are correct
            if (connectionString.Contains("SSL Mode") && !connectionString.Contains("SslMode"))
            {
                connectionString = connectionString.Replace("SSL Mode", "SslMode");
            }
            if (connectionString.Contains("User Id") && !connectionString.Contains("User ID") && !connectionString.Contains("Uid"))
            {
                connectionString = connectionString.Replace("User Id", "Uid");
            }
            if (connectionString.Contains("Password") && !connectionString.Contains("Pwd"))
            {
                connectionString = connectionString.Replace("Password", "Pwd");
            }
            
            // 4. Normalize the connection string to MySQL format (key=value;key=value)
            // Remove any spaces around = and ; characters
            connectionString = System.Text.RegularExpressions.Regex.Replace(connectionString, @"\s*=\s*", "=");
            connectionString = System.Text.RegularExpressions.Regex.Replace(connectionString, @"\s*;\s*", ";");
            
            // Remove any extra quotes within the connection string that might interfere
            if (connectionString.Contains("'") || connectionString.Contains("\""))
            {
                // Replace quoted values with temporary placeholders and restore later if needed
                Console.WriteLine("Found quotes within connection string, normalizing format...");
                
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
                for (int i = 0; i < Math.Min(connectionString.Length, 50); i++)
                {
                    Console.Write($"[{i}]={connectionString[i]} (ASCII: {(int)connectionString[i]}) ");
                }
                Console.WriteLine();
            }
            
            throw new Exception($"Invalid connection string format: {argEx.Message}", argEx);
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

