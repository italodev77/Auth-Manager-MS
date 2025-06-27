using Auth_ms.Enums;

namespace Auth_ms.Dtos;

public class EnterpriseResponseDto
{
    public int EnterpriseId { get; set; }
    public string EnterpriseName { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string EnterpriseEmail { get; set; } = string.Empty;
    public string DbPath { get; set; } = string.Empty;
    public string DbUsername { get; set; } = string.Empty;
    public EnterpriseStatus Status { get; set; }
}