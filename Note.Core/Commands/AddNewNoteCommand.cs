using System;

namespace Note.Core.Commands
{
    public class AddNewNoteCommand
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Guid OwnerId { get; private set; }
        public DateTime DateAdded { get; private set; }

        public AddNewNoteCommand(string title, string content, Guid ownerId, DateTime dateAdded)
        {
            Title = title;
            Content = content;
            OwnerId = ownerId;
            DateAdded = dateAdded;
        }
    }
}