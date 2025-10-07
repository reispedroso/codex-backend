namespace codex_backend.Models;

public class User
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password_Hash { get; set; }

    public Role? Role { get; set; }
    public ICollection<Bookstore>?  Bookstores { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}