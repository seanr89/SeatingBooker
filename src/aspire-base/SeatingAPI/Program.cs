using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "bookings");
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddTransient<LocationService>();
builder.Services.AddTransient<StaffService>();
builder.Services.AddTransient<DeskService>();
builder.Services.AddTransient<BookingService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Runs Migration and Seeding!
var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development Mode");
    app.MapOpenApi();
    app.MapScalarApiReference(_ => {
        _.WithTitle("Booking API");
        _.WithTheme(ScalarTheme.Mars);
        _.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        _.Servers = [];
    });
}

app.MapGet("/healthcheck", () => "App Healthy")
    .WithName("HealthCheck");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

