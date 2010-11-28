using System.Web.Mvc;
using System.Web.Security;
using Note.Core.Repositories;
using Note.Core.Services;
using Note.ViewModels;

namespace Note.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMembershipService membershipService;
        private readonly IUserRepository userRepository;
        public UserController(IAuthenticationService authenticationService, IMembershipService membershipService, IUserRepository userRepository)
        {
            this.authenticationService = authenticationService;
            this.userRepository = userRepository;
            this.membershipService = membershipService;
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new UserSignInViewModel());
        }

        [HttpPost]
        public ActionResult SignIn(UserSignInViewModel model, string returnUrl)
        {
            if (membershipService.ValidateUser(model.Username, model.Password))
            {
                authenticationService.SignIn(model.Username, model.StaySignedIn);
                if (string.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("index", "notes");
            }

            ModelState.AddModelError("", "The username or password provided is incorrect.");
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordRepeat)
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return View(model);
                }

                MembershipCreateStatus status = membershipService.CreateUser(model.Username, model.Password, model.Email);
                if (status == MembershipCreateStatus.Success)
                {
                    ViewData["RegistrationSuccessMessage"] = "You have registered. Hooray for you!";
                    return RedirectToAction("index", "notes");
                }
            }
            // Something has gone wrong if we are here so go back to the registration page.
            return View(model);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            authenticationService.SignOut();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public ActionResult List()
        {
            var users = userRepository.GetAll();

            return View("list", new ListUsersViewModel {Users = users});
        }

    }
}
