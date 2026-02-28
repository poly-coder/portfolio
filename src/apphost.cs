#:sdk Aspire.AppHost.Sdk@13.1.2

var builder = DistributedApplication.CreateBuilder(args);

// Epic 2 ── Supabase configuration contract (consumed when api/frontend/workers wiring is enabled).
// Keep variable names aligned with .env.example and project docs.
var supabaseConfig = new
{
    Url = builder.Configuration["SUPABASE_URL"],
    PublishableKey = builder.Configuration["SUPABASE_PUBLISHABLE_KEY"],
    SecretKey = builder.Configuration["SUPABASE_SECRET_KEY"],
    DbHost = builder.Configuration["SUPABASE_DB_HOST"],
    DbPort = builder.Configuration["SUPABASE_DB_PORT"],
    DbName = builder.Configuration["SUPABASE_DB_NAME"],
    DbUser = builder.Configuration["SUPABASE_DB_USER"],
    DbPassword = builder.Configuration["SUPABASE_DB_PASSWORD"],
    ConnectionString = builder.Configuration["SUPABASE_CONNECTION_STRING"],
};
_ = supabaseConfig;

// Epic 2 ── Uncomment once the .NET Web API project is added to src/backend/backend.slnx
// var api = builder.AddProject<Projects.Api>("api");

// Epic 3 ── Uncomment once TanStack Start is scaffolded in src/frontend
// var frontendDir = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "frontend"));
// var frontend = builder.AddNpmApp("frontend", frontendDir, "dev")
//     .WithHttpEndpoint(env: "PORT");
//     // .WithReference(api);  // add once api is wired

// Epic 6 ── Uncomment once Node workers are scaffolded in src/workers
// var workersDir = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "workers"));
// builder.AddNpmApp("workers", workersDir, "dev");

builder.Build().Run();
