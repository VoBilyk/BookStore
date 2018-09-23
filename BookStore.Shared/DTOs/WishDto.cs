namespace BookStore.Shared.DTOs
{
    using System;

    /// <summary>
    /// Data transfer object for item of wish list
    /// </summary>
    public class WishDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid BookId { get; set; }
    }
}
