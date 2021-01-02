# Lab 1 - OpenStack Keystone

## Identity Manager
Usaremos o serviço [Keystone](https://docs.openstack.org/keystone/latest/) para aprender alguns conceitos importantes de autenticação/autorização de usuários:
 - projetos
 - róis
 - quotas
 - *endpoints*

## Pre-reqs

1.	Listar os serviços Linux que compõem o Keystone:
    ```
    $ systemctl | grep devstack@keystone
    devstack@keystone.service                                                                        loaded active running   Devstack devstack@keystone.service
    ```

2.	Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@keystone
    ● devstack@keystone.service - Devstack devstack@keystone.service
       Loaded: loaded (/etc/systemd/system/devstack@keystone.service; enabled; vendor preset: enabled)
       Active: active (running) since Thu 2020-10-01 10:39:31 PDT; 3 days ago
     Main PID: 862 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@keystone.service
               ├─862 keystoneuWSGI maste
               ├─912 keystoneuWSGI worker
               └─914 keystoneuWSGI worker
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: DEBUG keystone.policy.backends.rules [None req-0b250eac-2081-49bb-bf22-f6fc6b232040 None
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: DEBUG keystone.common.authorization [None req-0b250eac-2081-49bb-bf22-f6fc6b232040 None N
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: [pid: 914|app: 0|req: 131/264] 192.168.17.131 () {62 vars in 1401 bytes} [Sun Oct  4 17:1
    ```

3.	Mostrar os logs do serviço:
    ```
    $ journalctl -f -u devstack@keystone
    -- Logs begin at Thu 2020-10-01 10:39:16 PDT. --
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: DEBUG keystone.policy.backends.rules [None req-0b250eac-2081-49bb-bf22-f6fc6b232040 None None] enforce identity:validate_token: {'is_delegated_auth': False, 'access_token_id': None, 'user_id': u'e17c0f6e879c4b8d860ec69a2b28ffff', 'roles': [u'ResellerAdmin', u'admin', u'service'], 'user_domain_id': u'default', 'consumer_id': None, 'trustee_id': None, 'is_domain': False, 'is_admin_project': True, 'trustor_id': None, 'token': <KeystoneToken (audit_id=-ZpKXigzR7eMBVPnVEKdFg, audit_chain_id=-ZpKXigzR7eMBVPnVEKdFg) at 0x7f686de864f0>, 'project_id': u'83667b3fd3964824ae4276cfc8610829', 'trust_id': None, 'project_domain_id': u'default'} {{(pid=914) enforce /opt/stack/keystone/keystone/policy/backends/rules.py:33}}
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: DEBUG keystone.common.authorization [None req-0b250eac-2081-49bb-bf22-f6fc6b232040 None None] RBAC: Authorization granted {{(pid=914) check_policy /opt/stack/keystone/keystone/common/authorization.py:240}}
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: [pid: 914|app: 0|req: 131/264] 192.168.17.131 () {62 vars in 1401 bytes} [Sun Oct  4 17:16:40 2020] GET /identity/v3/auth/tokens => generated 4921 bytes in 22 msecs (HTTP/1.1 200) 6 headers in 380 bytes (1 switches on core 0)
    Oct 04 17:16:40 ubuntu devstack@keystone.service[862]: DEBUG keystone.middleware.auth [None req-08e49b2e-1253-43d8-8810-83c8d135eab4 None None] Authenticating user token {{(pid=912) process_request /usr/local/lib/python2.7/dist-packages/keystonemiddleware/auth_token/__init__.py:400}}
    ...
    ```

4.	Mostrar o arquivo de configuração:
    ```
    $ less /etc/keystone/keystone.conf
    ```

5.	Carregar as credenciais de administrador e conferir que foram aplicadas no ambiente:
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

6.	Listar os módulos instalados no OpenStack:
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

7.	Mostrar todas as opções para um comando determinado do OpenStack (neste caso `openstack service`, mas serve para qualquer um):
    ```
    $ openstack service --help
    Command "service" matches:
      service create
      service delete
      service list
      service provider create
      service provider delete
      service provider list
      service provider set
      service provider show
      service set
      service show
    ```

## *Endpoints*

8.	Mostrar a saída de um comando de OpenStack em formato estendido (neste caso `openstack service list`, mas tem muitos outros que também aceitam):
    ```
    $ openstack service list --long
    +----------------------------------+-------------+----------------+-----------------------------------+---------+
    | ID                               | Name        | Type           | Description                       | Enabled |
    +----------------------------------+-------------+----------------+-----------------------------------+---------+
    | 14776d964367470ea97ae0f0395be6de | neutron     | network        | Neutron Service                   | True    |
    | 23dbf9f8ad1345e5b4d6d781c4b88e03 | cinder      | volume         | Cinder Volume Service             | True    |
    | 40301ed3d9744979a9e481d025cd3c9c | nova_legacy | compute_legacy | Nova Compute Service (Legacy 2.0) | True    |
    | 46a7ce69fbfd4e7691839b9442eafbbd | placement   | placement      | Placement Service                 | True    |
    | 5d8f64102deb4f62a559776a899cb63e | keystone    | identity       |                                   | True    |
    | 5f271b049588412d8e0a11b2fea5469c | nova        | compute        | Nova Compute Service              | True    |
    | 7cd558d4608647569509bf34b794c59f | cinderv2    | volumev2       | Cinder Volume Service V2          | True    |
    | 7e447465286a4560a661a31297e9d45d | cinderv3    | volumev3       | Cinder Volume Service V3          | True    |
    | 82020edd889243c2ac97416014376f12 | swift       | object-store   | Swift Service                     | True    |
    | cba857956e2349a5b4f6e1161862f340 | heat-cfn    | cloudformation | Heat CloudFormation Service       | True    |
    | d8b098869281469bbd90bbc762aad340 | glance      | image          | Glance Image Service              | True    |
    | fc980824a1954be882a9af7591f78e99 | heat        | orchestration  | Heat Orchestration Service        | True    |
    +----------------------------------+-------------+----------------+-----------------------------------+---------+
    ```

9.	Mostrar os *endpoints* dos módulos instalados no OpenStack:
    ```
    $ openstack catalog list
    +-------------+----------------+--------------------------------------------------------------------------------+
    | Name        | Type           | Endpoints                                                                      |
    +-------------+----------------+--------------------------------------------------------------------------------+
    | neutron     | network        | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131:9696/                                          |
    |             |                |                                                                                |
    | cinder      | volume         | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/volume/v1/faac34f01fb2464295bcea501b18b741     |
    |             |                |                                                                                |
    | nova_legacy | compute_legacy | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/compute/v2/faac34f01fb2464295bcea501b18b741    |
    |             |                |                                                                                |
    | placement   | placement      | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/placement                                      |
    |             |                |                                                                                |
    | keystone    | identity       | RegionOne                                                                      |
    |             |                |   admin: http://192.168.17.131/identity                                        |
    |             |                | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/identity                                       |
    |             |                |                                                                                |
    | nova        | compute        | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/compute/v2.1                                   |
    |             |                |                                                                                |
    | cinderv2    | volumev2       | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/volume/v2/faac34f01fb2464295bcea501b18b741     |
    |             |                |                                                                                |
    | cinderv3    | volumev3       | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/volume/v3/faac34f01fb2464295bcea501b18b741     |
    |             |                |                                                                                |
    | swift       | object-store   | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131:8080/v1/AUTH_faac34f01fb2464295bcea501b18b741  |
    |             |                | RegionOne                                                                      |
    |             |                |   admin: http://192.168.17.131:8080                                            |
    |             |                |                                                                                |
    | heat-cfn    | cloudformation | RegionOne                                                                      |
    |             |                |   internal: http://192.168.17.131/heat-api-cfn/v1                              |
    |             |                | RegionOne                                                                      |
    |             |                |   admin: http://192.168.17.131/heat-api-cfn/v1                                 |
    |             |                | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/heat-api-cfn/v1                                |
    |             |                |                                                                                |
    | glance      | image          | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/image                                          |
    |             |                |                                                                                |
    | heat        | orchestration  | RegionOne                                                                      |
    |             |                |   internal: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741 |
    |             |                | RegionOne                                                                      |
    |             |                |   admin: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741    |
    |             |                | RegionOne                                                                      |
    |             |                |   public: http://192.168.17.131/heat-api/v1/faac34f01fb2464295bcea501b18b741   |
    |             |                |                                                                                |
    +-------------+----------------+--------------------------------------------------------------------------------+
    ```

10.	Mostrar informação sobre um *endpoint* específico:
    ```
    $ openstack catalog show identity
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

## Projetos, usuários e róis

11.	Listar os projetos:
    ```
    $ openstack project list
    +----------------------------------+--------------------+
    | ID                               | Name               |
    +----------------------------------+--------------------+
    | 2c8b4728bc734b8494cf6063a1e6a9b9 | swiftprojecttest4  |
    | 3a44ca9b3f1443ec991fc5f3c04e6550 | admin              |
    | 429e48adaf69425a8f70a05428c7186d | invisible_to_admin |
    | 7997a29ad48643049a0e6940b84ca332 | swiftprojecttest1  |
    | 83667b3fd3964824ae4276cfc8610829 | service            |
    | bf12878841014a839254265091e5abe6 | alt_demo           |
    | f99e64fd5bbb4c8099e97e441f5dddf6 | swiftprojecttest2  |
    | faac34f01fb2464295bcea501b18b741 | demo               |
    +----------------------------------+--------------------+
    ```

12.	Criar um projeto e conferir que foi criado:
    ```
    $ openstack project create --description "Projeto FIAP" fiap
    +-------------+----------------------------------+
    | Field       | Value                            |
    +-------------+----------------------------------+
    | description | Projeto FIAP                     |
    | domain_id   | default                          |
    | enabled     | True                             |
    | id          | 0cdadaa5b492416f88a5379e110392cb |
    | is_domain   | False                            |
    | name        | fiap                             |
    | parent_id   | default                          |
    +-------------+----------------------------------+
    
    $ openstack project list
    +----------------------------------+--------------------+
    | ID                               | Name               |
    +----------------------------------+--------------------+
    | 2c8b4728bc734b8494cf6063a1e6a9b9 | swiftprojecttest4  |
    | 3a44ca9b3f1443ec991fc5f3c04e6550 | admin              |
    | 429e48adaf69425a8f70a05428c7186d | invisible_to_admin |
    | 51d9f2de8a974698a0f8f42dd1e395c2 | fiap               |
    | 7997a29ad48643049a0e6940b84ca332 | swiftprojecttest1  |
    | 83667b3fd3964824ae4276cfc8610829 | service            |
    | bf12878841014a839254265091e5abe6 | alt_demo           |
    | f99e64fd5bbb4c8099e97e441f5dddf6 | swiftprojecttest2  |
    | faac34f01fb2464295bcea501b18b741 | demo               |
    +----------------------------------+--------------------+
    
    $ openstack project list  | grep fiap
    | 51d9f2de8a974698a0f8f42dd1e395c2 | fiap               |
    ```

13.	Mostrar o projeto recém criado:
    ```
    $ openstack project show fiap
    +-------------+----------------------------------+
    | Field       | Value                            |
    +-------------+----------------------------------+
    | description | Projeto FIAP                     |
    | domain_id   | default                          |
    | enabled     | True                             |
    | id          | 51d9f2de8a974698a0f8f42dd1e395c2 |
    | is_domain   | False                            |
    | name        | fiap                             |
    | parent_id   | default                          |
    +-------------+----------------------------------+
    ```

14.	Criar um usuário:
    ```
    $ openstack user create --password-prompt aluno-fiap
    User Password:
    Repeat User Password:
    +---------------------+----------------------------------+
    | Field               | Value                            |
    +---------------------+----------------------------------+
    | domain_id           | default                          |
    | enabled             | True                             |
    | id                  | 46ca1d0eb6f44c9faeb83c44f2c7e17b |
    | name                | aluno-fiap                       |
    | options             | {}                               |
    | password_expires_at | None                             |
    +---------------------+----------------------------------+
    ```

15.	Listar os róis:
    ```
    $ openstack role list
    +----------------------------------+-----------------+
    | ID                               | Name            |
    +----------------------------------+-----------------+
    | 146e0c64a8934561a6431ed3d8eae8e4 | Member          |
    | 178aa4dd963546c5b3817dfa83be4f5e | anotherrole     |
    | 5e66e0a80e7a4b7fb0a363c16e8ad9f7 | heat_stack_user |
    | 783926282c054f1aa0e16794754cebfa | service         |
    | 9fe2ff9ee4384b1894a90878d3e92bab | _member_        |
    | e2c1c5a8dde74ec99e7bfa8186c48c6a | admin           |
    | ec2d6b8e586b4e149c4527dc104ffbf6 | ResellerAdmin   |
    +----------------------------------+-----------------+
    ```

16.	Associar o usuário com o projeto como membro:
    ```
    $ openstack role add --project fiap --user aluno-fiap Member
    ```

17.	Conferir a designação do rol:
    ```
    $ openstack role assignment list --project fiap
    +----------------------------------+----------------------------------+-------+----------------------------------+--------+-----------+
    | Role                             | User                             | Group | Project                          | Domain | Inherited |
    +----------------------------------+----------------------------------+-------+----------------------------------+--------+-----------+
    | 146e0c64a8934561a6431ed3d8eae8e4 | 46ca1d0eb6f44c9faeb83c44f2c7e17b |       | 51d9f2de8a974698a0f8f42dd1e395c2 |        | False     |
    +----------------------------------+----------------------------------+-------+----------------------------------+--------+-----------+
    ```

## Quotas

18.	Mostrar as quotas do projeto:
    ```
    $ openstack quota show fiap
    +-----------------------+----------------------------------+
    | Field                 | Value                            |
    +-----------------------+----------------------------------+
    | backup-gigabytes      | 1000                             |
    | backups               | 10                               |
    | cores                 | 20                               |
    | fixed-ips             | -1                               |
    | floating-ips          | 50                               |
    | gigabytes             | 1000                             |
    | gigabytes_lvmdriver-1 | -1                               |
    | groups                | 10                               |
    | health_monitors       | None                             |
    | injected-file-size    | 10240                            |
    | injected-files        | 5                                |
    | injected-path-size    | 255                              |
    | instances             | 10                               |
    | key-pairs             | 100                              |
    | l7_policies           | None                             |
    | listeners             | None                             |
    | load_balancers        | None                             |
    | location              | None                             |
    | name                  | None                             |
    | networks              | 100                              |
    | per-volume-gigabytes  | -1                               |
    | pools                 | None                             |
    | ports                 | 500                              |
    | project               | 51d9f2de8a974698a0f8f42dd1e395c2 |
    | project_name          | fiap                             |
    | properties            | 128                              |
    | ram                   | 51200                            |
    | rbac_policies         | 10                               |
    | routers               | 10                               |
    | secgroup-rules        | 100                              |
    | secgroups             | 10                               |
    | server-group-members  | 10                               |
    | server-groups         | 10                               |
    | snapshots             | 10                               |
    | snapshots_lvmdriver-1 | -1                               |
    | subnet_pools          | -1                               |
    | subnets               | 100                              |
    | volumes               | 10                               |
    | volumes_lvmdriver-1   | -1                               |
    +-----------------------+----------------------------------+
    ```

19.	Atualizar a quota de *cores* e comprovar que foi aplicada:
    ```
    $ openstack quota set --cores 30 fiap
    
    $ openstack quota show fiap
    +-----------------------+----------------------------------+
    | Field                 | Value                            |
    +-----------------------+----------------------------------+
    | backup-gigabytes      | 1000                             |
    | backups               | 10                               |
    | cores                 | 30                               |
    | fixed-ips             | -1                               |
    | floating-ips          | 50                               |
    | gigabytes             | 1000                             |
    | gigabytes_lvmdriver-1 | -1                               |
    | groups                | 10                               |
    | health_monitors       | None                             |
    | injected-file-size    | 10240                            |
    | injected-files        | 5                                |
    | injected-path-size    | 255                              |
    | instances             | 10                               |
    | key-pairs             | 100                              |
    | l7_policies           | None                             |
    | listeners             | None                             |
    | load_balancers        | None                             |
    | location              | None                             |
    | name                  | None                             |
    | networks              | 100                              |
    | per-volume-gigabytes  | -1                               |
    | pools                 | None                             |
    | ports                 | 500                              |
    | project               | 51d9f2de8a974698a0f8f42dd1e395c2 |
    | project_name          | fiap                             |
    | properties            | 128                              |
    | ram                   | 51200                            |
    | rbac_policies         | 10                               |
    | routers               | 10                               |
    | secgroup-rules        | 100                              |
    | secgroups             | 10                               |
    | server-group-members  | 10                               |
    | server-groups         | 10                               |
    | snapshots             | 10                               |
    | snapshots_lvmdriver-1 | -1                               |
    | subnet_pools          | -1                               |
    | subnets               | 100                              |
    | volumes               | 10                               |
    | volumes_lvmdriver-1   | -1                               |
    +-----------------------+----------------------------------+
    
    $ openstack quota show fiap | grep cores
    | cores                 | 30                               |
    ```

## *Clean-up*

20.	Deletar o projeto e usuário:
    ```
    $ openstack user delete aluno-fiap
    
    $ openstack project delete fiap
    ```

## Horizon

21. Refazer o mesmo processo via Horizon Dashboard:
    - Listar *endpoints*
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/keystone1.png)
    - Listar projetos
    - Criar projeto
    - Criar usuário
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/keystone2.png)
    - Associar usuário com projeto
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/keystone3.png)
    - Mostrar quotas de um projeto
    - Mudar quotas de um projeto
