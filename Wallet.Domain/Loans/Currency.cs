namespace Wallet.Domain.Loans;

public sealed record Currency
{
    public static readonly Currency Try = new("TRY");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Gbp = new("GBP");
    public static readonly IReadOnlyCollection<Currency> All = new[] { Try, Eur, Usd, Gbp };

    public string Code { get; init; }

    private Currency(string code) => Code = code;

    public static Currency GetByCode(string code)
    {
        return All.FirstOrDefault(p => p.Code == code) ?? throw new InvalidCurrencyException();
    }
}