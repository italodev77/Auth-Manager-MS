using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_ms.Entities;

[Table("TB_User")]
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string email { get; set; } = string.Empty;
    [Required]
    public string hashPassword { get; set; } = string.Empty;

}