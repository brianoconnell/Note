using System;
using Moq;
using Note.Core.CommandHandlers;
using Note.Core.Commands;
using Note.Core.Repositories;

namespace Note.Core.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class AddNewNoteCommandHandlerTests
    {
        [Test]
        public void HandleShouldAddNoteToRepository()
        {
            var mockRepository = new Mock<INoteRepository>();
            var now = DateTime.Now;
            var ownerId = Guid.NewGuid();
            var command = new AddNewNoteCommand("testTitle", "testContent", ownerId, now);
            var handler = new AddNewNoteCommandHandler(mockRepository.Object);

            handler.Handle(command);

            mockRepository.Verify(repos => repos.Add(It.Is<Entities.Note>(note => note.Title == "testTitle" &&
                                                                                  note.Content == "testContent" &&
                                                                                  note.OwnerId == ownerId &&
                                                                                  note.DateAdded == now)));
        }

        [Test]
        public void HandleThrowsExceptionWhenCommandIsMissingAndDoesNotAddNoteToRepository()
        {
            var mockRepository = new Mock<INoteRepository>();
            var handler = new AddNewNoteCommandHandler(mockRepository.Object);
            Assert.Throws<ArgumentNullException>(() => handler.Handle(null));
            mockRepository.Verify(x => x.Add(It.IsAny<Entities.Note>()), Times.Never());
        }
    }
}