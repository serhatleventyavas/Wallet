namespace Wallet.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; }

    protected Entity(Guid id, DateTime createdOnUtc, DateTime? updatedOnUtc = null)
    {
        Id = id;
        CreatedOnUtc = createdOnUtc;
        UpdatedOnUtc = updatedOnUtc;
    }
}