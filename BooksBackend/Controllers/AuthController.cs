using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.Auth;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class userauthController : ControllerBase
{
    private readonly IUserService _userService;

    public userauthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var response = await _userService.RegisterAsync(request.Username, request.Email, request.Password);
        return  Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _userService.LoginAsync(request.Email, request.Password);
        return  Ok(response);
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] refreshTokenDto refreshDto)
    {
        var response = await _userService.RefreshTokenAsync(refreshDto.refreshToken);
        return Ok(response);
    }


}
