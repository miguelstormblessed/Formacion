using Microsoft.VisualStudio.TestPlatform.TestHost;
using UsersManagement.Users.Domain;

namespace UsersTests.Users.Infraestructure;

public class UserModuleInfraestructureTestCase : InfraestructureTestCase<Program>
{
    protected IUserRepository UserRepository => this.GetService<IUserRepository>();
}