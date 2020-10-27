# Lab 6 - OpenStack Swift

## Object Storage Service
Usaremos o serviço Swift para aprender alguns conceitos importantes sobre armazenamento de objeto:
 - criação de containers
 - objetos
 - acesso via URL

1. Carregar as credenciais de administrador e conferir que foram carregadas no ambiente:
    ```
    
    ```

2. Conferir que o Swift foi instalado no OpenStack:
    ```
    
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
    $ sudo netstat -punlt  | grep 8080
    tcp        0      0 0.0.0.0:8080            0.0.0.0:*               LISTEN      842/python
    
    $ ps aux  | grep 842
    os          842  0.9  0.4 160576 58440 ?        Ss   15:56   0:24 /usr/bin/python /usr/local/bin/swift-proxy-server /etc/swift/proxy-server.conf -v
    os         6895  0.0  0.0  14224   940 pts/3    S+   16:38   0:00 grep --color=auto 842
    
    ```
 
5. Listar os serviços Linux que compõem o Swift:
    ```
    $ systemctl  | grep devstack@s
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
    $ swift -V 3 -A  http://192.168.17.131/identity/v3 stat
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
    $ swift -V 3 -A  http://192.168.17.131/identity/v3 list
    ```
 
13.	Criar um arquivo de teste:
    ```
    
    ```
 
14.	Subir o arquivo de teste a um container chamado `fiap`:
    ```
    
    ```
 
15.	Listar os containers:
    ```
    
    ```
 
16.	Listar o container `fiap`:
    ```
    
    ```
 
17.	Mostrar estatísticas de uso gerais:
    ```
    
    ```
 
18.	Mostrar estatísticas de uso do container `fiap`:
    ```
    
    ```
 
19.	Mostrar estatísticas de uso do arquivo de teste:
    ```
    
    ```
 
20.	Criar uma nova pasta e fazer o *download* do arquivo:
    ```
    
    ```
 
21.	Conferir o conteúdo do arquivo baixado:
    ```
    
    ```
 
22.	Configurar permissões de leitura para o usuário `demo` do projeto `demo`:
    ```
    
    ```

23.	Configurar permissões de escrita para o usuário `demo` do projeto `demo`:
    ```
    
    ```
 
24.	Deletar objeto e container
    ```
    
    ```
    
25.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de container
    - *Upload* de arquivo
    - *Download* de arquivo
    - Configuração de permissões
    - Abrir objeto via URL
    
    ![](/cld/openstack/img/swift1.png)
