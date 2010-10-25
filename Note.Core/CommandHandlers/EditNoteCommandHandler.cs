using System;
using Note.Core.Commands;
using Note.Core.Repositories;

namespace Note.Core.CommandHandlers
{
    public class EditNoteCommandHandler : ICommandHandler<EditNoteCommand>
    {
        private readonly INoteRepository noteRepository;

        public EditNoteCommandHandler(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public void Handle(EditNoteCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException("command");
            }

            var note = noteRepository.GetNote(command.NoteId);
            note.Title = command.Title;
            note.Content = command.Content;

            noteRepository.Update(note);
        }
    }
}