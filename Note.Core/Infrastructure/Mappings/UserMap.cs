using FluentNHibernate.Mapping;
using Note.Core.Entities;

namespace Note.Core.Infrastructure.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Username);
            Map(x => x.PasswordHash);
            Map(x => x.PasswordSalt);
            Map(x => x.Email);
            HasMany(x => x.Notes).KeyColumn("OwnerId").Cascade.AllDeleteOrphan();
        }
    }
}