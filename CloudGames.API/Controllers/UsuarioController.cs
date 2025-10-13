using Domain.DTOs;
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

        [Authorize(Policy = "UserPolicy")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] RegistroUsuarioDto dto)
        {
            var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
                return Unauthorized();

            try
            {
                var usuario = await _UsuarioService.UpdateAsync(int.Parse(id), dto);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _UsuarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
