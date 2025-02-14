
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
}