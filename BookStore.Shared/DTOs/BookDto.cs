namespace BookStore.Shared.DTOs
{
    using System;

    public class BookDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, author: {Author}, genre: {Genre}, price: {Price}";
        }
    }
}
