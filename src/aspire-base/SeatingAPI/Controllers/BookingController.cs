
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly ILogger<BookingController> _logger;
    public BookingController(BookingService bookingService, ILogger<BookingController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    /// <summary>
    /// Handle all bookings to be requested
    /// </summary>
    /// <returns>Collection of all bookings</returns>
    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        return Ok(await _bookingService.GetBookings());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationId"></param>
    /// <returns></returns>
    [HttpGet("{locationId}", Name = "GetBookingsForLocation")]
    public async Task<IActionResult> GetBookingsForLocation(int locationId)
    {
        var bookings = await _bookingService.GetBookingsForLocation(locationId);
        if (bookings == null)
        {
            return BadRequest();
        }
        return Ok(bookings);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deskId"></param>
    /// <returns></returns>
    [HttpGet("{deskId}", Name = "GetBookingsForDesk")]
    public async Task<IActionResult> GetBookingsForDesk(int deskId)
    {
        var bookings = await _bookingService.GetBookingsForDesk(deskId);
        if (bookings == null)
        {
            return BadRequest();
        }
        return Ok(bookings);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetBooking")]
    public async Task<IActionResult> GetBooking(int id)
    {
        var booking = await _bookingService.GetBooking(id);
        if (booking == null)
        {
            return BadRequest();
        }
        var dto = new BookingRequestDTO(booking.Id, booking.DeskId, booking.StaffId, booking.RequestDate,
            HelperMethods.GetStringFromRequestState(booking.State)
        );
        return Ok(dto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(List<BookingRequestDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{locationId}/{date}", Name = "GetBookingsForLocationOnDate")]
    public async Task<IActionResult> GetBookingsForLocationOnDate(int locationId, DateTime date)
    {
        _logger.LogInformation("BookingController:GetLocationBookingsForLocationOnDate");
        var bookings = await _bookingService.GetBookingsForLocationOnDate(locationId, date);
        if (bookings == null)
        {
            return BadRequest();
        }
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingRequestDTO bookingRequestDTO)
    {
        _logger.LogInformation("BookingController:CreateBooking");
        var res = await _bookingService.CreateBooking(bookingRequestDTO);
        if (res == null)
        {
            return BadRequest();
        }

        var dto = new BookingRequestDTO(res.Id, res.DeskId, res.StaffId, res.RequestDate,
            HelperMethods.GetStringFromRequestState(res.State));
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        _logger.LogInformation("BookingController:CancelBooking");
        var res = await _bookingService.CancelBooking(id);

        if (res == false)
        {
            return BadRequest();
        }
        return Ok();
    }
}