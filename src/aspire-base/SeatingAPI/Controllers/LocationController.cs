
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;
    private readonly IDeskService _deskService;
    public LocationController(ILocationService locationService,
        IDeskService deskService,
        ILogger<LocationController> logger)
    {
        _logger = logger;
        _locationService = locationService;
        _deskService = deskService;
    }

    /// <summary>
    /// Get all location data
    /// </summary>
    /// <returns>HTTPStatus event</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<LocationDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLocations()
    {
        _logger.LogDebug("GetLocations:called");
        var locations = await _locationService.GetLocations();
        if (locations == null)
        {
            return BadRequest();
        }

        List<LocationDTO> locationDTOs = [];
        foreach (Location location in locations)
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
    /// Request single location record  - includes desks and staff
    /// </summary>
    /// <param name="id">location id</param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetLocation")]
    [ProducesResponseType(typeof(LocationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLocation(int id)
    {
        _logger.LogInformation($"Getting Location : {id}");
        var location = await _locationService.GetLocation(id);
        if (location == null)
        {
            return BadRequest();
        }
        var locationDTO = new LocationDTO(location.Id, location.Name)
        {
            DeskCount = location.SeatingCount,
            Desks = [.. location.Desks.Select(x => new DeskDTO(
                x.Id, x.Name, location.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", default))]
        };
        return Ok(locationDTO);
    }

    /// <summary>
    /// ASYNC - reuqest desks and bookings for location on a single date!
    /// </summary>
    /// <param name="locationId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [ProducesResponseType(typeof(LocationBookingDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{locationId}/{date}", Name = "GetDesksAndBookingsForLocationOnDate")]
    public async Task<IActionResult> GetDesksAndBookingsForLocationOnDate(int locationId, DateTime date)
    {
        _logger.LogInformation($"Getting Desks and Bookings for Location {locationId} on {date}");
        var location = await _locationService.GetDesksAndBookingsForLocationOnDate(locationId, date);
        if (location == null)
        {
            return BadRequest();
        }
        // Build the DTO object with location, desk and booking data filtered properly
        var dto = new LocationBookingDTO(location.Id, location.Name, [])
        {
            Desks = location.Desks.Select(x => new LocationDeskDTO(
                x.Id, x.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", x.Active,
                x.BookingRequests.Select(br => new BookingRequestDTO(br.Id, br.DeskId, br.StaffId, br.RequestDate, 
                    HelperMethods.GetStringFromRequestState(br.State))).ToList())).ToList()
        };
        return Ok(dto);
    }
}