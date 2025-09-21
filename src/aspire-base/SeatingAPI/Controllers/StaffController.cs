
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatingAPI.Contracts.Reads;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService,
        ILogger<StaffController> logger)
    {
        _logger = logger;
        _staffService = staffService;
    }

    /// <summary>
    /// Handle request to get all staff DTO information
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StaffContract>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetStaff()
    {
        _logger.LogInformation("StaffController:GetStaff");
        var staff = await _staffService.GetStaff();
        if (staff == null)
        {
            return BadRequest();
        }
        List<StaffContract> dtos = [];
        foreach (Staff s in staff)
        {
            dtos.Add(new StaffContract(s.Id, s.Name, s.Email, s.Active)
            {
                LocationName = s.Location?.Name ?? "No Location"
            });
        }
        return Ok(dtos);
    }

    /// <summary>
    /// Handle the request to get a single staff member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetStaffById")]
    [ProducesResponseType(typeof(StaffContract), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetStaffById(int id)
    {
        _logger.LogInformation("StaffController:GetStaffById");
        var staff = await _staffService.GetStaffMember(id);
        if (staff == null)
        {
            return BadRequest();
        }
        // Build the DTO object!
        var dto = new StaffContract(staff.Id, staff.Name, staff.Email, staff.Active)
        {
            LocationName = staff.Location?.Name ?? "No Location"
        };
        return Ok(dto);
    }

    [HttpGet("{email}", Name = "GetStaffByEmail")]
    [ProducesResponseType(typeof(StaffContract), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetStaffByEmail(string email)
    {
        _logger.LogInformation("StaffController:GetStaffByEmail");
        var res = await _staffService.GetStaffMemberByEmail(email);
        if (res == null)
        {
            return BadRequest();
        }
        var dto = new StaffContract(res.Id, res.Name, res.Email, res.Active)
        {
            LocationName = res.Location?.Name ?? "No Location"
        };
        return Ok(dto);
    }
}
