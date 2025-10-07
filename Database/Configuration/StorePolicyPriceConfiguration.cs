using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class StorePolicyPriceConfiguration : IEntityTypeConfiguration<StorePolicyPrice>
{
    public void Configure(EntityTypeBuilder<StorePolicyPrice> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DurationInMonths)
        .IsRequired();

        builder.Property(p => p.Currency)
        .HasConversion<string>()
        .IsRequired();

        builder.Property(p => p.Price)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

        builder.HasOne(p => p.StorePolicy)
        .WithMany(sp => sp.Prices)
        .HasForeignKey(sp => sp.StorePolicyId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
