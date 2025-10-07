using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.Reservation)
               .WithMany()
               .HasForeignKey(r => r.ReservationId);
               
        builder.Property(r => r.Status)
            .HasConversion<string>();
    }
}
