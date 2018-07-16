using System;
using System.Collections.Generic;


namespace BookStore.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get();

        TEntity Get(Guid id);

        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete();

        void Delete(Guid id);
    }
}
