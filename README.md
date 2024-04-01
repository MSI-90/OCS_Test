
# Cервис API

Данный сервис предоставляет точки доступа для управления сущностями в базе данных PostgreSQL. Включает конфигурацию Docker Compose для настройки сервиса и базы данных.
Начало работы
<ul>
    <li> Клонируйте данный репозиторий. </li>
    <li> Установите Docker Desktop (версия 4.28.0 подойдёт) и Docker Compose, если они еще не установлены. </li>
</ul>
<dl> Использование: </dl>
<dt> Для запуска сервиса и базы данных запустите Docker Desktop и перейдите в каталог проекта, затем выполните: </dt>
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
        <li>
           В браузере Mozilla Firefox может наблюдаться сбой при поптыке обращения к документации Swagger.
        </li>
        <li>
            Для того чтобы можно было взаимодействоать с API необходимо знать идентификатор тестового пользователя, <br/>
            две тестовые записи будут добавлен в таблицу users базы данных TestworkOnNotion после успешного запуска приложения,
            сама база данных также будет создана, также будет создана таблицы applications
        </li>
        <li>
            Чтобы оценить работоспособность приложения и взаиможействия его с базой данной postgreSQL можно поделючиться с помощью СУБД postgreSQL к нашей тестовой базе данных, <br/>
            я использую инструмент pgAdmin, после запуска pgAdmin необходимо будет зарегистрировать новый сервер, введите данные для подключения к нашей базе данных
            <ul>
                <li>в pgAdmin жмем правой клаыишей мыши и выбираем пункт Register -> Server</li>
                <li>Во вкладке General в поле name: здесь можно ввести любую строку</li>
                <li>Во вкладке Connection Host name/address: пишем localhost</li>
                <li<>Port 5433</li>
                <li<>Maintenance database: TestworkOnNotion</li>
                <li<>Username: postgres</li>
                <li>Password: postgres</ul>
                <li>Нажимаем кнопку Save</li>
            </ul>
        </li>
    </ul>
    <p>
        После всех проделаных манипуляций вы должны увидеть подклюбченную базу данных TestworkOnNotion в списке баз данных указанного вами серввера во вкладке General -> name. Раскройте нашу базу данных и пеерейдите в раздел Schemas -> Public -> Tables: и вы должны увидеть здесь 3 тьблицы, 
        нас интересуют здесь 2 таблицы users и applications, правой клавишей по таблице users и выьираем Query Tool, открывается редактор SQL запросов, пишем команду для построения SQL запроса: SELECT * FROM users и нажимаем клавишу на клавиатуре F5. После чего ниже в разделе Data Output можно увидеть две строки и скопировать из любой строку в поле Id. Это и будет идентификатор пользователя
    </p>
</span>
