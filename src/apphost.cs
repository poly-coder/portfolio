#:sdk Aspire.AppHost.Sdk@13.1.2
#:project backend/Backend.Api/Backend.Api.csproj

using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddSupabaseOptions();

var api = builder.AddApiProject();

// Epic 3 ── Uncomment once TanStack Start is scaffolded in src/frontend
// var frontendDir = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "frontend"));
// var frontend = builder.AddNpmApp("frontend", frontendDir, "dev")
//     .WithHttpEndpoint(env: "PORT");
//     // .WithReference(api);  // add once api is wired

// Epic 6 ── Uncomment once Node workers are scaffolded in src/workers
// var workersDir = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "workers"));
// builder.AddNpmApp("workers", workersDir, "dev");

builder.Build().Run();

file sealed class SupabaseOptions
{
    public const string SectionName = "SUPABASE";

    [Required]
    [ConfigurationKeyName("URL")]
    public string Url { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("PUBLISHABLE_KEY")]
    public string PublishableKey { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("SECRET_KEY")]
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("DB_HOST")]
    public string DbHost { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("DB_PORT")]
    public string DbPort { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("DB_NAME")]
    public string DbName { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("DB_USER")]
    public string DbUser { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("DB_PASSWORD")]
    public string DbPassword { get; set; } = string.Empty;

    [Required]
    [ConfigurationKeyName("CONNECTION_STRING")]
    public string ConnectionString { get; set; } = string.Empty;

    public string MartenConnectionString
    {
        get
        {
            if (!TryBuildMartenConnectionString(out var connectionString))
            {
                throw new InvalidOperationException(
                    "SUPABASE__CONNECTION_STRING is required and must be a valid PostgreSQL connection string or postgres:// URI."
                );
            }

            return connectionString;
        }
    }

    public bool HasRequiredValues()
    {
        return !string.IsNullOrWhiteSpace(Url)
            && !string.IsNullOrWhiteSpace(PublishableKey)
            && !string.IsNullOrWhiteSpace(SecretKey)
            && !string.IsNullOrWhiteSpace(DbHost)
            && !string.IsNullOrWhiteSpace(DbPort)
            && !string.IsNullOrWhiteSpace(DbName)
            && !string.IsNullOrWhiteSpace(DbUser)
            && !string.IsNullOrWhiteSpace(DbPassword)
            && !string.IsNullOrWhiteSpace(ConnectionString);
    }

    public bool TryBuildMartenConnectionString(out string connectionString)
    {
        connectionString = string.Empty;

        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            return false;
        }

        try
        {
            connectionString = NormalizePostgresConnectionString(ConnectionString);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    private static string NormalizePostgresConnectionString(string value)
    {
        var connectionString = value.Trim().Trim('"', '\'');

        if (
            TryConvertPostgresUriToConnectionString(
                connectionString,
                out var convertedConnectionString
            )
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
                "Invalid PostgreSQL connection string. Use key/value pairs (Host=...;Database=...;Username=...;Password=...) or a postgres:// URI.",
                exception
            );
        }

        if (!ContainsKey(builder, "SSL Mode") && !ContainsKey(builder, "SslMode"))
        {
            builder["SSL Mode"] = "Require";
        }

        return builder.ConnectionString;
    }

    private static bool TryConvertPostgresUriToConnectionString(
        string value,
        out string connectionString
    )
    {
        connectionString = string.Empty;

        if (
            string.IsNullOrWhiteSpace(value) || !Uri.TryCreate(value, UriKind.Absolute, out var uri)
        )
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

    private static bool ContainsKey(DbConnectionStringBuilder builder, string key)
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
}

file static class DistributedApplicationExtensions
{
    public static IDistributedApplicationBuilder AddSupabaseOptions(
        this IDistributedApplicationBuilder builder
    )
    {
        builder
            .Services.AddOptions<SupabaseOptions>()
            .Bind(builder.Configuration.GetSection(SupabaseOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(
                options => options.HasRequiredValues(),
                "All SUPABASE__* values must be set and non-empty."
            )
            .Validate(
                options => options.TryBuildMartenConnectionString(out _),
                "SUPABASE__CONNECTION_STRING must be a valid PostgreSQL connection string or postgres:// URI."
            )
            .ValidateOnStart();

        return builder;
    }

    public static IResourceBuilder<ProjectResource> AddApiProject(
        this IDistributedApplicationBuilder builder
    )
    {
        var api = builder.AddProject<Projects.Backend_Api>("api");

        api.WithEnvironment(context =>
        {
            SupabaseOptions options = context
                .ExecutionContext.ServiceProvider.GetRequiredService<IOptions<SupabaseOptions>>()
                .Value;
            context.EnvironmentVariables.Add(
                "ConnectionStrings__Default",
                options.MartenConnectionString
            );
        });

        return api;
    }
}
