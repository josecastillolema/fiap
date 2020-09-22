# Lab 10 - Amazon API Gateway

Em este lab sobre **API Gateway** aprenderemos alguns conceitos do API gateway da plataforma da AWS:
 - Configuração de rotas
 - Throttling (limitação de requisições) 
 - Monitoramento
 
## Pre-reqs

- Dois URLs accessíveis. Por exemplo, dois apps no Beanstalk:
    * http://springboot-env.eba-7znjbf9p.us-eats-1.elasticbeanstalk.com
        ![](img/api0.png)
    * http://springboot-env-1.eba-7zbhbf9p.us-east-1.elasticbeanstalk.com
        ![](img/api1.png)


 ## Configuração do serviço
 
1. Acessar o serviço **API Gateway**:
   ![](img/api3.png)

2. Criar uma nova API HTTP:
   ![](img/api4.png)

3. Configurar o nome da API e as integrações, apontando para as duas URLs dos prereqs usando o método `GET`:
   ![](img/api5.png)
   
4. Configurar as rotas, `/v1` apontando para uma URL e `/v2` apontando para a outra:
   ![](img/api6.png)

5. Sem modificações na configuração padrão de *stages*:
   ![](img/api7.png)
   
6. Revisar as configurações e confirmar a criação:
   ![](img/api8.png)
   
7. Aguardar a criação da API:
   ![](img/api9.png)

8. 
