# Lab 6 - OpenStack Swift

## Object Storage Service
Usaremos o serviço Swift para aprender alguns conceitos importantes sobre armazenamento de objeto:
 - criação de containers
 - objetos
 - acesso via URL

1. Carregar as credenciais de administrador e conferir que foram carregadas no ambiente:
    ```
    $ source devstack/openrc admin
    WARNING: setting legacy OS_TENANT_NAME to support cli tools.

    $ env | grep OS_
    OS_PROJECT_DOMAIN_ID=default
    OS_REGION_NAME=RegionOne
    OS_USER_DOMAIN_ID=default
    OS_PROJECT_NAME=demo
    OS_IDENTITY_API_VERSION=3
    OS_PASSWORD=nomoresecret
    OS_AUTH_TYPE=password
    OS_AUTH_URL=http://192.168.17.131/identity
    OS_USERNAME=admin
    OS_TENANT_NAME=demo
    OS_VOLUME_API_VERSION=2
    ```

2. Conferir que o Swift foi instalado no OpenStack:
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
 
3. Mostrar informação sobre o *endpoint*:
    ```
    $ openstack catalog show swift
    +-----------+-------------------------------------------------------------------------------+
    | Field     | Value                                                                         |
    +-----------+-------------------------------------------------------------------------------+
    | endpoints | RegionOne                                                                     |
    |           |   public: http://192.168.17.131:8080/v1/AUTH_faac34f01fb2464295bcea501b18b741 |
    |           | RegionOne                                                                     |
    |           |   admin: http://192.168.17.131:8080                                           |
    |           |                                                                               |
    | id        | 82020edd889243c2ac97416014376f12                                              |
    | name      | swift                                                                         |
    | type      | object-store                                                                  |
    +-----------+-------------------------------------------------------------------------------+
    ```
 
4. Conferir que a porta 8080 está aberta e associada ao Swift:
    ```
    $ sudo netstat -punlt | grep 8080
    tcp        0      0 0.0.0.0:8080            0.0.0.0:*               LISTEN      842/python
    
    $ ps aux  | grep 842
    os          842  0.9  0.4 160576 58440 ?        Ss   15:56   0:24 /usr/bin/python /usr/local/bin/swift-proxy-server /etc/swift/proxy-server.conf -v
    os         6895  0.0  0.0  14224   940 pts/3    S+   16:38   0:00 grep --color=auto 842
    
    ```
 
5. Listar os serviços Linux que compõem o Swift:
    ```
    $ systemctl | grep devstack@s
   devstack@s-account.service                                                           loaded active running   Devstack devstack@s-account.service
   devstack@s-container.service                                                         loaded active running   Devstack devstack@s-container.service
   devstack@s-object.service                                                            loaded active running   Devstack devstack@s-object.service
   devstack@s-proxy.service                                                             loaded active running   Devstack devstack@s-proxy.service
    ```
 
6. Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@s*
    ● devstack@s-account.service - Devstack devstack@s-account.service
       Loaded: loaded (/etc/systemd/system/devstack@s-account.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 42min ago
     Main PID: 773 (swift-account-s)
       CGroup: /system.slice/system-devstack.slice/devstack@s-account.service
               ├─ 773 /usr/bin/python /usr/local/bin/swift-account-server /etc/swift/account-server/1.conf -v
               ├─1794 /usr/bin/python /usr/local/bin/swift-account-server /etc/swift/account-server/1.conf -v
               └─1844 /usr/bin/python /usr/local/bin/swift-account-server /etc/swift/account-server/1.conf -v

    ● devstack@s-container.service - Devstack devstack@s-container.service
       Loaded: loaded (/etc/systemd/system/devstack@s-container.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 42min ago
     Main PID: 829 (swift-container)
       CGroup: /system.slice/system-devstack.slice/devstack@s-container.service
               ├─ 829 /usr/bin/python /usr/local/bin/swift-container-server /etc/swift/container-server/1.conf -v
               ├─1845 /usr/bin/python /usr/local/bin/swift-container-server /etc/swift/container-server/1.conf -v
               └─1848 /usr/bin/python /usr/local/bin/swift-container-server /etc/swift/container-server/1.conf -v

    ● devstack@s-object.service - Devstack devstack@s-object.service
       Loaded: loaded (/etc/systemd/system/devstack@s-object.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 42min ago
     Main PID: 762 (swift-object-se)
       CGroup: /system.slice/system-devstack.slice/devstack@s-object.service
               ├─ 762 /usr/bin/python /usr/local/bin/swift-object-server /etc/swift/object-server/1.conf -v
               ├─1799 /usr/bin/python /usr/local/bin/swift-object-server /etc/swift/object-server/1.conf -v
               └─1832 /usr/bin/python /usr/local/bin/swift-object-server /etc/swift/object-server/1.conf -v

    ● devstack@s-proxy.service - Devstack devstack@s-proxy.service
       Loaded: loaded (/etc/systemd/system/devstack@s-proxy.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 42min ago
     Main PID: 842 (swift-proxy-ser)
       CGroup: /system.slice/system-devstack.slice/devstack@s-proxy.service
               ├─ 842 /usr/bin/python /usr/local/bin/swift-proxy-server /etc/swift/proxy-server.conf -v
               └─2203 /usr/bin/python /usr/local/bin/swift-proxy-server /etc/swift/proxy-server.conf -v
    ```
 
7. Mostrar os *logs* do serviço:
    ```
    $ journalctl -u devstack@s-proxy
    ```
 
8. Mostrar os arquivos de configuração:
    ```
    $ less /etc/swift/swift.conf
    ```
 
9. Mostrar os arquivos de configuração de uma forma mais clara (sem comentários nem linhas vazias):
    ```
    $ egrep -v "^#|^$" /etc/swift/swift.conf
    ```
 
10.	Conferir o *endpoint* do Keystone:
    ```
    $ openstack catalog show keystone
    +-----------+------------------------------------------+
    | Field     | Value                                    |
    +-----------+------------------------------------------+
    | endpoints | RegionOne                                |
    |           |   admin: http://192.168.17.131/identity  |
    |           | RegionOne                                |
    |           |   public: http://192.168.17.131/identity |
    |           |                                          |
    | id        | 5d8f64102deb4f62a559776a899cb63e         |
    | name      | keystone                                 |
    | type      | identity                                 |
    +-----------+------------------------------------------+
    ```
 
11.	Mostrar estatísticas de uso gerais (passando o endpoint do Keystone + `/v3`):
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 stat
                   Account: AUTH_faac34f01fb2464295bcea501b18b741
                Containers: 0
                   Objects: 0
                     Bytes: 0
           X-Put-Timestamp: 1603753102.67486
               X-Timestamp: 1603753102.67486
                X-Trans-Id: txc3d40d2b1fa541f4831b0-005f97548e
              Content-Type: text/plain; charset=utf-8
    X-Openstack-Request-Id: txc3d40d2b1fa541f4831b0-005f97548e
    ```
 
12.	Listar os containers (não deveria ter nenhum ainda):
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 list
    ```
 
13.	Criar um arquivo de teste:
    ```
    $ echo "sic mundus creatus est" > teste.txt
    
    $ cat teste.txt
    sic mundus creatus est
    ```
 
14.	Subir o arquivo de teste a um container chamado `fiap`:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 upload fiap teste.txt
    teste.txt
    ```
 
15.	Listar os containers:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 list
    fiap
    ```
 
16.	Listar o container `fiap`:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 list fiap
    teste
    ```
 
17.	Mostrar estatísticas de uso gerais:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 stat
                            Account: AUTH_faac34f01fb2464295bcea501b18b741
                         Containers: 1
                            Objects: 0
                              Bytes: 0
    Containers in policy "policy-0": 1
       Objects in policy "policy-0": 0
         Bytes in policy "policy-0": 0
        X-Account-Project-Domain-Id: default
             X-Openstack-Request-Id: tx5006ab414c09461c9a17f-005f975e6c
                        X-Timestamp: 1603753191.77860
                         X-Trans-Id: tx5006ab414c09461c9a17f-005f975e6c
                       Content-Type: text/plain; charset=utf-8
                      Accept-Ranges: bytes
    ```
 
18.	Mostrar estatísticas de uso do container `fiap`:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 stat fiap
                   Account: AUTH_faac34f01fb2464295bcea501b18b741
                 Container: fiap
                   Objects: 1
                     Bytes: 7
                  Read ACL:
                 Write ACL:
                   Sync To:
                  Sync Key:
             Accept-Ranges: bytes
          X-Storage-Policy: Policy-0
             Last-Modified: Mon, 26 Oct 2020 22:59:52 GMT
               X-Timestamp: 1603753191.79482
                X-Trans-Id: tx08d25d4bce94419282003-005f975ead
              Content-Type: text/plain; charset=utf-8
    X-Openstack-Request-Id: tx08d25d4bce94419282003-005f975ead
    ```
 
19.	Mostrar estatísticas de uso do arquivo de teste:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 stat fiap teste.txt
                   Account: AUTH_faac34f01fb2464295bcea501b18b741
                 Container: fiap
                    Object: teste.txt
              Content Type: application/octet-stream
            Content Length: 7
             Last Modified: Mon, 26 Oct 2020 23:00:23 GMT
                      ETag: 2136806bbc5aa640da28ebb726a403f6
                Meta Mtime: 1603753129.613298
             Accept-Ranges: bytes
               X-Timestamp: 1603753222.98572
                X-Trans-Id: tx993dcb092f184eafa24a4-005f975eca
    X-Openstack-Request-Id: tx993dcb092f184eafa24a4-005f975eca
    ```
 
20.	Criar uma nova pasta e fazer o *download* do arquivo:
    ```
    $ mkdir novapasta
    
    $ cd novapasta/
    
    $ swift -V 3 -A http://192.168.17.131/identity/v3 download fiap
    teste.txt [auth 0.034s, headers 0.066s, total 0.067s, 0.000 MB/s]
    ```
 
21.	Conferir o conteúdo do arquivo baixado:
    ```
    $ ls -lh
    total 8.0K  -rw-rw-r-- 1 os os 7 Oct 26 15:58 teste.txt
    
    $ cat teste.txt
    sic mundus creatus est
    ```
 
22.	Configurar permissões de leitura para o usuário `demo` no projeto `demo`:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 post fiap -r "demo:demo"
    ```

23.	Configurar permissões de escrita para o usuário `demo` no projeto `demo`:
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 post fiap -w "demo:demo"
    ```
 
24.	Deletar objeto e container
    ```
    $ swift -V 3 -A http://192.168.17.131/identity/v3 delete fiap
    fiap
    
    $ swift -V 3 -A http://192.168.17.131/identity/v3 stat
                            Account: AUTH_faac34f01fb2464295bcea501b18b741
                         Containers: 0
                            Objects: 0
                              Bytes: 0
    Containers in policy "policy-0": 0
       Objects in policy "policy-0": 0
         Bytes in policy "policy-0": 0
        X-Account-Project-Domain-Id: default
             X-Openstack-Request-Id: txfa0553a66cb941f89a3d7-005f975fa5
                        X-Timestamp: 1603753191.77860
                         X-Trans-Id: txfa0553a66cb941f89a3d7-005f975fa5
                       Content-Type: text/plain; charset=utf-8
                      Accept-Ranges: bytes
    ```
    
25.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de container
    - *Upload* de arquivo
    - *Download* de arquivo
    - Configuração de permissões
    - Abrir objeto via URL
    
    ![](/cld/openstack/img/swift1.png)
