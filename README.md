
# Cервис API

Данный сервис предоставляет точки доступа для управления сущностями в базе данных PostgreSQL. Включает конфигурацию Docker Compose для настройки сервиса и базы данных.
Начало работы

    Клонируйте данный репозиторий.
    Установите Docker Desktop (версия 4.28.0 подойдёт) и Docker Compose, если они еще не установлены.
Использование:

Для запуска сервиса и базы данных, перейдите в каталог проекта и выполните:

make run или docker-compose up --build -d
Точки доступа API

    Создание заявки: POST /applications
    Редактирование заявки: PUT /applications/{applicationId}
    Удаление заявки: DELETE /applications/{applicationId}
    Отправка заявки на рассмотрение: POST /applications/{applicationId}/submit
    Получение заявок поданных после указанной даты: GET /applications&submittedAfter="дата и время"
    Получение заявок не поданных и старше определенной даты: GET /applications&unsubmittedOlder="дата и время"
    Получение текущей не поданной заявки для указанного пользователя: GET /users/{userId}/currentapplication
    Получение заявки по идентификатору: GET /applications/{applicationId}
    Gолучение списка возможных типов активности: GET /activities

HOST http://localhost:8080 

SWAGGER документация http://localhost:8080/swagger/index.html

В браузере Mozilla Firefox может наблюдаться сбой при поптыке обращения к документации Swagger.  
