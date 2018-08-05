namespace BookStore.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> Get();

        TEntity Get(Guid id);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid id);
    }
}
