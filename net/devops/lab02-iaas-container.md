# Lab 2 - Containers

## Criando a instância
Usaremos a imagem `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api) para aprender alguns conceitos importantes dos **Azure Containers**:
 - Deploy de containers no Azure
 - Mapeamento de portas
 - ***Memory limits***
 
1. Acessar o serviço **Container Instances**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con01.png)

2. Parametrização da instância:
    * Imagem: `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api)
    * Tipo de imagem: pública
    * Tipo de sistema operacional: Linux
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con02.png)
   
3. Na aba de ***Networking***, liberar a porta 5000/TCP:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con03.png)

4. Revisar e confirmar a criação do container:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con04.png)

5. Aguardar a criacao da instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con05.png)
  
## Acessando a instância

6. Conferir o IP público para acessar o container:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con06.png)

7. Acessar o container pelo IP público **na porta 5000**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/con07.png)
