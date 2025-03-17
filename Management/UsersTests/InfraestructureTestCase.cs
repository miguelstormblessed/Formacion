using Microsoft.Extensions.DependencyInjection;

namespace UsersTests;

[Collection("Tests collection")]
public abstract class InfraestructureTestCase<T> where T : class
{
    private readonly TestWebApplicationFactory _factory;
    private readonly IServiceScope _scope;

    protected InfraestructureTestCase()
    {
        
        _factory = new TestWebApplicationFactory();
        _scope = _factory.Server.Services.CreateScope();
    }

    protected T GetService<T>() where T : class
    {
        return _scope.ServiceProvider.GetService<T>();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scope.Dispose();
            _factory.Dispose();
        }
    }

}