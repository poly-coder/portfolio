#:sdk Aspire.AppHost.Sdk@13.1.2

var builder = DistributedApplication.CreateBuilder(args);

// Epic 2 ── Uncomment once the .NET Web API project is added to src/backend/backend.slnx
// var api = builder.AddProject<Projects.Api>("api");

// Epic 3 ── Uncomment once TanStack Start is scaffolded in src/frontend
// var frontend = builder.AddNpmApp("frontend", "../frontend", "dev")
//     .WithHttpEndpoint(env: "PORT");
//     // .WithReference(api);  // add once api is wired

// Epic 6 ── Uncomment once Node workers are scaffolded in src/workers
// builder.AddNpmApp("workers", "../workers", "dev");

builder.Build().Run();
