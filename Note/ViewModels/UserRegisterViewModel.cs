using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Note.ViewModels
{
    public class UserRegisterViewModel
    {
        [DisplayName("Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a username")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must choose a password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please repeat the password")]
        public string PasswordRepeat { get; set; }

        [DisplayName("Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must supply an email address")]
        public string Email { get; set; }
    }
}