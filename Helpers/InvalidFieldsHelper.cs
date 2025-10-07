namespace codex_backend.Helpers;

public class InvalidFieldsHelper
{
    public static void ThrowIfInvalid(IReadOnlyList<string> errors)
    {
        if (errors.Any())
            throw new ArgumentException(string.Join("; ", errors));
    }

}