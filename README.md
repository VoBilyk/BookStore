# BookStore

## What is done:
* Created solution has following features:
    * Console application to interact with the user
    * managing the catalog of books - adding, editing, deleting book descriptions, commenting on books.
    * managing by clients - adding, editing, deleting items, creating comments, wishlist.
    * authorization.
    * searching. 

* Existed entities Book, Client, WishList(BookClient, n:m),Comments (Client Comments for Book, 1:n) 
* Created by multilayer architecture (Presentation layer, Business layer, Data Access layer)
* Data Access layer implemented by UnitOfWork pattern with repositories

*Used standart .NET Core IoC container, AutoMapper, FluentValidator, Bogus, Nlog, NUnit, FakeItEasy*

[SolarScanner analysis](https://sonarcloud.io/dashboard?id=5211bookstore)

##Updates:
* Upd1: Added logging used NLogger
* Upd2: Changed platform for ConsoleApplication to .NET Core
* Upd3: Added documentation
* Upd4: Added unit test used NUnit