﻿namespace BookStore.Shared.DTOs
{
    using System;

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