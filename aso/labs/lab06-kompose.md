# Lab 6 - Kompose

Kompose permite importar *templates* do Docker Swarm no Kubernetes.
 
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

2. Navegar até a pasta ***/fiap/aso/swarm/v1*** de este repositório *git*:
    ```
    $ cd fiap/aso/swarm/v1
    $ pwd
    /home/ubuntu/fiap/aso/swarm/v1
    $ ls
    docker-compose.yaml
    ```

3. Importar o *stack* definido no arquivo ***docker-compose.yml*** dentro do Kubernetes:
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

4. Conferir que o *stack* foi importado corretamente dentro do Kubernetes:
    ```
    $ kubectl get service/api
    NAME   TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)    AGE
    api    ClusterIP   10.152.183.40   <none>        3000/TCP   95s
    ```

5. Testar a API:
    ```
    $ curl 10.152.183.40:3000
    Benvido a API FIAP!
    ```
6. Remover o *stack*:
    ```
    $ kompose down
    INFO Deleting application in "default" namespace  
    INFO Successfully deleted Service: api            
    INFO Successfully deleted Service: mysql          
    INFO Successfully deleted Pod: api                
    INFO Successfully deleted Pod: mysql              
    INFO Successfully deleted PersistentVolumeClaim: db-data 
    ```
