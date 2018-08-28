﻿namespace BookStore.ConsoleApp
{
    using System;
    using System.Text;
    using System.Threading;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NLog;
    using NLog.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.DAL;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared;

    /// <summary>
    ///     Class for configuration application before starting application
    /// </summary>
    public static class Startup
    {
        /// <summary>
        ///     Configuration services
        /// </summary>
        /// <param name="services">Collection of services</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            // Configuration additional services
            ConfigurateCulture();

            // Instance injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IWishListService, WishListService>();

            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<Client>, ClientValidator>();
            services.AddTransient<IValidator<Comment>, CommentValidator>();
            services.AddTransient<IValidator<Wish>, WishValidator>();

            LogManager.LoadConfiguration("nlog.config");
            services.AddSingleton(new LoggerFactory().AddNLog());
            services.AddLogging();

            services.AddAutoMapper(cfg => { cfg.AddProfile<GeneralProfile>(); });

            services.AddSingleton<IOutputEnvironment, OutputEnvironment>();
            services.AddSingleton<IMenuVisualizer, MenuVisualizer>();
            services.AddTransient<MainPage>();
            services.AddTransient<ClientMenuPage>();
            services.AddTransient<BookMenuPage>();
        }

        /// <summary>
        ///     Configuration culture and localization
        /// </summary>
        public static void ConfigurateCulture()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var ci = Thread.CurrentThread.CurrentUICulture;
            LanguageSwitcher.Culture = ci;
        }
    }
}