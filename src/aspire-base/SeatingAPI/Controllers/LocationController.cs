
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly LocationService _locationService;
    public LocationController(LocationService locationService,
        ILogger<LocationController> logger)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        _logger.LogInformation("Getting Locations");
        return Ok(await _locationService.GetLocations());
    }

    [HttpGet("{id}", Name = "GetLocation")]
    public async Task<IActionResult> GetLocation(int id)
    {
        _logger.LogInformation($"Getting Location {id}");
        return Ok(await _locationService.GetLocation(id));
    }
}