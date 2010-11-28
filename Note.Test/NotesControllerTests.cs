using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Note.Controllers;
using Note.Core;
using Note.Core.Commands;
using Note.Core.Entities;
using Note.Core.Repositories;
using Note.ViewModels;
using NUnit.Framework;

namespace Note.Test
{
    [TestFixture]
    public class NotesControllerTests
    {
        [Test]
        public void GetToNewShouldReturnNewViewWithUninitializedViewModel()
        {
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockNoteRepository = new Mock<INoteRepository>();

            var controller = new NotesController(mockCommandInvoker.Object, mockNoteRepository.Object, mockUserRepository.Object);
            var result = controller.New() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("new", result.ViewName);
            var model = result.ViewData.Model as EditNoteViewModel;
            Assert.IsNotNull(model);
            Assert.IsNull(model.Content);
            Assert.IsNull(model.Title);
        }

        [Test]
        public void PostToNewShouldInvokeAddNewNoteCommand()
        {
            var noteNewViewModel = new EditNoteViewModel
            {
                Content = "This is some content",
                Title = "This is the title"
            };

            var id = Guid.NewGuid();
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetByUsername("boco")).Returns(new User { Id = id });
            var notesController = GetNotesController(mockCommandInvoker, new Mock<INoteRepository>(), mockUserRepository);

            notesController.New(noteNewViewModel);

            mockCommandInvoker.Verify(x =>
                x.Execute(
                    It.Is<AddNewNoteCommand>(
                        nnc => nnc.Content == noteNewViewModel.Content && nnc.Title == noteNewViewModel.Title && nnc.OwnerId == id)), Times.Once());
        }

        [Test]
        public void PostToNewShouldInvokeAddNewNoteCommandWithCurrentlYLoggedinUserAsNoteOwner()
        {
            var noteNewViewModel = new EditNoteViewModel
            {
                Content = "This is some content",
                Title = "This is the title"
            };

            var id = Guid.NewGuid();
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetByUsername("boco")).Returns(new User { Id = id });
            var notesController = GetNotesController(mockCommandInvoker, new Mock<INoteRepository>(), mockUserRepository);
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.Setup(x => x.HttpContext.User.Identity.Name).Returns("boco");
            notesController.ControllerContext = mockHttpContext.Object;
            notesController.New(noteNewViewModel);

            mockHttpContext.VerifyGet(x => x.HttpContext.User.Identity.Name, Times.Once());

        }

        [Test]
        public void PostToNewShouldRedirectToIndexAction()
        {
            var noteNewViewModel = new EditNoteViewModel
            {
                Content = "This is some content",
                Title = "This is the title"
            };

            var id = Guid.NewGuid();
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetByUsername("boco")).Returns(new User { Id = id });
            var notesController = GetNotesController(mockCommandInvoker, new Mock<INoteRepository>(), mockUserRepository);

            var result = notesController.New(noteNewViewModel) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("index", result.RouteValues["action"]);
        }

        [Test]
        public void GetToEditShouldRedirectToIndexWhenNoteDoesNotExist()
        {
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockNoteRepository = new Mock<INoteRepository>();

            mockUserRepository.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(new User
                                                                                           {
                                                                                               Notes =
                                                                                                   new List
                                                                                                   <Core.Entities.Note>()
                                                                                           });

            var notesController = GetNotesController(mockCommandInvoker, mockNoteRepository, mockUserRepository);
            var result = notesController.Edit(Guid.NewGuid().ToString()) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("index", result.RouteValues["action"]);
        }

        [Test]
        public void GetToEditShouldReturnEditViewForSpecifiedNote()
        {
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockNoteRepository = new Mock<INoteRepository>();
            Guid ownerId = Guid.NewGuid();
            Guid noteId = Guid.NewGuid();
            User user = new User();
            user.Notes = new List<Core.Entities.Note>{new Core.Entities.Note{
                Id = noteId,
                Title = "Test Title",
                Content = "Test Content",
                OwnerId = ownerId}};
            mockUserRepository.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(user);


            var notesController = GetNotesController(mockCommandInvoker, mockNoteRepository, mockUserRepository);
            var result = notesController.Edit(noteId.ToString()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("edit", result.ViewName);

            var model = result.ViewData.Model as EditNoteViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual("Test Title", model.Title);
            Assert.AreEqual("Test Content", model.Content);
        }

        [Test]
        public void PostToEditShouldRedirectToIndexForNonExistantNote()
        {
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockNoteRepository = new Mock<INoteRepository>();
            mockNoteRepository.Setup(x => x.GetNote(It.IsAny<Guid>())).Returns((Core.Entities.Note)null);
            var notesController = new NotesController(mockCommandInvoker.Object, mockNoteRepository.Object, mockUserRepository.Object);
            var result = notesController.Edit(null, Guid.NewGuid().ToString()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("index", result.RouteValues["action"]);
        }

        [Test]
        public void PostToEditShouldExecuteEditNoteCommandForTheSpecifiedNoteAndRedirectsToIndex()
        {
            var mockCommandInvoker = new Mock<ICommandInvoker>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockNoteRepository = new Mock<INoteRepository>();

            Guid noteId = Guid.NewGuid();
            Guid ownerId = Guid.NewGuid();
            var note = new Core.Entities.Note
                                          {
                                              Content = "Test Content",
                                              Title = "Test Title",
                                              Id = noteId,
                                              OwnerId = ownerId
                                          };
            mockNoteRepository.Setup(x => x.GetNote(noteId)).Returns(note);
            mockUserRepository.Setup(x => x.GetByUsername("boco")).Returns(new User { Id = ownerId });
            var notesController = GetNotesController(mockCommandInvoker, mockNoteRepository,
                                                      mockUserRepository);

            var editNoteViewModel = new EditNoteViewModel { Content = "New Content", Title = "New Title" };
            var result = notesController.Edit(editNoteViewModel, noteId.ToString()) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("index", result.RouteValues["action"]);
            mockCommandInvoker.Verify(x => x.Execute(It.Is<EditNoteCommand>(enc => enc.Content == editNoteViewModel.Content && enc.Title == editNoteViewModel.Title && enc.NoteId == noteId)), Times.Once());
        }

        private static NotesController GetNotesController(Mock<ICommandInvoker> mockCommandInvoker, Mock<INoteRepository> mockNoteRepository, Mock<IUserRepository> mockUserRepository)
        {
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.Setup(x => x.HttpContext.User.Identity.Name).Returns("boco");
            var notesController = new NotesController(mockCommandInvoker.Object, mockNoteRepository.Object,
                                                      mockUserRepository.Object) { ControllerContext = mockHttpContext.Object };
            return notesController;
        }
    }
}