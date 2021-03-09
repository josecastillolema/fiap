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
     * Serving Flask app "./application.py"
     * Environment: production
       WARNING: Do not use the development server in a production environment.
       Use a production WSGI server instead.
     * Debug mode: off
     * Running on http://0.0.0.0:5000/ (Press CTRL+C to quit)
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

**De volta na VM**

9. Clonar o repositório local criado nos passos anteriores (copiar a URI do repositorio git local no passo anterior) usando as credencias recém criadas:
    ```
    $ git clone https://fiap-app.scm.azurewebsites.net:443/fiap.git
    Cloning into 'fiap-app'...
    Username for 'https://fiap.scm.azurewebsites.net:443': jlema
    Password for 'https://jlema@fiap.scm.azurewebsites.net:443': 
    warning: You appear to have cloned an empty repository.
    ```

10. Copiar os 2 arquivos:
    ```
    $ cp fiap/net/devops/lab05-paas-app/* fiap-app
    
    $ ls fiap-app
    application.py  requirements.txt
    ```
    
11. Comittar os novos arquivos no repositorio git local da app:
    ```
    $ cd fiap-app
    
    $ git status
    On branch master

    No commits yet

    Untracked files:
      (use "git add <file>..." to include in what will be committed)

     application.py
     requirements.txt

    nothing added to commit but untracked files present (use "git add" to track)
    
    $ git add *
    
    $ git commit -m "v1"
    [master (root-commit) a64c6ae] v1
     Committer: Ubuntu <jose@joselito.qzy1ejut0yhe3jyjrxopscm2pe.bx.internal.cloudapp.net>
    Your name and email address were configured automatically based
    on your username and hostname. Please check that they are accurate.
    You can suppress this message by setting them explicitly. Run the
    following command and follow the instructions in your editor to edit
    your configuration file:

        git config --global --edit

    After doing this, you may fix the identity used for this commit with:

        git commit --amend --reset-author

     2 files changed, 13 insertions(+)
     create mode 100644 application.py
     create mode 100644 requirements.txt
     
    $ git push origin master
    Username for 'https://fiap.scm.azurewebsites.net:443': jlema
    Password for 'https://jlema@fiap.scm.azurewebsites.net:443': 
    Counting objects: 4, done.
    Compressing objects: 100% (4/4), done.
    Writing objects: 100% (4/4), 487 bytes | 487.00 KiB/s, done.
    Total 4 (delta 0), reused 0 (delta 0)
    remote: Deploy Async
    remote: Updating branch 'master'.
    remote: Updating submodules.
    remote: Preparing deployment for commit id 'a64c6ae136'.
    remote: Repository path is /home/site/repository
    remote: Running oryx build...
    remote: Operation performed by Microsoft Oryx, https://github.com/Microsoft/Oryx
    remote: You can report issues at https://github.com/Microsoft/Oryx/issues
    remote: 
    remote: Oryx Version: 0.2.20210120.1, Commit: 66c7820d7df527aaffabd2563a49ad57930999c9, ReleaseTagName: 20210120.1
    remote: 
    remote: Build Operation ID: |Me5qumZrHLs=.51783a9b_
    remote: Repository Commit : a64c6ae136e87b9f9a83de6e1aa81a8145f997e5
    remote: 
    remote: Detecting platforms...
    remote: Detected following platforms:
    remote:   python: 3.6.12
    remote: Warning: An outdated version of python was detected (3.6.12). Consider updating.\nVersions supported by Oryx: https://github.com/microsoft/Oryx
    remote: 
    remote: 
    remote: Using intermediate directory '/tmp/8d8e34659c81cee'.
    remote: 
    remote: Copying files to the intermediate directory...
    remote: Done in 0 sec(s).
    remote: 
    remote: Source directory     : /tmp/8d8e34659c81cee
    remote: Destination directory: /home/site/wwwroot
    remote: 
    remote: Python Version: /opt/python/3.6.12/bin/python3.6
    remote: Python Virtual Environment: antenv3.6
    remote: Creating virtual environment...
    remote: ......
    remote: Activating virtual environment...
    remote: Running pip install...
    remote: [21:58:00+0000] Collecting click==6.7 (from -r requirements.txt (line 1))
    remote: [21:58:01+0000]   Downloading https://files.pythonhosted.org/packages/34/c1/8806f99713ddb993c5366c362b2f908f18269f8d792aff1abfd700775a77/click-6.7-py2.py3-none-any.whl (71kB)
    remote: [21:58:01+0000] Collecting Flask==1.0.2 (from -r requirements.txt (line 2))
    remote: [21:58:01+0000]   Downloading https://files.pythonhosted.org/packages/7f/e7/08578774ed4536d3242b14dacb4696386634607af824ea997202cd0edb4b/Flask-1.0.2-py2.py3-none-any.whl (91kB)
    remote: [21:58:02+0000] Collecting itsdangerous==0.24 (from -r requirements.txt (line 3))
    remote: [21:58:02+0000]   Downloading https://files.pythonhosted.org/packages/dc/b4/a60bcdba945c00f6d608d8975131ab3f25b22f2bcfe1dab221165194b2d4/itsdangerous-0.24.tar.gz (46kB)
    remote: [21:58:03+0000] Collecting Jinja2>=2.10.1 (from -r requirements.txt (line 4))
    remote: [21:58:03+0000]   Downloading https://files.pythonhosted.org/packages/7e/c2/1eece8c95ddbc9b1aeb64f5783a9e07a286de42191b7204d67b7496ddf35/Jinja2-2.11.3-py2.py3-none-any.whl (125kB)
    remote: [21:58:03+0000] Collecting MarkupSafe==1.0 (from -r requirements.txt (line 5))
    remote: [21:58:03+0000]   Downloading https://files.pythonhosted.org/packages/4d/de/32d741db316d8fdb7680822dd37001ef7a448255de9699ab4bfcbdf4172b/MarkupSafe-1.0.tar.gz
    remote: [21:58:04+0000] Collecting Werkzeug>=0.15.3 (from -r requirements.txt (line 6))
    remote: [21:58:04+0000]   Downloading https://files.pythonhosted.org/packages/cc/94/5f7079a0e00bd6863ef8f1da638721e9da21e5bacee597595b318f71d62e/Werkzeug-1.0.1-py2.py3-none-any.whl (298kB)
    remote: [21:58:04+0000] Building wheels for collected packages: itsdangerous, MarkupSafe
    remote: [21:58:04+0000]   Running setup.py bdist_wheel for itsdangerous: started
    remote: [21:58:06+0000]   Running setup.py bdist_wheel for itsdangerous: finished with status 'done'
    remote: [21:58:06+0000]   Stored in directory: /usr/local/share/pip-cache/wheels/2c/4a/61/5599631c1554768c6290b08c02c72d7317910374ca602ff1e5
    remote: [21:58:06+0000]   Running setup.py bdist_wheel for MarkupSafe: started
    remote: [21:58:09+0000]   Running setup.py bdist_wheel for MarkupSafe: finished with status 'done'
    remote: [21:58:09+0000]   Stored in directory: /usr/local/share/pip-cache/wheels/33/56/20/ebe49a5c612fffe1c5a632146b16596f9e64676768661e4e46
    remote: [21:58:09+0000] Successfully built itsdangerous MarkupSafe
    remote: [21:58:10+0000] Installing collected packages: click, itsdangerous, MarkupSafe, Jinja2, Werkzeug, Flas
    remote: [21:58:11+0000] Successfully installed Flask-1.0.2 Jinja2-2.11.3 MarkupSafe-1.0 Werkzeug-1.0.1 click-6.7 itsdangerous-0.24
    remote: You are using pip version 18.1, however version 21.0.1 is available.
    remote: You should consider upgrading via the 'pip install --upgrade pip' command.
    remote: 
    remote: Compressing existing 'antenv3.6' folder...
    remote: Done in 3 sec(s).
    remote: Preparing output...
    remote: 
    remote: Copying files to destination directory '/home/site/wwwroot'...
    remote: Done in 0 sec(s).
    remote: 
    remote: Removing existing manifest file
    remote: Creating a manifest file...
    remote: Manifest file created.
    remote: 
    remote: Done in 26 sec(s).
    remote: Running post deployment command(s)...
    remote: Triggering recycle (preview mode disabled).
    remote: Deployment successful.
    remote: Deployment Logs : 'https://fiap.scm.azurewebsites.net/newui/jsonviewer?view_url=/api/deployments/a64c6ae136e87b9f9a83de6e1aa81a8145f997e5/log'
    To https://fiap.scm.azurewebsites.net:443/fiap.git
     * [new branch]      master -> master
    ```
    
12. Confirmar que o deploy ocorreu com sucesso:
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app10.png)


## *Logging* e monitoramento

9. Na descrição da aplicaçao podemos consultar algumas métricas básicas:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app11.png)

10. Para fazer *troubleshooting* da aplicação, se o endereço da mesma for https://fiap.azurewebsites.net/, acessar https://fiap.scm.azurewebsites.net/:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app12.png)

## Deployment slots

Para implementar algums estratégias de *release* (blue-green e canary) usaremos *deployment slots*:

11. Criar um novo *slot* para o ambiente de homolgação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app13.png)

12. Confirmar a URL do *slot* de homologação. Se o endereço da app for https://fiap.azurewebsites.net/, o endereço do ambiente de homologação será https://fiap-homol.azurewebsites.net/
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app14.png)
   
13. Acessar o endereço de homologação e confirmar que Azure fez deploy da aplicação de teste:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app15.png)

14. Para fazer **blue-green**, escolher a opçao ***Swap*** (Intercambiar):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app16.png)

16. Para fazer o **canary release**, ajustar as porcentagens conforme desejado (usar *browsers* diferentes para testar):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app17.png)
