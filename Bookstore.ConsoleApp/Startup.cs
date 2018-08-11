namespace BookStore.ConsoleApp
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

    public static class Startup
    {
        public static Guid? CurrentClientId { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            // Instance injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IWishListService, WishListService>();

            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<Client>, ClientValidator>();
            services.AddTransient<IValidator<Comment>, CommentValidator>();
            services.AddTransient<IValidator<Wish>, WishValidator>();

            NLog.LogManager.LoadConfiguration("nlog.config");
            services.AddSingleton(new LoggerFactory().AddNLog());
            services.AddLogging();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            services.AddTransient<MainPage>();
            services.AddTransient<ClientMenuPage>();
            services.AddTransient<BookMenuPage>();
        }
    }
}
