using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
// using SeatingAPI.Controllers;
// using SeatingAPI.Entities;
// using SeatingAPI.DTOs;
// using SeatingAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StaffControllerTest
{
    private readonly Mock<IStaffService> _staffServiceMock;
    private readonly Mock<ILogger<StaffController>> _loggerMock;
    private readonly StaffController _controller;

    public StaffControllerTest()
    {
        _staffServiceMock = new Mock<IStaffService>();
        _loggerMock = new Mock<ILogger<StaffController>>();
        _controller = new StaffController(_staffServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetStaff_ReturnsOk_WithStaffList()
    {
        var staffList = new List<Staff> { new Staff { Id = 1, Name = "Test", Email = "test@test.com", Active = true, Location = new Location { Name = "HQ" } } };
        _staffServiceMock.Setup(s => s.GetStaff()).ReturnsAsync(staffList);

        var result = await _controller.GetStaff();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dtos = Assert.IsAssignableFrom<IEnumerable<StaffDTO>>(okResult.Value);
        Assert.Single(dtos);
    }

    [Fact]
    public async Task GetStaff_ReturnsBadRequest_WhenNull()
    {
        _staffServiceMock.Setup(s => s.GetStaff()).ReturnsAsync((List<Staff>)null);
        var result = await _controller.GetStaff();
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetStaffMember_ReturnsOk_WhenFound()
    {
        var staff = new Staff { Id = 2, Name = "Jane", Email = "jane@test.com", Active = true, Location = new Location { Name = "Remote" } };
        _staffServiceMock.Setup(s => s.GetStaffMember(2)).ReturnsAsync(staff);
        var result = await _controller.GetStaffMember(2);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<StaffDTO>(okResult.Value);
        Assert.Equal("Jane", dto.Name);
    }

    [Fact]
    public async Task GetStaffMember_ReturnsBadRequest_WhenNotFound()
    {
        _staffServiceMock.Setup(s => s.GetStaffMember(99)).ReturnsAsync((Staff)null);
        var result = await _controller.GetStaffMember(99);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetStaffByEmail_ReturnsOk_WhenFound()
    {
        var staff = new Staff { Id = 3, Name = "Sam", Email = "sam@test.com", Active = false, Location = null };
        _staffServiceMock.Setup(s => s.GetStaffMemberByEmail("sam@test.com")).ReturnsAsync(staff);
        var result = await _controller.GetStaffByEmail("sam@test.com");
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<StaffDTO>(okResult.Value);
        Assert.Equal("Sam", dto.Name);
        Assert.Equal("No Location", dto.LocationName);
    }

    [Fact]
    public async Task GetStaffByEmail_ReturnsBadRequest_WhenNotFound()
    {
        _staffServiceMock.Setup(s => s.GetStaffMemberByEmail("notfound@test.com")).ReturnsAsync((Staff)null);
        var result = await _controller.GetStaffByEmail("notfound@test.com");
        Assert.IsType<BadRequestResult>(result);
    }
}
