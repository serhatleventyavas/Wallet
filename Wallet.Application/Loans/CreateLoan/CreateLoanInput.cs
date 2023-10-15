using Wallet.Domain.Users;

namespace Wallet.Application.Loans.CreateLoan;

public sealed record CreateLoanInput(
    string Title,
    string Description,
    string LoadTypeCode,
    decimal TotalAmount,
    string CurrencyCode,
    User User
);