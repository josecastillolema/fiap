# Lab 7 - OpenStack Heat

## Orchestration Service
Usaremos o serviço [Heat](https://docs.openstack.org/heat/latest/) para aprender alguns conceitos importantes sobre orquestração e *Infrastructure as Code* (IaC):
 - criação de pilhas (*stacks*)
 - listagem de recursos

## Pre-reqs

1. Carregar as credenciais de administrador e conferir que foram aplicadas no ambiente:
    ```
    $ source devstack/openrc admin
    WARNING: setting legacy OS_TENANT_NAME to support cli tools.
    ```
 
2. Conferir que o Heat foi instalado no OpenStack:
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
    $ openstack catalog show heat
    +-----------+--------------------------------------------------------------------------------+
    | Field     | Value                                                                          |
    +-----------+--------------------------------------------------------------------------------+
    | endpoints | RegionOne                                                                      |
    |           |   internal: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741 |
    |           | RegionOne                                                                      |
    |           |   admin: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741    |
    |           | RegionOne                                                                      |
    |           |   public: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741   |
    |           |                                                                                |
    | id        | fc980824a1954be882a9af7591f78e99                                               |
    | name      | heat                                                                           |
    | type      | orchestration                                                                  |
    +-----------+--------------------------------------------------------------------------------+
    ```

4. Listar os serviços Linux que compõem o Heat:
    ```
    $ systemctl | grep devstack@h
    devstack@h-api-cfn.service                                            loaded active running   Devstack devstack@h-api-cfn.service
    devstack@h-api.service                                                loaded active running   Devstack devstack@h-api.service
    devstack@h-eng.service                                                loaded active running   Devstack devstack@h-eng.service
    ```

5. Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@h*
    ● devstack@h-api.service - Devstack devstack@h-api.service
       Loaded: loaded (/etc/systemd/system/devstack@h-api.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 1 day 20h ago
     Main PID: 778 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@h-api.service
               ├─778 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-uwsgi.ini
               ├─868 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-uwsgi.ini
               └─869 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-uwsgi.ini

    ● devstack@h-eng.service - Devstack devstack@h-eng.service
       Loaded: loaded (/etc/systemd/system/devstack@h-eng.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 1 day 20h ago
     Main PID: 800 (heat-engine)
       CGroup: /system.slice/system-devstack.slice/devstack@h-eng.service
               ├─ 800 /usr/bin/python /usr/local/bin/heat-engine --config-file=/etc/heat/heat.conf
               ├─2381 /usr/bin/python /usr/local/bin/heat-engine --config-file=/etc/heat/heat.conf
               ├─2382 /usr/bin/python /usr/local/bin/heat-engine --config-file=/etc/heat/heat.conf
               ├─2384 /usr/bin/python /usr/local/bin/heat-engine --config-file=/etc/heat/heat.conf
               └─2389 /usr/bin/python /usr/local/bin/heat-engine --config-file=/etc/heat/heat.conf

    ● devstack@h-api-cfn.service - Devstack devstack@h-api-cfn.service
       Loaded: loaded (/etc/systemd/system/devstack@h-api-cfn.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:07 PDT; 1 day 20h ago
     Main PID: 874 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@h-api-cfn.service
               ├─874 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-cfn-uwsgi.ini
               ├─912 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-cfn-uwsgi.ini
               └─913 /usr/local/bin/uwsgi --ini /etc/heat/heat-api-cfn-uwsgi.ini
    ```

6. Mostrar os *logs* do serviço:
    ```
    $ journalctl -u devstack@h-eng
    ```

7. Mostrar os arquivos de configuração:
    ```
    $ less /etc/heat/heat.conf
    ```

## *Stacks*

8. Baixar o *template*:
    ```
    $ git clone https://github.com/josecastillolema/fiap
    Cloning into 'fiap'...
    remote: Enumerating objects: 10, done.
    remote: Counting objects: 100% (10/10), done.
    remote: Compressing objects: 100% (10/10), done.
    remote: Total 3716 (delta 4), reused 0 (delta 0), pack-reused 3706
    Receiving objects: 100% (3716/3716), 44.63 MiB | 3.88 MiB/s, done.
    Resolving deltas: 100% (1862/1862), done.
    Checking connectivity... done.
    ```
    
9. Conferir o *stack* que vai ser criado:
    ```yaml
    $ cd fiap/cld/openstack/lab07-heat/
    
    $ cat heat.yaml
    heat_template_version: 2016-04-08

    description: Servidor para FIAP CLD!

    parameters:
      flavor:
        type: string
        description: Flavor para o servidor web
        constraints:
        - custom_constraint: nova.flavor
      image:
        type: string
        description: Imagem para o servidor web
        constraints:
        - custom_constraint: glance.image
      private_network:
        type: string
        description: Rede interna
        constraints:
        - custom_constraint: neutron.network
      private_ip:
        type: string
        description: IP interna do servidor
        default: 10.0.1.200
      public_network:
        type: string
        description: Rede publica
        constraints:
        - custom_constraint: neutron.network

    resources:
      sec_group:
        type: OS::Neutron::SecurityGroup
        properties:
          rules:
          - remote_ip_prefix: 0.0.0.0/0
            protocol: icmp
          - remote_ip_prefix: 0.0.0.0/0
            protocol: tcp
            port_range_min: 80
            port_range_max: 80
          - remote_ip_prefix: 0.0.0.0/0
            protocol: tcp
            port_range_min: 22
            port_range_max: 22

      server_port:
        type: OS::Neutron::Port
        properties:
          network_id: { get_param: private_network }
          security_groups: [{ get_resource: sec_group }]

      server:
        type: OS::Nova::Server
        properties:
          image: { get_param: image }
          flavor: { get_param: flavor }
          networks:
            - port: { get_resource: server_port }

      floating_ip:
        type: OS::Neutron::FloatingIP
        properties:
          floating_network: { get_param: public_network }
          port_id: { get_resource: server_port }

    outputs:
      lburl:
        description: URL do servidor
        value:
          str_replace:
            template: http://IP_ADDRESS
            params:
              IP_ADDRESS: { get_attr: [ floating_ip, floating_ip_address ] }
        description: >
          Esta URL e a URL "externa" que pode ser usada para acessar o servidor WEB.
    ```

10. Criar um *stack*:
    ```
    $ openstack stack create -t heat.yaml fiap-stack --parameter image=cirros-0.3.5-x86_64-disk --parameter private_network=private --parameter flavor=m.fiap --parameter public_network=public
    +---------------------+--------------------------------------+
    | Field               | Value                                |
    +---------------------+--------------------------------------+
    | id                  | 9fe42824-9f12-4925-9e37-e5f586a5d4a2 |
    | stack_name          | fiap-stack                           |
    | description         | Servidor para FIAP CLD!              |
    | creation_time       | 2020-10-28T19:23:53Z                 |
    | updated_time        | None                                 |
    | stack_status        | CREATE_IN_PROGRESS                   |
    | stack_status_reason | Stack CREATE started                 |
    +---------------------+--------------------------------------+
    ```

11. Listar ate que fique em estado de `CREATE_COMPLATE` (pode demorar uns minutos):
    ```
    $ watch openstack stack list
    +--------------------------------------+------------+----------------------------------+-----------------+----------------------+--------------+
    | ID                                   | Stack Name | Project                          | Stack Status    | Creation Time        | Updated Time |
    +--------------------------------------+------------+----------------------------------+-----------------+----------------------+--------------+
    | 9fe42824-9f12-4925-9e37-e5f586a5d4a2 | fiap-stack | faac34f01fb2464295bcea501b18b741 | CREATE_COMPLETE | 2020-10-28T19:23:53Z | None         |
    +--------------------------------------+------------+----------------------------------+-----------------+----------------------+--------------+
    ```
 
12. Mostrar o *stack*, e conferir o *output*:
    ```
    $ openstack stack show fiap-stack
    +-----------------------+-----------------------------------------------------------------------------------------------------------------------------------+
    | Field                 | Value                                                                                                                             |
    +-----------------------+-----------------------------------------------------------------------------------------------------------------------------------+
    | id                    | 9fe42824-9f12-4925-9e37-e5f586a5d4a2                                                                                              |
    | stack_name            | fiap-stack                                                                                                                        |
    | description           | Servidor para FIAP CLD!                                                                                                           |
    | creation_time         | 2020-10-28T19:23:53Z                                                                                                              |
    | updated_time          | None                                                                                                                              |
    | stack_status          | CREATE_COMPLETE                                                                                                                   |
    | stack_status_reason   | Stack CREATE completed successfully                                                                                               |
    | parameters            | OS::project_id: faac34f01fb2464295bcea501b18b741                                                                                  |
    |                       | OS::stack_id: 9fe42824-9f12-4925-9e37-e5f586a5d4a2                                                                                |
    |                       | OS::stack_name: fiap-stack                                                                                                        |
    |                       | flavor: m.fiap                                                                                                                    |
    |                       | image: cirros-0.3.5-x86_64-disk                                                                                                   |
    |                       | private_ip: 10.0.1.200                                                                                                            |
    |                       | private_network: private                                                                                                          |
    |                       | public_network: public                                                                                                            |
    |                       |                                                                                                                                   |
    | outputs               | - description: Esta URL e a URL "externa" que pode ser usada para acessar o servidor                                              |
    |                       |     WEB.                                                                                                                          |
    |                       |   output_key: lburl                                                                                                               |
    |                       |   output_value: http://172.24.4.12                                                                                                |
    |                       |                                                                                                                                   |
    | links                 | - href: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741/stacks/fiap-stack/9fe42824-9f12-4925-9e37-e5f586a5d4a2 |
    |                       |   rel: self                                                                                                                       |
    |                       |                                                                                                                                   |
    | parent                | None                                                                                                                              |
    | disable_rollback      | True                                                                                                                              |
    | deletion_time         | None                                                                                                                              |
    | stack_user_project_id | ea246251b2af4463b0a1deffb3103121                                                                                                  |
    | capabilities          | []                                                                                                                                |
    | notification_topics   | []                                                                                                                                |
    | stack_owner           | None                                                                                                                              |
    | timeout_mins          | None                                                                                                                              |
    | tags                  | None                                                                                                                              |
    +-----------------------+-----------------------------------------------------------------------------------------------------------------------------------+
    ```

13. Listar os recursos que foram criados:
    ```
    $ openstack stack resource list fiap-stack
    +---------------+--------------------------------------+----------------------------+-----------------+----------------------+
    | resource_name | physical_resource_id                 | resource_type              | resource_status | updated_time         |
    +---------------+--------------------------------------+----------------------------+-----------------+----------------------+
    | sec_group     | 7e49b68f-6b9a-4b58-87df-134bd0e63fa8 | OS::Neutron::SecurityGroup | CREATE_COMPLETE | 2020-10-28T19:23:53Z |
    | floating_ip   | 31504dac-e833-4873-b40b-342435ef9524 | OS::Neutron::FloatingIP    | CREATE_COMPLETE | 2020-10-28T19:23:53Z |
    | server_port   | 89b5e872-67ee-48bc-b81a-60145a771a3f | OS::Neutron::Port          | CREATE_COMPLETE | 2020-10-28T19:23:53Z |
    | server        | f62370b4-7cb4-480e-9c13-8a23f7617fa4 | OS::Nova::Server           | CREATE_COMPLETE | 2020-10-28T19:23:53Z |
    +---------------+--------------------------------------+----------------------------+-----------------+----------------------+
    ```

14. Mostrar a VM, conferir que tem um *floating* IP assignado:
    ```
    $ openstack server list
    +--------------------------------------+--------------------------------+--------+---------------------------------------------------------------------+--------------------------+--------+
    | ID                                   | Name                           | Status | Networks                                                            | Image                    | Flavor |
    +--------------------------------------+--------------------------------+--------+---------------------------------------------------------------------+--------------------------+--------+
    | f62370b4-7cb4-480e-9c13-8a23f7617fa4 | fiap-stack-server-mr66iinpjgzg | ACTIVE | private=fdb5:7432:9bc4:0:f816:3eff:fe12:74d7, 10.0.0.4, 172.24.4.12 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+--------------------------------+--------+---------------------------------------------------------------------+--------------------------+--------+
    ```

## *Clean-up*

15. Deletar o *stack* e conferir que a VM foi deletada:
    ```
    $ openstack stack delete fiap-stack
    Are you sure you want to delete this stack(s) [y/N]? y
    
    $ openstack server list
    ```

## Horizon

16. Refazer o mesmo processo via Horizon Dashboard:
    - Criação de *stack*
    - Update de *stack*
    - Remoção de *stack*
 
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/heat1.png)
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/heat2.png)

    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/heat3.png)

    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/heat4.png)

    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/heat5.png)

