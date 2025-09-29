Backend-часть простой системы управления задачами (аналог Jira).   
Основные требования:

Задачи:
- CRUD-операции.
- Поля: автор, исполнитель, статус (New, InProgress, Done), приоритет (Low, Medium, High).
- Вложенности: подзадачи и связи между задачами (“related to”).

Пользователи:
- Авторизация: прикрутить простой вариант JWT
- Регистрация/хранение пользователей не требуется.

БД:
- EF Core.
- MSSQL или PostgreSQL (на выбор).
- Миграции через EF Core (Code First).

Технические требования:

- .NET 8, ASP.NET Core.
- Dependency Injection.
- MediatR CQRS
- Swagger для документации API.
- Логирование (можно стандартное).
- Желательно docker-compose для запуска.

Запуск:   
  docker-compose up -d   
  http://localhost:8080/   
  или http://localhost:8080/swagger/index.html   
