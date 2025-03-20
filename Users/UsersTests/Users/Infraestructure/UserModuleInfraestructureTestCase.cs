using Microsoft.VisualStudio.TestPlatform.TestHost;
using Users.Users.Domain;

namespace UsersTests.Users.Infraestructure;

public class UserModuleInfraestructureTestCase : InfraestructureTestCase<Program>
{
    protected IUserRepository UserRepository => this.GetService<IUserRepository>();
}