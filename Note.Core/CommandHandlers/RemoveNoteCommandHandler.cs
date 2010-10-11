using System;
using Ninject;
using Note.Core.Commands;
using Note.Core.Repositories;

namespace Note.Core.CommandHandlers
{
    public class RemoveNoteCommandHandler : ICommandHandler<RemoveNoteCommand>
    {
        private readonly INoteRepository repository;

        [Inject]
        public RemoveNoteCommandHandler(INoteRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(RemoveNoteCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException("command");
            }

            repository.Remove(command.Note);
        }
    }
}