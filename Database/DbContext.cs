using Microsoft.EntityFrameworkCore;
using codex_backend.Models;
using codex_backend.Database.Configuration;
using Microsoft.Extensions.Options;

namespace codex_backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Bookstore> Bookstores { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookItem> BookItems { get; set; }
    public DbSet<BookReview> BookReviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<StorePolicyPrice> StorePolicyPrice { get; set; }
    public DbSet<StorePolicy> StorePolicy { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BookItemConfiguration());
        modelBuilder.ApplyConfiguration(new BookReviewConfiguration());
        modelBuilder.ApplyConfiguration(new BookstoreConfiguration());
        modelBuilder.ApplyConfiguration(new StorePolicyConfiguration());
        modelBuilder.ApplyConfiguration(new StorePolicyPriceConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new RentalConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
