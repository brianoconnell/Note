using System.Collections.Generic;

namespace Note.ViewModels
{
    public class ListNotesViewModel
    {
        public IList<Core.Entities.Note> Notes { get; set; }
    }
}