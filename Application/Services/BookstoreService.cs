using codex_backend.Application.Dtos;
using codex_backend.Models;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;

namespace codex_backend.Application.Services
{
    public class BookstoreService(IBookstoreRepository bookstoreRepository) : IBookstoreService
    {
        private readonly IBookstoreRepository _bookstoreRepository = bookstoreRepository;

        public async Task<BookstoreReadDto> CreateBookstoreAsync(BookstoreCreateDto dto, Guid ownerUserId)
        {
            if (await _bookstoreRepository.GetBookstoreByNameAsync(dto.Name) is not null) throw new Exception($"Bookstore with name {dto.Name} already registered");

            var newBookstore = new Bookstore
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                OwnerUserId = ownerUserId,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                StoreLogoUrl = dto.StoreLogoUrl,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };

            await _bookstoreRepository.CreateBookstoreAsync(newBookstore);
            return MapToDto(newBookstore);
        }

        public async Task<IEnumerable<BookstoreReadDto>> GetAllBookstoresAsync()
        {
            var allBookstores = await _bookstoreRepository.GetAllBookstoresAsync() ?? throw new Exception("No bookstores available");
            return allBookstores.Select(MapToDto);
        }

        public async Task<BookstoreReadDto> GetBookstoreByIdAsync(Guid id)
        {
            var bookstoreById = await _bookstoreRepository.GetBookstoreByIdAsync(id)
            ?? throw new Exception($"Bookstore with id: {id} not found");

            return MapToDto(bookstoreById);
        }
        public async Task<BookstoreReadDto> GetSingleBookstoreByAdminId(Guid adminId)
        {
            var singleBookstore = await _bookstoreRepository.GetSingleBookstoreByAdminIdAsync(adminId)
            ?? throw new Exception("sla");

            return MapToDto(singleBookstore);
        }
        public async Task<BookstoreReadDto> GetBookstoreByNameAsync(string name)
        {
            var bookstoreByName = await _bookstoreRepository.GetBookstoreByNameAsync(name)
            ?? throw new Exception($"Bookstore: {name} not founded");
            return MapToDto(bookstoreByName);
        }

        public async Task<IEnumerable<BookstoreReadDto>> GetBookstoresByAdminIdAsync(Guid adminId)
        {
            var bookstoresByAdminId = await _bookstoreRepository.GetBookstoresByAdminIdAsync(adminId)
              ?? throw new Exception("Bookstore by Id not found!");

            return bookstoresByAdminId.Select(MapToDto);
        }
        public async Task<BookstoreReadDto> UpdateBookstoreAsync(Guid id, BookstoreUpdateDto dto)
        {
            var updateBookstore = await _bookstoreRepository.GetBookstoreByIdAsync(id);

            updateBookstore!.Name = dto.Name;
            updateBookstore.Street = dto.Street;
            updateBookstore.City = dto.City;
            updateBookstore.State = dto.State;
            updateBookstore.ZipCode = dto.ZipCode;

            await _bookstoreRepository.UpdateBookstoreAsync(updateBookstore);
            return MapToDto(updateBookstore);
        }

        public async Task DeleteBookstoreAsync(Guid id)
        {
            var deleteBookstore = await _bookstoreRepository.GetBookstoreByIdAsync(id);

            deleteBookstore.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);
            await _bookstoreRepository.UpdateBookstoreAsync(deleteBookstore);
        }
        private static BookstoreReadDto MapToDto(Bookstore b) => new()
        {
            Id = b.Id,
            Name = b.Name,
            OwnerUserId = b.OwnerUserId,
            City = b.City,
            State = b.State,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt
        };
    }
}