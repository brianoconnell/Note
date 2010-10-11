using System;

namespace Note.Core.Commands
{
    public class RemoveNoteCommand
    {
        public Entities.Note Note { get; private set; }

        public RemoveNoteCommand(Entities.Note note)
        {
            Note = note;
        }
    }
}