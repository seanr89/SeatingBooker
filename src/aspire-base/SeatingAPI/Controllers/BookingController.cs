
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
        var dto = new BookingRequestDTO
        {
            Id = booking.Id,
            DeskId = booking.DeskId,
            StaffId = booking.StaffId,
            RequestDate = booking.RequestDate,
            State = HelperMethods.GetStringFromRequestState(booking.State)
        };
        return Ok(dto);
    }

    [HttpGet("{locationId}/{date}", Name = "GetLocationBookingsForLocationOnDate")]
    public async Task<IActionResult> GetLocationBookingsForLocationOnDate(int locationId, DateTime date)
    {
        _logger.LogInformation("BookingController:GetLocationBookingsForLocationOnDate");
        var bookings = await _bookingService.GetLocationBookingsForLocationOnDate(locationId, date);
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
        var dto = new BookingRequestDTO
        {
            Id = res.Id,
            DeskId = res.DeskId,
            StaffId = res.StaffId,
            RequestDate = res.RequestDate,
            State = HelperMethods.GetStringFromRequestState(res.State)
        };
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