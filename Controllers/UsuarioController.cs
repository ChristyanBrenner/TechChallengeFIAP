using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _UsuarioService;
        public UsuarioController(IUsuarioService UsuarioService) 
        { 
            _UsuarioService = UsuarioService; 
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await _UsuarioService.GetAllAsync();

            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _UsuarioService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Policy = "UsuarioPolicy")]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (id == null) 
                return Unauthorized();

            var usuario = await _UsuarioService.GetByIdAsync(Guid.Parse(id));
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
    }
}
