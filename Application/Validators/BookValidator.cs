using codex_backend.Application.Dtos;
using System;

namespace codex_backend.Application.Validators;

public class BookValidator
{
    public static IReadOnlyList<string> ValidateBook(BookCreateDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Book title cannot be null or empty.");

        if (!string.IsNullOrWhiteSpace(dto.Synposis) && dto.Synposis.Length > 1000)
            errors.Add("Synopsis cannot be longer than 1000 characters.");

        if (dto.PublicationDate == default)
            errors.Add("Publication date must be set.");

        else if (dto.PublicationDate > DateTime.UtcNow.AddDays(1))
            errors.Add("Publication date cannot be in the future.");

        if (!string.IsNullOrWhiteSpace(dto.Language) && dto.Language.Length > 64)
            errors.Add("Language cannot be longer than 64 characters.");

        if (!string.IsNullOrWhiteSpace(dto.Publisher) && dto.Publisher.Length > 128)
            errors.Add("Publisher cannot be longer than 128 characters.");

        if (dto.PageCount <= 0)
            errors.Add("Page count must be greater than zero.");

        if (dto.PageCount > 10000)
            errors.Add("Page count cannot exceed 10,000.");

        if (!string.IsNullOrWhiteSpace(dto.CoverUrl))
        {
            if (dto.CoverUrl.Length > 512)
                errors.Add("Cover URL cannot be longer than 512 characters.");
            else if (!Uri.TryCreate(dto.CoverUrl, UriKind.Absolute, out _))
                errors.Add("Cover URL must be a valid absolute URL.");
        }

        if (dto.AuthorId == Guid.Empty)
            errors.Add("AuthorId must be set.");

        if (dto.CategoryId == Guid.Empty)
            errors.Add("CategoryId must be set.");
            
        if (dto.CreatedAt.HasValue && dto.CreatedAt.Value > DateTime.UtcNow.AddMinutes(1))
            errors.Add("CreatedAt cannot be in the future.");

        return errors;
    }

    public static IReadOnlyList<string> ValidateBookUpdate(BookUpdateDto dto)
    {
        var errors = new List<string>();
        if (dto.Title != null && string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Book title cannot be empty.");

        if (!string.IsNullOrWhiteSpace(dto.Synposis) && dto.Synposis.Length > 1000)
            errors.Add("Synopsis cannot be longer than 1000 characters.");

        if (dto.PublicationDate == default)
            errors.Add("Publication date must be set.");
        else if (dto.PublicationDate > DateTime.UtcNow.AddDays(1))
            errors.Add("Publication date cannot be in the future.");

        if (!string.IsNullOrWhiteSpace(dto.Language) && dto.Language.Length > 64)
            errors.Add("Language cannot be longer than 64 characters.");

        if (!string.IsNullOrWhiteSpace(dto.Publisher) && dto.Publisher.Length > 128)
            errors.Add("Publisher cannot be longer than 128 characters.");

        if (dto.PageCount <= 0)
            errors.Add("Page count must be greater than zero.");

        if (dto.PageCount > 10000)
            errors.Add("Page count cannot exceed 10,000.");

        if (!string.IsNullOrWhiteSpace(dto.CoverUrl))
        {
            if (dto.CoverUrl.Length > 512)
                errors.Add("Cover URL cannot be longer than 512 characters.");
            else if (!Uri.TryCreate(dto.CoverUrl, UriKind.Absolute, out _))
                errors.Add("Cover URL must be a valid absolute URL.");
        }
        if (dto.AuthorId == Guid.Empty)
            errors.Add("AuthorId must be set.");

        if (dto.CategoryId == Guid.Empty)
            errors.Add("CategoryId must be set.");

        if (dto.UpdatedAt.HasValue && dto.UpdatedAt.Value > DateTime.UtcNow.AddMinutes(1))
            errors.Add("UpdatedAt cannot be in the future.");

        return errors;
    }
}