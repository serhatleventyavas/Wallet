namespace Wallet.Domain.Loans;

public sealed class RequiredLoanTitleException: Exception;
public sealed class MinLengthLoanTitleException : Exception;
public sealed class MaxLengthLoanTitleException : Exception;
public sealed class MaxLengthLoanDescriptionException : Exception;
public sealed class MaxLoanCountException: Exception;
public sealed class InvalidLoanTypeException: Exception;
public sealed class InvalidCurrencyException: Exception;
public sealed class AmountMustBePositiveException: Exception;
public sealed class CurrenciesMustBeSameException: Exception;
public sealed class InvalidAmountException: Exception;