# Lab 3 - AWS ECS

## Criando a instância
Usaremos a imagem `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api) para aprender alguns conceitos importantes do [**Elastic Container Service**](https://aws.amazon.com/pt/ecs/):
 - Deploy de containers no ECS
 - Mapeamento de portas
 - ***Memory limits***
 
1. Acessar o serviço **ECS**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs0.png)

2. Lançar o assistente de criaçao de containers:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs1.png)

3. Seleccionar o [Fargate](https://aws.amazon.com/pt/fargate/) para hospedar o container:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs2.png)
   
4. Nomear o cluster:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs3.png)

5. Criar uma imagem customizada:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs4.png)

6. Parametrização da imagem:
    * Imagem: `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api)
    * Limite de memória: 512 MB
    * Mapeamento de porta: 5000
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs5.png)
   
7. Revisar as definições da *task*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs6.png)

8. Na configuração do serviço, desativar o balanceador de carga:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs7.png)

9. Manter as configurações padrão de rede:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs8.png)
   
10. Revisar as configurações:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs9.png)
   
11. Confirmar a criação do cluster:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs10.png)
  
## Acessando a instância
  
12. Mostrar as informações do cluster:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs11.png)

13. Na aba `Tasks`, mostrar a informação da única *task* (em este exemplo `39bb35ae-615...`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs12.png)

14. Conferir o IP público para acessar o container:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs13.png)

15. Acessar o container pelo IP público:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ecs14.png)
