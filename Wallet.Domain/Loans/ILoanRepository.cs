namespace Wallet.Domain.Loans;

public interface ILoanRepository
{
    public Task<int> GetTotalLoanCountByUserId(Guid userId);
    public Task Add(Loan loan);
}
