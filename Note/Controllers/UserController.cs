using System.Web.Mvc;
using System.Web.Security;
using Note.Core.Services;
using Note.ViewModels;

namespace Note.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMembershipService membershipService;

        public UserController(IAuthenticationService authenticationService, IMembershipService membershipService)
        {
            this.authenticationService = authenticationService;
            this.membershipService = membershipService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignIn()
        {
            return View(new UserSignInViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(UserSignInViewModel model, string returnUrl)
        {
            if (membershipService.ValidateUser(model.Username, model.Password))
            {
                authenticationService.SignIn(model.Username, model.StaySignedIn);
                if (string.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("list", "notes");
            }

            ModelState.AddModelError("", "The username or password provided is incorrect.");
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

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
                    return RedirectToAction("list", "notes");
                }
            }
            // Something has gone wrong if we are here so go back to the registration page.
            return View(model);
        }

        public ActionResult SignOut()
        {
            authenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
