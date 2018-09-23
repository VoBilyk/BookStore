namespace BookStore.ConsoleApp
{
    using System;
    using System.Text;
    using System.Threading;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Serializers;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.DAL;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared;
    using BookStore.Shared.Interfaces;
    using BookStore.Shared.Logger;

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
            services.AddSingleton<DataSource>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFileSerializer, BinaryFileSerializer>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IFileService, FileService>();

            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<Client>, ClientValidator>();
            services.AddTransient<IValidator<Comment>, CommentValidator>();
            services.AddTransient<IValidator<Wish>, WishValidator>();

            services.AddTransient<ICustomLoggerFactory, CustomLoggerFactory>();

            services.AddAutoMapper(cfg => { cfg.AddProfile<GeneralProfile>(); });

            services.AddTransient<IOutputEnvironment, OutputEnvironment>();
            services.AddTransient<IMenuVisualizer, MenuVisualizer>();

            services.AddTransient<MainPage>();
            services.AddTransient<IClientPage, ClientPage>();
            services.AddTransient<IBookPage, BookPage>();
            services.AddTransient<ISettingsPage, SettingsPage>();
        }

        /// <summary>
        ///     Configuration culture and localization
        /// </summary>
        public static void ConfigurateCulture()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var ci = Thread.CurrentThread.CurrentUICulture;
            LanguageSwitcher.Culture = ci;
        }
    }
}