# Lab 5 - Kubernetes - Uso

Orquestrando containers
--------------
Existem vários recursos dentro de um cluster Kubernetes:
 - **pod**: conjunto de um ou mais *containers*
 - **service**: cria um *endpoint* para acessar os *pods* de uma determinada *app*
 - **deployment**: define as propriedades desejadas de uma *app*
     - imagem
     - variáveis de entorno
     - volumes
     - número de réplicas

 
1. Navegar até a pasta `/fiap/aso/kubernetes` de este repositório *git*:
    <pre>
    <b>$ cd fiap/aso/kubernetes
    $ pwd</b>
    /home/ubuntu/fiap/aso/kubernetes
    <b>$ ls</b>
    api-deployment.yaml  api-service.yaml  mysql-deployment.yaml  mysql-pv.yaml  mysql-service.yaml
    </pre>
    
2. Criar o volume persistente do banco de dados:
    ```
    $ kubectl create -f mysql-pv.yaml
    persistentvolume/mysql-pv-volume created
    persistentvolumeclaim/mysql-pv-claim created
    ```

3. Criar os serviços:
    ```
    $ kubectl create -f mysql-service.yaml
    service/mysql created
    $ kubectl create -f api-service.yaml
    service/api-deployment created
    ```

4. Criar os *deployments*:
    ```
    $ kubectl create -f api-deployment.yaml
    deployment.apps/api-deployment created
    $ kubectl create -f mysql-deployment.yaml
    deployment.apps/mysql created
    ```
    
5. Confirmar a criação dos recursos:
   ```
   $ kubectl get all
   NAME                                  READY   STATUS    RESTARTS   AGE
   pod/api-deployment-7bcb964d7c-bmwmx   1/1     Running   0          2m26s
   pod/api-deployment-7bcb964d7c-n6svh   1/1     Running   0          2m26s
   pod/api-deployment-7bcb964d7c-tvr6w   1/1     Running   0          2m26s
   pod/mysql-59f74f847d-rjlxt            1/1     Running   0          2m20s

   NAME                     TYPE        CLUSTER-IP       EXTERNAL-IP   PORT(S)    AGE
   service/api-deployment   ClusterIP   10.152.183.144   <none>        5000/TCP   2m35s
   service/kubernetes       ClusterIP   10.152.183.1     <none>        443/TCP    41m
   service/mysql            ClusterIP   10.152.183.120   <none>        3306/TCP   2m42s

   NAME                             READY   UP-TO-DATE   AVAILABLE   AGE
   deployment.apps/api-deployment   3/3     3            3           2m26s
   deployment.apps/mysql            1/1     1            1           2m20s

   NAME                                        DESIRED   CURRENT   READY   AGE
   replicaset.apps/api-deployment-7bcb964d7c   3         3         3       2m26s
   replicaset.apps/mysql-59f74f847d            1         1         1       2m20s
   ```
   
6. Testar a API:
    ```
    $ curl 10.152.183.144:5000
    Benvido a API FIAP!
    ```

7. Testar a conexão da API com o banco de dados:
    ```
    $ curl 10.152.183.144:5000/getDados
    [{"id": 1234, "name": "Jose Castillo Lema"}]
    ```

8. Escalar o número de replicas da API (***scale-up***). Para iso, editar o arquivo ***api-deployment.yaml*** da seguinte forma:
    ```yaml
    $ cat api-deployment.yaml 
    apiVersion: apps/v1
    kind: Deployment
    metadata:
       name: api-deployment
    spec:
       replicas: 5
       selector:
         matchLabels:
           app: api
       template:
         metadata:
           labels:
             app: api
         spec:
           containers:
           - name: api
             image: josecastillolema/api
             imagePullPolicy: Always
             ports:
             - containerPort: 5000
    ```

9. Aplicar os novos parámetros:
    ```
    $ kubectl apply -f api-deployment.yaml
    Warning: kubectl apply should be used on resource created by either kubectl create --save-config or kubectl apply
    deployment.apps/api-deployment configured
    ```

10. Confirmar o ***scale-up***:
    ```
    $ kubectl get deployment api-deployment
    NAME             READY   UP-TO-DATE   AVAILABLE   AGE
    api-deployment   6/6     6            6           12s
    ```

11. Remover os recursos criados:
    ```
    $ kubectl delete deployment api-deployment 
    deployment.apps "api-deployment" deleted

    $ kubectl delete deployment mysql
    deployment.apps "mysql" deleted

    $ kubectl delete service api-deployment
    service "api-deployment" deleted

    $ kubectl delete service mysql
    service "mysql" deleted

    $ kubectl delete persistentvolumeclaim/mysql-pv-claim
    persistentvolumeclaim "mysql-pv-claim" deleted

    $ kubectl delete persistentvolume/mysql-pv-volume 
    persistentvolume "mysql-pv-volume" deleted
    ```
