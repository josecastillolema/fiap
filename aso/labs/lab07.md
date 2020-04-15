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

 
1. Inicialização do ***manager***:
    ```
    $ docker swarm init
    Swarm initialized: current node (1y4bix4oby6nq2jxx5ft4rhd0) is now a manager.

    To add a worker to this swarm, run the following command:

        docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377

    To add a manager to this swarm, run 'docker swarm join-token manager' and follow the instructions.
    ```
