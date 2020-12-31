# Lab 11 - Amazon API Gateway

Em este lab sobre [**API Gateway**](https://aws.amazon.com/pt/api-gateway/) aprenderemos alguns conceitos do API gateway da plataforma da AWS:
 - Configuração de rotas
 - *Throttling* (limitação do número de requisições por segundo) 
 - Monitoramento
 
## Pre-reqs

- Dois URLs accessíveis. Por exemplo, dois apps no Beanstalk:
    * http://springboot-env.eba-7znjbf9p.us-eats-1.elasticbeanstalk.com
        ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api1.png)
    * http://springboot-env-1.eba-7zbhbf9p.us-east-1.elasticbeanstalk.com
        ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api2.png)


 ## Configuração do serviço
 
1. Acessar o serviço **API Gateway**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api3.png)

2. Criar uma nova API HTTP:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api4.png)

3. Configurar o nome da API e as integrações, apontando para as duas URLs dos prereqs usando o método `GET`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api5.png)
   
4. Configurar as rotas, `/v1` apontando para uma URL e `/v2` apontando para a outra:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api6.png)

5. Sem modificações na configuração padrão de *stages*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api7.png)
   
6. Revisar as configurações e confirmar a criação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api8.png)
   
7. Aguardar a criação da API:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api9.png)

8. Testar a URL da API, a seguinte mensagem é normal pois não foi configurada a rota `/`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api10.png)

9. Testar as rotas `v1` e `v2` da API:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api11.png)
   
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api12.png)

10. Existe a possibilidade de limitar o número de requisições por segundo da API (*throttling*):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/api13.png)

