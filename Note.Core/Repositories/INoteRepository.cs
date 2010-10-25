using System;
using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Repositories
{
    public interface INoteRepository : IEntityRepository<Entities.Note>
    {
        IList<Entities.Note> GetByOwnerId(Guid userId);
        Entities.Note GetNote(Guid noteId);
    }
}