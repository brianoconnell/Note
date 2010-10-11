using System;
using Ninject;
using Note.Core.Commands;
using Note.Core.Entities;
using Note.Core.Repositories;

namespace Note.Core.CommandHandlers
{
    public class AddNewUserCommandHandler : ICommandHandler<AddNewUserCommand>
    {
        private readonly IUserRepository userRepository;

        [Inject]
        public AddNewUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Handle(AddNewUserCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException("command");
            }

            var user = new User
                            {
                                Username = command.UserName,
                                PasswordHash = command.PasswordHash,
                                PasswordSalt = command.PasswordSalt,
                                Email = command.Email
                            };

            userRepository.Add(user);
        }
    }
}