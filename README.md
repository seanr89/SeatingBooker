# SeatingBooker
dotnet app to create a web-based API to allow or seats to be booked on a single day!


## Aspire

### Commands

```
dotnet run ./src/aspire-base/aspire-base.AppHost/aspire-base.AppHost.csproj
```

## Models / Design

1. Locations
    - Site location where desks are available
2. Staff
    - Individual members of staff that are able to make requests for seats
3. Desk
    - Desk information per Locatio
4. BookingRequest
    - Individual Request and State