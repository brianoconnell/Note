using System;
using System.Web.Mvc;
using Ninject;
using Note.Core;
using Note.Core.Commands;
using Note.Core.Repositories;
using Note.Filters;
using Note.ViewModels;

namespace Note.Controllers
{
    public class NotesController : Controller
    {
        private readonly ICommandInvoker commandInvoker;
        private readonly INoteRepository noteRepository;
        private readonly IUserRepository userRepository;

        [Inject]
        public NotesController(ICommandInvoker commandInvoker, INoteRepository noteRepository, IUserRepository userRepository)
        {
            this.commandInvoker = commandInvoker;
            this.userRepository = userRepository;
            this.noteRepository = noteRepository;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [CompactFilter]
        public ActionResult New()
        {
            return View(new NoteNewViewModel());
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [CompactFilter]
        public ActionResult New(NoteNewViewModel model)
        {
            var user = userRepository.GetByUsername(User.Identity.Name);
            commandInvoker.Execute(new AddNewNoteCommand(model.Title, model.Content, user.Id, DateTime.Now));
            return RedirectToAction("list");
        }

        [Authorize]
        [CompactFilter]
        public ActionResult List()
        {
            var model = new ListNotesViewModel();
            var user = userRepository.GetByUsername(User.Identity.Name);
            model.Notes = noteRepository.GetByOwnerId(user.Id);
            return View(model);
        }
    }
}
