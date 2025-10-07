using System.Text.RegularExpressions;
using codex_backend.Application.Dtos;

namespace codex_backend.Application.Validators;

public class UserValidator
{
    public static IReadOnlyList<string> ValidateCreate(UserCreateDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required");
        else if(!IsValidEmail(dto.Email))
            errors.Add("Invalid email address");
        
        if (string.IsNullOrWhiteSpace(dto.Username))
            errors.Add("Username is required");
        else if (dto.Username.Length < 5 || dto.Username.Length > 20)
            errors.Add("Username must be between 5 and 20 characters");
        
        if (string.IsNullOrWhiteSpace((dto.Password)))
            errors.Add("Password is required");
        else if (!PasswordValidator.IsValidPassword(dto.Password))
            errors.Add("Password is invalid");

        return errors;
    }
    
    public static IReadOnlyList<string> ValidateUpdate(UserUpdateDto dto)
    {
        var errors = new List<string>();

        if (!string.IsNullOrWhiteSpace(dto.Email) && !IsValidEmail(dto.Email))
            errors.Add("Invalid email format");

        if (!string.IsNullOrWhiteSpace(dto.Username) &&
            (dto.Username.Length < 3 || dto.Username.Length > 20))
            errors.Add("Username must be between 3 and 20 characters");

        return errors;
    }

    
    private static bool IsValidEmail(string email)
        => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}