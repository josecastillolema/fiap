# Lab 2 - OpenStack Glance

## Image Service
Usaremos o serviço [Glance](https://docs.openstack.org/glance/latest/) para aprender alguns conceitos importantes de imagens/*snapshots* de máquinas virtuais:
 - formatos: qcow2, raw, vmdk, ami, ...
 - conversão entre formatos: ferramenta `qemu-img`
 - *snapshots*

## Pre-reqs

1. Carregar as credenciais de administrador e conferir que foram aplicadas no ambiente:
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
    
2. Listar os serviços Linux que compõem o Glance:
    ```
    $ systemctl | grep devstack@g
    ```
   
3. Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@g*
    ● devstack@g-api.service - Devstack devstack@g-api.service
       Loaded: loaded (/etc/systemd/system/devstack@g-api.service; enabled; vendor p
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 2min 19s ago
     Main PID: 757 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@g-api.service
               ├─757 glance-apiuWSGI maste
               ├─808 glance-apiuWSGI worker
               └─809 glance-apiuWSGI worker

    ● devstack@g-reg.service - Devstack devstack@g-reg.service
       Loaded: loaded (/etc/systemd/system/devstack@g-reg.service; enabled; vendor p
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 2min 20s ago
     Main PID: 750 (glance-registry)
       CGroup: /system.slice/system-devstack.slice/devstack@g-reg.service
               ├─ 750 /usr/bin/python /usr/local/bin/glance-registry --config-file=/
               ├─2287 /usr/bin/python /usr/local/bin/glance-registry --config-file=/
               ├─2309 /usr/bin/python /usr/local/bin/glance-registry --config-file=/
               ├─2327 /usr/bin/python /usr/local/bin/glance-registry --config-file=/
               └─2332 /usr/bin/python /usr/local/bin/glance-registry --config-file=/
    ```
    
4. Mostrar os logs do serviço:
    ```
    $ journalctl -u devstack@g-api
    ```
    
5. Mostrar os arquivos de configuração:
    ```
    $ less /etc/glance/glance-api.conf
    
    $ less /etc/glance/policy.json
    ```
    
6. Listar os módulos instalados no OpenStack:
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
    
    $ openstack service show glance
    +-------------+----------------------------------+
    | Field       | Value                            |
    +-------------+----------------------------------+
    | description | Glance Image Service             |
    | enabled     | True                             |
    | id          | d8b098869281469bbd90bbc762aad340 |
    | name        | glance                           |
    | type        | image                            |
    +-------------+----------------------------------+
    ```

## Imagens

7. Mostrar informação sobre a imagem (se encontra na pasta HOME do usuário):
    ```
    $ qemu-img info xenial-server-cloudimg-amd64-disk1.img
    image: xenial-server-cloudimg-amd64-disk1.img
    file format: qcow2
    virtual size: 2.2G (2361393152 bytes)
    disk size: 277M
    cluster_size: 65536
    Format specific information:
        compat: 0.10
        refcount bits: 16
    ```
    
8. Converter a imagem de formato qcow2 a formato raw:
    ```
    $ qemu-img convert -f qcow2 -O raw xenial-server-cloudimg-amd64-disk1.img xenial-server-cloudimg-amd64-disk1.raw
    ```

9. Listar as duas imagens e conferir a diferencia de tamanho:
    ```
    $ ls -lh xenial-server-cloudimg-amd64-disk1.*
    -rw-rw-r-- 1 os os 277M Mar 13  2018 xenial-server-cloudimg-amd64-disk1.img
    -rw-r--r-- 1 os os 2.2G Oct 19 11:27 xenial-server-cloudimg-amd64-disk1.raw
    ```

10.	Subir a imagem para o OpenStack:
    ```
    $ openstack image create --file xenial-server-cloudimg-amd64-disk1.img --disk-format qcow2 --public ubuntu-xenial
    +------------------+------------------------------------------------------+
    | Field            | Value                                                |
    +------------------+------------------------------------------------------+
    | checksum         | e924d1602ff88edca0a02e2ff129a810                     |
    | container_format | bare                                                 |
    | created_at       | 2020-10-19T18:31:17Z                                 |
    | disk_format      | qcow2                                                |
    | file             | /v2/images/029010d7-d0a1-429a-a6e7-79bec998bfb7/file |
    | id               | 029010d7-d0a1-429a-a6e7-79bec998bfb7                 |
    | min_disk         | 0                                                    |
    | min_ram          | 0                                                    |
    | name             | ubuntu-xenial                                        |
    | owner            | faac34f01fb2464295bcea501b18b741                     |
    | protected        | False                                                |
    | schema           | /v2/schemas/image                                    |
    | size             | 289996800                                            |
    | status           | active                                               |
    | tags             |                                                      |
    | updated_at       | 2020-10-19T18:31:22Z                                 |
    | virtual_size     | None                                                 |
    | visibility       | public                                               |
    +------------------+------------------------------------------------------+
    ```
    
    Se o paso anterior der o seguinte erro:
    ```
    $ openstack image create --file xenial-server-cloudimg-amd64-disk1.img --disk-format qcow2 --public ubuntu-xenial
    502 Bad Gateway: Bad Gateway: The proxy server received an invalid: response from an upstream server.: Apache/2.4.18 (Ubuntu) Server at 192.168.17.131 Port 80 (HTTP 502)
    ```
    
    Provavelmente a VM do DevStack foi restartada. Para concertar o Swift (*backend* do Glance):
    ```
    $ sudo mount -t xfs -o loop,noatime,nodiratime,nobarrier,logbufs=8 /opt/stack/data/drives/images/swift.img /opt/stack/data/drives/sdb1
    ```
    
    Tentar de novo o comando de criação de imagem.

11.	Listar as imagens que se encontram no ambiente e conferir o estado:
    ```
    $ openstack image list
    +--------------------------------------+--------------------------+--------+
    | ID                                   | Name                     | Status |
    +--------------------------------------+--------------------------+--------+
    | cd992dd3-2197-49fe-9f0e-43d783d18a5c | cirros-0.3.5-x86_64-disk | active |
    | 029010d7-d0a1-429a-a6e7-79bec998bfb7 | ubuntu-xenial            | active |
    +--------------------------------------+--------------------------+--------+
    ```

12.	Mostrar a informação sobre a imagem:
    ```
    $ openstack image show ubuntu-xenial
    +------------------+------------------------------------------------------+
    | Field            | Value                                                |
    +------------------+------------------------------------------------------+
    | checksum         | e924d1602ff88edca0a02e2ff129a810                     |
    | container_format | bare                                                 |
    | created_at       | 2020-10-19T18:31:17Z                                 |
    | disk_format      | qcow2                                                |
    | file             | /v2/images/029010d7-d0a1-429a-a6e7-79bec998bfb7/file |
    | id               | 029010d7-d0a1-429a-a6e7-79bec998bfb7                 |
    | min_disk         | 0                                                    |
    | min_ram          | 0                                                    |
    | name             | ubuntu-xenial                                        |
    | owner            | faac34f01fb2464295bcea501b18b741                     |
    | protected        | False                                                |
    | schema           | /v2/schemas/image                                    |
    | size             | 289996800                                            |
    | status           | active                                               |
    | tags             |                                                      |
    | updated_at       | 2020-10-19T18:31:22Z                                 |
    | virtual_size     | None                                                 |
    | visibility       | public                                               |
    +------------------+------------------------------------------------------+
    ```

13.	Colocar um metadato na imagem e conferir que foi aplicado na mesma: 
    ```
    $ openstack image set ubuntu-xenial --property os_name=linux
    
    $ openstack image show ubuntu-xenial
    +------------------+------------------------------------------------------+
    | Field            | Value                                                |
    +------------------+------------------------------------------------------+
    | checksum         | e924d1602ff88edca0a02e2ff129a810                     |
    | container_format | bare                                                 |
    | created_at       | 2020-10-19T18:31:17Z                                 |
    | disk_format      | qcow2                                                |
    | file             | /v2/images/029010d7-d0a1-429a-a6e7-79bec998bfb7/file |
    | id               | 029010d7-d0a1-429a-a6e7-79bec998bfb7                 |
    | min_disk         | 0                                                    |
    | min_ram          | 0                                                    |
    | name             | ubuntu-xenial                                        |
    | owner            | faac34f01fb2464295bcea501b18b741                     |
    | properties       | os_name='linux'                                      |
    | protected        | False                                                |
    | schema           | /v2/schemas/image                                    |
    | size             | 289996800                                            |
    | status           | active                                               |
    | tags             |                                                      |
    | updated_at       | 2020-10-19T18:32:17Z                                 |
    | virtual_size     | None                                                 |
    | visibility       | public                                               |
    +------------------+------------------------------------------------------+
    
    $ openstack image show ubuntu-xenial | grep prop
    | properties       | os_name='linux'                                      |
    ```

14.	Deletar a imagem:
    ```
    $ openstack image delete ubuntu-xenial
    ```

## Horizon

16.	Repetir o processo via Horizon Dashboard, criação de imagem e assignação de metadatos::
    - Criação de imagem
    - Assinação de metadata
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/glance1.png)
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/glance2.png)
