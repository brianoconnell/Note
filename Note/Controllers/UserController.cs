using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Note.Core;
using Note.Core.Services;
using Note.ViewModels;

namespace Note.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMembershipService membershipService;
        private readonly ICommandInvoker commandInvoker;

        public UserController(IAuthenticationService authenticationService, IMembershipService membershipService, ICommandInvoker commandInvoker)
        {
            this.authenticationService = authenticationService;
            this.membershipService = membershipService;
            this.commandInvoker = commandInvoker;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignIn()
        {
            return View(new UserSignInViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(UserSignInViewModel model, string returnUrl)
        {
            if(membershipService.ValidateUser(model.Username, model.Password))
            {
                authenticationService.SignIn(model.Username, model.StaySignedIn);
                if(string.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }
                
                return RedirectToAction("New", "Notes");
            }
            
            ModelState.AddModelError("","The username or password provided is incorrect.");
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        public ActionResult Register(UserRegisterViewModel model)
        {
            if(model.Password != model.PasswordRepeat)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View(model);
            }

            MembershipCreateStatus status = membershipService.CreateUser(model.Username, model.Password, model.Email);
            if(status == MembershipCreateStatus.Success)
            {
                ViewData["RegistrationSuccessMessage"] = "You have registered. Hooray for you!";
                return RedirectToAction("List", "Notes");
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
