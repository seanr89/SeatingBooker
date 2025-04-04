/**
Aspire Azure AppHost Program.cs
This is the entry point for the application. It is responsible for building the application and running it.
The builder is used to create the application and add services to it within Azure

Notes: keep names to lower case, no spaces, no special characters!
*/
var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

// Automatically provision an Application Insights resource
var insights = builder.AddAzureApplicationInsights("MyApplicationInsights");

// Create the DB service and ensure Azure Flexible Server is used!
var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
                        .WithPasswordAuthentication(username, password);
var seatDb = postgres.AddDatabase("bookings");

builder.AddProject<Projects.SeatingAPI>("seatapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Production")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb)
    .WithReference(insights)
    .PublishAsAzureContainerApp((module, app) =>
    {
        // Scale to 0
        app.Template.Scale.MinReplicas = 0;
        app.Template.Scale.MaxReplicas = 2;
    });

builder.Build().Run();