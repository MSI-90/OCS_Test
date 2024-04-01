
# Cервис API

Данный сервис предоставляет точки доступа для управления сущностями в базе данных PostgreSQL. Включает конфигурацию Docker Compose для настройки сервиса и базы данных.
Начало работы
<ul>
    <li> Клонируйте данный репозиторий. </li>
    <li> Установите Docker Desktop (версия 4.28.0 подойдёт) и Docker Compose, если они еще не установлены. </li>
</ul>
<dl> Использование: </dl>
<dt> Для запуска сервиса и базы данных, перейдите в каталог проекта и выполните: </dt>
<dd> make run или docker-compose up --build -d </dd>
<hr />
<p>
    <h4>Точки доступа API</h4>
</p>
<ul>
    <li> Создание заявки: POST /applications </li>
    <li> Редактирование заявки: PUT /applications/{applicationId} </li>
    <li> Удаление заявки: DELETE /applications/{applicationId} </li>
    <li> Отправка заявки на рассмотрение: POST /applications/{applicationId}/submit </li>
    <li> Получение заявок поданных после указанной даты: GET /applications&submittedAfter="дата и время" </li>
    <li> Получение заявок не поданных и старше определенной даты: GET /applications&unsubmittedOlder="дата и время" </li>
    <li> Получение текущей не поданной заявки для указанного пользователя: GET /users/{userId}/currentapplication </li>
    <li> Получение заявки по идентификатору: GET /applications/{applicationId} </li>
    <li> Gолучение списка возможных типов активности: GET /activities </li>
</ul>
<p>Доступ и документация</p>
<span>
    <ul>
        <li>
            <i>HOST</i> http://localhost:8080 
        </li>
        <li>
            <i> SWAGGER документация </i> http://localhost:8080/swagger/index.html
        </li>
        <li style = "text-decoration:underline;">
           В браузере Mozilla Firefox может наблюдаться сбой при поптыке обращения к документации Swagger.
        </li>
    </ul>    
</span>
