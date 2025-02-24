
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class StaffController : ControllerBase
{
    private readonly StaffService _staffService;
    public StaffController(StaffService staffService)
    {
        _staffService = staffService;
    }

    /// <summary>
    /// Handle request to get all data
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetStaff()
    {
        var staff = await _staffService.GetStaff();
        if (staff == null)
        {
            return BadRequest();
        }
        var dtos = new List<StaffDTO>();
        foreach (var s in staff)
        {
            dtos.Add(new StaffDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Active = s.Active,
                LocationName = s.Location?.Name ?? "No Location"
            });
        }
        return Ok(dtos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetStaffMember")]
    public async Task<IActionResult> GetStaffMember(int id)
    {
        var staff = await _staffService.GetStaffMember(id);
        if (staff == null)
        {
            return BadRequest();
        }
        var dto = new StaffDTO{
            Id = staff.Id,
            Name = staff.Name,
            Email = staff.Email,
            Active = staff.Active,
            LocationName = staff.Location?.Name ?? "No Location"
        };
        return Ok(dto);
    }
}