namespace codex_backend.Application.Dtos;

public class CategoryCreateDto
{
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CategoryReadDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}