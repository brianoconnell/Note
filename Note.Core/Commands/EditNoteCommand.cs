using System;

namespace Note.Core.Commands
{
    public class EditNoteCommand
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Guid NoteId { get; private set; }

        public EditNoteCommand(string title, string content, Guid noteId)
        {
            Title = title;
            Content = content;
            NoteId = noteId;
        }
    }
}