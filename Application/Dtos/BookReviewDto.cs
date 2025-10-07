namespace codex_backend.Application.Dtos;

public class BookReviewCreateDto
{

    public Guid BookId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

public class BookReviewUpdateDto
{

    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}


public class BookReviewReadDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}