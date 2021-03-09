# Lab 5 - App Service Plans

Em este lab sobre **App Service Plans** aprenderemos alguns conceitos importantes da camada de plataforma da Azure:
 - *Deploy* de aplicações
 - Plataformas/entornos de execução disponíveis
 - *Logging*
 - Monitoramento
 - Estratégias de *release*
    * Blue-Green
    * Canary
 
## Pre-reqs

- Uma VM com a imagem `Ubuntu Linux 18.04`
- `git`
- `python3`
- `pip3`
   
## *Deploy* em uma VM

1. Criar uma VM com a imagem `Ubuntu 18.04`

2. Logar na VM

3. Atualizar os repositorios:
    ```
    $ sudo apt update
    ```

4. Instalar o pip3 (gestor de pacotes do python3):
    ```
    $ sudo apt install python3-pip -y
    ```

5. Clonar o repositório:
    ```
    git clone https://github.com/josecastillolema/fiap
    ```

6. Navegar ate o diretorio `fiap/net/devops/lab05-paas-app`. O diretorio contem os [seguintes arquivos](https://github.com/josecastillolema/fiap/tree/master/net/devops/lab05-paas-app):
    - [**`application.py`**](https://github.com/josecastillolema/fiap/blob/master/net/devops/lab05-paas-app/application.py): Um serviço web escrito em Python que usa a biblioteca [Flask](https://flask.palletsprojects.com/en/1.1.x/).
    - [**`requirements.txt`**](https://github.com/josecastillolema/fiap/blob/master/net/devops/lab05-paas-app/requirements.txt): As dependências da aplicação. Podem ser instaladas usando `pip`, o gestor de dependências do Python.
 
7. Instalar as dependências:
    ```
    sudo pip3 install -r requirements.txt
    ```

8. Executar a aplicação:
    ```
    $ FLASK_APP=./application.py FLASK_RUN_HOST=0.0.0.0 flask run &
     * Serving Flask app "application"
     * Running on http://127.0.0.1:5000/ (Press CTRL+C to quit)
    ```

9. Testar o acesso local:
    ```
    $ curl localhost:5000
    <h1>Hola FIAP!</h1>
    MBA! o/
    ```

5. Testar o acesso remoto pela IP pública da VM (lembrando que é necessária a liberacão da porta 5000 no *security group* da VM):

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/eb10.png)

## *Deploy* na Azure
 
1. Acessar o serviço **Web Application**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/eb0.png)

2. Criar um novo *environment*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/eb1.png)

3. A aplicação é um serviço web:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/eb2.png)
   
4. Configurar o nome da apliação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/eb3.png)

5. Escolher o entorno de execução:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/net/devops/img/eb4.png)

7. Após uns minutos, conferir o estado da aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb6.png)

8. Accessar a URL da aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb7.png)

## *Logging* e monitoramento

9. Se for necessário fazer *troubleshooting* da aplicação, fazer *download* dos logs:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb8.png)

10. Para monitorar a aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb9.png)
