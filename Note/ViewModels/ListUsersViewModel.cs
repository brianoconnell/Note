using System.Collections.Generic;
using Note.Core.Entities;

namespace Note.ViewModels
{
    public class ListUsersViewModel
    {
        public IList<User> Users { get; set; }
    }
}