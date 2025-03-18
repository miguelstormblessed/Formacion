using FluentAssertions;
using UsersManagement.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;
using UsersTests.Users.Domain.ValueObject;

namespace UsersTests.Users.Infraestructure
{
    public class UserRepositoryTest : UserModuleInfraestructureTestCase
    {
        [Fact]
        public void ShouldSaveUserAndFindIt_WhenCallsDatabase()
        {
            // GIVEN
            Usuario user = UserMother.CreateRandom();
            // WHEN
            this.UserRepository.Save(user);
            Usuario? result = this.UserRepository.Find(user.Id);
            // THEN
            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task ShouldReturnAllUser_WhenSearchUsersIsInvoked()
        {
            // GIVEN
            Usuario user = UserMother.CreateRandom();
            this.UserRepository.Save(user);
            // WHEN
            IEnumerable<Usuario> result = await this.UserRepository.SearchUsers();
            // THEN
            result.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void ShouldDeleteUser_WhenDeleteIsInvoked()
        {
            // GIVEN
            Usuario user = Usuario.CreateUserActivated(
                UserIdMother.CreateRandom(),
                UserNameMother.CreateRandom(),
                UserEmailMother.CreateRandom(),
                VehicleResponseMother.CreateRandom());
            this.UserRepository.Save(user);
            // WHEN
            this.UserRepository.Delete(user.Id);
            Usuario? userDeleted = this.UserRepository.Find(user.Id);
            // THEN
            userDeleted?.State.Active.Should().BeFalse();
        }

        [Fact]
        public void ShouldUpdateUser_WhenUpdateIsInvoked()
        {
            // GIVEN
            Usuario user = UserMother.CreateRandom();
            this.UserRepository.Save(user);
            Usuario newUser = UserMother.CreateRandom();
            newUser.Id = user.Id;
            // WHEN
            this.UserRepository.Update(newUser);
            // THEN
            Usuario? userUpdated = this.UserRepository.Find(user.Id);
            userUpdated.Should().BeEquivalentTo(newUser);
        }
        
    }
}


