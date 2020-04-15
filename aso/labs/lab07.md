# Lab 5 - Kubernetes - Uso

Orquestrando containers
--------------
Kubernetes (k8s), da mesma forma que o Docker Swarm, permite orquestrar containers em um cluster formado por vários servidores. De esta forma conseguimos garantir as seguintes propriedades nos containers gerenciados pelo orquestrador:
 - **tolerância a falhas**: se um dos servidores do cluster cair, o container automaticamente será iniciado em outro servidor do cluster
 - **alta disponibilidade**: várias réplicas de cada container podem ser executadas em vários servidores do cluster
 - **escalabilidade**: o número de réplicas de cada container pode ser aumentado a qualquer momento em funçao da demanda
 
1. Inicialização do ***manager***:
    ```
    $ docker swarm init
    Swarm initialized: current node (1y4bix4oby6nq2jxx5ft4rhd0) is now a manager.

    To add a worker to this swarm, run the following command:

        docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377

    To add a manager to this swarm, run 'docker swarm join-token manager' and follow the instructions.
    ```
