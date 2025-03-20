using Users.Users.Domain.ValueObject;

namespace Users.Users.Domain
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<Usuario>> SearchUsers();
        void Save(Usuario usuario);
        void Update(Usuario usuario);
        
        Usuario? Find(UserId id);
        
        void Delete(UserId id);

    }
}
