using Application.Interfaces.Services;
using Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("[controller]")]
public sealed class UserController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var user = await _userService.GetAsync(GetUserId());
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] IEnumerable<Guid> ids)
    {
        var users = await _userService.GetAsync(ids);
        return Ok(users);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        await _userService.UpdateAsync(GetUserId(), request.Name);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        await _userService.DeleteAsync(GetUserId());
        return Ok();
    }
}
