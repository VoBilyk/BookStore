namespace BookStore.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Interfaces.Repositories;

    /// <inheritdoc/>
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly List<TEntity> _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">Data context</param>
        public GenericRepository(List<TEntity> context)
        {
            _db = context;
        }

        /// <inheritdoc/>
        public TEntity Get(Guid id)
        {
            var item = _db.Find(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Get()
        {
            return _db;
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _db.Where(predicate);
        }

        /// <inheritdoc/>
        public void Create(TEntity item)
        {
            var foundedItem = _db.Find(i => i.Id == item.Id);

            if (foundedItem != null)
            {
                throw new ArgumentException($"Item with id: {item.Id} has already exist");
            }

            _db.Add(item);
        }

        /// <inheritdoc/>
        public void Update(TEntity item)
        {
            var foundedItem = _db.Find(t => t.Id == item.Id);

            if (foundedItem == null)
            {
                throw new ArgumentException($"Item id: {item.Id} which need update don`t exists");
            }

            _db.Remove(foundedItem);
            _db.Add(item);
        }

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            var ticket = _db.Find(item => item.Id == id);

            if (ticket == null)
            {
                throw new ArgumentException($"Item with id: {id} don`t exists");
            }

            _db.Remove(ticket);
        }
    }
}
