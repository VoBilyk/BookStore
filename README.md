# BookStore

## Що зроблено:
* Створенно консольну програму, що виконує наступні функції:
    * управління каталогом книжок - додавання, редагування, видалення описів книжок, коментарів до книжок, ведення WishList клієнтів, пошук. 
* Присутні сутності Book, Client, WishList(BookClient, n:m),Comments (Client Comments for Book, 1:n) 
* Проект створено за багаторівневою архітектурою (Presentation layer, Business layer, Data Access layer)
* Data Access layer сформовано за UnitOfWork принципом

*Використані бібліотеки IoC контейнер, AutoMapper, Bogus*