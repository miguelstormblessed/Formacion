using UsersManagement.Counters.Domain;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests.UsersManagement.Counters.Infraestructure;

public class CounterModuleInfraestructureTestCase : InfraestructureTestCase<Program>
{
    protected ICountRepository CountRepository => this.GetService<ICountRepository>();
}