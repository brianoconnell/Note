using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using Ninject;

namespace Note.Core.Repositories
{
    public class NoteRepository : EntityRepository<Entities.Note>, INoteRepository
    {
        [Inject]
        public NoteRepository(ISession session) : base(session)
        {
        }

        public IList<Entities.Note> GetByOwnerId(Guid userId)
        {
            return session.Linq<Entities.Note>().Where(note => note.OwnerId == userId).ToList();
        }
    }
}