
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
}