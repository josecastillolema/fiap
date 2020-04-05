# Lab 2 - Docker (continuação)

Rodando mysql server
--------------
Usaremos o container `mysql` para aprender alguns conceitos de Docker:
 - variables de entorno
 - mapeamento de portas
 - persistencia de dados). 
 
Para isso, vamos precisar de trabalhar com dois terminais abertos (T1 e T2).

1. **[T1]** Obtencão da imagem
    ```
    $ docker pull mysql
    Using default tag: latest
    latest: Pulling from library/mysql
    c499e6d256d6: Pull complete 
    22c4cdf4ea75: Pull complete 
    6ff5091a5a30: Pull complete 
    2fd3d1af9403: Pull complete 
    0d9d26127d1d: Pull complete 
    54a67d4e7579: Pull complete 
    fe989230d866: Pull complete 
    3a808704d40c: Pull complete 
    826517d07519: Pull complete 
    69cd125db928: Pull complete 
    b5c43b8c2879: Pull complete 
    1811572b5ea5: Pull complete 
    Digest: sha256:b69d0b62d02ee1eba8c7aeb32eba1bb678b6cfa4ccfb211a5d7931c7755dc4a8
    Status: Downloaded newer image for mysql:latest
    docker.io/library/mysql:latest
    ```

2. **[T1]** Primeira tentativa rodando o mysql server
   ```
   $ docker run mysql
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 12:45:11+00:00 [ERROR] [Entrypoint]: Database is uninitialized and password option is not specified
	      You need to specify one of MYSQL_ROOT_PASSWORD, MYSQL_ALLOW_EMPTY_PASSWORD and MYSQL_RANDOM_ROOT_PASSWORD
   ```

3. **[T1]** Como mostra a mensagem de erro anterior, não e possível rodar o container mysql sem configurar alguma das seguintes opções em relação à autenticacao:
    - Uma senha para o usuario root do banco via `MYSQL_ROOT_PASSWORD`
    - Permitir uma senha de root vazia via `MYSQL_ALLOW_EMPTY_PASSWORD`
    - Criar uma senha de root aleatoria via `MYSQL_RANDOM_ROOT_PASSWORD`
    Vamos optar pela primeira opcao. Para passar variabeis de entorno ao container usaremos a opcao `-e`. E importante colocar todos os parametros opcionais do comando `docker run` **antes** do nome da imagem:
    ```
    $ docker run -e MYSQL_ROOT_PASSWORD=fiap mysql
    2020-04-05 12:50:57+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05 12:50:57+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
    2020-04-05 12:50:57+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05 12:50:57+00:00 [Note] [Entrypoint]: Initializing database files
    2020-04-05T12:50:57.314246Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T12:50:57.314331Z 0 [System] [MY-013169] [Server] /usr/sbin/mysqld (mysqld 8.0.19) initializing of server in progress as process 41
    2020-04-05T12:51:02.282097Z 5 [Warning] [MY-010453] [Server] root@localhost is created with an empty password ! Please consider switching off the --initialize-insecure option.
    2020-04-05 12:51:06+00:00 [Note] [Entrypoint]: Database files initialized
    2020-04-05 12:51:06+00:00 [Note] [Entrypoint]: Starting temporary server
    2020-04-05T12:51:07.201379Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T12:51:07.201523Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 91
    2020-04-05T12:51:07.896487Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    2020-04-05T12:51:07.905784Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    2020-04-05T12:51:07.927215Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 0  MySQL Community Server - GPL.
    2020-04-05 12:51:07+00:00 [Note] [Entrypoint]: Temporary server started.
    2020-04-05T12:51:07.991679Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock'
    Warning: Unable to load '/usr/share/zoneinfo/iso3166.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/leap-seconds.list' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone1970.tab' as time zone. Skipping it.

    2020-04-05 12:51:11+00:00 [Note] [Entrypoint]: Stopping temporary server
    2020-04-05T12:51:11.658536Z 10 [System] [MY-013172] [Server] Received SHUTDOWN from user root. Shutting down mysqld (Version: 8.0.19).
    2020-04-05T12:51:13.260962Z 0 [System] [MY-010910] [Server] /usr/sbin/mysqld: Shutdown complete (mysqld 8.0.19)  MySQL Community Server - GPL.
    2020-04-05 12:51:13+00:00 [Note] [Entrypoint]: Temporary server stopped

    2020-04-05 12:51:13+00:00 [Note] [Entrypoint]: MySQL init process done. Ready for start up.

    2020-04-05T12:51:14.024204Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T12:51:14.024356Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 1
    2020-04-05T12:51:14.639276Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    2020-04-05T12:51:14.648234Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    2020-04-05T12:51:14.668536Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 3306  MySQL Community Server - GPL.
    2020-04-05T12:51:14.783305Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock' bind-address: '::' port: 33060
    ```

4. **[T2]** Conseguimos rodar o container, vamos tentar acessar o banco desde o outro terminal. Para isso, precisamos instalar o mysql client:
    ```
    ubuntu@ip-172-31-47-198:~$ sudo apt install mysql-client -y
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    The following additional packages will be installed:
      libaio1 mysql-client-5.7 mysql-client-core-5.7 mysql-common
    The following NEW packages will be installed:
      libaio1 mysql-client mysql-client-5.7 mysql-client-core-5.7 mysql-common
    0 upgraded, 5 newly installed, 0 to remove and 34 not upgraded.
    Need to get 8607 kB of archives.
    After this operation, 61.8 MB of additional disk space will be used.
    Get:1 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 libaio1 amd64 0.3.110-5ubuntu0.1 [6476 B]
    Get:2 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client-core-5.7 amd64 5.7.29-0ubuntu0.18.04.1 [6642 kB]
    Get:3 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 mysql-common all 5.8+1.0.4 [7308 B]
    Get:4 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client-5.7 amd64 5.7.29-0ubuntu0.18.04.1 [1942 kB]
    Get:5 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client all 5.7.29-0ubuntu0.18.04.1 [9828 B]
    Fetched 8607 kB in 0s (37.9 MB/s)      
    Selecting previously unselected package libaio1:amd64.
    (Reading database ... 84272 files and directories currently installed.)
    Preparing to unpack .../libaio1_0.3.110-5ubuntu0.1_amd64.deb ...
    Unpacking libaio1:amd64 (0.3.110-5ubuntu0.1) ...
    Selecting previously unselected package mysql-client-core-5.7.
    Preparing to unpack .../mysql-client-core-5.7_5.7.29-0ubuntu0.18.04.1_amd64.deb ...
    Unpacking mysql-client-core-5.7 (5.7.29-0ubuntu0.18.04.1) ...
    Selecting previously unselected package mysql-common.
    Preparing to unpack .../mysql-common_5.8+1.0.4_all.deb ...
    Unpacking mysql-common (5.8+1.0.4) ...
    Selecting previously unselected package mysql-client-5.7.
    Preparing to unpack .../mysql-client-5.7_5.7.29-0ubuntu0.18.04.1_amd64.deb ...
    Unpacking mysql-client-5.7 (5.7.29-0ubuntu0.18.04.1) ...
    Selecting previously unselected package mysql-client.
    Preparing to unpack .../mysql-client_5.7.29-0ubuntu0.18.04.1_all.deb ...
    Unpacking mysql-client (5.7.29-0ubuntu0.18.04.1) ...
    Setting up mysql-common (5.8+1.0.4) ...
    update-alternatives: using /etc/mysql/my.cnf.fallback to provide /etc/mysql/my.cnf (my.cnf) in auto mode
    Setting up libaio1:amd64 (0.3.110-5ubuntu0.1) ...
    Setting up mysql-client-core-5.7 (5.7.29-0ubuntu0.18.04.1) ...
    Setting up mysql-client-5.7 (5.7.29-0ubuntu0.18.04.1) ...
    Setting up mysql-client (5.7.29-0ubuntu0.18.04.1) ...
    Processing triggers for man-db (2.8.3-2ubuntu0.1) ...
    Processing triggers for libc-bin (2.27-3ubuntu1) ...
    ```

