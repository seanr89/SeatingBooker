
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
        var desks = await _deskService.GetDesks();
        if (desks == null)
        {
            return BadRequest();
        }
        var dtos = new List<DeskDTO>();
        foreach (var desk in desks)
        {
            dtos.Add(new DeskDTO
            {
                Id = desk.Id,
                Name = desk.Name,
                Active = desk.Active,
                IsHotDesk = desk.IsHotDesk,
                Location = desk.Location?.Name ?? "No Location",
                StaffName = desk.Staff?.Name ?? "No Staff Assigned"
            });
        }
        return Ok(dtos);
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
            return BadRequest();
        }
        
        var dto = new DeskDTO
        {
            Id = desk.Id,
            Name = desk.Name,
            Active = desk.Active,
            IsHotDesk = desk.IsHotDesk,
            Location = desk.Location?.Name ?? "No Location",
            StaffName = desk.Staff?.Name ?? "No Staff Assigned"
        };

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