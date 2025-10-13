using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _JogoService;
        public JogoController(IJogoService JogoService) 
        { 
            _JogoService = JogoService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _JogoService.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _JogoService.GetByIdAsync(id));

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JogoDto dto)
        {
            var created = await _JogoService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{id:guid}/purchase")]
        public async Task<IActionResult> Purchase(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            await _JogoService.PurchaseAsync(Guid.Parse(userId), id);
            return Ok();
        }
    }
}
