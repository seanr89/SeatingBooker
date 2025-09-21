

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatingAPI.Contracts.Reads;
using SeatingAPI.Services.Interfaces;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;
    private readonly IDeskService _deskService;
    private readonly IAzureStorageService _azureStorageService;
    public LocationController(ILocationService locationService,
        IDeskService deskService, IAzureStorageService azureStorageService,
        ILogger<LocationController> logger)
    {
        _logger = logger;
        _locationService = locationService;
        _deskService = deskService;
        _azureStorageService = azureStorageService;
    }

    /// <summary>
    /// Get all location data
    /// </summary>
    /// <returns>HTTPStatus event</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<LocationContract>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLocations()
    {
        _logger.LogDebug("GetLocations:called");
        var locations = await _locationService.GetLocations();
        if (locations == null)
        {
            return BadRequest();
        }

        List<LocationContract> locationDTOs = [];
        foreach (Location location in locations)
        {
            // Unsure on the seating count work etc..!
            locationDTOs.Add(new LocationContract(location.Id, location.Name,
                location.Address1, location.Address2, location.City, location.State, location.Active)
            {
                Desks = [],
                DeskCount = 0
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
    [ProducesResponseType(typeof(LocationContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLocation(int id)
    {
        _logger.LogDebug($"Getting Location : {id}");
        Location location = await _locationService.GetLocation(id);
        if (location == null)
        {
            return BadRequest();
        }
        var locationDTO = new LocationContract(location.Id, location.Name,
            location.Address1, location.Address2, location.City, location.State, location.Active)
        {
            Desks = [.. location.Desks.Select(x => new DeskContract(
                x.Id, x.Name, location.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", default))],
            DeskCount = location.Desks.Count(),
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
    [ProducesResponseType(typeof(LocationBookingContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{locationId}/{date}", Name = "GetDesksAndBookingsForLocationOnDate")]
    public async Task<IActionResult> GetDesksAndBookingsForLocationOnDate(int locationId, DateTime date)
    {
        _logger.LogInformation($"GetDesksAndBookingsForLocationOnDate : {locationId} on {date}");
        var location = await _locationService.GetDesksAndBookingsForLocationOnDate(locationId, date);
        if (location == null)
        {
            return BadRequest();
        }
        // Build the DTO object with location, desk and booking data filtered properly
        var dto = new LocationBookingContract(location.Id, location.Name, [])
        {
            Desks = location.Desks.Select(x => new LocationDeskContract(
                x.Id, x.Name, x.IsHotDesk, x.Staff?.Name ?? "No Staff Assigned", x.Active,
                x.BookingRequests.Select(br => new BookingRequestContract(br.Id, br.DeskId, br.StaffId, br.RequestDate,
                    HelperMethods.GetStringFromRequestState(br.State))).ToList())).ToList()
        };
        return Ok(dto);
    }

    /// <summary>
    /// Handle request to get the seat map image for a location
    /// </summary>
    /// <param name="locationId"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{locationId}/seatmap", Name = "GetLocationSeatMapImage")]
    public async Task<IActionResult> GetLocationSeatMapImage(int locationId)
    {
        var fileName = $"location-{locationId}.png";
        var image = await _azureStorageService.GetFileAsync(fileName);

        if (image.Value == null)
        {
            return NotFound();
        }

        return File(image.Value, "image/png");
    }
}
