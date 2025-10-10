using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class RentalRepository(AppDbContext context) : IRentalRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Rental> CreateRentalAsync(Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
        await _context.SaveChangesAsync();
        return rental;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
    {
        return await _context.Rentals.ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetAllUserRentalsAsync(Guid userId)
    {
        return await _context.Rentals
                                    .Include(re => re.User)
                                    .Where(re => re.User!.Id == userId)
                                    .ToListAsync();
    }

    public async Task<Rental?> GetRentalByIdAsync(Guid rentalId)
    {
        return await _context.Rentals.FirstOrDefaultAsync(re => re.Id == rentalId);
    }

    public async Task<bool> UpdateRentalAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

}