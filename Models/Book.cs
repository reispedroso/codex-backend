namespace codex_backend.Models;

public class Book
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Synposis { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }
    public int PageCount { get; set; }
    public string? CoverUrl { get; set; }

    public Author? Author { get; set; }
    public Guid AuthorId { get; set; }
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}