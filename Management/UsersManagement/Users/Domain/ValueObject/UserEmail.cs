using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersManagement.Users.Domain.ValueObject;

public class UserEmail
{
    public string Email { get; set; }

    private UserEmail(string email)
    {
        this.CheckIfEmailIsCorrect(email);
        this.Email = email;
    }

    public static UserEmail Create(string email)
    {
        return new UserEmail(email);
    }
    

    private void CheckIfEmailIsCorrect(string email)
    {
        if (!email.Contains("@"))
        {
            throw new InvalidEmailException();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is UserEmail email && this.Email == email.Email;
    }
    
    
}