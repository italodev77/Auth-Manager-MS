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
    [Route("api/Autenticacao")]
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
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _context.User.AnyAsync(u => u.email == registerDto.Email))
                return BadRequest("E-mail já está em uso.");

            var user = new User
            {
                email = registerDto.Email,
                hashPassword = registerDto.Password,
            };

            user.hashPassword = _passwordHasher.HashPassword(user, registerDto.Password);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var newUser = await _context.User.FirstOrDefaultAsync(u => u.email == registerDto.Email);
            return CreatedAtAction(nameof(Register), new { email = newUser.email }, "Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == loginDto.Email);
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.hashPassword, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Senha incorreta.");
            
            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}

