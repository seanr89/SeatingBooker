var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .PublishAsAzurePostgresFlexibleServer();    
var seatDb = postgres.AddDatabase("bookings");

builder.AddProject<Projects.SeatingAPI>("SeatingAPI")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb).WaitFor(postgres);

builder.Build().Run();