namespace BookStore.Shared.DTOs
{
    using System;

    public class WishDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid BookId { get; set; }
    }
}
