using codex_backend.Application.Authorization.Common.Exceptions;
using codex_backend.Application.Repositories.Interfaces;

namespace codex_backend.Application.Handlers
{
    public class InventoryHandler(IBookItemRepository repository)
    {
        private readonly IBookItemRepository _repository = repository;

        public async Task ReserveBookItem(Guid bookItemId)
        {
            var bookItem = await _repository.GetBookItemByIdAsync(bookItemId)
                ?? throw new NotFoundException($"Item de livro com ID {bookItemId} não encontrado para reserva.");

            if (bookItem.Quantity <= 0)
            {
                throw new InvalidOperationException("Não há estoque disponível para este item.");
            }

            bookItem.Quantity -= 1;

            await _repository.UpdateBookItemAsync(bookItem);
        }
        public async Task ReturnBookItemToStock(Guid bookItemId)
        {
            var bookItem = await _repository.GetBookItemByIdAsync(bookItemId)
                ?? throw new NotFoundException($"Item de livro com ID {bookItemId} não encontrado para devolução ao estoque.");

            bookItem.Quantity += 1;

            await _repository.UpdateBookItemAsync(bookItem);
        }
    }
}