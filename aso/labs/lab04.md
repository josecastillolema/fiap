# Lab 3 - Docker Swarm

Orquestrando containers
--------------
Docker Swarm permite orquestrar containers em um cluster formado por vários servidores. De esta forma conseguimos garantir as seguintes propriedades nos containers gerenciados pelo orquestrador:
 - tolerancia a falhas: se um dos servidores do cluster cair, o container automaticamente sera iniciado em outro servidor do cluster
 - alta disponibilidade: varias replicas de cada container podem ser executadas em varios servidores do cluster
 - escalabilidade: o numero de replicas de cada container pode ser aumentado a qualquer momento em funcao da demanda
 
 Vamos trabalhar com duas máquinas virtuais (T1 e T2).

1. **[T1]** Inicializaçao do manager:
    ```
    $ docker swarm init
    Swarm initialized: current node (1y4bix4oby6nq2jxx5ft4rhd0) is now a manager.

    To add a worker to this swarm, run the following command:

        docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377

    To add a manager to this swarm, run 'docker swarm join-token manager' and follow the instructions.
    ```
 
2. **[T2]** Na segunda maquina virtual, inicializaçao do worker:
    ```
    $ docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377
    This node joined a swarm as a worker.
    ```

3. **[T1]** Listar os servidores que fazem parte do cluster Docker Swarm desde o manager:
    ```
    $ docker node ls
    ID                            HOSTNAME            STATUS              AVAILABILITY        MANAGER STATUS      ENGINE VERSION
    4hx9zm7docinufxy1mm62yynm     ip-172-31-32-255    Ready               Active                                  19.03.6
    1y4bix4oby6nq2jxx5ft4rhd0 *   ip-172-31-47-198    Ready               Active              Leader              19.03.6
    ```
    
4. **[T2]** Desde o worker nao e possivel executar nenhum comando do Docker Swarm, p.ex.:
    ```
    $ docker node ls
    Error response from daemon: This node is not a swarm manager. Worker nodes can't be used to view or modify cluster state. Please run this command on a manager node or promote the current node to a manager.
    ```
    
5. **[T1]** Criar o stack definido no arquivo ***docker-compose.yml***:
