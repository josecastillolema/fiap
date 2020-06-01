# Lab 1 - AWS EC2

## Criando a instancia
Usaremos a imagem oficial `mysql` para aprender alguns conceitos importantes do Docker:
 - **variáveis de entorno**: `docker run -e`
 - **mapeamento de portas**: `docker run -p`
 - **persistência de dados**: `docker run -v`
 
1. Acessar o serviço EC2:
   ![Screenshot of the VNF xterm](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-0.png)

2. **[T1]** Primeira tentativa executando o servidor MySQL
   ```
   $ docker run mysql
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
   2020-04-05 12:45:11+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
   2020-04-05 12:45:11+00:00 [ERROR] [Entrypoint]: Database is uninitialized and password option is not specified
	      You need to specify one of MYSQL_ROOT_PASSWORD, MYSQL_ALLOW_EMPTY_PASSWORD and MYSQL_RANDOM_ROOT_PASSWORD
   ```

3. **[T1]** Como mostra a mensagem de erro anterior, não e possível executar o container `mysql` sem configurar alguma das seguintes opções em relação à autenticação:
    - Uma senha para o usuario *root* do banco via `MYSQL_ROOT_PASSWORD`
    - Permitir uma senha de *root* vazia via `MYSQL_ALLOW_EMPTY_PASSWORD`
    - Criar uma senha de *root* aleatória via `MYSQL_RANDOM_ROOT_PASSWORD`

    Vamos optar pela primeira opção. Para passar variáveis de entorno ao container usaremos a opção `-e`. É importante colocar todos os parâmetros opcionais do comando `docker run` **antes** do nome da imagem:
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
    
## Accessando à instancia

4. **[T2]** Conseguimos executar o container, vamos tentar acessar o banco desde o outro terminal. Para isso, precisamos instalar o cliente do MySQL:
    ```
    $ sudo apt install mysql-client -y
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

    Alguns parametros do cliente MySQL:
    - *Hostname* do banco via `-h`
    - Usuario via `-u`
    - Senha via `-p`. A senha tem que ser digitada **sem espacos** depois do parametro.
    ```
    $ mysql -h 127.0.0.1 -u root -pfiap
    mysql: [Warning] Using a password on the command line interface can be insecure.
    ERROR 2003 (HY000): Can't connect to MySQL server on '127.0.0.1' (111)
    ```

6. **[T2]** Listar o container em execução para entender a falta de conectividade. A coluna `PORTS` mostra que a porta 3306 do container (a padrão do MySQL) não está mapeada a nenhuma porta do *host*.
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                 NAMES
    66696bdd281f        mysql               "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes        3306/tcp, 33060/tcp   funny_yonath
    ```

7. **[T2]** Parar o container:

    ```
    $ docker stop 66696bdd281f
    66696bdd281f
    ```
