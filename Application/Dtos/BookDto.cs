using System.ComponentModel.DataAnnotations;

namespace codex_backend.Application.Dtos
{
    public class BookCreateDto
    {

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Synposis { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string CoverUrl { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class BookUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Synposis { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string CoverUrl { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }


    public class BookReadDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synposis { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string CoverUrl { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}