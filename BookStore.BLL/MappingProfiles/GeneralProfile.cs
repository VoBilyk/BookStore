namespace BookStore.BLL.MappingProfiles
{
    using System.Linq;
    using AutoMapper;

    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Client, ClientDto>()
                .ForMember(dto => dto.WishListId, model => model.MapFrom(m => m.WishList.Select(x => x.Id)));

            CreateMap<ClientDto, Client>()
                .ForMember(model => model.WishList, dto => dto.Ignore());

            CreateMap<Comment, CommentDto>()
                .ForMember(dto => dto.ClientId, model => model.MapFrom(m => m.Client.Id))
                .ForMember(dto => dto.BookId, model => model.MapFrom(m => m.Book.Id));

            CreateMap<CommentDto, Comment>()
                .ForMember(model => model.Book, dto => dto.Ignore())
                .ForMember(model => model.Client, dto => dto.Ignore());
        }
    }
}
