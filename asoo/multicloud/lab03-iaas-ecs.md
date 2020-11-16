# Lab 3 - AWS ECS

## Criando a instancia
Usaremos a imagem `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api) para aprender alguns conceitos importantes do **Elastic Container Service**:
 - Deploy de containers no ECS
 - Mapeamento de portas
 - ***Memory limits***
 
1. Acessar o serviço **ECS**:
   ![](/mob/cloud/img/ecs0.png)

2. Lançar o assistente de criaçao de containers:
   ![](/mob/cloud/img/ecs1.png)

3. Seleccionar o [Fargate](https://aws.amazon.com/pt/fargate/) para hospedar o container:
   ![](/mob/cloud/img/ecs2.png)
   
4. Nomear o cluster:
   ![](/mob/cloud/img/ecs3.png)

5. Criar uma imagem customizada:
   ![](/mob/cloud/img/ecs4.png)

6. Parametrização da imagem:
    * Imagem: `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api)
    * Limite de memória: 512 MB
    * Mapeamento de porta: 5000
   ![](/mob/cloud/img/ecs5.png)
   
7. Revisar as definições da *task*:
   ![](/mob/cloud/img/ecs6.png)

8. Na configuração do serviço, desativar o balanceador de carga:
   ![](/mob/cloud/img/ecs7.png)

9. Manter as configurações padrão de rede:
   ![](/mob/cloud/img/ecs8.png)
   
10. Revisar as configurações:
   ![](/mob/cloud/img/ecs9.png)
   
11. Confirmar a criação do cluster:
   ![](/mob/cloud/img/ecs10.png)
   
12. Mostrar as informações do cluster:
   ![](/mob/cloud/img/ecs11.png)

13. Na aba `Tasks`, mostrar a informação da única *task* (em este exemplo `39bb35ae-615...`:
   ![](/mob/cloud/img/ecs12.png)

14. Conferir o IP público para acessar o container:
   ![](/mob/cloud/img/ecs13.png)

15. Acessar o container pelo IP público:
   ![](/mob/cloud/img/ecs14.png)
