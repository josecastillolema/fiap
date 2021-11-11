# Lab 13 - Amazon Lambda

Em este lab sobre [**Lambda**](https://aws.amazon.com/pt/lambda/) aprenderemos alguns conceitos do modulo de Function as a Service (FaaS) / *serverless* da plataforma da AWS:
 - Criação de funções Lambda
 - Teste de funções 
 - Criação de triggers (via API Gateway)
 
## Pre-reqs

- A seguinte tabela, com nome `Atmosfera` criada no DynamoDB:
    * sala: *primary key*, string
    * temperatura: number
    * humidade: number
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda0.png)

## Configuração do serviço

1. Acessar o serviço **Lambda**:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda1.png)

2. Criar uma nova função:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda2.png)
   
3. Criar uma primera função `getTemperatura` com Python como *runtime*:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda3.png)

4. Nas contas da **AWS Academy** é necessário mudar o *execution role* por `LabRole`:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda3_2.png)

5. Configurar o seguinte código para a função:
    ```python
    import json
    import boto3
    def lambda_handler(event, context):
       dynamodb = boto3.resource('dynamodb')
       tableTemperatures = dynamodb.Table('Atmosfera')
       response = tableTemperatures.scan()
       return {
          'statusCode': 200,
          'body': response['Items'][0]['temperatura']
       }
    ```
    O código lee o valor `temperatura` da tabela `Atmosfera` do DynamoDB.
    
    
6. Fazer *deploy* do código:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda4.png)

7. Vamos testar o código:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda5.png)

8. Criamos um evento de testes. A entrada do evento (o arquivo `json`) é indeferente em este caso específico, pois a API não está lendo entrada:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda6.png)

9. Executar o evento de testes recém criado `testeGetTemperatura`:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda7.png)
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda25.png)

10. Se tiver usando uma **conta "normal" da AWS** ir para o passo **22**, se tiver usando uma **conta do AWS Academy** continuar normalmente.

11. Agora vamos configurar um *trigger* para a função:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda14.png)

12. O *trigger* será um *endpoint* em uma nova API do API Gateway chamada `api-lambda`:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda15.png)

13. Conferir que o *trigger* foi criado e asociado à função:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda16.png)

14. Nos detalhes do *trigger* podemos ver a URL do *endpoint*:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda17.png)

15. Testamos o *endpoint* (se aparecer a mensagem `{"message":"Missing Authentication Token"}` aguardar um minuto e repetir o teste):
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda18.png)

## Configuração do segundo *endpoint* usando a mesma API

16. Repetir os pasos **2** e **3** para criar uma segunda função `getHumidade` com Python como *runtime*:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda19.png)
 
17. Configurar o seguinte código para a função e fazer *deploy* do mesmo:
    ```python
    import json
    import boto3
    def lambda_handler(event, context):
       dynamodb = boto3.resource('dynamodb')
       tableTemperatures = dynamodb.Table('Atmosfera')
       response = tableTemperatures.scan()
       return {
          'statusCode': 200,
          'body': response['Items'][0]['humidade']
       }
    ```

    O código lee o valor `humidade` da tabela `Atmosfera` do DynamoDB.

18. Testar a nova função como mostrado nos passos **7**, **8** e **9**:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda21.png)

19. Criar um *trigger* para a função usando *a mesma API* que no passo **16**:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda22.png)
    
20. Obter o endpoint nos detalhes do trigger:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda23.png)

21. Conferir o novo endpoint da API (se aparecer a mensagem `{"message":"Missing Authentication Token"}` aguardar um minuto e repetir o teste):
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda24.png)
    
## Criação do IAM *role* em contas "normais" da AWS

22. O teste deve falhar, pois a função não tem permissão para acessar o DynamoDB:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda8.png)

23. No IAM, procurar o *role* da função:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda9.png)

24. Adicionar uma nova *policy*: 
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda10.png)

25. A policy `AmazonDynamoDBReadOnlyAccess` vai dar acesso de leitura ao DynamoDB:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda11.png)

26. Estado final da *role*:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda12.png)

27. Ejecutar de novo o teste, agora deberia funcionar:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/lambda13.png)
