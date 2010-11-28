using System.Collections.Generic;
using Note.Core.Entities;

namespace Note.Core.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
        IList<User> GetAll();
    }
}