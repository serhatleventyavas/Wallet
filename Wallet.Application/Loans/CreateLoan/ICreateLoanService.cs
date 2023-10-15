using Wallet.Domain.Loans;

namespace Wallet.Application.Loans.CreateLoan;

public interface ICreateLoanService
{
    Task<Loan> Handle(CreateLoanInput input, CancellationToken cancellationToken);
}