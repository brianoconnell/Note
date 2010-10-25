using System;
using Note.Core.Entities;

namespace Note.Core
{
    public interface IEntityRepository<TEntity> where TEntity : IEntity
    {
        TEntity Load(Guid id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity user);
    }
}