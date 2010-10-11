using System;
using NHibernate;
using Ninject;

namespace Note.Core
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly IKernel container;
        private readonly ISession session;

        [Inject]
        public CommandInvoker(IKernel container, ISession session)
        {
            this.container = container;
            this.session = session;
        }

        public void Execute<T>(T command)
        {
            using (var transaction = session.BeginTransaction())
            {
                var handler = container.Get<ICommandHandler<T>>();
                try
                {
                    handler.Handle(command);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}