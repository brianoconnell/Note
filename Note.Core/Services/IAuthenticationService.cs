namespace Note.Core.Services
{
    public interface IAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}