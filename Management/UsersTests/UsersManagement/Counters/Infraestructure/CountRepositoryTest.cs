using FluentAssertions;
using UsersManagement.Counters.Domain;
using UsersTests.UsersAPI.Configuration;
using UsersTests.UsersManagement.Counters.Domain.ValueObject;

namespace UsersTests.UsersManagement.Counters.Infraestructure;
public class CountRepositoryTest : CounterModuleInfraestructureTestCase
{
    [Fact]
    public async Task ShouldFindCount()
    {
        // GIVEN
        string id = "6a853495-b793-4066-884e-b8ea4751ead8";
        // WHEN
        Count count = await this.CountRepository.Find(id);
        // THEN
        count.Should().NotBeNull();
        count.Should().BeOfType<Count>();
    }

    [Fact]
    public async Task ShouldUpdate()
    {
        // GIVEN
        string id = "6a853495-b793-4066-884e-b8ea4751ead8";
        Count count = await this.CountRepository.Find(id);
        int oldActives = count.ActiveUsers;
        int oldInactives = count.InactiveUsers;
        count.Update(oldActives + 1,oldInactives);
        // WHEN
        await this.CountRepository.Update(count);
        // THEN
        Count newCount = await this.CountRepository.Find(id);
        newCount.Should().NotBeNull();
        newCount.ActiveUsers.Should().Be(oldActives + 1);
        newCount.InactiveUsers.Should().Be(oldInactives); 
    }
}