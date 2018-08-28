namespace BookStore.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.Shared.Resources;

    /// <inheritdoc />
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly List<TEntity> _db;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="context">Data context</param>
        public GenericRepository(List<TEntity> context)
        {
            this._db = context;
        }

        /// <inheritdoc />
        public TEntity Get(Guid id)
        {
            var item = _db.Find(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"{Resource.NotFound}, {id}");
            }

            return item;
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> Get()
        {
            return _db;
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _db.Where(predicate);
        }

        /// <inheritdoc />
        public void Create(TEntity entity)
        {
            var foundedItem = _db.Find(i => i.Id == entity.Id);

            if (foundedItem != null)
            {
                throw new ArgumentException($"{Resource.AlreadyExist}, {entity.Id}");
            }

            _db.Add(entity);
        }

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            var foundedItem = _db.Find(t => t.Id == entity.Id);

            if (foundedItem == null)
            {
                throw new ArgumentException($"{Resource.NotExist}, {entity.Id}");
            }

            _db.Remove(foundedItem);
            _db.Add(entity);
        }

        /// <inheritdoc />
        public void Delete(Guid id)
        {
            var ticket = _db.Find(item => item.Id == id);

            if (ticket == null)
            {
                throw new ArgumentException($"{Resource.NotExist}, {id}");
            }

            _db.Remove(ticket);
        }
    }
}