using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase 
    {
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        _logger = logger;
        _authService = service;
    }
    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user) 
    {
    if (user == null || !ModelState.IsValid) {
        return BadRequest();
    }
    _authService.CreateUser(user);
    return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string email, string password) 
    {
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
    {
        return BadRequest();
    }

    var token = _authService.SignIn(email, password);

    if (string.IsNullOrWhiteSpace(token)) 
    {
        return Unauthorized();
    }

    return Ok(token);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser()
    {
        if(HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Posts_UserId");

        var userId = Int32.Parse(userIdClaim.Value);

        var user = _authService.GetUserById(userId);

        if (user == null)
        {
            return Unauthorized();
        }
        return (user);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public ActionResult<User> GetUserById(int userId) 
    {
        var user = _authService.GetUserById(userId);

        if (user == null) {
            return NotFound();
        }

        return Ok(user);
    }

}