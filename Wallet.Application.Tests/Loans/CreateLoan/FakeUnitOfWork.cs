using Wallet.Domain.Abstractions;

namespace Wallet.Application.Tests.Loans.CreateLoan;

internal class FakeUnitOfWork : IUnitOfWork
{
    public int Count { get; private set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Count += 1;
        return Task.FromResult(Count);
    }
}
