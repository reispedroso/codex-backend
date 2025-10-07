namespace codex_backend.Application.Dtos;

public class BookstoreCreateDto
{
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? StoreLogoUrl { get; set; }
    public Guid OwnerUserId { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class BookstoreReadDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? StoreLogoUrl { get; set; }
    public Guid OwnerUserId { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class BookstoreUpdateDto
{
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? StoreLogoUrl { get; set; }
    public Guid OwnerUserId { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}