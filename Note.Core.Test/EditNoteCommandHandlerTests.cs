using System;
using Moq;
using Note.Core.CommandHandlers;
using Note.Core.Commands;
using Note.Core.Repositories;
using NUnit.Framework;

namespace Note.Core.Test
{
    [TestFixture]
    public class EditNoteCommandHandlerTests
    {
        [Test]
        public void HandleShouldUpdateNoteInRepository()
        {
            var mockRepository = new Mock<INoteRepository>();
            Guid id = Guid.NewGuid();
            mockRepository.Setup(x => x.GetNote(id)).Returns(new Entities.Note());

            var commandHandler = new EditNoteCommandHandler(mockRepository.Object);
            var command = new EditNoteCommand("New Title", "New Content", id);

            commandHandler.Handle(command);
            mockRepository.Verify(x=>x.Update(It.Is<Entities.Note>(note => note.Title == command.Title && note.Content == command.Content)), Times.Once());
        }
    }
}