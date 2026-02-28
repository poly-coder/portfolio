using System.Data.Common;
using JasperFx;
using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var postgresConnectionString = ResolvePostgresConnectionString(
    builder.Configuration,
    builder.Environment
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMarten(options =>
{
    options.Connection(postgresConnectionString);
    options.AutoCreateSchemaObjects = builder.Environment.IsDevelopment()
        ? AutoCreate.CreateOrUpdate
        : AutoCreate.None;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapDefaultEndpoints();

var summaries = new[]
{
    "Freezing",
    "Bracing",
    "Chilly",
    "Cool",
    "Mild",
    "Warm",
    "Balmy",
    "Hot",
    "Sweltering",
    "Scorching",
};

app.MapGet(
        "/weatherforecast",
        () =>
        {
            var forecast = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        }
    )
    .WithName("GetWeatherForecast");

app.Run();

return;

static string ResolvePostgresConnectionString(
    IConfiguration configuration,
    IHostEnvironment environment
)
{
    var connectionString = configuration.GetConnectionString("Default");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "No PostgreSQL connection string configured. Set ConnectionStrings:Default."
        );
    }

    return NormalizePostgresConnectionString(connectionString, environment.IsDevelopment());
}

static string NormalizePostgresConnectionString(string connectionString, bool isDevelopment)
{
    connectionString = connectionString.Trim().Trim('"', '\'');

    if (
        TryConvertPostgresUriToConnectionString(connectionString, out var convertedConnectionString)
    )
    {
        connectionString = convertedConnectionString;
    }

    var builder = new DbConnectionStringBuilder();

    try
    {
        builder.ConnectionString = connectionString;
    }
    catch (ArgumentException exception)
    {
        throw new InvalidOperationException(
            "Invalid PostgreSQL connection string. Use a key/value connection string (Host=...;Database=...;Username=...;Password=...) or a postgres:// URI.",
            exception
        );
    }

    if (!ContainsKey(builder, "SSL Mode") && !ContainsKey(builder, "SslMode"))
    {
        builder["SSL Mode"] = "Require";
    }

    if (
        isDevelopment
        && !ContainsKey(builder, "Trust Server Certificate")
        && !ContainsKey(builder, "TrustServerCertificate")
    )
    {
        builder["Trust Server Certificate"] = true;
    }

    return builder.ConnectionString;
}

static bool TryConvertPostgresUriToConnectionString(string value, out string connectionString)
{
    connectionString = string.Empty;

    if (string.IsNullOrWhiteSpace(value) || !Uri.TryCreate(value, UriKind.Absolute, out var uri))
    {
        return false;
    }

    if (
        !string.Equals(uri.Scheme, "postgres", StringComparison.OrdinalIgnoreCase)
        && !string.Equals(uri.Scheme, "postgresql", StringComparison.OrdinalIgnoreCase)
    )
    {
        return false;
    }

    var builder = new DbConnectionStringBuilder
    {
        ["Host"] = uri.Host,
        ["Port"] = uri.IsDefaultPort ? "5432" : uri.Port.ToString(),
    };

    var database = uri.AbsolutePath.Trim('/');
    if (!string.IsNullOrWhiteSpace(database))
    {
        builder["Database"] = Uri.UnescapeDataString(database);
    }

    var userInfo = uri.UserInfo.Split(':', 2);
    if (userInfo.Length > 0 && !string.IsNullOrWhiteSpace(userInfo[0]))
    {
        builder["Username"] = Uri.UnescapeDataString(userInfo[0]);
    }

    if (userInfo.Length > 1 && !string.IsNullOrWhiteSpace(userInfo[1]))
    {
        builder["Password"] = Uri.UnescapeDataString(userInfo[1]);
    }

    connectionString = builder.ConnectionString;
    return true;
}

static bool ContainsKey(DbConnectionStringBuilder builder, string key)
{
    foreach (string existingKey in builder.Keys)
    {
        if (string.Equals(existingKey, key, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }

    return false;
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
