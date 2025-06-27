using System.ComponentModel.DataAnnotations;

namespace Auth_ms.Dtos;

public class LoginDto
{ 
        [Required] 
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
}