# Lab 4 - Neutron

## Network Service
Usaremos o serviço Neutron para aprender alguns conceitos importantes sobre virtualização de redes:
 - criação de redes/subredes virtuais
 - *virtual routers*
 - *security groups*
 - *floating IPs*
 

1.	Conferir que o Neutron foi instalado no OpenStack:
    ```
    $ openstack service list
    +----------------------------------+-------------+----------------+
    | ID                               | Name        | Type           |
    +----------------------------------+-------------+----------------+
    | 14776d964367470ea97ae0f0395be6de | neutron     | network        |
    | 23dbf9f8ad1345e5b4d6d781c4b88e03 | cinder      | volume         |
    | 40301ed3d9744979a9e481d025cd3c9c | nova_legacy | compute_legacy |
    | 46a7ce69fbfd4e7691839b9442eafbbd | placement   | placement      |
    | 5d8f64102deb4f62a559776a899cb63e | keystone    | identity       |
    | 5f271b049588412d8e0a11b2fea5469c | nova        | compute        |
    | 7cd558d4608647569509bf34b794c59f | cinderv2    | volumev2       |
    | 7e447465286a4560a661a31297e9d45d | cinderv3    | volumev3       |
    | 82020edd889243c2ac97416014376f12 | swift       | object-store   |
    | cba857956e2349a5b4f6e1161862f340 | heat-cfn    | cloudformation |
    | d8b098869281469bbd90bbc762aad340 | glance      | image          |
    | fc980824a1954be882a9af7591f78e99 | heat        | orchestration  |
    +----------------------------------+-------------+----------------+
    ```
 
2.	Mostrar informação sobre o endpoint:
    ```
    $ openstack catalog show neutron
    +-----------+---------------------------------------+
    | Field     | Value                                 |
    +-----------+---------------------------------------+
    | endpoints | RegionOne                             |
    |           |   public: http://192.168.17.131:9696/ |
    |           |                                       |
    | id        | 14776d964367470ea97ae0f0395be6de      |
    | name      | neutron                               |
    | type      | network                               |
    +-----------+---------------------------------------+
    ```
 
3.	Conferir que a porta 9696 está aberta e associada ao Neutron:
    ```
    $ sudo netstat -punlt  | grep 9696
    tcp        0      0 0.0.0.0:9696            0.0.0.0:*               LISTEN                                                                                                                      831/python
    
    $ ps aux  | grep 831
    os          831  0.2  0.9 279916 119168 ?       Ss   04:54   0:04 /usr/bin/python /usr/local/bin/neutron-ser                                                                                    ver --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
    os         6963  0.0  0.0  14224   972 pts/2    S+   05:29   0:00 grep --color=auto 831
    ```

4.	Conferir a saúde dos agentes que compõem o Neutron:
    ```
    $ openstack network agent list
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+--                                                                                    -------------------------+
    | ID                                   | Agent Type         | Host   | Availability Zone | Alive | State | B                                                                                    inary                    |
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+--                                                                                    -------------------------+
    | 1714e6c2-5907-4550-ba3b-cffeeabe6600 | L3 agent           | ubuntu | nova              | :-)   | UP    | n                                                                                    eutron-l3-agent          |
    | 6194150e-7771-4a73-8963-5cb0f7a2a10f | Metadata agent     | ubuntu | None              | :-)   | UP    | n                                                                                    eutron-metadata-agent    |
    | cd64f22e-e148-48ea-b3a8-29880934cb18 | DHCP agent         | ubuntu | nova              | :-)   | UP    | n                                                                                    eutron-dhcp-agent        |
    | cfc5d9ac-c59a-4e07-b740-8cbda8b6ef50 | Open vSwitch agent | ubuntu | None              | :-)   | UP    | n                                                                                    eutron-openvswitch-agent |
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+--                                                                                    -------------------------+
    ```
 
5.	Listar os serviços Linux que compõem o Neutron:
    ```
    $ systemctl  | grep devstack@q
    devstack@q-agt.service                                                                           loaded acti                                                                                    ve running   Devstack devstack@q-agt.service
    devstack@q-dhcp.service                                                                          loaded acti                                                                                    ve running   Devstack devstack@q-dhcp.service
    devstack@q-l3.service                                                                            loaded acti                                                                                    ve running   Devstack devstack@q-l3.service
    devstack@q-meta.service                                                                          loaded acti                                                                                    ve running   Devstack devstack@q-meta.service
    devstack@q-svc.service                                                                           loaded acti                                                                                    ve running   Devstack devstack@q-svc.service
    os@ubuntu:~$ systemctl  | grep openvswitch
    openvswitch-nonetwork.service                                                                    loaded acti                                                                                    ve exited    Open vSwitch Internal Unit
    openvswitch-switch.service                                                                       loaded acti                                                                                    ve exited    Open vSwitch
    ```
 
6.	Conferir a saúde dos serviços:
    ```
    ```
 
7.	Mostrar os logs do serviço:
    ```
    ```
    
8.	Mostrar os arquivos de configuração:
    ```
    ```

9.	Mostrar os arquivos de configuração de uma forma mais clara (sem comentários nem linhas vazias):
    ```
    ```
 
10.	Criar uma rede:
    ```
    ```
 
11.	Criar uma subrede:
    ```
    ```
 
12.	Subir duas vms via Horizon associadas à rede/subrede que acabamos de criar no passos anteriores. Acessar a uma delas via console e tentar pingar a outra vm pelo IP interno. Depois tenta pingar para a Internet (p.ex. 8.8.8.8, o servidor DNS da Google, não vai conseguir):
    ```
    ```
 
13.	Criar um roteador:
    ```
    ```

14.	Associar o roteador a rede externa (pública) do OpenStack:
    ```
    ```
 
15.	Adicionar uma interface do roteador à subrede previamente criada:
    ```
    ```

16.	Repetir os testes de ping via console na vm previamente criada:
    ```
    ```

17.	Reservar um IP público (floating IP):
    ```
    ```

18.	Associar o IP público à VM previamente criada:
    ```
    ```
 
19.	Criar um security group:
    ```
    ```

20.	Liberar o tráfego ICMP (para poder pingar à VM):
    ```
    ```

21.	Liberar o tráfego TCP (para conseguir acessar via SSH à VM):
    ```
    ```

22.	Associar o security group à VM via Horizon (e retirar o default):
    ```
    ```

23.	Tentar pingar a VM pelo IP interno:
    ```
    ```

24.	Conferir o ID do roteador para saber a qual namespace temos que acessar:
    ```
    ``` 
 
25.	Listar os namespaces:
    ```
    ```

26.	Acessar ao namespace do router:
    ```
    ``` 

27.	Conferir os IPs (do gateway e floating IPs):
    ```
    ```

28.	Pingar à VM e acessar via SSH:
    ```
    ```

29.	Listar as portas:
    ```
    ```

30.	Mostar informação sobre uma porta determinada:
    ```
    ```

31.	Encontrar a interface associada a porta:
    ```
    ```
    
32.	Mostrar a porta no openvswitch:
    ```
    ```

33.	Mostrar os bridges e as interfaces configuradas no OpenvSwitch:
    ```
    ```
    
34.	Deletar vms, rede, subrede e roteador
    ```
    ```

35.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de rede
    - Criação de subrede
    - Criação de rotaedor
    - Assignar interfaces ao roteador
    - Reservar floating IP e associar a instância
    - Criação de security group
    - Liberar regras no security group

