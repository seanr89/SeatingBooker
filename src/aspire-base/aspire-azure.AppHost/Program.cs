/**
Aspire Azure AppHost Program.cs
This is the entry point for the application. It is responsible for building the application and running it.
The builder is used to create the application and add services to it within Azure

Notes: keep names to lower case, no spaces, no special characters!
*/
var builder = DistributedApplication.CreateBuilder(args);

// Create the DB service and ensure Azure Flexible Server is used!
var postgres = builder.AddPostgres("postgres")
    .PublishAsAzurePostgresFlexibleServer();    
var seatDb = postgres.AddDatabase("bookings");

builder.AddProject<Projects.SeatingAPI>("seatapi")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb).WaitFor(postgres);

builder.Build().Run();