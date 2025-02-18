
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetDeskById")]
    public async Task<IActionResult> GetDeskById(int id)
    {
        var desk = await _deskService.GetDeskById(id);
        if (desk == null)
        {
            return NotFound();
        }
        
        var dto = new DeskDTO
        {
            Id = desk.Id,
            Name = desk.Name,
            Active = desk.Active,
            IsHotDesk = desk.IsHotDesk,
            Location = desk.Location?.Name,
            StaffName = desk.Staff?.Name
        };

        return Ok(dto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("{id}/{date}", Name = "CheckDeskStatusForDate")]
    public async Task<IActionResult> CheckDeskStatusForDate(int id, DateTime date)
    {
        var result = await _deskService.CheckDeskStatusForDate(id, date);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(HelperMethods.GetStringFromRequestState(result));
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