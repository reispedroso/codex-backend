using codex_backend.Enums;

namespace codex_backend.Models;

public class BookItem
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid BookstoreId { get; set; }
    
    public int Quantity { get; set; }
    public BookCondition Condition { get; set; }

    public Bookstore? Bookstore { get; set; }
    public Book? Book { get; set; }


    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

}