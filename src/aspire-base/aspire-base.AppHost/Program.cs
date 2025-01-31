var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").AddDatabase("seatingDb");

builder.AddProject<Projects.SeatingAPI>("SeatingAPI")
    .WithReference(postgres);

builder.Build().Run();
