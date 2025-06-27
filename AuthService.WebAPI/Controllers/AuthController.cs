using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Auth_ms.Data;
using Auth_ms.Dtos;
using Auth_ms.Entities;
using Auth_ms.Services;
using Auth_ms.Config; // Se seu TokenService estiver aí

namespace Auth_ms.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly TokenService _tokenService;

        public AuthController(ApiDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (await _context.User.AnyAsync(u => u.email == model.Email))
                return BadRequest("E-mail já está em uso.");

            var user = new User
            {
                Email = model.Email,
                hashPassword = _passwordHasher.HashPassword(null, model.Password),
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { email = user.Email }, "Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.hashPassword, model.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Senha incorreta.");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
