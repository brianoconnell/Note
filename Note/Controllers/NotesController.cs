using System;
using System.Web.Mvc;
using Ninject;
using Note.Core;
using Note.Core.Commands;
using Note.Core.Repositories;
using Note.ViewModels;

namespace Note.Controllers
{
    public class NotesController : Controller
    {
        private readonly ICommandInvoker commandInvoker;
        private readonly INoteRepository noteRepository;

        [Inject]
        public NotesController(ICommandInvoker commandInvoker, INoteRepository noteRepository)
        {
            this.commandInvoker = commandInvoker;
            this.noteRepository = noteRepository;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult New()
        {
            return View(new NoteNewViewModel());
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(NoteNewViewModel model)
        {
            commandInvoker.Execute(new AddNewNoteCommand(model.Title, model.Content, Guid.NewGuid(), DateTime.Now));
            return RedirectToAction("new");
        }
    }
}
