using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Status)
            .HasConversion<string>();

        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.BookItem)
               .WithMany()
               .HasForeignKey(r => r.BookItemId);

    }
}
