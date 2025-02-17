
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        return Ok(await _bookingService.GetBookings());
    }

    public async Task<IActionResult> GetBooking(int id)
    {
        var booking = await _bookingService.GetBooking(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(BookingRequestDTO bookingRequestDTO)
    {
        var res = await _bookingService.CreateBooking(bookingRequestDTO);

        if (res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
}