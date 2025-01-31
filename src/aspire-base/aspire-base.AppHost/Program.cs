var builder = DistributedApplication.CreateBuilder(args);

var SeatingAPI = builder.AddProject<Projects.SeatingAPI>("SeatingAPI");

builder.Build().Run();
