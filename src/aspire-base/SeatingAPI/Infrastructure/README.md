# Migration Creation
# Useful EFCore DB Command

Below are a list of of useful commands that can be used to support the extending and management of a DB when using EFCore as a context.

## Create Migration

windows: cmd is `dotnet-ef migrations add InitModel `
linux/bash: cmd is `dotnet ef migrations add InitModel `

## Seeding
`[ContextSeeder](./ContextSeeder.cs)`ContextSeeder is the data seeding class that can be called!
Currently seeding is:
- Locations
- Staff
- Desks
- Bookings (In-Test)