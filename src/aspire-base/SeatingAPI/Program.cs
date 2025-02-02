var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddTransient<LocationService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/locations", (LocationService service) => service.GetLocations())
    .WithName("GetLocations");

app.MapGet("/locations/{id}", (LocationService service, int id) => service.GetLocation(id))
    .WithName("GetLocation");

// app.UseHttpsRedirection();

app.Run();

