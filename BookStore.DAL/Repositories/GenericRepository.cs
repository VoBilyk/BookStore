namespace BookStore.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BookStore.DAL.Interfaces;

    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        protected readonly List<TEntity> _db;

        public GenericRepository(List<TEntity> context)
        {
            _db = context;
        }

        public TEntity Get(Guid id)
        {
            var item = _db.Find(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }

        public IEnumerable<TEntity> Get()
        {
            return _db;
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var foundedItems = _db.Where(predicate);

            // TODO: check
            if (foundedItems == null)
            {
                throw new ArgumentException($"Can`t find items");
            }

            return foundedItems;
        }

        public void Create(TEntity item)
        {
            var foundedItem = _db.Find(i => i.Id == item.Id);

            if (foundedItem != null)
            {
                throw new ArgumentException($"Item with id: {item.Id} has alredy exist");
            }

            _db.Add(item);
        }

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
