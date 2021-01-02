# Lab 5 - OpenStack Cinder

## Block Storage Service

Usaremos o serviço [Cinder](https://docs.openstack.org/cinder/latest/) para aprender alguns conceitos importantes sobre armazenamento de bloco:
 - criação de volumes
 - *snapshots*
 - partição, formatação e montagem de volumes
 
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

2. Conferir que o Cinder foi instalado no OpenStack:
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
    $ openstack catalog show cinder
    +-----------+----------------------------------------------------------------------------+
    | Field     | Value                                                                      |
    +-----------+----------------------------------------------------------------------------+
    | endpoints | RegionOne                                                                  |
    |           |   public: http://192.168.17.131/volume/v1/faac34f01fb2464295bcea501b18b741 |
    |           |                                                                            |
    | id        | 23dbf9f8ad1345e5b4d6d781c4b88e03                                           |
    | name      | cinder                                                                     |
    | type      | volume                                                                     |
    +-----------+----------------------------------------------------------------------------+
    ```

4. Mostrar informação sobre os serviços OpenStack que compõem o Cinder:
    ```
    $ openstack volume service list
    +------------------+--------------------+------+---------+-------+----------------------------+
    | Binary           | Host               | Zone | Status  | State | Updated At                 |
    +------------------+--------------------+------+---------+-------+----------------------------+
    | cinder-scheduler | ubuntu             | nova | enabled | up    | 2020-10-26T23:22:11.000000 |
    | cinder-volume    | ubuntu@lvmdriver-1 | nova | enabled | up    | 2020-10-26T23:22:07.000000 |
    +------------------+--------------------+------+---------+-------+----------------------------+
    ```

5. Listar os serviços Linux que compõem o Cinder:
    ```
    $ systemctl | grep devstack@c
    devstack@c-api.service                                             loaded active running   Devstack devstack@c-api.service
    devstack@c-sch.service                                             loaded active running   Devstack devstack@c-sch.service
    devstack@c-vol.service                                             loaded active running   Devstack devstack@c-vol.service    
    ```
     
6. Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@c*
    ● devstack@c-api.service - Devstack devstack@c-api.service
       Loaded: loaded (/etc/systemd/system/devstack@c-api.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 26min ago
     Main PID: 805 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@c-api.service
               ├─805 cinder-apiuWSGI maste
               ├─888 cinder-apiuWSGI worker
               └─891 cinder-apiuWSGI worker

    ● devstack@c-sch.service - Devstack devstack@c-sch.service
       Loaded: loaded (/etc/systemd/system/devstack@c-sch.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 15:56:06 PDT; 26min ago
     Main PID: 784 (cinder-schedule)
       CGroup: /system.slice/system-devstack.slice/devstack@c-sch.service
               └─784 /usr/bin/python /usr/local/bin/cinder-scheduler --config-file /etc/cinder/cinder.conf

    ● devstack@c-vol.service - Devstack devstack@c-vol.service
       Loaded: loaded (/etc/systemd/system/devstack@c-vol.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-26 16:01:43 PDT; 21min ago
     Main PID: 3901 (cinder-volume)
       CGroup: /system.slice/system-devstack.slice/devstack@c-vol.service
               ├─3901 /usr/bin/python /usr/local/bin/cinder-volume --config-file /etc/cinder/cinder.conf
               └─3912 /usr/bin/python /usr/local/bin/cinder-volume --config-file /etc/cinder/cinder.conf
    ```

7. Mostrar os logs do serviço:
    ```
    $ journalctl -u devstack@c-vol
    ```

8. Mostrar os arquivos de configuração:
    ```
    $ less /etc/cinder/cinder.conf
    $ less /etc/cinder/policy.json
    ```

## Volumes

9. Criar um volume vazio de 1 GB:
    ```
    $ openstack volume create teste-fiap --size 1
    +---------------------+--------------------------------------+
    | Field               | Value                                |
    +---------------------+--------------------------------------+
    | attachments         | []                                   |
    | availability_zone   | nova                                 |
    | bootable            | false                                |
    | consistencygroup_id | None                                 |
    | created_at          | 2020-10-26T23:00:56.770211           |
    | description         | None                                 |
    | encrypted           | False                                |
    | id                  | cab7679c-d7dd-4826-93ec-f4b78b434467 |
    | migration_status    | None                                 |
    | multiattach         | False                                |
    | name                | teste-fiap                           |
    | properties          |                                      |
    | replication_status  | None                                 |
    | size                | 1                                    |
    | snapshot_id         | None                                 |
    | source_volid        | None                                 |
    | status              | creating                             |
    | type                | lvmdriver-1                          |
    | updated_at          | None                                 |
    | user_id             | fe2d2a5507ed4ad2919258d7252cebc6     |
    +---------------------+--------------------------------------+
    ```
 
10. Listar os volumes e mostrar o volume recém criado:
    ```
    $ openstack volume list
    +--------------------------------------+------------+-----------+------+-------------+
    | ID                                   | Name       | Status    | Size | Attached to |
    +--------------------------------------+------------+-----------+------+-------------+
    | abccb06a-5e9e-422b-a2c0-9f6768071785 | teste-fiap | available |    1 |             |
    | cab7679c-d7dd-4826-93ec-f4b78b434467 | teste-fiap | error     |    1 |             |
    +--------------------------------------+------------+-----------+------+-------------+
    ```

11. Criar uma VM:
    ```
    $ openstack server create --network private --flavor m.fiap --image cirros-0.3.5-x86_64-disk vm
    +-------------------------------------+-----------------------------------------------------------------+
    | Field                               | Value                                                           |
    +-------------------------------------+-----------------------------------------------------------------+
    | OS-DCF:diskConfig                   | MANUAL                                                          |
    | OS-EXT-AZ:availability_zone         |                                                                 |
    | OS-EXT-SRV-ATTR:host                | None                                                            |
    | OS-EXT-SRV-ATTR:hypervisor_hostname | None                                                            |
    | OS-EXT-SRV-ATTR:instance_name       |                                                                 |
    | OS-EXT-STS:power_state              | NOSTATE                                                         |
    | OS-EXT-STS:task_state               | scheduling                                                      |
    | OS-EXT-STS:vm_state                 | building                                                        |
    | OS-SRV-USG:launched_at              | None                                                            |
    | OS-SRV-USG:terminated_at            | None                                                            |
    | accessIPv4                          |                                                                 |
    | accessIPv6                          |                                                                 |
    | addresses                           |                                                                 |
    | adminPass                           | NZX7c7qpXoif                                                    |
    | config_drive                        |                                                                 |
    | created                             | 2020-10-26T23:28:07Z                                            |
    | flavor                              | m.fiap (a0683bcb-b937-4c75-be19-7641eceeff78)                   |
    | hostId                              |                                                                 |
    | id                                  | 8d2d0c89-1ae6-4ad2-9299-171c14a29567                            |
    | image                               | cirros-0.3.5-x86_64-disk (cd992dd3-2197-49fe-9f0e-43d783d18a5c) |
    | key_name                            | None                                                            |
    | name                                | vm                                                              |
    | progress                            | 0                                                               |
    | project_id                          | faac34f01fb2464295bcea501b18b741                                |
    | properties                          |                                                                 |
    | security_groups                     | name='default'                                                  |
    | status                              | BUILD                                                           |
    | updated                             | 2020-10-26T23:28:06Z                                            |
    | user_id                             | fe2d2a5507ed4ad2919258d7252cebc6                                |
    | volumes_attached                    |                                                                 |
    +-------------------------------------+-----------------------------------------------------------------+
    ```

12. Anexar o volume a uma instancia previamente criada (neste caso `vm`):
    ```
    $ openstack server add volume vm teste-fiap
    ```

13. Conferir que o volume foi anexado:
    ```
    $ openstack server show vm
    +-------------------------------------+-----------------------------------------------------------------+
    | Field                               | Value                                                           |
    +-------------------------------------+-----------------------------------------------------------------+
    | OS-DCF:diskConfig                   | MANUAL                                                          |
    | OS-EXT-AZ:availability_zone         | nova                                                            |
    | OS-EXT-SRV-ATTR:host                | ubuntu                                                          |
    | OS-EXT-SRV-ATTR:hypervisor_hostname | ubuntu                                                          |
    | OS-EXT-SRV-ATTR:instance_name       | instance-0000000a                                               |
    | OS-EXT-STS:power_state              | Running                                                         |
    | OS-EXT-STS:task_state               | None                                                            |
    | OS-EXT-STS:vm_state                 | active                                                          |
    | OS-SRV-USG:launched_at              | 2020-10-26T23:28:12.000000                                      |
    | OS-SRV-USG:terminated_at            | None                                                            |
    | accessIPv4                          |                                                                 |
    | accessIPv6                          |                                                                 |
    | addresses                           | private=fdb5:7432:9bc4:0:f816:3eff:fe7c:12a1, 10.0.0.8          |
    | config_drive                        |                                                                 |
    | created                             | 2020-10-26T23:28:06Z                                            |
    | flavor                              | m.fiap (a0683bcb-b937-4c75-be19-7641eceeff78)                   |
    | hostId                              | 4621bfea4420d850fc73fcc3691454ce013120c75edf0c1bd98473eb        |
    | id                                  | 8d2d0c89-1ae6-4ad2-9299-171c14a29567                            |
    | image                               | cirros-0.3.5-x86_64-disk (cd992dd3-2197-49fe-9f0e-43d783d18a5c) |
    | key_name                            | None                                                            |
    | name                                | vm                                                              |
    | progress                            | 0                                                               |
    | project_id                          | faac34f01fb2464295bcea501b18b741                                |
    | properties                          |                                                                 |
    | security_groups                     | name='default'                                                  |
    | status                              | ACTIVE                                                          |
    | updated                             | 2020-10-26T23:29:07Z                                            |
    | user_id                             | fe2d2a5507ed4ad2919258d7252cebc6                                |
    | volumes_attached                    | id='abccb06a-5e9e-422b-a2c0-9f6768071785'                       |
    +-------------------------------------+-----------------------------------------------------------------+
    
    $ openstack server show vm | grep volumes
    | volumes_attached                    | id='abccb06a-5e9e-422b-a2c0-9f6768071785'                       |
    ```

14. Acessar à vm:
    ```
    $ virsh list
     Id    Name                           State
    ----------------------------------------------------
     2     instance-0000000a              running

    $ virsh console 2
    Connected to domain instance-0000000a
    Escape character is ^]

    login as 'cirros' user. default password: 'cubswin:)'. use 'sudo' for root.
    vm login: cirros
    Password:
    $
    ```

### Dentro da VM:

15. Conferir que o novo volume foi entregue a VM:
    ```
    $ ls /dev/vd*
    /dev/vda   /dev/vda1  /dev/vdb
    ```

16. Criar uma partição no novo disco:
    ```
    $ sudo fdisk /dev/vdb
    Device contains neither a valid DOS partition table, nor Sun, SGI or OSF disklabel
    Building a new DOS disklabel with disk identifier 0xaba72f7d.
    Changes will remain in memory only, until you decide to write them.
    After that, of course, the previous content won't be recoverable.

    Warning: invalid flag 0x0000 of partition table 4 will be corrected by w(rite)

    Command (m for help): m
    Command action
       a   toggle a bootable flag
       b   edit bsd disklabel
       c   toggle the dos compatibility flag
       d   delete a partition
       l   list known partition types
       m   print this menu
       n   add a new partition
       o   create a new empty DOS partition table
       p   print the partition table
       q   quit without saving changes
       s   create a new empty Sun disklabel
       t   change a partition's system id
       u   change display/entry units
       v   verify the partition table
       w   write table to disk and exit
       x   extra functionality (experts only)

    Command (m for help): n
    Partition type:
       p   primary (0 primary, 0 extended, 4 free)
       e   extended
    Select (default p): p
    Partition number (1-4, default 1): 1
    First sector (2048-2097151, default 2048):
    Using default value 2048
    Last sector, +sectors or +size{K,M,G} (2048-2097151, default 2097151):
    Using default value 2097151

    Command (m for help): w
    The partition table has been altered!

    Calling ioctl() to re-read partition table.
    Syncing disks.
    ```

17. Conferir que a partição foi criada:
    ```
    $ ls /dev/vd*
    /dev/vda   /dev/vda1  /dev/vdb   /dev/vdb1
    ```

18. Formatar a partição:
    ```
    $ sudo mkfs.ext4 /dev/vdb1
    mke2fs 1.42.2 (27-Mar-2012)
    Filesystem label=
    OS type: Linux
    Block size=4096 (log=2)
    Fragment size=4096 (log=2)
    Stride=0 blocks, Stripe width=0 blocks
    65536 inodes, 261888 blocks
    13094 blocks (5.00%) reserved for the super user
    First data block=0
    Maximum filesystem blocks=268435456
    8 block groups
    32768 blocks per group, 32768 fragments per group
    8192 inodes per group
    Superblock backups stored on blocks:
            32768, 98304, 163840, 229376

    Allocating group tables: done
    Writing inode tables: done
    Creating journal (4096 blocks): done
    Writing superblocks and filesystem accounting information: done
    ```
  
19. Criar uma nova pasta para montar o volume:
    ```
    $ sudo mkdir /mnt/nuevo-vol
    ```

20. Montar o novo volume (pode ignorar o erro):
    ```
    $ sudo mount /dev/vdb1 /mnt/nuevo-vol
    [  201.700545] EXT3-fs (vdb1): error: couldn't mount because of unsupported optional features (240)
    ```
21. Confirmar que o volume foi montado:
    ```
    $ mount
    rootfs on / type rootfs (rw)
    /dev on /dev type devtmpfs (rw,relatime,size=21792k,nr_inodes=5448,mode=755)
    /dev/vda1 on / type ext3 (rw,relatime,errors=continue,user_xattr,acl,barrier=1,data=ordered)
    /proc on /proc type proc (rw,relatime)
    sysfs on /sys type sysfs (rw,relatime)
    devpts on /dev/pts type devpts (rw,relatime,gid=5,mode=620,ptmxmode=000)
    tmpfs on /dev/shm type tmpfs (rw,relatime,mode=777)
    tmpfs on /run type tmpfs (rw,nosuid,relatime,size=200k,mode=755)
    /dev/vdb1 on /mnt/nuevo-vol type ext4 (rw,relatime,user_xattr,barrier=1,data=ordered)

    $ mount | grep nuevo-vol
    /dev/vdb1 on /mnt/nuevo-vol type ext4 (rw,relatime,user_xattr,barrier=1,data=ordered)
    ```

22. Criar um arquivo no novo volume:
    ```
    $ sudo touch /mnt/nuevo-vol/arquivo.txt
    
    $ ls  /mnt/nuevo-vol/
    /mnt/nuevo-vol/arquivo.txt
    ```

### De volta no DevStack:

23. Criar um *snapshot* do volume:
    ```
    $ openstack volume snapshot create --volume teste-fiap teste-fiap-snap
    Invalid volume: Volume abccb06a-5e9e-422b-a2c0-9f6768071785 status must be available, but current status is: in-use. (HTTP 400) (Request-ID: req-7c30ee3f-20d4-422e-a6d3-2ed44a631de2)
    ```
    
    OpenStack não recomenda fazer *snapshots* de volumes em uso. Para forzar:
    ```
    $ openstack volume snapshot create --volume teste-fiap teste-fiap-snap --force
    +-------------+--------------------------------------+
    | Field       | Value                                |
    +-------------+--------------------------------------+
    | created_at  | 2020-10-26T23:33:56.630662           |
    | description | None                                 |
    | id          | b55e6a7c-3072-4879-8f35-bf68901d0e1a |
    | name        | teste-fiap-snap                      |
    | properties  |                                      |
    | size        | 1                                    |
    | status      | creating                             |
    | updated_at  | None                                 |
    | volume_id   | abccb06a-5e9e-422b-a2c0-9f6768071785 |
    +-------------+--------------------------------------+
    ```

24. Listar e mostrar o *snapshot*:
    ```
    $ openstack volume snapshot list
    +--------------------------------------+-----------------+-------------+-----------+------+
    | ID                                   | Name            | Description | Status    | Size |
    +--------------------------------------+-----------------+-------------+-----------+------+
    | b55e6a7c-3072-4879-8f35-bf68901d0e1a | teste-fiap-snap | None        | available |    1 |
    +--------------------------------------+-----------------+-------------+-----------+------+
    
    $ openstack volume snapshot show teste-fiap-snap
    +--------------------------------------------+--------------------------------------+
    | Field                                      | Value                                |
    +--------------------------------------------+--------------------------------------+
    | created_at                                 | 2020-10-26T23:33:57.000000           |
    | description                                | None                                 |
    | id                                         | b55e6a7c-3072-4879-8f35-bf68901d0e1a |
    | name                                       | teste-fiap-snap                      |
    | os-extended-snapshot-attributes:progress   | 100%                                 |
    | os-extended-snapshot-attributes:project_id | faac34f01fb2464295bcea501b18b741     |
    | properties                                 |                                      |
    | size                                       | 1                                    |
    | status                                     | available                            |
    | updated_at                                 | 2020-10-26T23:33:57.000000           |
    | volume_id                                  | abccb06a-5e9e-422b-a2c0-9f6768071785 |
    +--------------------------------------------+--------------------------------------+
    ```

25. Mostrar o volume no LVM:
    ```
    $ sudo lvs
      LV                                             VG                        Attr       LSize Pool Origin                                      Data%  Meta%  Move Log Cpy%Sync Convert
      _snapshot-b55e6a7c-3072-4879-8f35-bf68901d0e1a stack-volumes-lvmdriver-1 swi-a-s--- 1.00g      volume-abccb06a-5e9e-422b-a2c0-9f6768071785 0.00
      volume-abccb06a-5e9e-422b-a2c0-9f6768071785    stack-volumes-lvmdriver-1 owi-aos--- 1.00g                               
      volume-dba8b01e-f259-468e-b370-c94732ea00be    stack-volumes-lvmdriver-1 -wi-a----- 1.00g
   
    $ sudo vgs
      VG                        #PV #LV #SN Attr   VSize  VFree
      stack-volumes-default       1   0   0 wz--n- 10.01g 10.01g
      stack-volumes-lvmdriver-1   1   3   1 wz--n- 10.01g  7.01g
      
    $ sudo  pvs
      PV         VG                        Fmt  Attr PSize  PFree
      /dev/loop1 stack-volumes-default     lvm2 a--  10.01g 10.01g
      /dev/loop2 stack-volumes-lvmdriver-1 lvm2 a--  10.01g  7.01g
    ```

26. Desatachar o volume da VM e confirmar que volta para o estado `available`:
    ```
    $ openstack server remove volume vm teste-fiap
    
    $ openstack volume list
    +--------------------------------------+------------+-----------+------+-------------+
    | ID                                   | Name       | Status    | Size | Attached to |
    +--------------------------------------+------------+-----------+------+-------------+
    | abccb06a-5e9e-422b-a2c0-9f6768071785 | teste-fiap | available |    1 |             |
    +--------------------------------------+------------+-----------+------+-------------+
    ```

27. Anexar o volume a outra VM, monta-lo e conferir que o arquivo criado continua presente

28. Desanexar e deletar volumes e *snapshots*:
    ```
    $ openstack volume snapshot delete teste-fiap-snap
    
    $ openstack volume delete teste-fiap
    ```

## Horizon

29. Refazer o mesmo processo via Horizon Dashboard:
    - Criação de volume vazio
    - Criação de volume a partir de imagem
    - *Snapshot* de volume
    - Atachar/desatachar volumes a VMs
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/cinder1.png)
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/cinder2.png)
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/cinder3.png)
    
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/openstack/img/cinder4.png)
