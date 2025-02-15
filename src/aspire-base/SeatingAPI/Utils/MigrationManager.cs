using Microsoft.EntityFrameworkCore;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        Console.WriteLine("Migrating Database");
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                    ContextSeeder.SeedData(appContext).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        }
        return webApp;
    }

    public static bool TestConnection(AppDbContext context)
    {
        try
        {
            context.Database.OpenConnection();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}