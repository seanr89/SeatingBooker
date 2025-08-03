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

    [HttpGet(Name = "CheckDbConnection")]
    public IActionResult CheckDbConnection()
    {
        var connected = _appDbContext.Database.CanConnect();
        if (connected)
            return Ok("Db Connected");
        return BadRequest();
    }
}