using System;
using System.Collections.Generic;

namespace BookStore.Shared.DTO
{

    public class ClientDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public ICollection<Guid> WishListId { get; set; }


        public override string ToString()
        {
            return $"Fullname: {FirstName} {SecondName}";
        }
    }
}