5. **[T2]** Tentemos acessar ao banco.
Alguns parametros do cliente mysql:
    - Hostname do banco via `-h`
    - Usuario via `-u`
    - Senha via `-p`. A senha tem que ser digitadas **sem espacos** depois do parametro.
    ```
    ubuntu@ip-172-31-47-198:~$ mysql -h 127.0.0.1 -u root -pfiap
    mysql: [Warning] Using a password on the command line interface can be insecure.
    ERROR 2003 (HY000): Can't connect to MySQL server on '127.0.0.1' (111)
    ```

6. **[T2]** Listemos o container em execucao para entender a falta de conectividade. A porta 3306 do container (a padrao do mysql) nao esta mapeada a nenhuma porta do host.
    ```
    ubuntu@ip-172-31-47-198:~$ docker ps
    CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                 NAMES
    66696bdd281f        mysql               "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes        3306/tcp, 33060/tcp   funny_yonath
    ```

7. **[T2]** Parar o container:

    ```
    ubuntu@ip-172-31-47-198:~$ docker stop 66696bdd281f
    66696bdd281f
    ```

8. **[T1]** Vamos adicionar ao nosso comando `docker run` o parametro `-p` responsavel pelo mapeamento de portas. Recebe um argumento do tipo x:y, a onde x é a porta do lado do host e y a porta do lado do container.
   ```
   ubuntu@ip-172-31-47-198:~$ docker run -e MYSQL_ROOT_PASSWORD=fiap -p 3306:3306 mysql
   2020-04-05 13:04:50+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 13:04:50+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
   2020-04-05 13:04:50+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 13:04:50+00:00 [Note] [Entrypoint]: Initializing database files
   2020-04-05T13:04:50.654920Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
   2020-04-05T13:04:50.655015Z 0 [System] [MY-013169] [Server] /usr/sbin/mysqld (mysqld 8.0.19) initializing of server in progress as process 44
   2020-04-05T13:04:55.455802Z 5 [Warning] [MY-010453] [Server] root@localhost is created with an empty password ! Please consider switching off the --initialize-insecure option.
   2020-04-05 13:05:00+00:00 [Note] [Entrypoint]: Database files initialized
   2020-04-05 13:05:00+00:00 [Note] [Entrypoint]: Starting temporary server
   2020-04-05T13:05:00.502158Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
   2020-04-05T13:05:00.502316Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 94
   2020-04-05T13:05:01.215942Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
   2020-04-05T13:05:01.225111Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
   2020-04-05T13:05:01.246461Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 0  MySQL Community Server - GPL.
   2020-04-05 13:05:01+00:00 [Note] [Entrypoint]: Temporary server started.
   2020-04-05T13:05:01.290695Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock'
   Warning: Unable to load '/usr/share/zoneinfo/iso3166.tab' as time zone. Skipping it.
   Warning: Unable to load '/usr/share/zoneinfo/leap-seconds.list' as time zone. Skipping it.
   Warning: Unable to load '/usr/share/zoneinfo/zone.tab' as time zone. Skipping it.
   Warning: Unable to load '/usr/share/zoneinfo/zone1970.tab' as time zone. Skipping it.

   2020-04-05 13:05:03+00:00 [Note] [Entrypoint]: Stopping temporary server
   2020-04-05T13:05:03.846009Z 10 [System] [MY-013172] [Server] Received SHUTDOWN from user root. Shutting down mysqld (Version: 8.0.19).
   2020-04-05T13:05:05.562972Z 0 [System] [MY-010910] [Server] /usr/sbin/mysqld: Shutdown complete (mysqld 8.0.19)  MySQL Community Server - GPL.
   2020-04-05 13:05:05+00:00 [Note] [Entrypoint]: Temporary server stopped

   2020-04-05 13:05:05+00:00 [Note] [Entrypoint]: MySQL init process done. Ready for start up.

   2020-04-05T13:05:06.222224Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
   2020-04-05T13:05:06.222368Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 1
   2020-04-05T13:05:06.859803Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
   2020-04-05T13:05:06.868634Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
   2020-04-05T13:05:06.890778Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 3306  MySQL Community Server - GPL.
   2020-04-05T13:05:07.005006Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock' bind-address: '::' port: 33060
   ```

