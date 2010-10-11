using System;

namespace Note.Core.Entities
{
    public class Note : IEntity
    {
        public virtual Guid Id { get; private set; }
        public virtual Guid OwnerId { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
    }
}