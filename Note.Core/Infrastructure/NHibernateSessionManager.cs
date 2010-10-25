using System.Runtime.Remoting.Messaging;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using Configuration = NHibernate.Cfg.Configuration;
using System.Configuration;

namespace Note.Core.Infrastructure
{
    /// <summary>
    /// Handles the creation and management of NHibernate sessions
    ///  and transactions.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        private const string TransactionKey = "CONTEXT_TRANSACTION";
        private ISessionFactory sessionFactory;


        private NHibernateSessionManager()
        {
            InitSessionManager();
        }

        private void InitSessionManager()
        {
            sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("NotesDb")).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entities.IEntity>())
                .BuildSessionFactory();
        }

        public static NHibernateSessionManager Instance
        {
            get { return Nested.NHibernateSessionManager; }
        }


        private class Nested
        {
            internal static readonly NHibernateSessionManager NHibernateSessionManager =
                new NHibernateSessionManager();
        }

        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = ThreadSession;
            if (session != null && session.IsOpen)
                throw new CacheException("You cannot register an interceptor once a session is open.");
            GetSession(interceptor);
        }

        public ISession GetSession()
        {
            return GetSession(null);
        }

        public ISession GetSession(IInterceptor interceptor)
        {
            var session = ThreadSession;

            if (session == null)
            {
                session = interceptor != null ? Instance.sessionFactory.OpenSession(interceptor) : Instance.sessionFactory.OpenSession();

                ThreadSession = session;
            }

            return session;
        }

        public static void CloseSession()
        {
            ISession session = ThreadSession;

            ThreadSession = null;

            if (session != null && session.IsOpen)
                session.Close();
        }

        public void BeginTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                ThreadTransaction = transaction;
            }
        }

        public static void CommitTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            try
            {
                if (transaction != null &&
                    transaction.WasCommitted == false &&
                    transaction.WasRolledBack == false)
                {
                    transaction.Commit();
                    ThreadTransaction = null;
                }
            }
            catch (HibernateException)
            {
                RollbackTransaction();
                throw;
            }
        }

        private static void RollbackTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            try
            {
                ThreadTransaction = null;
                if (transaction != null &&
                    transaction.WasCommitted == false &&
                    transaction.WasRolledBack == false &&
                    transaction.IsActive)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                CloseSession();
            }
        }

        private static ITransaction ThreadTransaction
        {
            get
            {
                if (IsInWebContext)
                    return (ITransaction)HttpContext.Current.Items[TransactionKey];

                return (ITransaction)CallContext.GetData(TransactionKey);
            }
            set
            {
                if (IsInWebContext)
                    HttpContext.Current.Items[TransactionKey] = value;
                else
                    CallContext.SetData(TransactionKey, value);
            }
        }

        private static ISession ThreadSession
        {
            get
            {
                if (IsInWebContext)
                    return (ISession)HttpContext.Current.Items[SessionKey];

                return (ISession)CallContext.GetData(SessionKey);
            }
            set
            {
                if (IsInWebContext)
                    HttpContext.Current.Items[SessionKey] = value;
                else
                    CallContext.SetData(SessionKey, value);
            }
        }

        private static bool IsInWebContext
        {
            get { return HttpContext.Current != null; }
        }

        private const string SessionKey = "CONTEXT_SESSION";
    }

}