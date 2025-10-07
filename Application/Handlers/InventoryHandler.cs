using codex_backend.Application.Repositories.Interfaces;

namespace codex_backend.Application.Handlers;

public class InventoryHandler(IBookItemRepository repository)
{
    private readonly IBookItemRepository _repository = repository;
    public async Task ReserveBookItem(Guid bookItemId)
    {
        var bookItem = await _repository.GetBookItemByIdAsync(bookItemId)
        ?? throw new Exception("some error");

        bookItem.Quantity -= 1;

        await _repository.UpdateBookItemAsync(bookItem);
    }
}