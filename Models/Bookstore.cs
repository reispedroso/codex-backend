namespace codex_backend.Models;

public class Bookstore
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }

    public string? StoreLogoUrl { get; set; }
    public User? User { get; set; }
    public Guid OwnerUserId { get; set; }


    public ICollection<StorePolicy>? Policies { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}