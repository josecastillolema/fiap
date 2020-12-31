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
    ![](img/lambda0.png)

 ## Configuração do serviço

1. Acessar o serviço **Lambda**:
    ![](img/lambda1.png)

2. Criar uma nova função:
    ![](img/lambda2.png)
   
3. Criar uma primera função `getTemperatura` com Python como *runtime*:
    ![](img/lambda3.png)

4. Configurar o seguinte código para a função:
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
    
    
5. Fazer *deploy* do código:
    ![](img/lambda4.png)

6. Vamos testar o código:
    ![](img/lambda5.png)

7. Criamos um evento de testes. A entrada do evento (o arquivo `json`) é indeferente em este caso específico, pois a API não está lendo entrada:
    ![](img/lambda6.png)

 8. Executar o evento de testes recém criado `testeGetTemperatura`:
    ![](img/lambda7.png)

 9. O teste deve falhar, pois a função não tem permissão para acessar o DynamoDB:
    ![](img/lambda8.png)

 10. No IAM, procurar o *role* da função:
    ![](img/lambda9.png)

 11. Adicionar uma nova *policy*: 
    ![](img/lambda10.png)

 12. A policy `AmazonDynamoDBReadOnlyAccess` vai dar acesso de leitura ao DynamoDB:
    ![](img/lambda11.png)

 13. Estado final da *role*:
    ![](img/lambda12.png)

 14. Ejecutar de novo o teste, agora deberia funcionar:
    ![](img/lambda13.png)

 15. Agora vamos configurar um *trigger* para a função:
    ![](img/lambda14.png)

 16. O *trigger* será um *endpoint* em uma nova API do API Gateway chamada `api-lambda`:
    ![](img/lambda15.png)

 17. Conferir que o *trigger* foi criado e asociado à função:
    ![](img/lambda16.png)

 18. Nos detalhes do *trigger* podemos ver a URL do *endpoint*:
    ![](img/lambda17.png)

 19. Testamos o *endpoint* (se aparecer a mensagem `{"message":"Missing Authentication Token"}` aguardar um minuto e repetir o teste):
    ![](img/lambda18.png)

 ## Configuração do segundo *endpoint* usando a mesma API

20. Repetir os pasos **2** e **3** para criar uma segunda função `getHumidade` com Python como *runtime*:
    ![](img/lambda19.png)
 
21. Configurar o seguinte código para a função e fazer *deploy* do mesmo:
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

22. Configurar as devidas permissões para a role da nova função no IAM, como mostrado nos passos **10**, **11**, **12** e **13**:
    ![](img/lambda20.png)

23. Testar a nova função como mostrado nos passos **7** e **8**:
    ![](img/lambda21.png)

23. Criar um *trigger* para a função usando *a mesma API* que no passo **16**:
    ![](img/lambda22.png)
    
24. Obter o endpoint nos detalhes do trigger:
    ![](img/lambda23.png)

25. Conferir o novo endpoint da API (se aparecer a mensagem `{"message":"Missing Authentication Token"}` aguardar um minuto e repetir o teste):
    ![](img/lambda24.png)
