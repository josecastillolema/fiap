# Lab 8 - AWS DynamoDB

Em este lab sobre **DynamoDB** aprenderemos alguns conceitos importantes na criação de DBaaS NoSQL:
 - Criação de tabelas
 - Inserção/consulta de dados via console
 - Inserção/consulta via código `python`

## Pre-reqs

- Uma VM com a imagem `Ubuntu Linux 18.04`

- No painel da Vocareum, accessar as credenciais da conta para acesso programático:
   ![](/mob/cloud/img/d0.png)
   ![](/mob/cloud/img/d1.png)
   
- Copiar as credencias no arquivo `~/.aws/credentials` dentro da VM:
    ```
    $ cat ~/.aws/credentials 
    [default]
    aws_access_key_id=ASIAY7VWXB2OOVWYLAGN
    aws_secret_access_key=KBIiKTVfM4weELy/fnyo2whIX1CU1rIzRGAuKrIY
    aws_session_token=FwoGZXIvYXdzEHsaDHJaxudSvX0PU3dK+iLBAaNXq+ioeMWV0o10aUbmolUTn/Qipy4YuXeGE4iQPYpLdtLd+djB78dl1PdjD50Hzbr9kr3T7YN2Y9YSG949dIThcvLBgTgJCB008YXTaUSClqKtppKGdhTymdhfUuiYin9m5DDgYDvnQhmt/9ukDWe8lzpFVz6NvjPnfgQrRCfViCs4KPCWz3WqPM6Q7opJM+FPFySWWY57TlzJ4919JpDLsLaE0CBSJQqgj0CWT/rX6zhh1rAQ3gGD8MRGipe6Chwol4qM+QUyLfX8HXTVHQTnTdspoG0ARfrtJglg9imONXKaHIFopyaajJZ12OgQjUKhl3u+WA==
    ```
   
## Criando a tabela
 
1. Acessar o serviço **DynamoDB**:
   
2. Criar uma nova tabela:
   ![](/mob/cloud/img/d2.png)
   
3. Configurar o nome da tabela e a chave primaria da mesma:
   ![](/mob/cloud/img/d3.png)

4. Aguardar a criação da tabela:
   ![](/mob/cloud/img/d4.png)

5. Na aba `Items`, adicionar um novo item:
   ![](/mob/cloud/img/d5.png)

6. Confirmar a criação do item:
   ![](/mob/cloud/img/d6.png)


## Accessando via código `python`

7. Na VM, clonar o repostiorio das aulas:
    ```
    $ git clone https://github.com/josecastillolema/fiap.git
    ```

8. Navegar ate a pasta dos códigos de este lab:
    ```
    $ cd fiap/mob/cloud/lab08-paas-dynamo/
    ```
    
9. Conferir o código:
    * Carrega a tabela `Alunos` da *region* `us-east-1` 
    * Faz um *scan* de todos os dados da tabela
    * Insere um novo aluno
    ```python
    from pprint import pprint
    import boto3

    dynamodb = boto3.resource('dynamodb', region_name='us-east-1')
    table = dynamodb.Table('Alunos')

    def scan(dynamodb, table):
        response = table.scan()
        data = response['Items']
        while 'LastEvaluatedKey' in response:
            response = table.scan(ExclusiveStartKey=response['LastEvaluatedKey'])
            data.extend(response['Items'])
        print (data)

     def put_aluno(dynamodb, table, rm, mail, nome, tlfne):
        response = table.put_item(
           Item={
                'RM': rm,
                'mail': mail,
                'nome': nome,
                'tfne': tlfne
            }
        )
        return response

     if __name__ == '__main__':
        print("\nTestando scan:")
        scan(dynamodb, table)

        resp = put_aluno(dynamodb, table, "RM234472", 'rm234472@fiap.com.br', "Jonas Kahnwald", 11636229987)
        print("\nIserindo aluno:")
        pprint(resp)
     ```
 10. Instalar as dependências:
     ```
     $ sudo apt install -y python3-boto3
     ```
 
 11. Rodar o código:
     ```
     $ python3 dynamodb.py 

      Testando scan:
      [{'mail': 'rm234472@fiap.com.br', 'nome': 'Jonas Kahnwald', 'RM': 'RM234472', 'tlfne': Decimal('11636229987')}, {'mail': 'rm338132@fiap.com.br', 'nome': 'Joao Lopez', 'RM': 'RM338132', 'tfne': Decimal('11981041293')}]

      Iserindo aluno:
      {'ResponseMetadata': {'HTTPHeaders': {'connection': 'keep-alive',
                                            'content-length': '2',
                                            'content-type': 'application/x-amz-json-1.0',
                                            'date': 'Thu, 30 Jul 2020 17:41:36 GMT',
                                            'server': 'Server',
                                            'x-amz-crc32': '2745614147',
                                            'x-amzn-requestid': 'KDPGDR3S23O19GMPMPOPHCNEO3VV4KQNSO5AEMVJF66Q9ASUAAJG'},
                            'HTTPStatusCode': 200,
                            'RequestId': 'KDPGDR3S23O19GMPMPOPHCNEO3VV4KQNSO5AEMVJF66Q9ASUAAJG',
                            'RetryAttempts': 0}}
     ```

12. No console do DynamoDB, conferir que o novo aluno foi inserido:
   ![](/mob/cloud/img/d7.png)
