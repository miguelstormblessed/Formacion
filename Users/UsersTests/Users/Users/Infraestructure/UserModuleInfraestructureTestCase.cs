using Users.Users.Domain;

namespace UsersTests.Users.Users.Infraestructure;

public class UserModuleInfraestructureTestCase : InfraestructureTestCase<Program>
{
    protected IUserRepository UserRepository => this.GetService<IUserRepository>();
}