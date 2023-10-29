# Lab 5 - AWS RDS

Em este lab sobre [**Relational Database Service (RDS)**](https://aws.amazon.com/pt/rds/) aprenderemos alguns conceitos importantes na criação de serviços de DBaaS NoSQL:
 - Criação de instâncias RDS
 - Plataformas/entornos disponíveis
 - *Logging*
 - Monitoramento

## Criação da instância RDS
 
1. Acessar o serviço **RDS**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds01.png)

2. Criar um novo *database*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds02.png)

3. Escolher `MySQL` como *backend*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds03.png)
   
4. Selecionar a camada gratuita, e configurar nome, usuário e senha para o banco:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds04.png)

5. Parametrizaçao de *storage*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds05.png)
   
6. Se formos acessar desde a nossa máquina local, habilitar o acesso público ao banco:
 
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds06.png)

7. Revisar as configurações e confirmar a criação da instância RDS:
 
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds07.png)

8. Aguardar a correta criação da instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds08.png)
   
9. Na descrição da instância, note-se o *endpoint* e a porta da mesma:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds09.png)

10. Testemos a conectividade com a instância:
    ```
    $ telnet fiapdb.cpuzlc9blsa2.us-east-1.rds.amazonaws.com 3306         
    Trying 18.210.97.78...
    telnet: connect to address 18.210.97.78: Operation timed out
    telnet: Unable to connect to remote host
    ```
    
11. Como vimos no passo anterior, a instância não está acessível na porta 3306. Precisamos criar um novo *security group* e liberar a porta. Para isso, acessar o serviço Virtual Private Cloud (VPC):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds10.png)

12. Criar um novo *security group*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds11.png)

13. Liberar a porta 3306 desde *anywhere*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds12.png)

14. De volta no serviço RDS, vamos atualizar a instância com o novo *security group*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds13.png)

15. Nos ajustes de conetividade, incluir o novo *security group*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds14.png)

16. Aguardar a configuraçao ser aplicada na instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/rds15.png)

17. Testar novamente a conetividade:
    ```
    $ telnet fiapdb.cpuzlc9blsa2.us-east-1.rds.amazonaws.com 3306
    Trying 18.210.97.78...
    Connected to ec2-18-210-97-78.compute-1.amazonaws.com.
    Escape character is '^]'.
    8.0.20a#Xm7w(?M:w{%Z-
                         /mysql_native_password
    ```


## Testando o acesso ao banco

### Pre-reqs

 - `mysql` (client)

Se não tiver os pre-reqs na máquina local pular para o [teste em uma VM no EC2](#deploy-em-uma-vm-no-ec2).

## Teste local

 1. Usar o cliente `mysql` para acessar ao banco:
    ```
    $ mysql -h fiapdb.cpuzlc9blsa2.us-east-1.rds.amazonaws.com -u admin -p
    Enter password: 
    Welcome to the MySQL monitor.  Commands end with ; or \g.
    Your MySQL connection id is 26
    Server version: 8.0.20 Source distribution

    Copyright (c) 2000, 2021, Oracle and/or its affiliates.

    Oracle is a registered trademark of Oracle Corporation and/or its
    affiliates. Other names may be trademarks of their respective
    owners.

    Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

    mysql> show databases;
    +--------------------+
    | Database           |
    +--------------------+
    | information_schema |
    | mysql              |
    | performance_schema |
    +--------------------+
    3 rows in set (0.15 sec)
    ```
   
## Teste em uma VM no EC2

Se tiver feito o deploy local pode pular esta seção.

1. Criar uma VM com a imagem `Amazon Linux`

2. Logar na VM

3. Instalar o cliente `mysql`:
    ```
    $ sudo yum install mariadb
    ```

4. Usar o cliente `mysql` para acessar ao banco:
    ```
    $ mysql -h fiapdb.cpuzlc9blsa2.us-east-1.rds.amazonaws.com -u admin -p
    Enter password: 
    Welcome to the MySQL monitor.  Commands end with ; or \g.
    Your MySQL connection id is 26
    Server version: 8.0.20 Source distribution

    Copyright (c) 2000, 2021, Oracle and/or its affiliates.

    Oracle is a registered trademark of Oracle Corporation and/or its
    affiliates. Other names may be trademarks of their respective
    owners.

    Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

    mysql> show databases;
    +--------------------+
    | Database           |
    +--------------------+
    | information_schema |
    | mysql              |
    | performance_schema |
    +--------------------+
    3 rows in set (0.15 sec)
    ```
