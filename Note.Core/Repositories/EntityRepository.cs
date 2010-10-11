using System;
using NHibernate;
using Note.Core.Entities;

namespace Note.Core.Repositories
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : IEntity
    {
        protected readonly ISession session;

        public EntityRepository(ISession session)
        {
            this.session = session;
        }

        public TEntity Load(Guid id)
        {
            return session.Load<TEntity>(id);
        }

        public void Add(TEntity entity)
        {
            this.session.Save(entity);
        }

        public void Remove(TEntity entity)
        {
            this.session.Delete(entity);
        }
    }
}