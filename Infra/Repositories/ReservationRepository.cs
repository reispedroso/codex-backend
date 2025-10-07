using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class ReservationRepository(AppDbContext context) : IReservationRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetAllUserReservationsAsync(Guid userId)
    {
        return await _context.Reservations
                                .Include(rs => rs.User)
                                .Where(rs => rs.User!.Id == userId)
                                .ToListAsync();
    }

    public async Task<Reservation?> GetReservationByIdAsync(Guid reservationId)
    {
        return await _context.Reservations.FirstOrDefaultAsync(rs => rs.Id == reservationId);
    }

    public async Task<bool> UpdateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}