10. **[T2]** Confirmar o mapeamento de portas:
   ```
   ubuntu@ip-172-31-47-198:~$ docker ps
   CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                               NAMES
   38e202140601        mysql               "docker-entrypoint.s…"   11 seconds ago      Up 9 seconds        0.0.0.0:3306->3306/tcp, 33060/tcp   awesome_greider
   ```

10. **[T2]** Tentar novamente o acesso o banco de dados:
    ```
    ubuntu@ip-172-31-47-198:~$ mysql -h 127.0.0.1 -u root -pfiap 
    Welcome to the MySQL monitor.  Commands end with ; or \g.
    Your MySQL connection id is 8
    Server version: 8.0.19 MySQL Community Server - GPL

    Copyright (c) 2000, 2020, Oracle and/or its affiliates. All rights reserved.

    Oracle is a registered trademark of Oracle Corporation and/or its
    affiliates. Other names may be trademarks of their respective
    owners.

    Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

    mysql> exit
    Bye
    ```

11. **[T2]** Parar o container:
    ```
    ubuntu@ip-172-31-47-198:~$ docker stop 38e202140601
    38e202140601
    ```

12. **[T1]** Conseguimos acessar o banco. Porem, containers sao efemeros. Qualquer dado criado no banco sera perdido apos a finalizacao do container. Para conseguir persistencia de dados, vamos adicionar ao nosso comando `docker run` o parametro `-v`. Recebe um argumento do tipo x:y, a onde x e o nome do volume e y a pasta a onde esse volume vai ser mapeado dentro do container.
    ```
    $ docker run -e MYSQL_ROOT_PASSWORD=fiap -p 3306:3306 -v voldb:/var/lib/mysql mysql
    2020-04-05 13:19:02+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05 13:19:02+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
    2020-04-05 13:19:02+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05 13:19:02+00:00 [Note] [Entrypoint]: Initializing database files
    2020-04-05T13:19:02.164095Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T13:19:02.164181Z 0 [System] [MY-013169] [Server] /usr/sbin/mysqld (mysqld 8.0.19) initializing of server in progress as process 41
    2020-04-05T13:19:06.841206Z 5 [Warning] [MY-010453] [Server] root@localhost is created with an empty password ! Please consider switching off the --initialize-insecure option.
    2020-04-05 13:19:11+00:00 [Note] [Entrypoint]: Database files initialized
    2020-04-05 13:19:11+00:00 [Note] [Entrypoint]: Starting temporary server
    2020-04-05T13:19:12.009941Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T13:19:12.010078Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 91
    2020-04-05T13:19:12.728905Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    2020-04-05T13:19:12.737985Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    2020-04-05T13:19:12.759072Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 0  MySQL Community Server - GPL.
    2020-04-05 13:19:12+00:00 [Note] [Entrypoint]: Temporary server started.
    2020-04-05T13:19:12.813271Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock'
    Warning: Unable to load '/usr/share/zoneinfo/iso3166.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/leap-seconds.list' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone1970.tab' as time zone. Skipping it.

    2020-04-05 13:19:15+00:00 [Note] [Entrypoint]: Stopping temporary server
    2020-04-05T13:19:15.329084Z 10 [System] [MY-013172] [Server] Received SHUTDOWN from user root. Shutting down mysqld (Version: 8.0.19).
    2020-04-05T13:19:17.069433Z 0 [System] [MY-010910] [Server] /usr/sbin/mysqld: Shutdown complete (mysqld 8.0.19)  MySQL Community Server - GPL.
    2020-04-05 13:19:17+00:00 [Note] [Entrypoint]: Temporary server stopped

    2020-04-05 13:19:17+00:00 [Note] [Entrypoint]: MySQL init process done. Ready for start up.

    2020-04-05T13:19:17.701649Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T13:19:17.701788Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 1
    2020-04-05T13:19:18.349463Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    2020-04-05T13:19:18.357986Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    2020-04-05T13:19:18.378623Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 3306  MySQL Community Server - GPL.
    2020-04-05T13:19:18.485139Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock' bind-address: '::' port: 33060
    ```
    
