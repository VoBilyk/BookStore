﻿namespace BookStore.BLL.MappingProfiles
{
    using AutoMapper;

    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    /// <inheritdoc/>
    public class GeneralProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralProfile"/> class.
        /// </summary>
        public GeneralProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Comment, CommentDto>()
                .ForMember(dto => dto.ClientId, model => model.MapFrom(m => m.Client.Id))
                .ForMember(dto => dto.BookId, model => model.MapFrom(m => m.Book.Id));

            CreateMap<CommentDto, Comment>()
                .ForMember(model => model.Book, dto => dto.Ignore())
                .ForMember(model => model.Client, dto => dto.Ignore());

            CreateMap<Wish, WishDto>();
            CreateMap<WishDto, Wish>();
        }
    }
}
