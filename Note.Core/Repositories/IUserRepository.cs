using Note.Core.Entities;

namespace Note.Core.Repositories
{
    public interface IUserRepository : IEntityRepository<Entities.User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
        void Update(User user);
    }
}