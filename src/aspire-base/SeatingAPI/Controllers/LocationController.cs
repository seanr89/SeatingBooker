
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
        var locations = await _locationService.GetLocations();
        if (locations == null)
        {
            return NotFound();
        }
        List<LocationDTO> locationDTOs = new List<LocationDTO>();
        foreach (var location in locations)
        {
            locationDTOs.Add(new LocationDTO
            {
                Id = location.Id,
                Name = location.Name,
                Desks = location.Desks.Select(x => new DeskDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = location.Name,
                    IsHotDesk = x.IsHotDesk,
                    StaffName = x.Staff?.Name ?? "No Staff Assigned"
                }).ToList()
            });
        }
        return Ok(locationDTOs);
    }

    [HttpGet("{id}", Name = "GetLocation")]
    public async Task<IActionResult> GetLocation(int id)
    {
        _logger.LogInformation($"Getting Location {id}");
        var location = await _locationService.GetLocation(id);
        if (location == null)
        {
            return NotFound();
        }
        var locationDTO = new LocationDTO
        {
            Id = location.Id,
            Name = location.Name,
            Desks = location.Desks.Select(x => new DeskDTO
            {
                Id = x.Id,
                Name = x.Name,
                Location = location.Name,
                IsHotDesk = x.IsHotDesk,
                StaffName = x.Staff?.Name ?? "No Staff Assigned"
            }).ToList()
        };
        return Ok(locationDTO);
    }
}