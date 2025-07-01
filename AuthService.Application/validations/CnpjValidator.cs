using System.Text.RegularExpressions;

namespace AuthService.Application.validations;

public static class CnpjValidator
{
    public static bool IsValid(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;
        
        var cleaned = Regex.Replace(cnpj, "[^0-9]", "");
        
        if (cleaned.Length != 14)
            return false;
        
        var regex = new Regex(@"^\d{14}$");
        if (!regex.IsMatch(cleaned))
            return false;

        return true;
    }
}