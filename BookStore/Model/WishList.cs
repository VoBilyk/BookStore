using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class WishList
    {
        private static readonly Lazy<WishList> lazy = new Lazy<WishList>(() => new WishList());
        public static WishList Instance { get { return lazy.Value; } }

        private List<Wish> wishes;

        public IEnumerable<Wish> GetWishes { get { return wishes; } }


        private WishList()
        {
            wishes = new List<Wish>();
        }

        public void AddWish(Client client, Book book)
        {
            wishes.Add(new Wish(client, book));
        }

        public void RemoveWish(Client client, Book book)
        {
            if (!wishes.Exists(wish => (wish.Client == client) && (wish.Book == book)))
            {
                throw new ArgumentException($"Wish doesn`t exist");
            }

            wishes.RemoveAll(wish => (wish.Client == client) && (wish.Book == book));
        }


        public IEnumerable<Wish> FindByClient(Client client)
        {
            return wishes.FindAll(wish => wish.Client == client);
        }

        public IEnumerable<Wish> FindByBook(Book book)
        {
            return wishes.FindAll(wish => wish.Book == book);
        }
    }
}
