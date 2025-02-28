
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

    /// <summary>
    /// Get location data
    /// Array based
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        _logger.LogInformation("Getting Locations");
        var locations = await _locationService.GetLocations();
        if (locations == null)
        {
            return BadRequest();
        }
        List<LocationDTO> locationDTOs = new List<LocationDTO>();
        foreach (var location in locations)
        {
            // Unsure on the seating count work etc..!
            locationDTOs.Add(new LocationDTO(location.Id, location.Name)
            {
                Desks = [],
                DeskCount = location.SeatingCount
            });
        }
        return Ok(locationDTOs);
    }

    /// <summary>
    /// Request single location data!
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetLocation")]
    public async Task<IActionResult> GetLocation(int id)
    {
        _logger.LogInformation($"Getting Location {id}");
        var location = await _locationService.GetLocation(id);
        if (location == null)
        {
            return BadRequest();
        }
        var locationDTO = new LocationDTO(location.Id, location.Name)
        {
            DeskCount = location.SeatingCount,
            Desks = location.Desks.Select(x => new DeskDTO(x.Id, x.Name, location.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", default)).ToList()
        };
        return Ok(locationDTO);
    }
}