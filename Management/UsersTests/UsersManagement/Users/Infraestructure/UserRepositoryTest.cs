using FluentAssertions;
using UsersManagement.Shared;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersAPI.Configuration;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Users.Infraestructure
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


