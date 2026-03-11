using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Claims;

namespace PaperTrade.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IClaimsService claimsService;

    public UserController(IUserService userService, IClaimsService claimsService)
    {
        this.userService = userService;
        this.claimsService = claimsService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var objectId = claimsService.GetObjectId(User);
        if (string.IsNullOrEmpty(objectId))
        {
            return Unauthorized();
        }

        var user = await userService.GetCurrentUserAsync(objectId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}