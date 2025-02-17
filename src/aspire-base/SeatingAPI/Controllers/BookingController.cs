
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

    [HttpGet("{id}", Name = "GetBooking")]
    public async Task<IActionResult> GetBooking(int id)
    {
        var booking = await _bookingService.GetBooking(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpGet("{locationId}/{date}", Name = "GetLocationBookingsForLocationOnDate")]
    public async Task<IActionResult> GetLocationBookingsForLocationOnDate(int locationId, DateTime date)
    {
        var bookings = await _bookingService.GetLocationBookingsForLocationOnDate(locationId, date);
        if (bookings == null)
        {
            return NotFound();
        }
        return Ok(bookings);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, BookingRequestDTO bookingRequestDTO)
    {
        throw new NotImplementedException();
        // var res = await _bookingService.UpdateBooking(id, bookingRequestDTO);

        // if (res == null)
        // {
        //     return BadRequest();
        // }
        // return Ok(res);
    }
}