using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class BookstoreConfiguration : IEntityTypeConfiguration<Bookstore>
{
    public void Configure(EntityTypeBuilder<Bookstore> builder)
    {
        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasOne(bs => bs.User)
               .WithMany(u => u.Bookstores)
               .HasForeignKey(bs => bs.OwnerUserId);
    }
}
