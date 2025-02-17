
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class DeskController : ControllerBase
{
    private readonly DeskService _deskService;
    public DeskController(DeskService deskService)
    {
        _deskService = deskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDesks()
    {
        return Ok(await _deskService.GetDesks());
    }

    [HttpGet("{id}", Name = "GetDeskById")]
    public async Task<IActionResult> GetDeskById(int id)
    {
        return Ok(await _deskService.GetDeskById(id));
    }

    [HttpGet("{id}/{date}", Name = "CheckDeskStatusForDate")]
    public async Task<IActionResult> CheckDeskStatusForDate(int id, DateTime date)
    {
        return Ok(await _deskService.CheckDeskStatusForDate(id, date));
    }
}