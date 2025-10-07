using codex_backend.Application.Dtos;

namespace codex_backend.Application.Validators;

public class BookItemValidator
{
    public static IReadOnlyList<string> ValidateBookItem(BookItemCreateDto dto)
    {
        var errors = new List<string>();

        if (dto.Quantity <= 0)
            errors.Add("Quantity must be greater than zero.");

        else if (dto.BookId == Guid.Empty)
        {
            errors.Add("BookId cannot be an empty GUID.");
        }

        else if (dto.BookstoreId == Guid.Empty)
        {
            errors.Add("BookstoreId cannot be an empty GUID.");
        }

        return errors;

    }

    public static IReadOnlyList<string> ValidateBookItemUpdate(BookItemUpdateDto dto)
    {
        var errors = new List<string>();

        if (dto.Quantity <= 0)
        {
            errors.Add("Quantity must be greater than zero.");
        }

        if (dto.Id == Guid.Empty)
        {
            errors.Add("Id cannot be an empty GUID.");
        }

        return errors;
    }
}