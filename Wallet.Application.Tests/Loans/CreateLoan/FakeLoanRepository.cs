using Wallet.Domain.Loans;

namespace Wallet.Application.Tests.Loans.CreateLoan;

internal class FakeLoanRepository : ILoanRepository
{
    public List<Loan> AddedLoanList { get; private set; }
    public int TotalCalledGetTotalLoanCountByUserId { get; private set; }
    private int _getTotalLoanCountByUserIdResult = 0;

    public FakeLoanRepository(int getTotalLoanCountByUserIdResult = 0)
    {
        AddedLoanList = new List<Loan>();
        _getTotalLoanCountByUserIdResult = getTotalLoanCountByUserIdResult;
    }
    
    public Task Add(Loan loan)
    {
        AddedLoanList.Add(loan);
        return Task.CompletedTask;
    }

    public Task<int> GetTotalLoanCountByUserId(Guid userId)
    {
        TotalCalledGetTotalLoanCountByUserId += 1;
        return Task.FromResult(_getTotalLoanCountByUserIdResult);
    }
}
