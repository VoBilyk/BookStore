namespace BookStore.Shared.DTOs
{
    using System;
    using System.Collections.Generic;

    public class ClientDto
    {
        public ClientDto()
        {
            WishedBooksId = new List<Guid>();
            CommentsId = new List<Guid>();
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<Guid> WishedBooksId { get; set; }

        public List<Guid> CommentsId { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {SecondName}";
        }
    }
}
