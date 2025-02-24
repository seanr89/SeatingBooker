var builder = DistributedApplication.CreateBuilder(args);

// var username = builder.AddParameter("username", secret: true);
// var password = builder.AddParameter("password", secret: true);

var postgres = builder.AddPostgres("postgres");

// postgres.WithPgAdmin(c => c.WithHostPort(5050).WaitFor(postgres));
var seatDb = postgres.AddDatabase("bookings");

builder.AddProject<Projects.SeatingAPI>("SeatingAPI")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb).WaitFor(postgres);

builder.Build().Run();