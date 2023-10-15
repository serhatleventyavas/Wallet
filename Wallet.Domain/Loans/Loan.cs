using Wallet.Domain.Abstractions;
using Wallet.Domain.Users;

namespace Wallet.Domain.Loans;

public sealed class Loan: Entity
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public LoanType LoanType { get; private set; } 
    public Money TotalAmount { get; private set; }

    private Loan(Guid id, Guid userId, string title, string description, LoanType loanType, Money totalAmount) : base(id, DateTime.UtcNow)
    {
        UserId = userId;
        SetTitle(title);
        SetDescription(description);
        LoanType = loanType;
        TotalAmount = totalAmount;
    }

    public static Loan Create(User user,
        string title, string description, LoanType loanType, Money totalAmount)
    {
        var userId = user.Id;

        var loan = new Loan(Guid.NewGuid(), userId, title, description, loanType, totalAmount);
        return loan;
    }

    internal void SetTitle(string title) { 
        var trimTitle = title.Trim();
        if (string.IsNullOrEmpty(trimTitle))
        {
            throw new RequiredLoanTitleException();
        }

        if (trimTitle.Length < 3)
        {
            throw new MinLengthLoanTitleException();
        }

        if (trimTitle.Length > 120)
        {
            throw new MaxLengthLoanTitleException();
        }
        Title = title; 
    }

    internal void SetDescription(string description) {
        var trimDescription = description.Trim();
        if (trimDescription.Length > 255)
        {
            throw new MaxLengthLoanDescriptionException();
        }
        Description = description;
    }
}
