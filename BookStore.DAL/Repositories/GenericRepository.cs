using System;
using System.Linq;
using System.Collections.Generic;
using BookStore.DAL.Interfaces;

namespace BookStore.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly List<TEntity> db;

        public GenericRepository(List<TEntity> context)
        {
            this.db = context;
        }

        public TEntity Get(Guid id)
        {
            var item = db.Find(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }

        public IEnumerable<TEntity> Get()
        {
            return db;
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var foundedItems = db.Where(predicate);

            // TODO: check
            if (foundedItems == null)
            {
                throw new ArgumentException($"Can`t find items");
            }

            return foundedItems;
        }

        public void Create(TEntity item)
        {
            var foundedItem = db.Find(i => i.Id == item.Id);

            if (foundedItem != null)
            {
                throw new ArgumentException($"Item with id: {item.Id} has alredy exist");
            }

            db.Add(item);
        }

        public void Update(TEntity item)
        {
            var foundedItem = db.Find(t => t.Id == item.Id);

            if (foundedItem == null)
            {
                throw new ArgumentException($"Item id: {item.Id} which need update don`t exists");
            }

            db.Remove(foundedItem);
            db.Add(item);
        }

        public void Delete(Guid id)
        {
            var ticket = db.Find(item => item.Id == id);

            if (ticket == null)
            {
                throw new ArgumentException($"Item with id: {id} don`t exists");
            }

            db.Remove(ticket);
        }

        public void Delete()
        {
            db.Clear();
        }
    }
}
