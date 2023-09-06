# eCommerceDAPPER.API
eCommerce: ORM DAPPER + .NET5


--Docker

docker run -d 
--name eCommerceDAPPER
-p 3306:3306 
-v eCommerceDAPPERdb:/var/lib/mysql 
-e MYSQL_ROOT_PASSWORD=1234 
-e MYSQL_PASSWORD=1234 
-e MYSQL_ROOT_HOST=% mysql:5.7

-- Utilizando um gestor de banco conect na base criada e rode os seguintes comandos

CREATE database eCommerceDAPPER
use eCommerceDAPPER

rode os SCRIPTS 
createTable.sql | insertTable.sql | procedure.sql 


Para TESTE COM INSOMNIA
https://github.com/moablive/eCommerceDAPPER.INSOMNIA
