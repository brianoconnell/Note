using System;
using Ninject;
using Note.Core.Commands;
using Note.Core.Repositories;

namespace Note.Core.CommandHandlers
{
    public class AddNewNoteCommandHandler : ICommandHandler<AddNewNoteCommand>
    {
        private readonly INoteRepository noteRepository;

        [Inject]
        public AddNewNoteCommandHandler(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public void Handle(AddNewNoteCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException("command");
            }

            var note = new Entities.Note
                           {
                               Content = command.Content,
                               DateAdded = command.DateAdded,
                               Title = command.Title,
                               OwnerId = command.OwnerId
                           };

            this.noteRepository.Add(note);
        }
    }
}