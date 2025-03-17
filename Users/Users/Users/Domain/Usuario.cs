using Cojali.Shared.Domain.Entity;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Domain
{
    public class Usuario : AggregateRoot
    {
        private Usuario(UserId id,UserName userName, UserEmail email, UserState state, VehicleResponse vehicle)
        {
            this.Id = id;
            this.Name = userName;
            this.Email = email;
            this.State = state;
            this.Vehicle = vehicle;
        }
        protected Usuario(){}
        public UserId Id { get;  set; }
        public UserName Name { get; set; }
        public UserEmail Email { get; set; }
        public UserState State { get; set; }
        public VehicleResponse Vehicle { get; set; }
        
        
        public static Usuario CreateUserActivated(UserId id, UserName username, UserEmail email, VehicleResponse vehicle)
        {
            // hacemos new del usuario
            Usuario newUser = new Usuario(id, username, email, UserState.Create(true), vehicle);
            
            // creamos el evento con la información del usuario
            ActivatedUserCreated activatedUserCreated =
                ActivatedUserCreated.Create(id.Id, username.Name, email.Email, true);
            
            // al usuario creado activado le registramos su evento
            newUser.Record(activatedUserCreated);

            // retornamos el usuario creado activado y con el evento
            return newUser;
        }

        public static Usuario CreateUserDeactivated(UserId id, UserName username, UserEmail email, VehicleResponse vehicle)
        {
            // New user created
            Usuario newUser =  new Usuario(id, username, email, UserState.Create(false), vehicle);
            
            InactiveUserCreated inactiveUserCreated = InactiveUserCreated.Create(id.Id, username.Name, email.Email, false);
            
            newUser.Record(inactiveUserCreated);
            
            return newUser;
        }

        public static Usuario CreateActivatedUserMother(UserId id, UserName username, UserEmail email, VehicleResponse vehicle)
        {
            return new Usuario(id, username, email, UserState.Create(true), vehicle);
        }
        
        public static Usuario CreateDeativatedUserMother(UserId id, UserName username, UserEmail email, VehicleResponse vehicle)
        {
            return new Usuario(id, username, email, UserState.Create(false), vehicle);
        }

        public void Delete(UserId Id, UserName Username, UserEmail Email)
        {
            this.State = UserState.Create(false);
            UserDeleted userDeleted = UserDeleted.Create(Id.Id, Username.Name, Email.Email, this.State.Active);
            this.Record(userDeleted);
        }

        public void Update(UserId newId, UserName newUsername, UserEmail newEmail, UserState newState, VehicleResponse newVehicle)
        {
            var oldUuid = this.Id;
            var oldUsername = this.Name;
            var oldEmail = this.Email;
            var oldState = this.State;
            var olvedVehicle = this.Vehicle;
            
            this.Id = newId;
            this.Name = newUsername;
            this.Email = newEmail;
            this.State = newState;
            this.Vehicle = newVehicle;
            UserUpdated userUpdated = UserUpdated.Create(
                newId.Id, newUsername.Name, newEmail.Email, newState.Active, 
                oldUsername.Name, oldEmail.Email, oldState.Active);
            this.Record(userUpdated);
            
        }
        public override bool Equals(object obj)
        {
            return obj is Usuario && this.Id.Equals(((Usuario)obj).Id) 
                && this.Name.Equals(((Usuario)obj).Name)
                && this.Email.Equals(((Usuario)obj).Email)
                && this.State.Equals(((Usuario)obj).State);
        }

        
    }
}
