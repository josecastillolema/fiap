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

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app01.png)

## Criaçao do serviço na Azure
 
1. Acessar o serviço **Web Application**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app02.png)

2. Criar uma nova *web application*.
    - Runtime: `Python 3.6`
    - SKU: `S1` ou algum outro SKU que tenha permissão para usar *deployment slots*
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app03.png)

3. Revisar a configuração do serviço:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app04.png)
   
4. Aguardar a criação do serviço:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app05.png)

5. Acessar a URL do serviço criado:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app06.png)

6. Foi desplegado automaticamente um site de teste:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app07.png)
   
## Configuraçao do repositório `git`

Criaremos um repositório `git` local para automatizar o *deploy* da aplicação.

7. No *Deployment center*, na aba de configuraçao criar um repositório git local:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app08.png)
   
8. Na aba de credenciais configurar um usuário e senha para poder clonar o repositório:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app09.png)

## Deploy da aplicaçao

De volta na VM, copiar a URI do passo anterior

## *Logging* e monitoramento

9. No *Deployment center*, na aba de configuraçao criar um repositório git local:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb8.png)

10. Para monitorar a aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eb9.png)
