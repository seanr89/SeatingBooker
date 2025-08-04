using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Scalar.AspNetCore;
using SeatingAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from the .env file
// Load environment variables from .env file if in development mode
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Loading environment variables from .env file");
    Env.Load();
}

//var credentialsFileLocation = builder.Configuration.GetValue<string>("GoogleCredentialsFileLocation");
var firebaseProjectName = builder.Configuration.GetValue<string>("FirebaseProjectName");
var firebaseApiKey = builder.Configuration.GetValue<string>("FirebaseApiKey");

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
DependencyInjection.AddSeatingApiServices(builder.Services);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"https://securetoken.google.com/flutauth-a041b";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://securetoken.google.com/flutauth-a041b",
        ValidateAudience = true,
        ValidAudience = firebaseProjectName,
        ValidateLifetime = true
    };
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddEndpointsApiExplorer();

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

if(!app.Environment.IsDevelopment()){
    // The following line enables Application Insights telemetry collection.
    builder.Services.AddApplicationInsightsTelemetry();
}

// Moved out of Debug mode as its quite useful in production too!!
app.MapOpenApi();
app.MapScalarApiReference(_ =>
{
    _.WithTitle("Booking API");
    _.WithTheme(ScalarTheme.Mars);
    _.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    _.Servers = [];
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

