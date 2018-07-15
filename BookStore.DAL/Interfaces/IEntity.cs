using System;

namespace BookStore.DAL.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
