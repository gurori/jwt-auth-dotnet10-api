using Application.Interfaces.Services;
using Application.Models.Users;
using Infrastructure.Auth.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AuthController(IUserService userService, IOptions<JwtOptions> jwtOptions)
        : BaseController
    {
        private readonly IUserService _userService = userService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            await _userService.RegisterAsync(
                request.Name,
                request.Email,
                request.Password,
                request.Role
            );

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            string token = await _userService.LoginAsync(request.Email, request.Password);
            SetAuthCookie(token);

            return Ok();
        }

        private void SetAuthCookie(string token)
        {
            Response.Cookies.Append(
                _jwtOptions.JwtCookieName,
                token,
                new CookieOptions
                {
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpiresDays),
                }
            );
        }
    }
}
