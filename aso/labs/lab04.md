# Lab 4 - Docker Swarm

Orquestrando containers
--------------
Docker Swarm permite orquestrar containers em um cluster formado por vários servidores. De esta forma conseguimos garantir as seguintes propriedades nos containers gerenciados pelo orquestrador:
 - **tolerância a falhas**: se um dos servidores do cluster cair, o container automaticamente será iniciado em outro servidor do cluster
 - **alta disponibilidade**: várias réplicas de cada container podem ser executadas em vários servidores do cluster
 - **escalabilidade**: o número de réplicas de cada container pode ser aumentado a qualquer momento em funçao da demanda
 
 Vamos trabalhar com duas máquinas virtuais (**T1** e **T2**).

1. **[T1]** Inicialização do ***manager***:
    ```
    $ docker swarm init
    Swarm initialized: current node (1y4bix4oby6nq2jxx5ft4rhd0) is now a manager.

    To add a worker to this swarm, run the following command:

        docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377

    To add a manager to this swarm, run 'docker swarm join-token manager' and follow the instructions.
    ```
 
2. **[T2]** Na segunda maquina virtual, inicialização do ***worker***:
    ```
    $ docker swarm join --token SWMTKN-1-5it2k13vtptja3tl2xpgjywr856a4r7siuve20r2ev9h98gfrj-498uqdu6x8o74b816orz6s5gn 172.31.47.198:2377
    This node joined a swarm as a worker.
    ```

3. **[T1]** Listar os servidores que fazem parte do cluster Docker Swarm desde o *manager*:
    ```
    $ docker node ls
    ID                            HOSTNAME            STATUS              AVAILABILITY        MANAGER STATUS      ENGINE VERSION
    4hx9zm7docinufxy1mm62yynm     ip-172-31-32-255    Ready               Active                                  19.03.6
    1y4bix4oby6nq2jxx5ft4rhd0 *   ip-172-31-47-198    Ready               Active              Leader              19.03.6
    ```
    
4. **[T2]** Desde o *worker* não é possível executar nenhum comando do Docker Swarm, p.ex.:
    ```
    $ docker node ls
    Error response from daemon: This node is not a swarm manager. Worker nodes can't be used to view or modify cluster state. Please run this command on a manager node or promote the current node to a manager.
    ```
    
5. **[T1]** Navegar ate a pasta ***/fiap/aso/swarm/v1*** de este repositório *git*:
    ```
    $ cd fiap/aso/swarm/v1
    $ pwd
    /home/ubuntu/fiap/aso/swarm/v1
    $ ls
    docker-compose.yaml
    ```

