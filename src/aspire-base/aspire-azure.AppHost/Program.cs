/**
Aspire Azure AppHost Program.cs
This is the entry point for the application. It is responsible for building the application and running it.
The builder is used to create the application and add services to it within Azure

Notes: keep names to lower case, no spaces, no special characters!
*/
var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

// Create the DB service and ensure Azure Flexible Server is used!
// var postgres = builder.AddPostgres("postgres")
//     .PublishAsAzurePostgresFlexibleServer();  
var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
                        .WithPasswordAuthentication(username, password);
var seatDb = postgres.AddDatabase("bookings");

builder.AddProject<Projects.SeatingAPI>("seatapi")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb)
    .PublishAsAzureContainerApp((module, app) =>
    {
        // Scale to 0
        app.Template.Scale.MinReplicas = 0;
        app.Template.Scale.MaxReplicas = 2;
    });

builder.Build().Run();