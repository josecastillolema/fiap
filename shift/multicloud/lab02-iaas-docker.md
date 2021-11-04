# Lab 2 - Docker

## Criando a instancia
Usaremos a imagem `josecastillolema/api` hospedada no [Docker Hub](https://hub.docker.com/r/josecastillolema/api) para aprender alguns conceitos importantes do docker. 
 - instalação
 - mapeamento de portas

A imagem `josecastillolema/api` contem os [seguintes arquivos](lab02-iaas-docker):
   - [**`api.py`**](lab02-iaas-docker/api.py): Uma simples API escrita em Python que usa a biblioteca [Flask](https://flask.palletsprojects.com/en/1.1.x/).
   - [**`requirements.txt`**](lab02-iaas-docker/requirements.txt): As dependências da aplicação. Podem ser instaladas usando `pip`, o gestor de dependências do Python.
   - [**`Dockerfile`**](lab02-iaas-docker/Dockerfile): O arquivo usado por ´docker´ para criar a imagem

## Pre-reqs

Usaremos uma máquina virtual no EC2 com a imagem oficial `Amazon Linux` para rodar o Docker:

- Uma maquina virtual com `Amazon Linux`. Seguir os passos do lab [lab 01 - EC2](/shift/multicloud/lab01-iaas-ec2.md). 
 
## Instalação do Docker

1. **[T1]** Instalação do Docker    
    a. Instalação dos pacotes
    ```
    $ sudo yum install -y docker
    ```
    
    b. Iniciar o serviço:
    ```
    $ sudo systemctl start docker
    $ sudo systemctl enable docker
    ```
    
    c. Conferir que o usuário não faz parte do grupo `docker`, e consecuentemente nao tem permissão para rodar comandos `docker`:
    ```
    $ groups
    ec2-user adm wheel systemd-journal
    ```

    d. Adicionar o usuário (`ec2-user`) ao grupo `docker`:
    ```
    $ sudo usermod -aG docker ec2-user
    ```

    e. Reiniciar a VM para que as mudanças de grupo sejam aplicadas:
    ```
    $ sudo reboot
    Connection to ec2-18-210-19-170.compute-1.amazonaws.com closed by remote host.
    Connection to ec2-18-210-19-170.compute-1.amazonaws.com closed.
    ```

    f. Após o reboot, confirmar que o usuário pertence ao grupo `docker`:
    ```
    $ groups
    ec2-user adm wheel systemd-journal docker
    ```

     g. Rodar um `docker version` para validar a instalação, e conferir que é mostrada tanto a versão do cliente **quanto a do servidor**:
     ```
     $ docker version
     Client:
      Version:           19.03.6
      API version:       1.40
      Go version:        go1.12.17
      Git commit:        369ce74a3c
      Built:             Fri Feb 28 23:45:43 2020
      OS/Arch:           linux/amd64
      Experimental:      false

     Server:
      Engine:
       Version:          19.03.6
       API version:      1.40 (minimum version 1.12)
       Go version:       go1.12.17
       Git commit:       369ce74a3c
       Built:            Wed Feb 19 01:06:16 2020
       OS/Arch:          linux/amd64
       Experimental:     false
      containerd:
       Version:          1.3.3-0ubuntu1~18.04.2
       GitCommit:        
      runc:
       Version:          spec: 1.0.1-dev
       GitCommit:        
      docker-init:
       Version:          0.18.0
       GitCommit:
     ```

## Obtenção da imagem

2. Listar as imagens do repositório local (o catálogo deveria estar vazio, pois não baixamos nenhuma imagem ainda):
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ```
    
3. Buscar imagens dentro do catálogo do [DockerHub](https://hub.docker.com/):
    ```
    $ docker search mongodb
    NAME                                DESCRIPTION                                     STARS               OFFICIAL            AUTOMATED
    mongo                               MongoDB document databases provide high avai…   7041                [OK]                
    mongo-express                       Web-based MongoDB admin interface, written w…   735                 [OK]                
    tutum/mongodb                       MongoDB Docker image – listens in port 27017…   229                                     [OK]
    bitnami/mongodb                     Bitnami MongoDB Docker Image                    123                                     [OK]
    frodenas/mongodb                    A Docker Image for MongoDB                      18                                      [OK]
    centos/mongodb-32-centos7           MongoDB NoSQL database server                   8                                       
    webhippie/mongodb                   Docker images for MongoDB                       7                                       [OK]
    centos/mongodb-26-centos7           MongoDB NoSQL database server                   5                                       
    centos/mongodb-36-centos7           MongoDB NoSQL database server                   5                                       
    eses/mongodb_exporter               mongodb exporter for prometheus                 4                                       [OK]
    neowaylabs/mongodb-mms-agent        This Docker image with MongoDB Monitoring Ag…   4                                       [OK]
    centos/mongodb-34-centos7           MongoDB NoSQL database server                   3                                       
    quadstingray/mongodb                MongoDB with Memory and User Settings           3                                       [OK]
    tozd/mongodb                        Base image for MongoDB server.                  2                                       [OK]
    mongodbsap/mongodbdocker                                                            2                                       
    zadki3l/mongodb-oplog               Simple mongodb image with single-node replic…   2                                       [OK]
    ssalaues/mongodb-exporter           MongoDB Replicaset Prometheus Compatible Met…   2                                       
    xogroup/mongodb_backup_gdrive       Docker image to create a MongoDB database ba…   1                                       [OK]
    bitnami/mongodb-exporter                                                            1                                       
    openshift/mongodb-24-centos7        DEPRECATED: A Centos7 based MongoDB v2.4 ima…   1                                       
    ansibleplaybookbundle/mongodb-apb   An APB to deploy MongoDB.                       1                                       [OK]
    targetprocess/mongodb_exporter      MongoDB exporter for prometheus                 0                                       [OK]
    gebele/mongodb                      mongodb                                         0                                       [OK]
    phenompeople/mongodb                 MongoDB is an open-source, document databas…   0                                       [OK]
    astronomerio/mongodb-source         Mongodb source.                                 0                                       [OK]
    ```
 
4. Fazer o *download* (`pull`) da imagem `josecastillolema/api` no repositório local:
    ```
    $ docker pull josecastillolema/api
    Using default tag: latest
    latest: Pulling from josecastillolema/api
    741437d97401: Pull complete 
    34d8874714d7: Pull complete 
    0a108aa26679: Pull complete 
    7f0334c36886: Pull complete 
    65c95cb8b3be: Pull complete 
    d6518cb8a076: Pull complete 
    978af0c41163: Pull complete 
    2279c641f679: Pull complete 
    0adc21e4cdc7: Pull complete 
    44e1126d38c8: Pull complete 
    24c75b074ef0: Pull complete 
    Digest: sha256:3da8f411cec4ffa960543c9260b193badde97397eb16f35bcac5e8e320bd5393
    Status: Downloaded newer image for josecastillolema/api:latest
    docker.io/josecastillolema/api:latest
    ```
    
5. Listar as imagens novamente, conferir que existe a imagem `josecastillolema/api`:
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    josecastillolema/api   latest              ad8814253c1b        2 years ago         933MB
    ```

## Testes com o container

6. Rodar um comando de exemplo (`hostname`) dentro do container:
    ```
    $ docker run josecastillolema/api hostname
    c293c1989a56
    ```

7. Medir o tempo do comando anterior:
    ```
    $ time docker run josecastillolema/api hostname
    7aa02808ccfc

    real	0m0.812s
    user	0m0.023s
    sys	0m0.027s
    ```
    Note-se que em menos de um segundo:
    - Docker criou o container
    - Rodou o comando `hostname` nele
    - Printou a sainda
    - Deletou o container
    
8. Conferir que tanto container quanto o *host* compartilham o Kernel:
    ```
    $ uname -a
    Linux ip-172-31-60-180 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux

    $ docker run josecastillolema/api uname -a
    Linux de0407ee790f 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux
    ```

9. Executar a imagem `josecastillolema/api` em modo interativo. Observe-se que o `prompt` muda quando logamos no container: usuário `root` com hostname `5b83d8b5b521` (o ID do container em este caso).
    ```
    $ docker run -it josecastillolema/api bash
    root@d8924e5138b3:/#
    ```

10. Criar um arquivo ainda dentro do container e sair do container:
    ```
    root@d8924e5138b3:/# touch meuArquivo
    
    root@d8924e5138b3:/# ls
    bin   dev  home  lib32  libx32  meuArquivo  opt   root  sbin  sys  usr
    boot  etc  lib   lib64  media   mnt         proc  run   srv   tmp  var

    root@d8924e5138b3:/# exit
    ```
    
11. Conferir que o container não está mais em execução:
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
    ```
    
12. Rodar o container novamente, e confirmar que o arquivo criado não existe mais. **Containers são efêmeros**, não armazenam nenhum tipo de mudança, sejam arquivos, dados, *softwares* instalados, etc.:
    ```
    $ docker run -it josecastillolema/api bash
    root@5b83d8b5b521:/# ls
    bin   dev  home  lib32  libx32  mnt  proc  run   srv  tmp  var
    boot  etc  lib   lib64  media   opt  root  sbin  sys  usr

    root@5b83d8b5b521:/# ls meuArquivo
    ls: meuArquivo: No such file or directory
    
    root@5b83d8b5b521:/# exit
    ```

## Execuçao do container

13. Vamos adicionar ao comando `docker run` o parâmetro `-p`, responsável pelo mapeamento de portas. Recebe um argumento do tipo ***x:y***, a onde ***x*** é a porta do lado do host e ***y*** a porta do lado do container. O parâmetro `-d` configura `docker` para rodar o container no *background*:
   ```
   $ docker run -d -p 5000:5000 josecastillolema/api
   ```

14. Conferir que o container está em execução:
   ```
   $ docker ps
   CONTAINER ID        IMAGE                  COMMAND             CREATED             STATUS                            PORTS                    NAMES
   e691f0fd676b        josecastillolema/api   "./api.py"          9 seconds ago       Up 8 seconds (health: starting)   0.0.0.0:5000->5000/tcp   funny_roentgen
   ```
   
15. Após 30 segundos da sua criação, o container deberia transicionar para estado `healthy` (columna `STATUS`):
   ```
   $ docker ps
   CONTAINER ID        IMAGE                  COMMAND             CREATED              STATUS                        PORTS                    NAMES
   e691f0fd676b        josecastillolema/api   "./api.py"          About a minute ago   Up About a minute (healthy)   0.0.0.0:5000->5000/tcp   funny_roentgen
   ```
   
16. Testar localmente a API:
   ```
   $ curl localhost:5000
   Benvido a API FIAP!
   ```

17. Testar o acesso remoto pela IP pública da VM (lembrando que é necessária a liberacão da porta 5000 no security group da VM):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/docker01.png)
