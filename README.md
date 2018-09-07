# BookStore

[Sandcastle Documentation](https://drive.google.com/file/d/124P1sjU_ZkF7DtyY7S8jme6ci7MD7b01/view?usp=sharing)

[SolarScanner code analysis](https://sonarcloud.io/dashboard?id=5211bookstore)

## What is done:
* Solution created by .NET Core (It was tested on Windows and Ubuntu 18.04) and has following features:
    * Console application to interact with the user used current UI system localization (ukrainian or english by default)
    * managing the catalog of books - adding, searching, editing, deleting book descriptions, commenting on books
    * managing by clients - adding, searching, editing, deleting items, creating comments, wishlist
    * authorization

* Existed entities Book, Client, WishList(BookClient, n:m), Comments (Client Comments for Book, 1:n) 
* Created by multilayer architecture (Presentation layer, Business layer, Data Access layer)
* Data Access layer implemented by UnitOfWork pattern with repositories

*Used standart .NET Core IoC container, AutoMapper, FluentValidator, Bogus, Nlog, NUnit, FakeItEasy*

##Updates:
* Upd1: added validation in services for create/update operation used FluentValidator
* Upd2: Added logging used NLogger
* Upd3: Changed platform for ConsoleApplication to .NET Core
* Upd4: Added documentation
* Upd5: Added unit test used NUnit

###First branch -> Second branch
* Upd1: added custom logger with loger factory. Logger has queue with buffer by 10 elements (it can be changed through settings).
    It also has event AddedLog which is notify when log was added to queue.
* Upd2: added working with files. There is ability save or restore collection with data to/from file in csv, xml, bin, json.
    For json serializer used NewtonSoft, for csv - ServiceStack, for xml and bin used standart .NET libraries.
* Upd3: generated documentation used Sandcastle Help File Builder