using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    private readonly IDeskService _deskService;
    public HomeController(IDeskService deskService, ILogger<HomeController> logger)
    {
        _logger = logger;
        _deskService = deskService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok("Hello World!");
    
    }
}