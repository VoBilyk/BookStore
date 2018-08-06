namespace Bookstore.ConsoleApp
{
    using System;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.DAL;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    public static class Startup
    {
        public static Guid? CurrentClientId { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            // Instance injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IService<BookDto>, BookService>();
            services.AddScoped<IService<ClientDto>, ClientService>();
            services.AddScoped<IService<CommentDto>, CommentService>();
            services.AddScoped<IService<WishDto>, WishListService>();

            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<Client>, ClientValidator>();
            services.AddTransient<IValidator<Comment>, CommentValidator>();
            services.AddTransient<IValidator<Wish>, WishValidator>();

            NLog.LogManager.LoadConfiguration("nlog.config");
            services.AddSingleton(new LoggerFactory().AddNLog());
            services.AddLogging();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            services.AddSingleton(_ => config.CreateMapper());

            services.AddTransient<MainPage>();
            services.AddTransient<ClientMenuPage>();
            services.AddTransient<BookMenuPage>();
        }
    }
}
