
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

    [HttpGet("{id}", Name = "GetStaff")]
    public async Task<IActionResult> GetStaff(int id)
    {
        return Ok(await _staffService.GetStaff(id));
    }
}