6. **[T1]** Mostrar o conteúdo do arquivo ***docker-compose.yaml***. São definidos dois serviços:
    - **api**: a API escrita em Python, que tem dependência (consulta) o serviço *mysql*, com **3 réplicas**
    - **mysql**: o servidor MySQL, com mapeamento de portas (porta 3306), persistência de dados (pasta */var/lib/mysql*) e algumas variáveis de entorno, com **1 réplica**
    ```
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

7. **[T1]** Criar o *stack* definido no arquivo ***docker-compose.yml***:
    ```
    $ docker stack deploy -c docker-compose.yaml stackFiap
    Creating network stackFiap_default
    Creating service stackFiap_api
    Creating service stackFiap_mysql
    ```

8. **[T1]** Conferir que o *stack* foi criado corretamente:
    ```
    $ docker stack ls
    NAME                SERVICES            ORCHESTRATOR
    stackFiap           2                   Swarm
    ```

9. **[T1]** Conferir que os serviços foram criados corretamente:
    ```
    $ docker service ls
    ID                  NAME                MODE                REPLICAS            IMAGE                           PORTS
    vlh0cjv5nd65        stackFiap_api       replicated          3/3                 josecastillolema/api:latest     *:3000->5000/tcp
    tmns8lwyrb9f        stackFiap_mysql     replicated          1/1                 josecastillolema/mysql:latest   *:3306->3306/tcp
    ```

10. **[T1]** Conferir quais containers foram criados na primeira máquina virtual (em este caso o banco de dados e uma instancia da API):
    ```
    $ docker ps
    CONTAINER ID        IMAGE                           COMMAND                  CREATED             STATUS                   PORTS                 NAMES
    44cfaf700b20        josecastillolema/mysql:latest   "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes             3306/tcp, 33060/tcp   stackFiap_mysql.1.wr5ia70abxco05ypimayagrm0
    2239ae187e86        josecastillolema/api:latest     "./api.py"               2 minutes ago       Up 2 minutes (healthy)   5000/tcp              stackFiap_api.1.qedwp50z4l5dhskg66txj91d7
    ```

11. **[T2]** Conferir quais containers foram criados na segunda máquina virtual (em este caso duas instancias da API):
    ```
    $ docker ps
    CONTAINER ID        IMAGE                         COMMAND             CREATED             STATUS                   PORTS               NAMES
    48fdb1fb8cb7        josecastillolema/api:latest   "./api.py"          2 minutes ago       Up 2 minutes (healthy)   5000/tcp            stackFiap_api.3.rpl9ggawgeo7edietartr7ca0
    3201f87fc015        josecastillolema/api:latest   "./api.py"          2 minutes ago       Up 2 minutes (healthy)   5000/tcp            stackFiap_api.2.rt2zby2s1s8igdzysd3gbmv9w
    ```

12. **[T1]** Aumentar o numero de replicas da API (***scale out***):
    ```
    $ docker service scale stackFiap_api=10
    stackFiap_api scaled to 10
    overall progress: 10 out of 10 tasks 
    1/10: running   [==================================================>] 
    2/10: running   [==================================================>] 
    3/10: running   [==================================================>] 
    4/10: running   [==================================================>] 
    5/10: running   [==================================================>] 
    6/10: running   [==================================================>] 
    7/10: running   [==================================================>] 
    8/10: running   [==================================================>] 
    9/10: running   [==================================================>] 
    10/10: running   [==================================================>] 
    verify: Service converged
    ```

13. **[T1]** Confirmar o novo numero de replicas:
    ```
    $ docker service ls
    ID                  NAME                MODE                REPLICAS            IMAGE                           PORTS
    vlh0cjv5nd65        stackFiap_api       replicated          10/10               josecastillolema/api:latest     *:3000->5000/tcp
    tmns8lwyrb9f        stackFiap_mysql     replicated          1/1                 josecastillolema/mysql:latest   *:3306->3306/tcp
    ```

14. **[T1]** Diminuir o numero de replicas da API (***scale in***):
    ```
    $ docker service scale stackFiap_api=8
    stackFiap_api scaled to 8
    overall progress: 8 out of 8 tasks 
    1/8: running   [==================================================>] 
    2/8: running   [==================================================>] 
    3/8: running   [==================================================>] 
    4/8: running   [==================================================>] 
    5/8: running   [==================================================>] 
    6/8: running   [==================================================>] 
    7/8: running   [==================================================>] 
    8/8: running   [==================================================>] 
    verify: Service converged 
    ```

15. **[T1]** Vamos desligar o *worker* (servidor **T2**). Antes disso, conferir os containers que estão rodando no *manager* (neste caso, o banco de dados e 4 replicas da API):
    ```
    $ docker ps
    CONTAINER ID        IMAGE                           COMMAND                  CREATED             STATUS                    PORTS                 NAMES
    43b63e9b6440        josecastillolema/api:latest     "./api.py"               4 minutes ago       Up 4 minutes (healthy)    5000/tcp              stackFiap_api.6.s82fwwvh7u7rnm5m9c4g5r9be
    33c0a2fb04b2        josecastillolema/api:latest     "./api.py"               4 minutes ago       Up 4 minutes (healthy)    5000/tcp              stackFiap_api.8.8qb8bezn59rea0pyf8i3dgd6n
    325de1e84b51        josecastillolema/api:latest     "./api.py"               4 minutes ago       Up 4 minutes (healthy)    5000/tcp              stackFiap_api.4.vciwnzaxakavsj2t3p2tlaekc
    44cfaf700b20        josecastillolema/mysql:latest   "docker-entrypoint.s…"   11 minutes ago      Up 11 minutes             3306/tcp, 33060/tcp   stackFiap_mysql.1.wr5ia70abxco05ypimayagrm0
    2239ae187e86        josecastillolema/api:latest     "./api.py"               11 minutes ago      Up 11 minutes (healthy)   5000/tcp              stackFiap_api.1.qedwp50z4l5dhskg66txj91d7
    ```

16. **[T2]** Desligar o *worker*:
    ```
    $ sudo shutdown -h now
    Connection to ec2-3-85-40-189.compute-1.amazonaws.com closed by remote host.
    Connection to ec2-3-85-40-189.compute-1.amazonaws.com closed.
    ```
    
17. **[T1]** Apos uns instantes, confirmar que o *worker* aparece como ***down***:
    ```
    $ docker node ls
    ID                            HOSTNAME            STATUS              AVAILABILITY        MANAGER STATUS      ENGINE VERSION
    4hx9zm7docinufxy1mm62yynm     ip-172-31-32-255    Down                Active                                  19.03.6
    1y4bix4oby6nq2jxx5ft4rhd0 *   ip-172-31-47-198    Ready               Active              Leader              19.03.6
    ```

18. **[T1]** Confirmar que os containers que estavam rodando no *worker* (servidor **T2**), foram recriados no *manager* (neste caso, 4 replicas da API):
    ```
    $ docker ps
    CONTAINER ID        IMAGE                           COMMAND                  CREATED              STATUS                        PORTS                 NAMES
    2bbcefe6fc28        josecastillolema/api:latest     "./api.py"               About a minute ago   Up About a minute (healthy)   5000/tcp              stackFiap_api.3.15hrsir7ac5a2eun79wbp6ftf
    41067e32567d        josecastillolema/api:latest     "./api.py"               About a minute ago   Up About a minute (healthy)   5000/tcp              stackFiap_api.7.ldetjf1ykn7kyaokadcaod0di
    497a7b5d061f        josecastillolema/api:latest     "./api.py"               About a minute ago   Up About a minute (healthy)   5000/tcp              stackFiap_api.5.kk77a42tdo6u3c04zhz6uu00s
    28a3f6c31f48        josecastillolema/api:latest     "./api.py"               About a minute ago   Up About a minute (healthy)   5000/tcp              stackFiap_api.2.1bjyj87e8ts56zy2s20h4n2gs
    43b63e9b6440        josecastillolema/api:latest     "./api.py"               7 minutes ago        Up 7 minutes (healthy)        5000/tcp              stackFiap_api.6.s82fwwvh7u7rnm5m9c4g5r9be
    33c0a2fb04b2        josecastillolema/api:latest     "./api.py"               7 minutes ago        Up 7 minutes (healthy)        5000/tcp              stackFiap_api.8.8qb8bezn59rea0pyf8i3dgd6n
    325de1e84b51        josecastillolema/api:latest     "./api.py"               7 minutes ago        Up 7 minutes (healthy)        5000/tcp              stackFiap_api.4.vciwnzaxakavsj2t3p2tlaekc
    44cfaf700b20        josecastillolema/mysql:latest   "docker-entrypoint.s…"   14 minutes ago       Up 14 minutes                 3306/tcp, 33060/tcp   stackFiap_mysql.1.wr5ia70abxco05ypimayagrm0
    2239ae187e86        josecastillolema/api:latest     "./api.py"               15 minutes ago       Up 15 minutes (healthy)       5000/tcp              stackFiap_api.1.qedwp50z4l5dhskg66txj91d7
    ```

19. **[T1]** Remover o *stack*:
    ```
    $ docker stack rm stackFiap
    Removing service stackFiap_api
    Removing service stackFiap_mysql
    Removing network stackFiap_default
    ```
