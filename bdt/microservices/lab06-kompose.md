# Lab 6 - Kompose

[Kompose](https://kompose.io/) permite importar *templates* do Docker Swarm no Kubernetes.
 
## Instalação

1. Instalação do Kompose

    a. Obtenção do executável:
    ```
    $ curl -L https://github.com/kubernetes/kompose/releases/download/v1.17.0/kompose-linux-amd64 -o kompose
      % Total    % Received % Xferd  Average Speed   Time    Time     Time  Current
                                     Dload  Upload   Total   Spent    Left  Speed
    100   630  100   630    0     0   3500      0 --:--:-- --:--:-- --:--:--  3519
    100 50.0M  100 50.0M    0     0  31.3M      0  0:00:01  0:00:01 --:--:-- 37.8M
    ```
    b. Ajuste da permissão de escrita:
    ```
    $ chmod +x kompose
    ```
    c. Mover o executável para a pasta correspondente:
    ```
    $ sudo mv ./kompose /usr/local/bin/
    ```

## Uso

2. Clonar este repositório *git*:
    ```
    $ git clone https://github.com/josecastillolema/fiap
    Cloning into 'fiap'...
    remote: Enumerating objects: 121, done.
    remote: Counting objects: 100% (121/121), done.
    remote: Compressing objects: 100% (116/116), done.
    remote: Total 4068 (delta 60), reused 3 (delta 1), pack-reused 3947
    Receiving objects: 100% (4068/4068), 51.48 MiB | 44.22 MiB/s, done.
    Resolving deltas: 100% (2059/2059), done.
    ```

3. Navegar até a pasta ***/fiap/bdt/microservices/swarm/v1*** de este repositório *git*:
    ```
    $ cd fiap/bdt/microservices/swarm/v1
    $ pwd
    /home/ubuntu/fiap/bdt/microservices/swarm/v1
    $ ls
    docker-compose.yaml
    ```
    
4. Conferir o conteúdo do arquivo ***docker-compose.yml***:
    ```yaml
    $ cat docker-compose.yaml 
    version: '3'
    services:
      api:
        image: josecastillolema/api
        ports:
          - "3000:5000"
        depends_on:
          - mysql
        deploy:
          replicas: 3
          #update_config:
          #  parallelism: 1
          #  delay: 10is
          mode: replicated
          restart_policy:
            condition: on-failure
      mysql:
        image: josecastillolema/mysql
        ports:
          - "3306:3306"
        volumes:
          - db-data:/var/lib/mysql
        environment:
          MYSQL_USER: root
          MYSQL_DATABASE: fiapdb
          MYSQL_ROOT_PASSWORD: senhaFiap
        deploy:
          replicas: 1
          # resources:
          #   limits:
          #     cpus: "0.1"
          #     memory: 50M
          restart_policy:
            condition: on-failure
    volumes:
      db-data:
    ```

5. Importar o *stack* definido no arquivo ***docker-compose.yml*** dentro do Kubernetes:
    ```
    $ kompose up
    INFO We are going to create Kubernetes Deployments, Services and PersistentVolumeClaims for your Dockerized application. If you need different kind of resources, use the 'kompose convert' and 'kubectl create -f' commands instead. 

    INFO Deploying application in "default" namespace 
    INFO Successfully created Service: api            
    INFO Successfully created Service: mysql          
    INFO Successfully created Pod: api                
    INFO Successfully created Pod: mysql              
    INFO Successfully created PersistentVolumeClaim: db-data of size 100Mi. If your cluster has dynamic storage provisioning, you don't have to do anything. Otherwise you have to create PersistentVolume to make PVC work 

    Your application has been deployed to Kubernetes. You can run 'kubectl get deployment,svc,pods,pvc' for details.
    ```

## Validação

6. Conferir que o *stack* foi importado corretamente dentro do Kubernetes:
    ```
    $ kubectl get service/api
    NAME   TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)    AGE
    api    ClusterIP   10.152.183.40   <none>        3000/TCP   95s
    ```

7. Aguardar os pods ficar em estado `running`
    ```
    $ kubectl get pod api
    NAME   READY   STATUS    RESTARTS   AGE
    api    1/1     Running   0          2m52s
    ```

7. Testar a API:
    ```
    $ curl 10.152.183.40:3000
    Benvido a API FIAP!
    ```

## *Clean-up*

8. Remover o *stack*:
    ```
    $ kompose down
    INFO Deleting application in "default" namespace  
    INFO Successfully deleted Service: api            
    INFO Successfully deleted Service: mysql          
    INFO Successfully deleted Pod: api                
    INFO Successfully deleted Pod: mysql              
    INFO Successfully deleted PersistentVolumeClaim: db-data 
    ```
