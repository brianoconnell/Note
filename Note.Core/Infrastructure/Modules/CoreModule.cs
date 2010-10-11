using NHibernate;
using Ninject;
using Ninject.Modules;

namespace Note.Core.Infrastructure.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISession>().ToProvider(new NHibernateSessionProvider());
            Bind<IKernel>().ToProvider(new KernelProvider());
            Bind<ICommandInvoker>().To<CommandInvoker>();
        }
    }
}