using Microsoft.AspNetCore.Mvc;
using Auth_ms.Dtos;
using Auth_ms.Services;

namespace Auth_ms.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Create([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(Create), new { id = user.Email }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            try
            {
                var updated = await _userService.UpdateUserAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _userService.DeleteUserAsync(id);
                return Ok(deleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
    }
}
