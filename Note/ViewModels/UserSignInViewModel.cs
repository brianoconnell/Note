using System.ComponentModel;

namespace Note.ViewModels
{
    public class UserSignInViewModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Stay signed in")]
        public bool StaySignedIn { get; set; }
    }
}