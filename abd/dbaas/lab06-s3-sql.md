# Lab 6 - Consultas SQL no S3

Em este lab sobre [**Simple Storage Service (S3)**](https://aws.amazon.com/pt/rds/) aprenderemos como fazer consultas SQL contra objetos.

## Criação do *bucket*/objeto
 
1. Criar um *bucket* chamado `fiapdb` e fazer upload do [seguinte database](https://aws-tc-largeobjects.s3-us-west-2.amazonaws.com/CUR-TF-200-ACBDFO-1/Lab1/lab1.csv) em formato `.csv`.

## Consulta SQL

3. Na descrição do objeto, selecionar a opção de `Query with S3 Select`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/s3-sql00.png)

3. Ejecutar a consulta padrão, que retorna as 5 primeiras filas:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/s3-sql01.png)
   
4. Da lista de templates, escolher a consulta `SELECT count(*) FROM s3object s`:

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/s3-sql02.png)

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/s3-sql03.png)

5. Ejecutar a nova consulta, que retorna o número de elementos da tabela:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/abd/dbaas/img/s3-sql04.png)
