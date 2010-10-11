using FluentNHibernate.Mapping;

namespace Note.Core.Infrastructure.Mappings
{
    public class NoteMap : ClassMap<Entities.Note>
    {
        public NoteMap()
        {
            Table("Notes");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Title);
            Map(x => x.Content);
            Map(x => x.OwnerId);
            Map(x => x.DateAdded);
        }
    }
}