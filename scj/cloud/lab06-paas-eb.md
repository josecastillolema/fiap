# Lab 6 - AWS Elastic Beanstalk

Em este lab sobre **Elastic Beanstalk (EB)** aprenderemos alguns conceitos importantes da camada de plataforma da AWS:
 - *Deploy* de aplicações
 - Plataformas/entornos de execução disponíveis
 - *Logging*
 - Monitoramento

## *Deploy* local

1. Clonar o repositório:
    ```
    git clone https://github.com/josecastillolema/fiap
    ```
    
2. Navegar ate o diretorio `fiap/scj/cloud/lab06-paas`. O diretorio contem os [seguintes arquivos](lab06-paas-eb):
 - [**`application.py`**](lab06-paas-eb/application.py): Um serviço web escrito em Python que usa a biblioteca [Flask](https://flask.palletsprojects.com/en/1.1.x/).
 - [**`requirements.txt`**](lab06-paas-eb/requirements.txt): As dependências da aplicação. Podem ser instaladas usando `pip`, o gestor de dependências do Python.
 
3. Instalar as dependências:
    ```
    pip3 install -r requirements.txt
    ```
    
4. Executar a aplicação:
```
$ python3 application.py
 * Serving Flask app "application" (lazy loading)
 * Environment: production
   WARNING: Do not use the development server in a production environment.
   Use a production WSGI server instead.
 * Debug mode: off
 * Running on http://127.0.0.1:5000/ (Press CTRL+C to quit)
```

5. Testar o acesso local:
   ![](img/eb10.png)

## *Deploy* na AWS
 
1. Acessar o serviço **Elastic Beanstalk**:
   ![](img/eb0.png)

2. Criar um novo *environment*:
   ![](/mob/cloud/img/eb1.png)

3. A aplicação é um serviço web:
   ![](/mob/cloud/img/eb2.png)
   
4. Configurar o nome da apliação:
   ![](/mob/cloud/img/eb3.png)

5. Escolher o entorno de execução:
   ![](/mob/cloud/img/eb4.png)
   
6. Fazer o *upload* da aplicação. O arquivo comprimido deve conter os [seguintes arquivos](lab06-paas-eb):
 - [**`application.py`**](lab06-paas-eb/application.py): Um serviço web escrito em Python que usa a biblioteca [Flask](https://flask.palletsprojects.com/en/1.1.x/).
 - [**`requirements.txt`**](lab06-paas-eb/requirements.txt): As dependências da aplicação. Podem ser instaladas usando `pip`, o gestor de dependências do Python.
 
   ![](/mob/cloud/img/eb5.png)

7. Após uns minutos, conferir o estado da aplicação:
   ![](/mob/cloud/img/eb6.png)

8. Accessar a URL da aplicação:
   ![](/mob/cloud/img/eb7.png)

## *Logging* e monitoramento

9. Se for necessário fazer *troubleshooting* da aplicação, fazer *download* dos logs:
   ![](/mob/cloud/img/eb8.png)

10. Para monitorar a aplicação:
   ![](/mob/cloud/img/eb9.png)

