
using Microsoft.EntityFrameworkCore;

public static class ContextSeeder
{
    public static async Task SeedData(AppDbContext context)
    {
        Console.WriteLine("Seeding data");
        await SeedLocations(context);
        await SeedStaff(context);
        await SeedDesks(context);
        
    }

    static async Task SeedLocations(AppDbContext context)
    {
        if (await context.Locations.AnyAsync())
        {
            return;
        }
        List<Location> locations = new List<Location>
        {
            new Location
            {
                Name = "London Office",
                Active = true,
                SeatingCount = 10
            },
            new Location
            {
                Name = "Belfast Office",
                Active = true,
                SeatingCount = 5
            },
            new Location
            {
                Name = "Dublin Office",
                Active = true,
                SeatingCount = 5
            },
            new Location
            {
                Name = "New York Office",
                Active = true,
                SeatingCount = 5
            },
            new Location
            {
                Name = "San Francisco Office",
                Active = true,
                SeatingCount = 5
            },
            new Location
            {
                Name = "Sydney Office",
                Active = true,
                SeatingCount = 5
            },
        };
        await context.Locations.AddRangeAsync(locations);
        await context.SaveChangesAsync();
    }

    static async Task SeedDesks(AppDbContext context)
    {
        if(await context.Desks.AnyAsync())
        {
            return;
        }
        List<Desk> desks = [
            new Desk
            {
                Id = 1,
                LocationId = 1,
                Name = "Desk 1",
                Active = true,
                IsHotDesk = true
            },
            new Desk
            {
                Id = 2,
                LocationId = 1,
                Name = "Desk 2",
                Active = true,
                IsHotDesk = false,
                StaffId = 2
            },
            new Desk
            {
                Id = 3,
                LocationId = 1,
                Name = "Desk 3",
                Active = true,
                IsHotDesk = true
            },
            new Desk
            {
                Id = 4,
                LocationId = 1,
                Name = "Desk 4",
                Active = true,
                IsHotDesk = true
            },
            new Desk
            {
                Id = 5,
                LocationId = 1,
                Name = "Desk 5",
                Active = true,
                IsHotDesk = false,
                StaffId = 2
            },
            new Desk
            {
                Id = 6,
                LocationId = 1,
                Name = "Desk 6",
                Active = true,
                IsHotDesk = true
            },
            new Desk
            {
                Id = 7,
                LocationId = 1,
                Name = "Desk 7",
                Active = true,
                IsHotDesk = true
            },
            new Desk
            {
                Id = 8,
                LocationId = 1,
                Name = "Desk 8",
                Active = true,
                IsHotDesk = true
            },
        ];
        await context.Desks.AddRangeAsync(desks);
        await context.SaveChangesAsync();
    }

    static async Task SeedStaff(AppDbContext context)
    {
        if(await context.Staff.AnyAsync())
        {
            return;
        }

        List<Staff> staff = new List<Staff>
        {
            new() {
                Id = 1,
                Name = "Staff 1",
                Email = "staff1@email.com",
                Active = true,
                LocationId = 1
            },
            new() {
                Id = 2,
                Name = "Sean Rafferty",
                Email = "srafferty89@email.com",
                Active = true,
                LocationId = 1
            },new() {
                Id = 3,
                Name = "Staff 3",
                Email = "staff3@email.com",
                Active = true,
                LocationId = 1
            },new() {
                Id = 4,
                Name = "Staff 4",
                Email = "staff4@email.com",
                Active = true,
                LocationId = 1
            },new() {
                Id = 5,
                Name = "Staff 5",
                Email = "staff5@email.com",
                Active = true,
                LocationId = 1
            },new() {
                Id = 6,
                Name = "Staff 6",
                Email = "staff6@email.com",
                Active = true,
                LocationId = 1
            },
            new() {
                Id = 7,
                Name = "Staff 7",
                Email = "staff7@gmail.com",
                Active = true,
                LocationId = 1
            },
            new() {
                Id = 8,
                Name = "Staff 8",
                Email = "",
                Active = true,
                LocationId = 1
            },
            new() {
                Id = 9,
                Name = "Staff 9",
                Email = "",
                Active = true,
                LocationId = 1
            },
            new() {
                Id = 10,
                Name = "Staff 10",
                Email = "",
                Active = true,
                LocationId = 1
            },
        };
        await context.Staff.AddRangeAsync(staff);
        await context.SaveChangesAsync();
    }
}