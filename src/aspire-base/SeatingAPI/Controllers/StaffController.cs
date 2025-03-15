
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
    /// Handle request to get all staff DTO information
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StaffDTO>), 200)]
    [ProducesResponseType(400)]
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
            dtos.Add(new StaffDTO(s.Id, s.Name, s.Email, s.Active)
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
    [HttpGet("{id}", Name = "GetStaffMember")]
    [ProducesResponseType(typeof(StaffDTO), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetStaffMember(int id)
    {
        var staff = await _staffService.GetStaffMember(id);
        if (staff == null)
        {
            return BadRequest();
        }
        // Build the DTO object!
        var dto = new StaffDTO(staff.Id, staff.Name, staff.Email, staff.Active)
        {
            LocationName = staff.Location?.Name ?? "No Location"
        };
        return Ok(dto);
    }

    [HttpGet("{email}", Name = "GetStaffByEmail")]
    [ProducesResponseType(typeof(StaffDTO), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetStaffByEmail(string email)
    {
        throw new NotImplementedException();
    }
}