namespace Note.Core.Commands
{
    public class AddNewUserCommand
    {
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public string Email { get; private set; }

        public AddNewUserCommand(string userName, string passwordHash, string passwordSalt, string email)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Email = email;
        }
    }
}