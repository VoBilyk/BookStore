# BookStore

## Що зроблено:
* Створено solution з наступними функціями:
    * Console application для взаємодії з користувачем
    * управління каталогом книжок - додавання, редагування, видалення описів книжок, коментарів до книжок.
    * ведення WishList клієнтів.
    * пошук. 
    * управління клієнтами - додавання, редагування, видалення.
    * авторизація користувача
* Присутні сутності Book, Client, WishList(BookClient, n:m),Comments (Client Comments for Book, 1:n) 
* Проект створено за багаторівневою архітектурою (Presentation layer, Business layer, Data Access layer)
* Data Access layer сформовано за UnitOfWork паттерном, що містить repositories

*Використано IoC контейнер .NET Core, AutoMapper, Bogus, Nlog*

##Updates:
* Upd1: Added logging used NLogger
* Upd2: Changed platform for ConsoleApplication to .NET Core
* Upd3: Added documentation
* Upd4: Added unit test used NUnit