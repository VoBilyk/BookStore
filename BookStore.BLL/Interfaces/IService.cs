namespace BookStore.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IService<TDto>
    {
        TDto Get(Guid id);

        List<TDto> GetAll();

        TDto Find(TDto dto);

        void Create(TDto dto);

        void Update(Guid id, TDto dto);

        void Delete(Guid id);
    }
}
