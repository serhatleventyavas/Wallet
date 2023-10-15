namespace Wallet.Domain.Loans;

public sealed record Money
{
    public decimal Amount { get; init; } 
    public Currency Currency { get; init; }
    
    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new AmountMustBePositiveException();
        }

        return new Money(amount, currency);
    }
    
    private static void Validate(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new CurrenciesMustBeSameException();
        }

        if (first.Amount < 0)
        {
            throw new AmountMustBePositiveException();        
        }

        if (second.Amount < 0)
        {
            throw new AmountMustBePositiveException();        
        }

        if (first.Amount >= int.MaxValue)
        {
            throw new InvalidAmountException();
        }

        if (second.Amount >= int.MaxValue)
        {
            throw new InvalidAmountException();
        }
    }

    public static Money operator +(Money first, Money second)
    {
        Validate(first, second);

        var tempSum = first.Amount + second.Amount;

        if (tempSum >= int.MaxValue)
        {
            throw new InvalidAmountException();
        }

        var tempDiff = tempSum - first.Amount;

        if (tempDiff != second.Amount)
        {
            throw new InvalidAmountException();
        }

        return new Money(first.Amount + second.Amount, first.Currency);
    }

    public static Money operator -(Money first, Money second)
    {
        Validate(first, second);

        var diff = first.Amount - second.Amount;

        if (diff < 0)
        {
            throw new AmountMustBePositiveException();
        }

        return new Money(diff, first.Currency);
    }
}