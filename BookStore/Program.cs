﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Initializate();

            Menu.Run();
        }

        static void Initializate()
        {
            BookCatalog.Instance.AddBook(new Book("The Dark Half", "Stephen King", "Fantasy", 40));
            BookCatalog.Instance.AddBook(new Book("It", "Stephen King", "Horror", 66));
            BookCatalog.Instance.AddBook(new Book("Harry Potter", "Joanne Rowling", "Fantasy", 30));


            ClientService.Instance.AddClient(new Client("Volodymyr", "Bilyk"));
            ClientService.Instance.AddClient(new Client("Roman", "Velikiy"));
            ClientService.Instance.AddClient(new Client("Dmytro", "Horobriy"));
        }
    }
}
