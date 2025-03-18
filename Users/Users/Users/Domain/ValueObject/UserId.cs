using System.Text.RegularExpressions;
using Users.Shared.Users.Domain.Exceptions;

namespace UsersManagement.Users.Domain.ValueObject;

public class UserId
{
    public string Id { get; set; }

    private UserId(string id)
    {
        this.ChecksId(id);
        this.Id = id;
    }
    
    public static UserId Create(string id)
    {
        return new UserId(id);
    }

    private void ChecksId(string id)
    {
        // Expresión regular para UUID
        string uuidPattern = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";

        // Verificar si el id coincide con el patrón de UUID
        if (!Regex.IsMatch(id, uuidPattern))
        {
            throw new InvalidIdException();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is UserId id && this.Id.Equals(id.Id);
    }

}