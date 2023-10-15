namespace Wallet.Domain.Loans;

public sealed record LoanType
{
    public static readonly LoanType PersonalFinanceCredit = new("PersonalFinanceCredit");
    public static readonly LoanType Mortgage= new ("Mortgage");
    public static readonly LoanType CreditCardInstallment = new("CreditCardInstallment");
    public static readonly IReadOnlyCollection<LoanType> All = new[] { PersonalFinanceCredit, Mortgage, CreditCardInstallment };

    public string Code { get; init; }

    private LoanType(string code)
    {
        Code = code;
    }

    public static LoanType GetByCode(string code)
    {
        return All.FirstOrDefault(p => p.Code == code) ?? throw new InvalidLoanTypeException();
    }
}
