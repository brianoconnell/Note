using System.ComponentModel;

namespace Note.ViewModels
{
    public class EditNoteViewModel
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Content")]
        public string Content { get; set; }
    }
}