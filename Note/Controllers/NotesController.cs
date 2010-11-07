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
        [HttpGet]
        //[CompactFilter]
        public ActionResult Index()
        {
            return View("index");
        }


        [Authorize]
        [HttpGet]
        [CompactFilter]
        public ActionResult New()
        {
            return View("new", new EditNoteViewModel());
        }

        [Authorize]
        [HttpPost]
        [CompactFilter]
        public ActionResult New(EditNoteViewModel model)
        {
            var user = userRepository.GetByUsername(User.Identity.Name);
            commandInvoker.Execute(new AddNewNoteCommand(model.Title, model.Content, user.Id, DateTime.Now));
            return RedirectToAction("list");
        }

        [Authorize]
        [HttpGet]
        [CompactFilter]
        public ActionResult List()
        {
            var model = new ListNotesViewModel();
            var user = userRepository.GetByUsername(User.Identity.Name);
            model.Notes = noteRepository.GetByOwnerId(user.Id);
            return View("list", model);
        }

        [Authorize]
        [HttpGet]
        [CompactFilter]
        public ActionResult Edit(string noteId)
        {
            Guid noteGuid = Guid.Parse(noteId);
            // Make sure the Note belongs to this user
            Core.Entities.Note note = noteRepository.GetNote(noteGuid);
            if (note == null)
            {
                return RedirectToAction("list");
            }

            var user = userRepository.GetByUsername(User.Identity.Name);
            if (note.OwnerId != user.Id)
            {
                return RedirectToAction("list");
            }

            return View("edit", new EditNoteViewModel { Title = note.Title, Content = note.Content });
        }

        [Authorize]
        [HttpPost]
        [CompactFilter]
        public ActionResult Edit(EditNoteViewModel model, string noteId)
        {
            Guid noteGuid = Guid.Parse(noteId);
            Core.Entities.Note note = noteRepository.GetNote(noteGuid);
            if (note == null)
            {
                return RedirectToAction("list");
            }

            var user = userRepository.GetByUsername(User.Identity.Name);
            if (note.OwnerId != user.Id)
            {
                return RedirectToAction("list");
            }

            commandInvoker.Execute(new EditNoteCommand(model.Title, model.Content, noteGuid));

            return RedirectToAction("list");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAllJson()
        {
            var model = new ListNotesViewModel();
            var user = userRepository.GetByUsername(User.Identity.Name);
            model.Notes = noteRepository.GetByOwnerId(user.Id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
