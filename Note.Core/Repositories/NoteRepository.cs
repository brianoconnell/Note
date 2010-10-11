using System;
using NHibernate;
using Ninject;

namespace Note.Core.Repositories
{
    public class NoteRepository : EntityRepository<Entities.Note>, INoteRepository
    {
        [Inject]
        public NoteRepository(ISession session) : base(session)
        {
        }
    }
}