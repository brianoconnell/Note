using System.ComponentModel;

namespace Note.ViewModels
{
    public class UserRegisterViewModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string PasswordRepeat { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }
    }
}