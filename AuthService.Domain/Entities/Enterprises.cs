using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth_ms.Enums;

namespace Auth_ms.Entities;

[Table("TB_Enterprises")]
public class Enterprises
{
    [Key] 
    [Column("enterprise_id")] 
    public int EnterpriseId { get; set; }

    [Required]
    [Column("enterprise_name", TypeName = "varchar(255)")]
    public string EnterpriseName { get; set; } = string.Empty;
    
    [Required]
    [Column("cnpj", TypeName = "varchar(14)")]
    public string cnpj { get; set; }


    [Required]
    [EmailAddress]
    [Column("enterprise_email", TypeName = "varchar(255)")]
    public string EnterpriseEmail { get; set; } = string.Empty;

    [Required]
    [Column("database_path", TypeName = "varchar(255)")]
    public string DbPath { get; set; } = string.Empty;

    [Required]
    [Column("database_username", TypeName = "varchar(100)")]
    public string DbUsername { get; set; } = string.Empty;

    [Required]
    [Column("database_password", TypeName = "varchar(100)")]
    public string EnterpriseDbPassword { get; set; } = string.Empty;
    
    [Required]
    public EnterpriseStatus Status { get; set; } = EnterpriseStatus.Active;
}