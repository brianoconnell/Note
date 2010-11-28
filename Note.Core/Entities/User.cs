using System;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class User : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set;}
        public virtual string Email{ get; set;}
        public virtual IList<Note> Notes { get; set; }
    }
}