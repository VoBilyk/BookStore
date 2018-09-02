# BookStore

## What is done:
* Solution created by .NET Core (It was tested on Windows and Ubuntu 18.04) and has following features:
    * Console application to interact with the user used current UI system localization (ukrainian or english by default)
    * managing the catalog of books - adding, finding, editing, deleting book descriptions, commenting on books.
    * managing by clients - adding, finding, editing, deleting items, creating comments, wishlist.
    * authorization.
    * searching. 

* Existed entities Book, Client, WishList(BookClient, n:m), Comments (Client Comments for Book, 1:n) 
* Created by multilayer architecture (Presentation layer, Business layer, Data Access layer)
* Data Access layer implemented by UnitOfWork pattern with repositories

*Used standart .NET Core IoC container, AutoMapper, FluentValidator, Bogus, Nlog, NUnit, FakeItEasy*

[SolarScanner analysis](https://sonarcloud.io/dashboard?id=5211bookstore)

##Updates:
* Upd1: Added logging used NLogger
* Upd2: Changed platform for ConsoleApplication to .NET Core
* Upd3: Added documentation
* Upd4: Added unit test used NUnit

###First branch -> Second branch
* Upd1: added custom logger with loger factory. Logger has queue with buffer by 10 elements (it can be changed through settings). It also has event AddedLog which is notify when log was added to queue.