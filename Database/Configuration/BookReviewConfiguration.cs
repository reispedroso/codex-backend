using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
{
    public void Configure(EntityTypeBuilder<BookReview> builder)
    {
        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.BookId)
               .IsRequired();

        builder.HasOne(bi => bi.Book)
               .WithMany()
               .HasForeignKey(bi => bi.BookId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(bi => bi.BookId);

    }
}
