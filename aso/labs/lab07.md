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

 
1. Navegar até a pasta ***/fiap/aso/kubernetes*** de este repositório *git*:
    ```
    $ cd fiap/aso/kubernetes
    $ pwd
    /home/ubuntu/fiap/aso/kubernetes
    $ ls
    api-deployment.yaml  api-service.yaml  mysql-deployment.yaml  mysql-pv.yaml  mysql-service.yaml
    ```
    
2. 
