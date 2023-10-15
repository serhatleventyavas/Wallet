using Wallet.Application.Loans.CreateLoan;
using Wallet.Domain.Loans;
using Wallet.Domain.Users;

namespace Wallet.Application.Tests.Loans.CreateLoan;

public class CreateLoanServiceTests
{
    [Fact]
    public async Task Given_Empty_Title_Throw_RequiredLoanTitleException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "",
            Description: "",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<RequiredLoanTitleException>(() => service.Handle(input, new CancellationToken()));

    }

    [Fact]
    public async Task Given_2_Chars_Title_Throw_MinLengthLoanTitleException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: new string('*',2),
            Description: "",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<MinLengthLoanTitleException>(() => service.Handle(input, new CancellationToken()));

    }

    [Fact]
    public async Task Given_121_Chars_Title_Throw_MaxLengthLoanTitleException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: new string('*', 121),
            Description: "",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<MaxLengthLoanTitleException>(() => service.Handle(input, new CancellationToken()));

    }

    [Fact]
    public async Task Given_256_Chars_Description_Throw_MaxLengthLoanDescriptionException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: new string('*', 256),
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<MaxLengthLoanDescriptionException>(() => service.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Given_Empty_Loan_Type_Code_Throw_InvalidLoanTypeException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: "",
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<InvalidLoanTypeException>(() => service.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Given_Not_Exists_Code_Loan_Type_Code_Throw_InvalidLoanTypeException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: "invalidCode",
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<InvalidLoanTypeException>(() => service.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Given_Empty_Currency_Code_Throw_InvalidCurrencyException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: ""
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<InvalidCurrencyException>(() => service.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Given_Not_Exists_Currency_Code_Throw_InvalidCurrencyException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: "invalid_code"
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<InvalidCurrencyException>(() => service.Handle(input, new CancellationToken()));
    }
    
    [Fact]
    public async Task Given_Negative_Amount_Throw_AmountMustBePositiveException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: -10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository());

        await Assert.ThrowsAsync<AmountMustBePositiveException>(() => service.Handle(input, new CancellationToken()));
    }
    
    [Fact]
    public async Task Given_Freemium_User_With_5_Loans_Total_Throw_MaxLoanCountException()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", false);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );
        var service = new CreateLoanService(new FakeUnitOfWork(), new FakeLoanRepository(5));

        await Assert.ThrowsAsync<MaxLoanCountException>(() => service.Handle(input, new CancellationToken()));
    }
    
    [Fact]
    public async Task Given_Correct_Input_And_Freemium_User_Create_Loan_Successfully()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", false);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );

        var fakeUnitOfWork = new FakeUnitOfWork();
        var fakeLoanRepository = new FakeLoanRepository(4);
        var service = new CreateLoanService(fakeUnitOfWork, fakeLoanRepository);

        var loan = await service.Handle(input, new CancellationToken());
        
        Assert.Equal(1, fakeLoanRepository.TotalCalledGetTotalLoanCountByUserId);
        Assert.Equal(1, fakeUnitOfWork.Count);
        Assert.Contains(loan, fakeLoanRepository.AddedLoanList);
        Assert.NotNull(loan);
        Assert.Equal(loan.UserId, user.Id);
        Assert.Equal(loan.LoanType.Code, input.LoadTypeCode);
        Assert.Equal(loan.TotalAmount.Currency.Code, input.CurrencyCode);
        Assert.Equal(loan.TotalAmount.Amount, input.TotalAmount);
        Assert.Equal(loan.Title, input.Title);
        Assert.Equal(loan.Description, input.Description);
    }
    
    [Fact]
    public async Task Given_Correct_Input_And_Premium_User_Create_Loan_Successfully()
    {
        var user = User.Create("Serhat", "Yavaş", "serhatleventyavas@gmail.com", "test123", true);
        var input = new CreateLoanInput(
            User: user,
            Title: "Title",
            Description: "Description",
            LoadTypeCode: LoanType.Mortgage.Code,
            TotalAmount: 10_000,
            CurrencyCode: Currency.Try.Code
        );

        var fakeUnitOfWork = new FakeUnitOfWork();
        var fakeLoanRepository = new FakeLoanRepository(5);
        var service = new CreateLoanService(fakeUnitOfWork, fakeLoanRepository);

        var loan = await service.Handle(input, new CancellationToken());
        
        Assert.Equal(0, fakeLoanRepository.TotalCalledGetTotalLoanCountByUserId);
        Assert.Equal(1, fakeUnitOfWork.Count);
        Assert.Contains(loan, fakeLoanRepository.AddedLoanList);
        Assert.NotNull(loan);
        Assert.Equal(loan.UserId, user.Id);
        Assert.Equal(loan.LoanType.Code, input.LoadTypeCode);
        Assert.Equal(loan.TotalAmount.Currency.Code, input.CurrencyCode);
        Assert.Equal(loan.TotalAmount.Amount, input.TotalAmount);
        Assert.Equal(loan.Title, input.Title);
        Assert.Equal(loan.Description, input.Description);
    }
}