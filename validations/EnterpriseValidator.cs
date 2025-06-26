using System.Text.RegularExpressions;
using Auth_ms.Entities;
using Auth_ms.validations;

public static class EnterpriseValidator
{
    public static EnterpriseValidationResult ValidateCreate(Enterprises enterprise)
    {
        var result = new EnterpriseValidationResult();

        if (string.IsNullOrWhiteSpace(enterprise.EnterpriseName))
            result.AddError("O nome da empresa é obrigatório.");

        if (string.IsNullOrWhiteSpace(enterprise.cnpj))
            result.AddError("O CNPJ é obrigatório.");
        else if (!Regex.IsMatch(enterprise.cnpj, @"^\d{14}$"))
            result.AddError("CNPJ inválido. Deve conter 14 dígitos numéricos.");

        return result;
    }

    public static EnterpriseValidationResult ValidateUpdateOrDelete(Enterprises enterprise)
    {
        var result = new EnterpriseValidationResult();

        if (enterprise.EnterpriseId <= 0)
            result.AddError("ID inválido.");

        if (string.IsNullOrWhiteSpace(enterprise.cnpj))
            result.AddError("CNPJ é obrigatório.");

        return result;
    }
}