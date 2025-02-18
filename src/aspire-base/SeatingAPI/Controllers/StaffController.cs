
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

    [HttpGet]
    public async Task<IActionResult> GetStaff()
    {
        return Ok(await _staffService.GetStaff());
    }

    [HttpGet("{id}", Name = "GetStaffMember")]
    public async Task<IActionResult> GetStaffMember(int id)
    {
        var staff = await _staffService.GetStaffMember(id);
        if (staff == null)
        {
            return NotFound();
        }
        var dto = new StaffDTO{
            Id = staff.Id,
            Name = staff.Name,
            Email = staff.Email,
            Active = staff.Active,
            LocationName = staff.Location?.Name
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStaff(CreateStaffDTO staff)
    {
        var res = await _staffService.CreateStaff(staff);

        if (res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }

    //TODO: Implement UpdateStaff
}