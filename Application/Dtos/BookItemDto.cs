using System.ComponentModel.DataAnnotations;
using codex_backend.Enums;

namespace codex_backend.Application.Dtos;

public class BookItemCreateDto
{

    [Required]
    public Guid BookId { get; set; }
    [Required]
    public Guid BookstoreId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public BookCondition Condition { get; set; }
}

public class BookItemUpdateDto
{
    [Required]
    public int Quantity { get; set; }
    public BookCondition Condition { get; set; }

}

public class BookItemReadDto
{
    public Guid Id { get; set; }
    public Guid BookstoreId { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public BookCondition Condition { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}