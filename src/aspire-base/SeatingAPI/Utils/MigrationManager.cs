using Microsoft.EntityFrameworkCore;

public static class MigrationManager
{
    /// <summary>
    /// Handles the migration of the database triggers
    /// And initial seeding of the database!
    /// </summary>
    /// <param name="webApp"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static WebApplication MigrateDatabase(this WebApplication webApp, bool seed = true)
    {
        //Console.WriteLine("Migrating Database");
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                    if (seed)
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