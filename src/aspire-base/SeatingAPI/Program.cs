using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Aspire Requirements
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "bookings");
// Reference looping handle due to EF Core DB Context loops in models!
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Include cors support for later mapping
builder.Services.AddCors();

// Add Service Injections here
builder.Services.AddTransient<LocationService>();
builder.Services.AddTransient<StaffService>();
builder.Services.AddTransient<DeskService>();
builder.Services.AddTransient<BookingService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Runs Migration and Seeding!
var app = builder.Build();

// Allow any origin etc.. (Flutters Webview needs this)
app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
);

// Run the migration and seeding of the database
app.MigrateDatabaseAndSeed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    Console.WriteLine("Development Mode");
}
else{
    // The following line enables Application Insights telemetry collection.
    builder.Services.AddApplicationInsightsTelemetry();
}

// Moved out of Debug mode as its quite useful in production too!!
app.MapOpenApi();
app.MapScalarApiReference(_ => {
    _.WithTitle("Booking API");
    _.WithTheme(ScalarTheme.Mars);
    _.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    _.Servers = [];
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