13. **[T2]** Accesar o banco e criar um database:
    ```
    $ mysql -h 127.0.0.1 -u root -pfiap
    mysql: [Warning] Using a password on the command line interface can be insecure.
    Welcome to the MySQL monitor.  Commands end with ; or \g.
    Your MySQL connection id is 8
    Server version: 8.0.19 MySQL Community Server - GPL

    Copyright (c) 2000, 2020, Oracle and/or its affiliates. All rights reserved.

    Oracle is a registered trademark of Oracle Corporation and/or its
    affiliates. Other names may be trademarks of their respective
    owners.

    Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

    mysql> create database fiap;
    Query OK, 1 row affected (0.04 sec)

    mysql> exit
    Bye
    ```

14. **[T2]** Parar o container:
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                               NAMES
    27a9ff19cbd0        mysql               "docker-entrypoint.s…"   5 minutes ago       Up 5 minutes        0.0.0.0:3306->3306/tcp, 33060/tcp   laughing_lovelace
    $ docker stop 27a9ff19cbd0
    27a9ff19cbd0
    ```

15. **[T2]** Confirmar que o volume persiste mesmo depois da finalizacao do container:
   ```
   $ docker volume ls
   DRIVER              VOLUME NAME
   local               voldb
   ```

16. **[T1]** Rodar novamente o container, para conferir que os dados foram persistidos:
    ```
    $ docker run -e MYSQL_ROOT_PASSWORD=fiap -p 3306:3306 -v voldb:/var/lib/mysql mysql
    2020-04-05 13:28:24+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05 13:28:24+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
    2020-04-05 13:28:24+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    2020-04-05T13:28:24.792093Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    2020-04-05T13:28:24.792236Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 1
    2020-04-05T13:28:25.434639Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    2020-04-05T13:28:25.443450Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    2020-04-05T13:28:25.465214Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 3306  MySQL Community Server - GPL.
    2020-04-05T13:28:25.590278Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock' bind-address: '::' port: 33060
    ```

17. **[T1]** Acessar o banco e confirmar que o database criado foi persistido:
    ```
    ubuntu@ip-172-31-47-198:~$ mysql -h 127.0.0.1 -u root -p
    Enter password: 
    Welcome to the MySQL monitor.  Commands end with ; or \g.
    Your MySQL connection id is 8
    Server version: 8.0.19 MySQL Community Server - GPL

    Copyright (c) 2000, 2020, Oracle and/or its affiliates. All rights reserved.

    Oracle is a registered trademark of Oracle Corporation and/or its
    affiliates. Other names may be trademarks of their respective
    owners.

    Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

    mysql> show databases;
    +--------------------+
    | Database           |
    +--------------------+
    | information_schema |
    | fiap               |
    | mysql              |
    | performance_schema |
    | sys                |
    +--------------------+
    5 rows in set (0.00 sec)
    
    mysql> exit
    Bye
    ```
