using Ninject.Modules;
using Note.Core.Repositories;

namespace Note.Core.Infrastructure.Modules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INoteRepository>().To<NoteRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}