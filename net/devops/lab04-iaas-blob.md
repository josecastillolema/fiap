# Lab 4 - Blob storage

Em este lab sobre **Azure Blob Storage** aprenderemos alguns conceitos importantes do armazenamento de objetos:
 - Criação de *containers*
 - Criação de objetos (*blobs*)
 - Controle de permissões de acesso
 - Hospedagem de sites estáticos

## Criação do *bucket*
 
1. Accessar o serviço **Storage account**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob01.png)

2. Criar um novo *storage account* de tipo *blob storage* e com redundancia local (o nome deve ser único na Azure):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob02.png)

3. Habilitar acesso público:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob03.png)
   
4. Revisar e confirmar a criaçao da conta de armazenamento.

5. Aguardar a criação da conta:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob05.png)

6. Na descriçao da conta, acessar a aba de ***Containers:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob06.png)

6. Criar um novo container, de nome `imgs` e com acesso público:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob07.png)

7. Dentro do container, fazer upload de uma imagem qualquer:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob08.png)

## Acessando do objeto

8. Na descrição do objeto, copiar a URL:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob09.png)

9. Accessar a imagem pela URL do objeto:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/blob10.png)    
