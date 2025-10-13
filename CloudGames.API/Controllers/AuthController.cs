using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) 
        { 
            _auth = auth; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDto dto)
        {
            try
            {
                var token = await _auth.RegisterAsync(dto);
                return Created("", new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _auth.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
