namespace BookStore
{
    using AutoMapper;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.DAL;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Running app
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<MainPage>().Run();
        }

        private static void ConfigureServices(IServiceCollection services)
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

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            services.AddSingleton(_ => config.CreateMapper());

            services.AddSingleton<MainPage>();
            services.AddSingleton<ClientMenuPage>();
            services.AddSingleton<BookMenuPage>();
        }
    }
}
