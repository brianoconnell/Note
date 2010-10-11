using System;
using System.Web.Security;
using Ninject.Modules;
using Note.Core.Services;

namespace Note.Core.Infrastructure.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthenticationService>().To<FormsAuthenticationService>();
            Bind<IMembershipService>().To<UserService>();
            Bind<MembershipProvider>().To<NoteMembershipProvider>();
        }
    }
}