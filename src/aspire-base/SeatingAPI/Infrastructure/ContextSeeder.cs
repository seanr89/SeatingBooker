
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
                Name = "Location 1",
                Active = true,
                SeatingCount = 10
            },
            new Location
            {
                Name = "Location 2",
                Active = true,
                SeatingCount = 5
            },
            new Location
            {
                Name = "Location 3",
                Active = false,
                SeatingCount = 40
            }
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
        List<Desk> desks = new List<Desk>{
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
        };
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
            new Staff
            {
                Id = 1,
                Name = "Staff 1",
                Email = "staff1@email.com",
                Active = true,
                LocationId = 1
            },
            new Staff
            {
                Id = 2,
                Name = "Sean Rafferty",
                Email = "srafferty89@email.com",
                Active = true,
                LocationId = 1
            },new Staff
            {
                Id = 3,
                Name = "Staff 3",
                Email = "staff3@email.com",
                Active = true,
                LocationId = 1
            },new Staff
            {
                Id = 4,
                Name = "Staff 4",
                Email = "staff4@email.com",
                Active = true,
                LocationId = 1
            },new Staff
            {
                Id = 5,
                Name = "Staff 5",
                Email = "staff5@email.com",
                Active = true,
                LocationId = 1
            },
        };
        await context.Staff.AddRangeAsync(staff);
        await context.SaveChangesAsync();
    }
}