using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class BookItemConfiguration : IEntityTypeConfiguration<BookItem>
{
       public void Configure(EntityTypeBuilder<BookItem> builder)
       {
              builder.HasKey(bi => bi.Id);

              builder.Property(bi => bi.Condition)
                    .HasConversion<string>();

              builder.HasOne(bi => bi.Bookstore)
                     .WithMany()
                     .HasForeignKey(bi => bi.BookstoreId);

              builder.HasOne(bi => bi.Book)
                     .WithMany()
                     .HasForeignKey(bi => bi.BookId);

              builder.Property(bi => bi.Quantity)
                      .IsRequired();

              builder.Property(bi => bi.Condition)
                      .IsRequired();
       }
}
