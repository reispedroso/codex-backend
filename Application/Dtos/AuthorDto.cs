using System.ComponentModel.DataAnnotations;

namespace codex_backend.Application.Dtos
{
    public class AuthorCreateDto
    {
        [Required]
        public string Name { get; set; } = "";
        public string Biography { get; set; } = "";
        public string Nationality { get; set; } = "";
        public DateTime CreatedAt { get; set; }

    }
    public class AuthorReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Biography { get; set; } = "";
        public string Nationality { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class AuthorUpdateDto
    {
        public string Name { get; set; } = "";
        public string Biography { get; set; } = "";
        public string Nationality { get; set; } = "";
        public DateTime UpdatedAt { get; set; }
    }
}