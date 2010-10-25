using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Note.Core.Entities;

namespace Note.Core.Repositories
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public User GetByUsername(string username)
        {
            return session.Linq<User>().SingleOrDefault(user => user.Username == username);
        }

        public User GetByEmail(string email)
        {
            return session.Linq<User>().SingleOrDefault(user => user.Email == email);
        }
    }
}