
public static class DependencyInjection
{
    /// <summary>
    /// Adds the necessary services for the Seating API.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSeatingApiServices(this IServiceCollection services)
    {
        // Add your services here, e.g.:
        // services.AddScoped<IYourService, YourServiceImplementation>();
        
        return services;
    }
}