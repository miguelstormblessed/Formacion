using Moq;
using UsersManagement.Counters.Domain;

namespace UsersTests.UsersManagement.Counters.Application;

public class CountersModuleApplicationUnitTestCase
{
    protected readonly Mock<ICountRepository> CountersRepository;
    protected readonly string uuid = "6a853495-b793-4066-884e-b8ea4751ead8";
    protected CountersModuleApplicationUnitTestCase()
    {
        CountersRepository = new Mock<ICountRepository>();
    }

    protected void ShouldFindCount(Count count)
    {
        this.CountersRepository.Setup(repo => repo.Find(this.uuid)).ReturnsAsync(count);
    }

    protected void ShouldUpdateCount(Count count)
    {
        this.CountersRepository.Setup(repo => repo.Update(count));
    }

    protected void ShouldHaveCalledFindeOnceWithCorrectParameters()
    {
        this.CountersRepository.Verify(repo => repo.Find(this.uuid), Times.Once);
    }

    protected void ShouldHaveCalledUpdateOnceWithCorrectParameters(Count count)
    {
        this.CountersRepository.Verify(repo => repo.Update(count), Times.Once);
    }
    
    
}