# BookStore

## Що зроблено:
* Created .NET Core Console application, whith have following functions:
    * управління каталогом книжок - додавання, редагування, видалення описів книжок, коментарів до книжок.
    * ведення WishList клієнтів.
    * пошук. 
    * управління клієнтами - додавання, редагування, видалення.
    * авторизація користувача
* Присутні сутності Book, Client, WishList(BookClient, n:m),Comments (Client Comments for Book, 1:n) 
* Проект створено за багаторівневою архітектурою (Presentation layer, Business layer, Data Access layer)
* Data Access layer сформовано за UnitOfWork принципом

*Використані IoC контейнер .NET Core, AutoMapper, Bogus, Nloger*

##Upd1
* Added NLogger