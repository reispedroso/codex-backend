namespace codex_backend.Application.Dtos;

public class CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
}

public class CategoryUpdateDto
{

    public string Name { get; set; } = string.Empty;
}

// O ReadDto permanece o mesmo.
public class CategoryReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}