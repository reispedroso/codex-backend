namespace codex_backend.Models;

public class BookReview
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = "";
    
    public Book? Book { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}