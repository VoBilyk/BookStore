namespace BookStore.Shared.DTOs
{
    using System;

    /// <summary>
    /// Data transfer object for comment
    /// </summary>
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid ClientId { get; set; }

        public Guid BookId { get; set; }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
