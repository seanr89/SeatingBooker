# SeatingBooker Aspire Base

This project is a .NET Aspire-based backend API for managing seating bookings, staff, desks, and locations. It uses Entity Framework Core with PostgreSQL and is designed for easy local development and deployment.

## Project Structure

```
aspire-base.sln                # Solution file
aspire-base.AppHost/           # Aspire AppHost for local orchestration
aspire-azure.AppHost/          # Azure-specific AppHost for cloud deployment
aspire-base.ServiceDefaults/   # Shared service defaults and extensions
SeatingAPI/                    # Main Web API project
SeatingAPI.Tests/              # Unit tests for the API
```

### Key Folders
- **SeatingAPI/**: Contains the main API logic, controllers, services, DTOs, and EF Core setup.
- **aspire-base.AppHost/**: Local orchestration and configuration for Aspire.
- **aspire-azure.AppHost/**: Azure deployment configuration.
- **aspire-base.ServiceDefaults/**: Common service configuration and extensions.
- **SeatingAPI.Tests/**: Test project for API endpoints and logic.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- (Optional) Docker for containerized local development

## Getting Started

### 1. Clone the Repository
```sh
git clone <your-repo-url>
cd aspire-base
```

### 2. Set Up the Database
- Update the connection string in `SeatingAPI/appsettings.Development.json` if needed.
- Ensure PostgreSQL is running and accessible.

### 3. Run Database Migrations & Seed Data
Migrations and seeding are run automatically on app startup.

### 4. Run the API Locally
```sh
dotnet run --project SeatingAPI/SeatingAPI.csproj
```
Or use Aspire AppHost for orchestrated local run:
```sh
dotnet run --project aspire-base.AppHost/aspire-base.AppHost.csproj
```

### 5. Build the Solution
```sh
dotnet build
```

### 6. Run Tests
```sh
dotnet test SeatingAPI.Tests/SeatingAPI.Tests.csproj
```

## API Documentation
- OpenAPI/Swagger UI is available at `/swagger` when running locally.
- Scalar API Reference is available at `/scalar`.

## CORS
CORS is enabled to allow any origin, method, and header (suitable for web and mobile clients).

## Notes
- The API uses Newtonsoft.Json with reference loop handling for EF Core models.
- Service registration and dependency injection are handled in `Program.cs`.
- For Azure deployment, see `aspire-azure.AppHost/` and `azure.yaml`.

---

For more details, see inline comments in the code and the `next-steps.md` files in each AppHost folder.
