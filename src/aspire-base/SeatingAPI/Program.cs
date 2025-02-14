using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.AddNpgsqlDbContext<AppDbContext>("seatingDb");
builder.Services.AddTransient<LocationService>();
builder.Services.AddTransient<StaffService>();
builder.Services.AddTransient<DeskService>();
builder.Services.AddTransient<BookingService>();



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Runs Migration and Seeding!
var app = builder.Build().MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(_ => {
        _.WithTitle("Seating API");
        _.WithTheme(ScalarTheme.Mars);
        _.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        _.Servers = [];
    });
}

// app.MapGet("/", () => "Hello World!");

app.MapGet("/healthcheck", () => "Healthy")
    .WithName("HealthCheck");

app.MapGet("/locations", (LocationService service) => service.GetLocations())
    .WithName("GetLocations");

app.MapGet("/locations/{id}", (LocationService service, int id) => service.GetLocation(id))
    .WithName("GetLocation");

app.UseHttpsRedirection();

app.Run();

