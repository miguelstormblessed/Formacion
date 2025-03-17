using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersManagement.Users.Domain.ValueObject;

public class UserName
{
    public string Name { get; set; }

    private UserName(string name)
    {
        this.CheckIfNameIsValid(name);
        this.Name = name;
    }
    

    public static UserName Create(string name)
    {
        return new UserName(name);
    }

    private void CheckIfNameIsValid(string name)
    {
        if(name.Length <= 5)
        {
            throw new InvalidUserNameException();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is UserName name && this.Name.Equals(name.Name);
    }
    
    
}