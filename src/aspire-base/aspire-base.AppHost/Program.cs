var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").AddDatabase("seatingDb", "seatingDb");

builder.AddProject<Projects.SeatingAPI>("SeatingAPI")
    .WithReference(postgres);

builder.Build().Run();
