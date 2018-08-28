namespace BookStore.Shared.DTOs
{
    using System;
    using System.Collections.Generic;

    using BookStore.Shared.Resources;

    public class BookDto
    {
        public BookDto()
        {
            WishedClientsId = new List<Guid>();
            UserCommentsId = new List<Guid>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }

        public List<Guid> WishedClientsId { get; set; }

        public List<Guid> UserCommentsId { get; set; }

        public override string ToString()
        {
            return $"{Resource.Name}: {Name}, {Resource.Author}: {Author}, {Resource.Genre}: {Genre}, {Resource.Price}: {Price}";
        }
    }
}
