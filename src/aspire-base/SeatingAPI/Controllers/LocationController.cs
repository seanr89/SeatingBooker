
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly LocationService _locationService;
    private readonly DeskService _deskService;
    public LocationController(LocationService locationService,
        DeskService deskService,
        ILogger<LocationController> logger)
    {
        _logger = logger;
        _locationService = locationService;
        _deskService = deskService;
    }

    /// <summary>
    /// Get location data
    /// Array based
    /// </summary>
    /// <returns>HTTPStatus event</returns>
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

    /// <summary>
    /// TODO: needs to be mapped out etc...
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
        var dto = new LocationBookingDTO(location.Id, location.Name, [])
        {
            Desks = location.Desks.Select(x => new LocationDeskDTO(
                x.Id, x.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", x.Active,
                x.BookingRequests.Select(br => new BookingRequestDTO(br.Id, br.DeskId, br.StaffId, br.RequestDate, 
                    HelperMethods.GetStringFromRequestState(br.State))).ToList())).ToList()
        };
        //TODO: Map out the location to a DTO?
        return Ok(dto);
    }
}