using System;
using System.Security.Cryptography;
using System.Web.Security;
using Ninject;
using Note.Core.Commands;
using Note.Core.Entities;
using Note.Core.Repositories;

namespace Note.Core.Services
{
    public class NoteMembershipProvider : MembershipProvider
    {
        private readonly IUserRepository usersRepository;
        private readonly ICommandInvoker commandInvoker;

        [Inject]
        public NoteMembershipProvider(IUserRepository usersRepository, ICommandInvoker commandInvoker)
        {
            this.usersRepository = usersRepository;
            this.commandInvoker = commandInvoker;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            // Check if the user already exists or the email address is a duplicate
            var existingUser = usersRepository.GetByUsername(username);
            if (existingUser != null)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            existingUser = usersRepository.GetByEmail(email);
            if (existingUser != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            // Generate a cryptographic random number
            string salt = CreateSalt(32);
            string hashedPassword = CreateHashedPassword(password, salt);
            
            var command = new AddNewUserCommand(username, hashedPassword, salt, email);
            commandInvoker.Execute(command);
            status = MembershipCreateStatus.Success;
            return null;
        }

        private static string CreateHashedPassword(string password, string salt)
        {
            string saltAndPassword = string.Concat(password, salt);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, "sha1");
        }

        private static string CreateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[saltSize];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = usersRepository.GetByUsername(username);

            // Make sure the old passwords match
            string hashedOldPassword = CreateHashedPassword(oldPassword, user.PasswordSalt);
            if (hashedOldPassword != user.PasswordHash)
                return false;

            string newSalt = CreateSalt(32);
            user.PasswordHash = CreateHashedPassword(newPassword, newSalt);
            user.PasswordSalt = newSalt;
            usersRepository.Update(user);

            return true;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            User user = usersRepository.GetByUsername(username);
            if(user == null)
            {
                return false;
            }

            string hashedPassword = CreateHashedPassword(password, user.PasswordSalt);
            return (hashedPassword == user.PasswordHash);
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}