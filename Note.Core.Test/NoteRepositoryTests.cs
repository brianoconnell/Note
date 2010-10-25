using System;
using Moq;
using NHibernate;
using Note.Core.Repositories;
using NUnit.Framework;

namespace Note.Core.Test
{
    [TestFixture]
    public class NoteRepositoryTests
    {
        [Test]
        public void EntityIsSavedInSessionWhenAddIsCalled()
        {
            var mockSession = new Mock<ISession>();
            
            NoteRepository noteRepository = new NoteRepository(mockSession.Object);
            Entities.Note note = new Entities.Note();
            noteRepository.Add(note);

            mockSession.Verify(x => x.Save(note), Times.Once());
        }

        [Test]
        public void EntityIsLoadedFromSessionWhenLoadIsCalled()
        {
            var mockSession = new Mock<ISession>();
            NoteRepository noteRepository = new NoteRepository(mockSession.Object);

            Guid id = Guid.NewGuid();
            noteRepository.Load(id);

            mockSession.Verify(x => x.Load<Entities.Note>(id), Times.Once());
        }

        [Test]
        public void EntityIsDeletedFromSessionWhenRemoveIsCalled()
        {
            var mockSession = new Mock<ISession>();
            NoteRepository noteRepository = new NoteRepository(mockSession.Object);

            Entities.Note note = new Entities.Note();
            noteRepository.Remove(note);

            mockSession.Verify(x => x.Delete(note), Times.Once());
        }

        [Test]
        public void NoteIsUpdatedInSessionWhenUpdateIsCalled()
        {
            var mockSession = new Mock<ISession>();
            NoteRepository noteRepository = new NoteRepository(mockSession.Object);

            Entities.Note note = new Entities.Note();
            noteRepository.Update(note);

            mockSession.Verify(x => x.Update(note), Times.Once());
        }
    }
}