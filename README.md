<<<<<<< HEAD
# tennis-scoreboard
tennis-scoreboard-service: REST API, ASP.NET Core, MySQL.Data, ConnectionPool, MySQL

# Swagger documentation
<a href=""> Social media API OpenAPI specification <a/>

### Application is available at <a href=""> tennis-scoreboard <a/>

# Техническое задание
Веб-приложение, реализующее табло счёта теннисного матча.

## Мотивация проекта

- Создать клиент-серверное приложение с веб-интерфейсом
- Сверстать простой веб-интерфейс без сторонних библиотек
- Познакомиться с архитектурным паттерном MVC(S)

## Функционал приложения

Работа с матчами:

- Создание нового матча
- Просмотр законченных матчей, поиск матчей по именам игроков
- Подсчёт очков в текущем матче

## База данных

В качестве базы данных используется EF Core. Это in-memory SQL база для C#. In-memory означает то, что движок БД
и сами таблицы существуют только внутри памяти приложения. При использовании in-memory хранилища необходимо
инициализировать таблицы базы данных при каждом старте приложения.

#### Таблица `Players` - игроки

| Имя колонки | Тип     | Комментарий                   |
|-------------|---------|-------------------------------|
| ID          | Int     | Первичный ключ, автоинкремент |
| Name        | Varchar | Имя игрока                    |

Индексы:

- Индекс колонки `Name`, для эффективности поиска игроков по имени

### Таблица `Matches` - завершенные матчи

Для упрощения, в БД сохраняются только доигранные матчи в момент их завершения.

| Имя колонки | Тип | Комментарий                                     |
|-------------|-----|-------------------------------------------------|
| ID          | Int | Первичный ключ, автоинкремент                   |
| Player1     | Int | Айди первого игрока, внешний ключ на Players.ID |
| Player2     | Int | Айди второго игрока, внешний ключ на Players.ID |
| Winner      | Int | Айди победителя, внешний ключ на Players.ID     |

## MVCS

MVCS - архитектурный паттерн, особенно хорошо подходящий под реализацию подобных приложений. 
=======
# tennis-scoreboard
>>>>>>> d37fdeae32cb03e66f46a8d569a8e1260b30a7b7
