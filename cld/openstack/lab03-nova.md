# Lab 3 - OpenStack Nova

## Compute Service
Usaremos o serviço Nova para aprender alguns conceitos importantes sobre máquinas virtuais:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**

1. Conferir se as extensões de virtualizações estão presentes no processador:
    ```
    $ grep -E ' svm | vmx ' /proc/cpuinfo
    ```

2.	Listar os serviços Linux que compõem o Nova:
    ```
    $ systemctl | grep devstack@n
    devstack@n-api-meta.service                                                                      loaded active running   Devstack devstack@n-api-meta.service
    devstack@n-api.service                                                                           loaded active running   Devstack devstack@n-api.service
    devstack@n-cauth.service                                                                         loaded active running   Devstack devstack@n-cauth.service
    devstack@n-cond-cell1.service                                                                    loaded active running   Devstack devstack@n-cond-cell1.service
    devstack@n-cpu.service                                                                           loaded active running   Devstack devstack@n-cpu.service
    devstack@n-novnc.service                                                                         loaded active running   Devstack devstack@n-novnc.service
    devstack@n-sch.service                                                                           loaded active running   Devstack devstack@n-sch.service
    devstack@n-super-cond.service                                                                    loaded active running   Devstack devstack@n-super-cond.service
    ```

3.	Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@n*
    ● devstack@n-cauth.service - Devstack devstack@n-cauth.service
       Loaded: loaded (/etc/systemd/system/devstack@n-cauth.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 798 (nova-consoleaut)
       CGroup: /system.slice/system-devstack.slice/devstack@n-cauth.service
               └─798 /usr/bin/python /usr/local/bin/nova-consoleauth --config-file /etc/nova/nova.conf
               
    ● devstack@n-sch.service - Devstack devstack@n-sch.service
       Loaded: loaded (/etc/systemd/system/devstack@n-sch.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 848 (nova-scheduler)
       CGroup: /system.slice/system-devstack.slice/devstack@n-sch.service
               └─848 /usr/bin/python /usr/local/bin/nova-scheduler --config-file /etc/nova/nova.conf
               
    ● devstack@n-novnc.service - Devstack devstack@n-novnc.service
       Loaded: loaded (/etc/systemd/system/devstack@n-novnc.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 861 (nova-novncproxy)
       CGroup: /system.slice/system-devstack.slice/devstack@n-novnc.service
               └─861 /usr/bin/python /usr/local/bin/nova-novncproxy --config-file /etc/nova/nova.conf --web /opt/stack/noV
               
    ● devstack@n-super-cond.service - Devstack devstack@n-super-cond.service
       Loaded: loaded (/etc/systemd/system/devstack@n-super-cond.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 749 (nova-conductor)
       CGroup: /system.slice/system-devstack.slice/devstack@n-super-cond.service
               ├─ 749 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova.conf
               ├─2363 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova.conf
               └─2368 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova.conf
               
    ● devstack@n-cond-cell1.service - Devstack devstack@n-cond-cell1.service
       Loaded: loaded (/etc/systemd/system/devstack@n-cond-cell1.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 752 (nova-conductor)
       CGroup: /system.slice/system-devstack.slice/devstack@n-cond-cell1.service
               ├─ 752 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova_cell1.conf
               ├─2365 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova_cell1.conf
               └─2370 /usr/bin/python /usr/local/bin/nova-conductor --config-file /etc/nova/nova_cell1.conf
               
    ● devstack@n-api-meta.service - Devstack devstack@n-api-meta.service
       Loaded: loaded (/etc/systemd/system/devstack@n-api-meta.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 770 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@n-api-meta.service
               ├─770 nova-api-metauWSGI maste
               ├─877 nova-api-metauWSGI worker
               ├─878 nova-api-metauWSGI worker
               └─879 nova-api-metauWSGI http
    
    ● devstack@n-api.service - Devstack devstack@n-api.service
       Loaded: loaded (/etc/systemd/system/devstack@n-api.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 746 (uwsgi)
       Status: "uWSGI is ready"
       CGroup: /system.slice/system-devstack.slice/devstack@n-api.service
               ├─746 nova-apiuWSGI maste
               ├─839 nova-apiuWSGI worker
               └─841 nova-apiuWSGI worker
               
    ● devstack@n-cpu.service - Devstack devstack@n-cpu.service
       Loaded: loaded (/etc/systemd/system/devstack@n-cpu.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 15min ago
     Main PID: 769 (nova-compute)
       CGroup: /system.slice/system-devstack.slice/devstack@n-cpu.service
               └─769 /usr/bin/python /usr/local/bin/nova-compute --config-file /etc/nova/nova-cpu.conf
    ```

4.	Mostrar os *logs* do serviço:
    ```
    $ journalctl -u devstack@n*
    ```

5. Carregar as credenciais de OpenStack:
    ```
    $ source devstack/openrc admin
    WARNING: setting legacy OS_TENANT_NAME to support cli tools.
    
    $ env | grep OS
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
    LESSCLOSE=/usr/bin/lesspipe %s %s
    ```

6.	Listar os módulos do OpenStack:
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

    $ openstack service show nova
    +-------------+----------------------------------+
    | Field       | Value                            |
    +-------------+----------------------------------+
    | description | Nova Compute Service             |
    | enabled     | True                             |
    | id          | 5f271b049588412d8e0a11b2fea5469c |
    | name        | nova                             |
    | type        | compute                          |
    +-------------+----------------------------------+
    ```

7.	Mostrar o arquivo de configuração:
    ```
    $ less /etc/nova/nova.conf
    ```

8.	Mostrar os hypervisors disponíveis:
    ```
    $ openstack hypervisor list
    +----+---------------------+-----------------+----------------+-------+
    | ID | Hypervisor Hostname | Hypervisor Type | Host IP        | State |
    +----+---------------------+-----------------+----------------+-------+
    |  1 | ubuntu              | QEMU            | 192.168.17.131 | up    |
    +----+---------------------+-----------------+----------------+-------+
    ```

9.	Mostrar a descrição do hypervisor:
    ```
    $ openstack hypervisor show ubuntu
    +----------------------+-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    | Field                | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
    +----------------------+-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    | aggregates           | []                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
    | cpu_info             | {"vendor": "Intel", "model": "IvyBridge", "arch": "x86_64", "features": ["pge", "avx", "clflush", "sep", "syscall", "tsc_adjust", "vme", "tsc", "sse", "xsave", "erms", "cmov", "smep", "nx", "pat", "osxsave", "lm", "msr", "fpu", "fxsr", "sse4.1", "pae", "sse4.2", "pclmuldq", "pcid", "tsc-deadline", "mmx", "arat", "cx8", "mce", "de", "aes", "mca", "pse", "pni", "popcnt", "apic", "fsgsbase", "f16c", "invtsc", "lahf_lm", "rdtscp", "sse2", "ss", "hypervisor", "ssse3", "cx16", "pse36", "mtrr", "rdrand", "x2apic"], "topology": {"cores": 1, "cells": 1, "threads": 1, "sockets": 4}} |
    | current_workload     | 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | disk_available_least | 84                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
    | free_disk_gb         | 97                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
    | free_ram_mb          | 11485                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
    | host_ip              | 192.168.17.131                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
    | host_time            | 11:39:34                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
    | hypervisor_hostname  | ubuntu                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
    | hypervisor_type      | QEMU                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
    | hypervisor_version   | 2008000                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
    | id                   | 1                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | load_average         | 0.25, 0.33, 0.60                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
    | local_gb             | 97                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
    | local_gb_used        | 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | memory_mb            | 11997                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
    | memory_mb_used       | 512                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
    | running_vms          | 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | service_host         | ubuntu                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
    | service_id           | 2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | state                | up                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
    | status               | enabled                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
    | uptime               | 16 min                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
    | users                | 2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | vcpus                | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    | vcpus_used           | 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
    +----------------------+-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    ```

10.	Mostrar estatísticas de uso dos hypervisors:
    ```
    $ openstack hypervisor stats show
    +----------------------+-------+
    | Field                | Value |
    +----------------------+-------+
    | count                | 1     |
    | current_workload     | 0     |
    | disk_available_least | 84    |
    | free_disk_gb         | 97    |
    | free_ram_mb          | 11485 |
    | local_gb             | 97    |
    | local_gb_used        | 0     |
    | memory_mb            | 11997 |
    | memory_mb_used       | 512   |
    | running_vms          | 0     |
    | vcpus                | 4     |
    | vcpus_used           | 0     |
    +----------------------+-------+
    ```

11.	Listar os *flavors*:
    ```
    $ openstack flavor list
    +--------------------------------------+-----------+-------+------+-----------+-------+-----------+
    | ID                                   | Name      |   RAM | Disk | Ephemeral | VCPUs | Is Public |
    +--------------------------------------+-----------+-------+------+-----------+-------+-----------+
    | 1                                    | m1.tiny   |   512 |    1 |         0 |     1 | True      |
    | 2                                    | m1.small  |  2048 |   20 |         0 |     1 | True      |
    | 3                                    | m1.medium |  4096 |   40 |         0 |     2 | True      |
    | 4                                    | m1.large  |  8192 |   80 |         0 |     4 | True      |
    | 42                                   | m1.nano   |    64 |    0 |         0 |     1 | True      |
    | 4e4c0fe2-bf78-448b-bab9-e5b186d313d8 | mini      |    64 |    1 |         0 |     1 | True      |
    | 5                                    | m1.xlarge | 16384 |  160 |         0 |     8 | True      |
    | 84                                   | m1.micro  |   128 |    0 |         0 |     1 | True      |
    | c1                                   | cirros256 |   256 |    0 |         0 |     1 | True      |
    | d1                                   | ds512M    |   512 |    5 |         0 |     1 | True      |
    | d2                                   | ds1G      |  1024 |   10 |         0 |     1 | True      |
    | d3                                   | ds2G      |  2048 |   10 |         0 |     2 | True      |
    | d4                                   | ds4G      |  4096 |   20 |         0 |     4 | True      |
    +--------------------------------------+-----------+-------+------+-----------+-------+-----------+
    ```

12.	Mostrar informações sobre um *flavor*:
    ```
    $ openstack flavor show m1.tiny
    +----------------------------+---------+
    | Field                      | Value   |
    +----------------------------+---------+
    | OS-FLV-DISABLED:disabled   | False   |
    | OS-FLV-EXT-DATA:ephemeral  | 0       |
    | access_project_ids         | None    |
    | disk                       | 1       |
    | id                         | 1       |
    | name                       | m1.tiny |
    | os-flavor-access:is_public | True    |
    | properties                 |         |
    | ram                        | 512     |
    | rxtx_factor                | 1.0     |
    | swap                       |         |
    | vcpus                      | 1       |
    +----------------------------+---------+
    ```

13.	Criar um *flavor*: 
    ```
    $ openstack flavor create --public --ram 64 --vcpus 1 --disk 1 m.fiap
    +----------------------------+--------------------------------------+
    | Field                      | Value                                |
    +----------------------------+--------------------------------------+
    | OS-FLV-DISABLED:disabled   | False                                |
    | OS-FLV-EXT-DATA:ephemeral  | 0                                    |
    | disk                       | 1                                    |
    | id                         | a0683bcb-b937-4c75-be19-7641eceeff78 |
    | name                       | m.fiap                               |
    | os-flavor-access:is_public | True                                 |
    | properties                 |                                      |
    | ram                        | 64                                   |
    | rxtx_factor                | 1.0                                  |
    | swap                       |                                      |
    | vcpus                      | 1                                    |
    +----------------------------+--------------------------------------+
    ```

14.	Criar uma chave:
    ```
    $ openstack keypair create chave-fiap > chave-fiap.pem
    ```

15.	Conferir o conteúdo da chave:
    ```
    $ cat chave-fiap.pem
    -----BEGIN RSA PRIVATE KEY-----
    MIIEpAIBAAKCAQEAv2niqYsP2O0LE8OUVyFSUDuLPnrIHy8TDBqg0iy8MVEBL6F1
    I3lRBl8F2qPU9Xb2JAkfaqxgd+nBc9dykh166Q1TYc6+sPau6GymUGlmAvknUcqU
    Ojs2DbNEctEgKb2FIxJfTxlv+DOxOnkp2yNS1120VXKSTZaSwWkNLhzeNI3L4ijh
    Aenemwt4k7U94PTVEmZfYLD7i+JmdOeJpXIaJrQIw1IqYoQdIW+GXet83ldDcVeM
    BK6YtnlXv2c34Sf5DwiejofT8K7STSwpYkeP7uO6k76RtNcZYoqBZWNs+/Lm0+KL
    pGfs02fIH4OR1XwUnHqTtcHE1POWQ8OQIT28EQIDAQABAoIBAQCX9HwvMilrgYro
    rrwFi3toHE0HVbunHdzWIBGJqF/iGreBU4DFn5qWHztrfeIi18TBiMh7C8sthtG/
    n8rheivH9X87R0CVBdCGzTIe5f1I4Pi48sRHkhfwOl9dcu1Alaaq1/v3tG7yMD9C
    90smH121gTsXFnRUyfNJPZdsAxjXyFSAKbnrELNH2JHLNVj4qNrVWHYwqhJZSx5p
    a9wqtPKCjZcqhDq4cRdvcgTNMr16N72OV6sw0v6cbzjGjXAonyy/lMt/ecN2soh+
    tNdpAgru7/vMMWjhR+0lVvCqCrIw/2ZuE1Lkzulkf3Ih2N+gg1fWIGnX9Lw7B4Of
    5HcxuU5NAoGBAPOIajs5em4RBcQMVMWNcT7P56Q6mQOAnpECRzAxTudnz7HGn6wF
    IWFbYKakUzGUJgbmJXO8H8U0yUlbdc82yOBbYd13WS4WkAz7EFsgkPTGbSTaZsG9
    33L+E13C1bmeqBnjGkKt1qaHZVDTzQ8OWg6ay7i/byv+4ki0BZnD3KkfAoGBAMk2
    bdPenS3X/Rsbp/RZEzbttobyRPePW+gLb899QTFSOMM352hhqt5za629wonTLvql
    z0c/VL4BuHRaxSUvZ+6SzeHBrpOp3BeDD60QR44b4wPJDaATJe4oLcEn3k10/c6Z
    8jpKHS3Go8HyzxTkD5CbA94JJAsEoHeev8rYw4TPAoGARGZRJ/c52sYvL2QjPyU9
    5F1yex72MRSj9KiGJBQFTFtM62qVGDSMrpKCr+tBbpBkqdVkOYBiD/qGenMUwLFr
    dBBWiWRnCNnPdcXiTyXzcLx2lT4+VDYnF14jRFdfvfXA0xyFGKtIuZcXrr7+PjdS
    tf1mMKqb5+h7192wIQLw7BMCgYEAnwfr6ibqqA8sNz62koPMkf9z4liddeTSySYw
    6xeebTMFNhZ7SZ7YBBXJp3pxxakqWmSu3SsK+Vo2xY/wfaFoTcGuA56nMoJwOA0Y
    WLqjM4iK9rTzle9MbV1IPIAcTbAH4kD+mF93jHSRfXtBfMt8GdjLR7SFzkeL5L+N
    6u9EKTUCgYBUwwXRr+rrRtxKeO0iVmcyMyNtXWEhSXvfxR3zAdXYJ2z++wS1tPUU
    7nyFI8L2LrAiHLs0D8e1SmbvkUkVQ9jn9+W52BH9I3s3Z/15VfOkNPNCPeZHTh3l
    N0O5xtIRsj1V33eQf6j8eaUXPqofXshHjCkI8/9osjx9rB5+L/gjAw==
    -----END RSA PRIVATE KEY-----
    ```

16.	Listar as chaves disponíveis:
    ```
    $ openstack keypair list
    +------------+-------------------------------------------------+
    | Name       | Fingerprint                                     |
    +------------+-------------------------------------------------+
    | chave-fiap | 83:d4:98:bf:8a:06:b2:87:68:4e:72:a4:d2:a0:b9:07 |
    +------------+-------------------------------------------------+
    ```

17.	Assignar as permissões certas na chave e conferir que foram aplicadas:
    ```
    $ chmod 600 chave-fiap.pem
    
    $ ll chave-fiap.pem
    -rw------- 1 os os 1680 Oct 19 11:41 chave-fiap.pem
    ```

18.	Listar as imagens:
    ```
    $ openstack image list
    +--------------------------------------+--------------------------+--------+
    | ID                                   | Name                     | Status |
    +--------------------------------------+--------------------------+--------+
    | cd992dd3-2197-49fe-9f0e-43d783d18a5c | cirros-0.3.5-x86_64-disk | active |
    +--------------------------------------+--------------------------+--------+
    ```

19.	Criar a VM: 
    ```
    $ openstack server create --flavor m.fiap --image cirros-0.3.5-x86_64-disk --key-name chave-fiap vmfiap01
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
    | adminPass                           | ov2dNjfSTpjX                                                    |
    | config_drive                        |                                                                 |
    | created                             | 2020-10-19T18:42:32Z                                            |
    | flavor                              | m.fiap (a0683bcb-b937-4c75-be19-7641eceeff78)                   |
    | hostId                              |                                                                 |
    | id                                  | fd676d99-1d15-4690-b69e-02d71947d1c5                            |
    | image                               | cirros-0.3.5-x86_64-disk (cd992dd3-2197-49fe-9f0e-43d783d18a5c) |
    | key_name                            | chave-fiap                                                      |
    | name                                | vmfiap01                                                        |
    | progress                            | 0                                                               |
    | project_id                          | faac34f01fb2464295bcea501b18b741                                |
    | properties                          |                                                                 |
    | security_groups                     | name='default'                                                  |
    | status                              | BUILD                                                           |
    | updated                             | 2020-10-19T18:42:32Z                                            |
    | user_id                             | fe2d2a5507ed4ad2919258d7252cebc6                                |
    | volumes_attached                    |                                                                 |
    +-------------------------------------+-----------------------------------------------------------------+
    ```

20.	Listar as VMs:
    ```
    $ openstack server list
    +--------------------------------------+----------+--------+-------------------------------------------------------+--------------------------+--------+
    | ID                                   | Name     | Status | Networks                                              | Image                    | Flavor |
    +--------------------------------------+----------+--------+-------------------------------------------------------+--------------------------+--------+
    | fd676d99-1d15-4690-b69e-02d71947d1c5 | vmfiap01 | ACTIVE | private=fdb5:7432:9bc4:0:f816:3eff:fe7f:435, 10.0.0.3 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+----------+--------+-------------------------------------------------------+--------------------------+--------+
    ```

21.	Mostrar a *url* do console:
    ```
    $ openstack console url show vmfiap01
    +-------+-------------------------------------------------------------------------------------+
    | Field | Value                                                                               |
    +-------+-------------------------------------------------------------------------------------+
    | type  | novnc                                                                               |
    | url   | http://192.168.17.131:6080/vnc_auto.html?token=3875ba97-5bce-41c9-a4ba-9df2579fe389 |
    +-------+-------------------------------------------------------------------------------------+
    ```

22.	Acessar por console a vm e criar uma pasta ou um arquivo qualquer:
    - Pela URL do console
    - Directo pelo hypervisor:
    ```
    $ virsh list
     Id    Name                           State
    ----------------------------------------------------
     1     instance-00000005              running

    $ virsh console 1
    Connected to domain instance-00000005
    Escape character is ^]

    login as 'cirros' user. default password: 'cubswin:)'. use 'sudo' for root.
    vmfiap01 login: cirros
    Password:
    $ touch teste-fiap
    $ ls
    teste-fiap
    ```
    
23.	Mostrar o *log* da VM:
    ```
    $ openstack console log show vmfiap01
    [    0.000000] Initializing cgroup subsys cpuset
    [    0.000000] Initializing cgroup subsys cpu
    [    0.000000] Linux version 3.2.0-80-virtual (buildd@batsu) (gcc version 4.6.3 (Ubuntu/Linaro 4.6.3-1ubuntu5) ) #116-Ubuntu SMP Mon Mar 23 17:28:52 UTC 2015 (Ubuntu 3.2.0-80.116-virtual 3.2.68)
    [    0.000000] Command line: LABEL=cirros-rootfs ro console=tty1 console=ttyS0
    [    0.000000] KERNEL supported cpus:
    [    0.000000]   Intel GenuineIntel
    [    0.000000]   AMD AuthenticAMD
    [    0.000000]   Centaur CentaurHauls
    [    0.000000] BIOS-provided physical RAM map:
    [    0.000000]  BIOS-e820: 0000000000000000 - 000000000009fc00 (usable)
    [    0.000000]  BIOS-e820: 000000000009fc00 - 00000000000a0000 (reserved)
    [    0.000000]  BIOS-e820: 00000000000f0000 - 0000000000100000 (reserved)
    [    0.000000]  BIOS-e820: 0000000000100000 - 0000000003fdc000 (usable)
    [    0.000000]  BIOS-e820: 0000000003fdc000 - 0000000004000000 (reserved)
    [    0.000000]  BIOS-e820: 00000000fffc0000 - 0000000100000000 (reserved)
    [    0.000000] NX (Execute Disable) protection: active
    [    0.000000] SMBIOS 2.8 present.
    [    0.000000] No AGP bridge found
    [    0.000000] last_pfn = 0x3fdc max_arch_pfn = 0x400000000
    [    0.000000] x86 PAT enabled: cpu 0, old 0x7040600070406, new 0x7010600070106
    [    0.000000] found SMP MP-table at [ffff8800000f6a40] f6a40
    [    0.000000] init_memory_mapping: 0000000000000000-0000000003fdc000
    ```

24.	Mostrar os eventos relacionados à VM:
    ```
    $ openstack server event list vmfiap01
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    | Request ID                               | Server ID                            | Action | Start Time                 |
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    | req-83d22008-bdbd-41b4-8aae-e9ba807b6aba | fd676d99-1d15-4690-b69e-02d71947d1c5 | create | 2020-10-19T18:42:31.000000 |
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    ```

25.	Desligar a VM:
    ```
    $ openstack server stop vmfiap01
    ```

26.	Listar as VMs e conferir que foi desligada:
    ```
    $ openstack server list
    +--------------------------------------+----------+---------+-------------------------------------------------------+--------------------------+--------+
    | ID                                   | Name     | Status  | Networks                                              | Image                    | Flavor |
    +--------------------------------------+----------+---------+-------------------------------------------------------+--------------------------+--------+
    | fd676d99-1d15-4690-b69e-02d71947d1c5 | vmfiap01 | SHUTOFF | private=fdb5:7432:9bc4:0:f816:3eff:fe7f:435, 10.0.0.3 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+----------+---------+-------------------------------------------------------+--------------------------+--------+
    ```

27.	Listar novamente os eventos relacionados à VM e conferir que foi registrado o evento de *shutdown* da mesma:
    ```
    $ openstack server event list vmfiap01
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    | Request ID                               | Server ID                            | Action | Start Time                 |
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    | req-a407d6cc-cb7d-41b0-92c3-2eea58fe0b53 | fd676d99-1d15-4690-b69e-02d71947d1c5 | stop   | 2020-10-19T18:48:22.000000 |
    | req-83d22008-bdbd-41b4-8aae-e9ba807b6aba | fd676d99-1d15-4690-b69e-02d71947d1c5 | create | 2020-10-19T18:42:31.000000 |
    +------------------------------------------+--------------------------------------+--------+----------------------------+
    ```

28.	Ligar novamente a VM:
    ```
    $ openstack server start vmfiap01
    ```

29.	Criar um snapshot da VM:
    ```
    $ openstack server image create vmfiap01 --name vmfiap01_snap
    +------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    | Field            | Value                                                                                                                                                                                                                                                  |
    +------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    | checksum         | None                                                                                                                                                                                                                                                   |
    | container_format | None                                                                                                                                                                                                                                                   |
    | created_at       | 2020-10-19T18:49:32Z                                                                                                                                                                                                                                   |
    | disk_format      | None                                                                                                                                                                                                                                                   |
    | file             | /v2/images/7b29041a-73f6-438e-91f9-09ff7423a585/file                                                                                                                                                                                                   |
    | id               | 7b29041a-73f6-438e-91f9-09ff7423a585                                                                                                                                                                                                                   |
    | min_disk         | 1                                                                                                                                                                                                                                                      |
    | min_ram          | 0                                                                                                                                                                                                                                                      |
    | name             | vmfiap01_snap                                                                                                                                                                                                                                         |
    | owner            | faac34f01fb2464295bcea501b18b741                                                                                                                                                                                                                       |
    | properties       | base_image_ref='cd992dd3-2197-49fe-9f0e-43d783d18a5c', boot_roles='admin', image_type='snapshot', instance_uuid='fd676d99-1d15-4690-b69e-02d71947d1c5', owner_project_name='demo', owner_user_name='admin', user_id='fe2d2a5507ed4ad2919258d7252cebc6' |
    | protected        | False                                                                                                                                                                                                                                                  |
    | schema           | /v2/schemas/image                                                                                                                                                                                                                                      |
    | size             | None                                                                                                                                                                                                                                                   |
    | status           | queued                                                                                                                                                                                                                                                 |
    | tags             |                                                                                                                                                                                                                                                        |
    | updated_at       | 2020-10-19T18:49:32Z                                                                                                                                                                                                                                   |
    | virtual_size     | None                                                                                                                                                                                                                                                   |
    | visibility       | private                                                                                                                                                                                                                                                |
    +------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    ```

30.	Listar as imagens (aguardar que fique em estado `active`):
    ```
    $ openstack image list
    +--------------------------------------+--------------------------+--------+
    | ID                                   | Name                     | Status |
    +--------------------------------------+--------------------------+--------+
    | cd992dd3-2197-49fe-9f0e-43d783d18a5c | cirros-0.3.5-x86_64-disk | active |
    | 7b29041a-73f6-438e-91f9-09ff7423a585 | vmfiap01_snap            | queued |
    +--------------------------------------+--------------------------+--------+
    
    $ openstack image list
    +--------------------------------------+--------------------------+--------+
    | ID                                   | Name                     | Status |
    +--------------------------------------+--------------------------+--------+
    | cd992dd3-2197-49fe-9f0e-43d783d18a5c | cirros-0.3.5-x86_64-disk | active |
    | 7b29041a-73f6-438e-91f9-09ff7423a585 | vmfiap01_snap            | active |
    +--------------------------------------+--------------------------+--------+
    ```

31.	Instanciar o `snapshot`:
    ```
    $ openstack server create --flavor m.fiap --image vmfiap01_snap vmfiap02
    +-------------------------------------+-------------------------------------------------------+
    | Field                               | Value                                                 |
    +-------------------------------------+-------------------------------------------------------+
    | OS-DCF:diskConfig                   | MANUAL                                                |
    | OS-EXT-AZ:availability_zone         |                                                       |
    | OS-EXT-SRV-ATTR:host                | None                                                  |
    | OS-EXT-SRV-ATTR:hypervisor_hostname | None                                                  |
    | OS-EXT-SRV-ATTR:instance_name       |                                                       |
    | OS-EXT-STS:power_state              | NOSTATE                                               |
    | OS-EXT-STS:task_state               | scheduling                                            |
    | OS-EXT-STS:vm_state                 | building                                              |
    | OS-SRV-USG:launched_at              | None                                                  |
    | OS-SRV-USG:terminated_at            | None                                                  |
    | accessIPv4                          |                                                       |
    | accessIPv6                          |                                                       |
    | addresses                           |                                                       |
    | adminPass                           | 4VVPhyX8EXk6                                          |
    | config_drive                        |                                                       |
    | created                             | 2020-10-19T18:50:58Z                                  |
    | flavor                              | m.fiap (a0683bcb-b937-4c75-be19-7641eceeff78)         |
    | hostId                              |                                                       |
    | id                                  | 24731592-0fa0-4ecc-8c1e-8ebe8ec2249e                  |
    | image                               | vmfiap01_snap (7b29041a-73f6-438e-91f9-09ff7423a585)  |
    | key_name                            | None                                                  |
    | name                                | vmfiap02                                              |
    | progress                            | 0                                                     |
    | project_id                          | faac34f01fb2464295bcea501b18b741                      |
    | properties                          |                                                       |
    | security_groups                     | name='default'                                        |
    | status                              | BUILD                                                 |
    | updated                             | 2020-10-19T18:50:58Z                                  |
    | user_id                             | fe2d2a5507ed4ad2919258d7252cebc6                      |
    | volumes_attached                    |                                                       |
    +-------------------------------------+-------------------------------------------------------+
    ```

32.	Listar as VMs usando o comando `virsh`:
    ```
    $ virsh list
     Id    Name                           State
    ----------------------------------------------------
     3     instance-00000005              running
     4     instance-00000006              running
    ```

33. Logar na nova VM e confirmar que o arquivo criado previamente existe:
    ```
    $ virsh console 4
    Connected to domain instance-00000006
    Escape character is ^]

    login as 'cirros' user. default password: 'cubswin:)'. use 'sudo' for root.
    vmfiap02 login: cirros
    Password:
    $ ls
    teste-fiap
    ```

34.	Mostrar as informações da definição da VM:
    ```
    $ virsh dumpxml 3
    <domain type='qemu' id='3'>
      <name>instance-00000005</name>
      <uuid>fd676d99-1d15-4690-b69e-02d71947d1c5</uuid>
      <metadata>
        <nova:instance xmlns:nova="http://openstack.org/xmlns/libvirt/nova/1.0">
          <nova:package version="16.1.1"/>
          <nova:name>vmfiap01</nova:name>
          <nova:creationTime>2020-10-19 18:49:00</nova:creationTime>
          <nova:flavor name="m.fiap">
            <nova:memory>64</nova:memory>
            <nova:disk>1</nova:disk>
            <nova:swap>0</nova:swap>
            <nova:ephemeral>0</nova:ephemeral>
            <nova:vcpus>1</nova:vcpus>
          </nova:flavor>
          <nova:owner>
            <nova:user uuid="fe2d2a5507ed4ad2919258d7252cebc6">admin</nova:user>
            <nova:project uuid="faac34f01fb2464295bcea501b18b741">demo</nova:project>
          </nova:owner>
          <nova:root type="image" uuid="cd992dd3-2197-49fe-9f0e-43d783d18a5c"/>
        </nova:instance>
      </metadata>
      <memory unit='KiB'>65536</memory>
      <currentMemory unit='KiB'>65536</currentMemory>
      <vcpu placement='static'>1</vcpu>
      <cputune>
        <shares>1024</shares>
      </cputune>
      <resource>
        <partition>/machine</partition>
      </resource>
      <sysinfo type='smbios'>
        <system>
          <entry name='manufacturer'>OpenStack Foundation</entry>
          <entry name='product'>OpenStack Nova</entry>
          <entry name='version'>16.1.1</entry>
          <entry name='serial'>311453db-b8d5-3f93-65ff-6fca5aa96025</entry>
          <entry name='uuid'>fd676d99-1d15-4690-b69e-02d71947d1c5</entry>
          <entry name='family'>Virtual Machine</entry>
        </system>
      </sysinfo>
      <os>
        <type arch='x86_64' machine='pc-i440fx-zesty'>hvm</type>
        <boot dev='hd'/>
        <smbios mode='sysinfo'/>
      </os>
      <features>
        <acpi/>
        <apic/>
      </features>
      <cpu>
        <topology sockets='1' cores='1' threads='1'/>
      </cpu>
      <clock offset='utc'/>
      <on_poweroff>destroy</on_poweroff>
      <on_reboot>restart</on_reboot>
      <on_crash>destroy</on_crash>
      <devices>
        <emulator>/usr/bin/qemu-system-x86_64</emulator>
        <disk type='file' device='disk'>
          <driver name='qemu' type='qcow2' cache='none'/>
          <source file='/opt/stack/data/nova/instances/fd676d99-1d15-4690-b69e-02d71947d1c5/disk'/>
          <backingStore type='file' index='1'>
            <format type='raw'/>
            <source file='/opt/stack/data/nova/instances/_base/ca55405a2b972c659cab9c6fc7fb9fb060ebe7d1'/>
            <backingStore/>
          </backingStore>
          <target dev='vda' bus='virtio'/>
          <alias name='virtio-disk0'/>
          <address type='pci' domain='0x0000' bus='0x00' slot='0x04' function='0x0'/>
        </disk>
        <controller type='usb' index='0' model='piix3-uhci'>
          <alias name='usb'/>
          <address type='pci' domain='0x0000' bus='0x00' slot='0x01' function='0x2'/>
        </controller>
        <controller type='pci' index='0' model='pci-root'>
          <alias name='pci.0'/>
        </controller>
        <interface type='bridge'>
          <mac address='fa:16:3e:7f:04:35'/>
          <source bridge='qbree2ccd25-fb'/>
          <target dev='tapee2ccd25-fb'/>
          <model type='virtio'/>
          <driver name='qemu'/>
          <alias name='net0'/>
          <address type='pci' domain='0x0000' bus='0x00' slot='0x03' function='0x0'/>
        </interface>
        <serial type='pty'>
          <source path='/dev/pts/2'/>
          <log file='/opt/stack/data/nova/instances/fd676d99-1d15-4690-b69e-02d71947d1c5/console.log' append='off'/>
          <target port='0'/>
          <alias name='serial0'/>
        </serial>
        <console type='pty' tty='/dev/pts/2'>
          <source path='/dev/pts/2'/>
          <log file='/opt/stack/data/nova/instances/fd676d99-1d15-4690-b69e-02d71947d1c5/console.log' append='off'/>
          <target type='serial' port='0'/>
          <alias name='serial0'/>
        </console>
        <input type='mouse' bus='ps2'>
          <alias name='input0'/>
        </input>
        <input type='keyboard' bus='ps2'>
          <alias name='input1'/>
        </input>
        <graphics type='vnc' port='5900' autoport='yes' listen='127.0.0.1' keymap='en-us'>
          <listen type='address' address='127.0.0.1'/>
        </graphics>
        <video>
          <model type='cirrus' vram='16384' heads='1' primary='yes'/>
          <alias name='video0'/>
          <address type='pci' domain='0x0000' bus='0x00' slot='0x02' function='0x0'/>
        </video>
        <memballoon model='virtio'>
          <stats period='10'/>
          <alias name='balloon0'/>
          <address type='pci' domain='0x0000' bus='0x00' slot='0x05' function='0x0'/>
        </memballoon>
      </devices>
      <seclabel type='dynamic' model='apparmor' relabel='yes'>
        <label>libvirt-fd676d99-1d15-4690-b69e-02d71947d1c5</label>
        <imagelabel>libvirt-fd676d99-1d15-4690-b69e-02d71947d1c5</imagelabel>
      </seclabel>
      <seclabel type='dynamic' model='dac' relabel='yes'>
        <label>+64055:+123</label>
        <imagelabel>+64055:+123</imagelabel>
      </seclabel>
    </domain>
    ```

35.	Deletar as vms:
    ```
    $ openstack server delete vmfiap01
    $ openstack server delete vmfiap02
    $ openstack image delete vmfiap01_snap
    ```

36. Recriar desde o Horizon Dashboard:
    - Criação de *flavor* 
    - Criação de VM
    - Criação de *snapshot*
    - Criação de VM a partir de *snapshot*
    ![](/cld/openstack/img/nova1.png)
    ![](/cld/openstack/img/nova2.png)
    ![](/cld/openstack/img/nova3.png)
