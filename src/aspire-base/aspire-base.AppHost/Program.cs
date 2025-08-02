using Microsoft.AspNetCore.Identity;

var builder = DistributedApplication.CreateBuilder(args);

// Define parameters for username and password
var myUsername = builder.AddParameter("postgresuser", secret: true); // Marking as secret is recommended
var myPassword = builder.AddParameter("mypassword", secret: true); // Marking as secret is recommended

var postgres = builder.AddPostgres("postgres", userName: myUsername, password: myPassword, port: 5432);
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