namespace Auth_ms.Dtos;

using Auth_ms.Enums;
using System.ComponentModel.DataAnnotations;

public class UpdateEnterpriseDto
{

    [Required]
    public string EnterpriseName { get; set; } = string.Empty;

    [Required]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EnterpriseEmail { get; set; } = string.Empty;

    [Required]
    public string DbPath { get; set; } = string.Empty;

    [Required]
    public string DbUsername { get; set; } = string.Empty;

    [Required]
    public string EnterpriseDbPassword { get; set; } = string.Empty;

    public EnterpriseStatus Status { get; set; } = EnterpriseStatus.Active;
}
