var builder = DistributedApplication.CreateBuilder(args);

// var username = builder.AddParameter("username", secret: true);
// var password = builder.AddParameter("password", secret: true);

var postgres = builder.AddPostgres("postgres", port: 5432);
var databaseName = "bookings";
var creationScript = $$"""
    -- Create the database
    CREATE DATABASE {{databaseName}};

    """;

// postgres.WithPgAdmin(c => c.WithHostPort(5050).WaitFor(postgres));
var seatDb = postgres.AddDatabase("bookings")
                .WithCreationScript(creationScript);

builder.AddProject<Projects.SeatingAPI>("seatapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Debug")
    .WithExternalHttpEndpoints()
    .WithReference(seatDb).WaitFor(seatDb);

builder.Build().Run();