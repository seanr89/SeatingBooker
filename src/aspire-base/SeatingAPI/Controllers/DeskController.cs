
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
        var result = await _deskService.CheckDeskStatusForDate(id, date);
        if (result == null)
        {
            return BadRequest();
        }
        var res = result switch
        {
            RequestState.Free => "Free",
            RequestState.Booked => "Booked",
            RequestState.Pending => "Pending",
            RequestState.Cancelled => "Cancelled",
            _ => "Unknown"
        };
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDesk(CreateDeskDTO desk)
    {
        var res = await _deskService.CreateDesk(desk);

        if (res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
}