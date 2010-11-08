using System;
using System.Collections.Generic;
using Moq;
using NHibernate;
using NHibernate.Linq;
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

        [Test]
        [Ignore]
        public void GetByOwnerIdOnlyReturnsNotesFromSpecifiedOwner()
        {
            Guid ownerId = Guid.NewGuid();
            var mockSession = new Mock<ISession>();
            var mockQueryable = new Mock<INHibernateQueryable<Entities.Note>>();
            IList<Entities.Note> notes = new List<Entities.Note>();
            notes.Add(new Entities.Note{OwnerId = ownerId});
            notes.Add(new Entities.Note{OwnerId = Guid.NewGuid()});
            mockQueryable.Setup(x => x.GetEnumerator()).Returns(notes.GetEnumerator);
            mockSession.Setup(x => x.Linq<Entities.Note>()).Returns(mockQueryable.Object);

            var noteRepository = new NoteRepository(mockSession.Object);
            var result = noteRepository.GetByOwnerId(ownerId);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(ownerId, result[0].OwnerId);
        }
    }
}