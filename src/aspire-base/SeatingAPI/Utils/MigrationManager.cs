using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public static class MigrationManager
{
    /// <summary>
    /// Handles the migration of the database triggers
    /// And initial seeding of the database!
    /// </summary>
    /// <param name="webApp">wepApp object with DI context</param>
    /// <param name="seed">Control if you want to seed in data - default = true</param>
    /// <returns></returns>
    public static WebApplication MigrateDatabaseAndSeed(this WebApplication webApp, bool seed = true)
    {
        Console.WriteLine("Migrating Database");
        try
        {
            using (var scope = webApp.Services.CreateScope())
            {
                TestConnection(scope.ServiceProvider.GetRequiredService<AppDbContext>());
                using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    appContext.Database.Migrate();
                    if (seed)
                        ContextSeeder.SeedData(appContext).Wait();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //Log errors or do anything you think it's needed
            throw;
        }
        return webApp;
    }

    /// <summary>
    /// Test the connection to the database
    /// </summary>
    /// <param name="context">DbContext to test</param>
    /// <returns>True if the connection is successful, false otherwise</returns>
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