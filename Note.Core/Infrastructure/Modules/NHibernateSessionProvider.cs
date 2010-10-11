using NHibernate;
using Ninject.Activation;

namespace Note.Core.Infrastructure.Modules
{
    public class NHibernateSessionProvider : Provider<ISession>
    {
        protected override ISession CreateInstance(IContext context)
        {
            return NHibernateSessionManager.Instance.GetSession();
        }
    }
}