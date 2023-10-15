using Wallet.Domain.Abstractions;
using Wallet.Domain.Loans;

namespace Wallet.Application.Loans.CreateLoan;

public sealed class CreateLoanService : ICreateLoanService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoanRepository _loanRepository;

    public CreateLoanService(IUnitOfWork unitOfWork, ILoanRepository loanRepository)
    {
        _unitOfWork = unitOfWork;
        _loanRepository = loanRepository;
    }

    public async Task<Loan> Handle(CreateLoanInput input, CancellationToken cancellationToken)
    {
        var user = input.User;
        var title = input.Title;
        var description = input.Description;
        var loadTypeCode = input.LoadTypeCode;
        var currencyCode = input.CurrencyCode;
        var totalAmount = input.TotalAmount;

        var currency = Currency.GetByCode(currencyCode);
        var amount = Money.Create(totalAmount, currency);
        var loanType = LoanType.GetByCode(loadTypeCode);

        if (!user.IsPremium)
        {
            var totalLoanCount = await _loanRepository.GetTotalLoanCountByUserId(user.Id);
            if (totalLoanCount >= 5)
            {
                throw new MaxLoanCountException();
            }
        }

        var loan = Loan.Create(user, title, description, loanType, amount);

        await _loanRepository.Add(loan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loan;
    }
}