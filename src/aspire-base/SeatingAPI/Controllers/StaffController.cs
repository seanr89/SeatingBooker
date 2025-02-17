
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
        return Ok(await _staffService.GetStaffMember(id));
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
}