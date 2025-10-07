using System.Text.RegularExpressions;

namespace codex_backend.Application.Validators;

public static class PasswordValidator
{
    private static readonly Regex UpperCase = new(@"[A-Z]", RegexOptions.Compiled);
    private static readonly Regex LowerCase = new(@"[a-z]", RegexOptions.Compiled);
    private static readonly Regex Digit = new(@"[0-9]", RegexOptions.Compiled);
    private static readonly Regex SpecialChar = new(@"[\W_]", RegexOptions.Compiled);

    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return password.Length >= 8
               && UpperCase.IsMatch(password)
               && LowerCase.IsMatch(password)
               && Digit.IsMatch(password);

    }
}