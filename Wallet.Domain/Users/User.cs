using Wallet.Domain.Abstractions;

namespace Wallet.Domain.Users;

public sealed class User : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsPremium { get; private set; } 

    private User(Guid id, string firstName, string lastName, string email, string password, bool isPremium) : base(id, DateTime.UtcNow)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        IsPremium = isPremium;
    }

    public static User Create(string firstName, string lastName, string email, string password, bool isPremium)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email, password, isPremium);

        return user;
    }
}

