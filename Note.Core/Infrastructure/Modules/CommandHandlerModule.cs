using Ninject.Modules;
using Note.Core.CommandHandlers;
using Note.Core.Commands;

namespace Note.Core.Infrastructure.Modules
{
    public class CommandHandlerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandler<AddNewNoteCommand>>().To<AddNewNoteCommandHandler>();
            Bind<ICommandHandler<RemoveNoteCommand>>().To<RemoveNoteCommandHandler>();
            Bind<ICommandHandler<AddNewUserCommand>>().To<AddNewUserCommandHandler>();
            Bind<ICommandHandler<EditNoteCommand>>().To<EditNoteCommandHandler>();
        }
    }
}