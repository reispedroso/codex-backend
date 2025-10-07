using System.Numerics;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codex_backend.Database.Configuration;

public class StorePolicyConfiguration : IEntityTypeConfiguration<StorePolicy>
{
    public void Configure(EntityTypeBuilder<StorePolicy> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne<Bookstore>()
        .WithMany(b => b.Policies)
        .HasForeignKey(sp => sp.BookstoreId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(sp => sp.LateFeePerDay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

      
        builder.Property(sp => sp.GracePeriodDays)
            .IsRequired();

   
        builder.Property(sp => sp.MaxRenewals)
            .IsRequired();

        builder.HasMany(sp => sp.Prices)
            .WithOne(p => p.StorePolicy!)
            .HasForeignKey(p => p.StorePolicyId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
