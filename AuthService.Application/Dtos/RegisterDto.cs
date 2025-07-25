using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Auth_ms.Dtos;

public class RegisterDto
{
    

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    
    public string HashPassword => ComputeHash(Password);

    private string ComputeHash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}