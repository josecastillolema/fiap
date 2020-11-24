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

 
1. Navegar até a pasta `/fiap/bdt/microservices/kubernetes` de este repositório *git*:
    <pre>
    <b>$ cd fiap/bdt/microservices/kubernetes
    $ pwd</b>
    /home/ubuntu/fiap/bdt/microservices/kubernetes
    <b>$ ls</b>
    api-deployment.yaml  api-service.yaml  mysql-deployment.yaml  mysql-pv.yaml  mysql-service.yaml
    </pre>
    
2. Conferir as definições do volume persistente:
    ```yaml
    $ cat mysql-pv.yaml 
    kind: PersistentVolume
    apiVersion: v1
    metadata:
      name: mysql-pv-volume
      labels:
        type: local
    spec:
      storageClassName: manual
      capacity:
        storage: 1Gi
      accessModes:
        - ReadWriteOnce
      hostPath:
        path: "/mnt/data"
    ---
    apiVersion: v1
    kind: PersistentVolumeClaim
    metadata:
      name: mysql-pv-claim
    spec:
      storageClassName: manual
      accessModes:
        - ReadWriteOnce
      resources:
        requests:
          storage: 1Gi
    ```

3. Criar o volume persistente do banco de dados:
    ```
    $ kubectl create -f mysql-pv.yaml
    persistentvolume/mysql-pv-volume created
    persistentvolumeclaim/mysql-pv-claim created
    ```

4. Conferir as definições do serviço `api-deployment`:
    ```yaml
    $ cat api-service.yaml
    apiVersion: v1
    kind: Service
    metadata:
       name: api-deployment
    spec:
       ports:
       - port: 5000
         targetPort: 5000
       selector:
         app: api
    ```

5. Criar os serviços:
    ```
    $ kubectl create -f mysql-service.yaml
    service/mysql created
    $ kubectl create -f api-service.yaml
    service/api-deployment created
    ```

6. Conferir as definições do *deployment* `api-deployment`:
    ```yaml
    $ cat api-deployment.yaml 
    apiVersion: apps/v1
    kind: Deployment
    metadata:
       name: api-deployment
    spec:
       replicas: 3
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

7. Criar os *deployments*:
    ```
    $ kubectl create -f api-deployment.yaml
    deployment.apps/api-deployment created
    $ kubectl create -f mysql-deployment.yaml
    deployment.apps/mysql created
    ```
    
8. Confirmar a criação dos recursos:
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
   
9. Testar a API:
    ```
    $ curl 10.152.183.144:5000
    Benvido a API FIAP!
    ```

10. Testar a conexão da API com o banco de dados:
    ```
    $ curl 10.152.183.144:5000/getDados
    [{"id": 1234, "name": "Jose Castillo Lema"}]
    ```

11. Escalar o número de replicas da API (***scale-up***). Para iso, editar o arquivo ***api-deployment.yaml*** da seguinte forma:
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

12. Aplicar os novos parámetros:
    ```
    $ kubectl apply -f api-deployment.yaml
    Warning: kubectl apply should be used on resource created by either kubectl create --save-config or kubectl apply
    deployment.apps/api-deployment configured
    ```

13. Confirmar o ***scale-up***:
    ```
    $ kubectl get deployment api-deployment
    NAME             READY   UP-TO-DATE   AVAILABLE   AGE
    api-deployment   6/6     6            6           12s
    ```

14. Remover os recursos criados:
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
