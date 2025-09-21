using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _appDbContext;
    public HomeController(ILogger<HomeController> logger,
        AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    [HttpGet(Name = "HealthCheck")]
    public IActionResult HealthCheck()
    {
        return Ok("Healthy");
    }

    [HttpGet(Name = "CheckDbConnection")]
    public IActionResult CheckDbConnection()
    {
        var connected = _appDbContext.Database.CanConnect();
        if (connected)
            return Ok("Db Connected");
        return BadRequest();
    }

    [Authorize]
    [HttpGet(Name = "ResetDb")]
    public IActionResult ResetDb()
    {
        _appDbContext.BookingRequests.RemoveRange(_appDbContext.BookingRequests);
        _appDbContext.Desks.RemoveRange(_appDbContext.Desks);
        _appDbContext.Staff.RemoveRange(_appDbContext.Staff);
        _appDbContext.Locations.RemoveRange(_appDbContext.Locations);

        _appDbContext.SaveChanges();
        return Ok("Db Reset");
    
    }
}