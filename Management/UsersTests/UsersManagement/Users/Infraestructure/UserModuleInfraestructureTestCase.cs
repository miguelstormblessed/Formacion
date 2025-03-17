using UsersManagement.Users.Domain;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests.UsersManagement.Users.Infraestructure;

public class UserModuleInfraestructureTestCase : InfraestructureTestCase<Program>
{
    protected IUserRepository UserRepository => this.GetService<IUserRepository>();
}