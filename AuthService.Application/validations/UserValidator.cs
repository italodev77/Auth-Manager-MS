using System.Text.RegularExpressions;
using Auth_ms.Entities;
using Auth_ms.Validations;

public static class UserValidator
{
    public static UserValidationResult ValidateCreate(User user)
    {
        var result = new UserValidationResult();

        if (string.IsNullOrWhiteSpace(user.email))
            result.AddError("O e-mail é obrigatório.");
        else if (!Regex.IsMatch(user.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            result.AddError("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(user.hashPassword))
            result.AddError("A senha é obrigatória.");

        return result;
    }

    public static UserValidationResult ValidateUpdateOrDelete(User user)
    {
        var result = new UserValidationResult();

        if (user.UserId <= 0)
            result.AddError("ID de usuário inválido.");

        return result;
    }
}