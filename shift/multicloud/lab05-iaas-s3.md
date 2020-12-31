# Lab 5 - AWS S3

Em este lab sobre [**Simple Storage Service (S3)**](https://aws.amazon.com/pt/s3/) aprenderemos alguns conceitos importantes do armazenamento de objetos:
 - Criação de *buckets*
 - Criação de objetos
 - Controle de permissões de acesso
 - Hospedagem de sites estáticos

## Criação do *bucket*
 
1. Accessar o serviço **S3**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-02.png)

2. Criar um novo *bucket*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-03.png)

3. Escolher um nome para o *bucket*. O nome deve ser único globalmente:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-04.png)
   
4. Em esta aba podem ser configuradas algumas opções do *bucket*, como versionamento, *logging*, etc.:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-05.png)

5. Habilitar accesso público no *bucket*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-06.png)

6. Confirmar a criação do *bucket*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-08.png)

7. Conferir que o *bucket* foi criado:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-09.png)

## Criação do objeto

8. Fazer o *upload* de uma imagem qualquer:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-10.png)

9. Habilitar accesso público no objeto:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-11.png)

10. Seleccionar o *tier* `Standard`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-12.png)

11. Confirmar a criação do objeto:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-13.png)

12. Conferir que o objeto foi criado:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-14.png)

13. Na descrição do objeto, podemos obter a url do mesmo:

    |          | bucket-name |                   | objeto        |
    |----------|-------------|-------------------|-------------- |
    | https:// | fiap-mba    | .s3.amazonaws.com | sicmundus.jpg |

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-15.png)    

14. Accessar a imagem pela URL do objeto:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-16.png)    


## Hospedagem de sites estáticos
    
15. Alterar a configuração do bucket para permitir o armazenamento de sites estáticos:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-17.png)    

16. Introducir o nome do arquivo `html` principal:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-18.png)    

17. Fazer *upload* do arquivo [`index.html`](https://github.com/josecastillolema/fiap/blob/master/shift/multicloud/lab05-iaas-s3/index.html) (ou de qualquer outro arquivo `html`) com permissão de accesso público, como descrito na [criação de objeto](#criação-do-objeto)

17. A URL no modo armazenamento de sites estáticos apresenta um novo formato:

    |          | bucket-name |            | region    |               |
    |----------|-------------|------------|---------- | --------------|
    | https:// | fiap-mba    | s3-website | us-east-1 | amazonaws.com |

18. Acessar o site pela nova URL do *bucket*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/s3-19.png)    
