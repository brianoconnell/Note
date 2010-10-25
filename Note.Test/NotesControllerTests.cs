using System;
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
            mockUserRepository.Setup(x => x.GetByUsername("boco")).Returns(new User {Id = id});
            var notesController = GetNotesController(mockUserRepository, mockCommandInvoker, new Mock<INoteRepository>());

            notesController.New(noteNewViewModel);

            mockCommandInvoker.Verify(x =>
                x.Execute(
                    It.Is<AddNewNoteCommand>(
                        nnc => nnc.Content == noteNewViewModel.Content && nnc.Title == noteNewViewModel.Title && nnc.OwnerId == id)), Times.Once());
        }

        private static NotesController GetNotesController(Mock<IUserRepository> mockUserRepository, Mock<ICommandInvoker> mockCommandInvoker, Mock<INoteRepository> mockNoteRepository)
        {
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.Setup(x => x.HttpContext.User.Identity.Name).Returns("boco");
            var notesController = new NotesController(mockCommandInvoker.Object, mockNoteRepository.Object,
                                                      mockUserRepository.Object)
                                      {ControllerContext = mockHttpContext.Object};
            return notesController;
        }
    }
}