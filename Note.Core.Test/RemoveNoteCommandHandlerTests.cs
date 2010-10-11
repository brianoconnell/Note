using System;
using Moq;
using Note.Core.CommandHandlers;
using Note.Core.Commands;
using Note.Core.Repositories;
using NUnit.Framework;

namespace Note.Core.Test
{
    [TestFixture]
    public class RemoveNoteCommandHandlerTests
    {
        [Test]
        public void HandleShouldRemoveNoteFromRepository()
        {   
            var note = new Entities.Note();
            var removeNoteCommand = new RemoveNoteCommand(note);

            var mockRepository = new Mock<INoteRepository>();
            var removeNoteCommandHandler = new RemoveNoteCommandHandler(mockRepository.Object);
            
            removeNoteCommandHandler.Handle(removeNoteCommand);

            mockRepository.Verify(x => x.Remove(note), Times.Once());
        }

        [Test]
        public void HandleShouldThrowExceptionWhenCommandIsMissingAndDoesNotRemoveNote()
        {
            var mockRepository = new Mock<INoteRepository>();
            var removeNoteCommandHandler = new RemoveNoteCommandHandler(mockRepository.Object);

            Assert.Throws<ArgumentNullException>(() => removeNoteCommandHandler.Handle(null));

            mockRepository.Verify(x => x.Remove(It.IsAny<Entities.Note>()), Times.Never());
        }
    }
}