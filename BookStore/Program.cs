using System;
using AutoMapper;
using FluentValidation;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using BookStore.ConsoleApp.Menu;
using BookStore.DAL;
using BookStore.DAL.Interfaces;
using BookStore.BLL.Interfaces;
using BookStore.BLL.Services;
using BookStore.BLL.Validators;
using BookStore.DAL.Models;
using BookStore.Shared.DTO;


namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);


            // Running app
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<MainPage>().Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            //Instance injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IService<BookDto>, BookService>();
            services.AddScoped<IService<ClientDto>, ClientService>();
            services.AddScoped<IService<CommentDto>, CommentService>();
            services.AddScoped(_ => MapperConfiguration().CreateMapper());
            services.AddTransient<AbstractValidator<Book>, BookValidator>();
            services.AddTransient<AbstractValidator<Client>, ClientValidator>();
            services.AddTransient<AbstractValidator<Comment>, CommentValidator>();
            services.AddSingleton<MainPage>();
        }

        public static MapperConfiguration MapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();

                cfg.CreateMap<Client, ClientDto>()
                    .ForMember(dto => dto.WishListId, model => model.MapFrom(m => m.WishList.Select(x => x.Id)));

                cfg.CreateMap<ClientDto, Client>()
                    .ForMember(model => model.WishList, dto => dto.Ignore());

                cfg.CreateMap<Comment, CommentDto>()
                    .ForMember(dto => dto.ClientId, model => model.MapFrom(m => m.Client.Id))
                    .ForMember(dto => dto.BookId, model => model.MapFrom(m => m.Book.Id));

                cfg.CreateMap<CommentDto, Comment>()
                    .ForMember(model => model.Book, dto => dto.Ignore())
                    .ForMember(model => model.Client, dto => dto.Ignore());
            });

            return config;
        }
    }
}
