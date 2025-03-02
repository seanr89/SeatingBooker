
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

    /// <summary>
    /// Support request to get all desks
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetDesks()
    {
        var desks = await _deskService.GetDesks();
        if (desks == null)
        {
            return BadRequest();
        }
        var dtos = new List<DeskDTO>();
        foreach (var desk in desks)
        {
            dtos.Add(new DeskDTO(desk.Id, desk.Name, desk.Location?.Name ?? "No Location", desk.IsHotDesk, desk.Staff?.Name ?? "No Staff Assigned", desk.Active));
        }
        return Ok(dtos);
    }

    /// <summary>
    /// Support simple call for a single desk and its details
    /// Bookings included
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetDeskById")]
    public async Task<IActionResult> GetDeskById(int id)
    {
        var desk = await _deskService.GetDeskById(id);
        if (desk == null)
        {
            return BadRequest();
        }
        
        var dto = new DeskDTO(desk.Id, desk.Name, desk.Location?.Name ?? "No Location", 
            desk.IsHotDesk, desk.Staff?.Name ?? "No Staff Assigned", desk.Active);

        return Ok(dto);
    }

    /// <summary>
    /// Return the current state of the desk for the given date
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
}