namespace codex_backend.Models;

public class Author
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public string? Nationality { get; set; }

    public ICollection<Book?> Books { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}