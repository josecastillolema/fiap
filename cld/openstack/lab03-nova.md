# Lab 3 - Nova

## Compute Service
Usaremos o serviço Nova para aprender alguns conceitos importantes sobre máquinas virtuais:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**

1. Conferir se as extensões de virtualizações estão presentes no processador:
    ```
    $ grep -E ' svm | vmx ' /proc/cpuinfo
    $
    ```

2.	Listar os serviços Linux que compõem o Nova:
    ```
    $ systemctl  | grep devstack@n
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
    $ openstack hypervisor stats  show
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
    ```

12.	Mostrar informações sobre um *flavor*:
    ```
    ```

13.	Criar um *flavor*: 
    ```
    ```

14.	Criar uma chave:
    ```
    ```

15.	Conferir o conteúdo da chave:
    ```
    ```

16.	Listar as chaves disponíveis:
    ```
    ```

17.	Assignar as permissões certas na chave e conferir que foram aplicadas:
    ```
    ```

18.	Listar as imagens:
    ```
    ```

19.	Criar a VM: 
    ```
    ```

20.	Listar as VMs:
    ```
    ```

21.	Mostrar a *url* do console:
    ```
    ```

22.	Acessar por console a vm e criar uma pasta ou um arquivo qualquer:
    - Pela URL do console
    - Directo pelo hypervisor:
    ```
    ```
    
23.	Mostrar o *log* da VM:
    ```
    ```

24.	Mostrar os eventos relacionados à VM:
    ```
    ```

25.	Desligar a VM:
    ```
    ```

26.	Listar as VMs e conferir que foi desligada:
    ```
    ```

27.	Listar novamente os eventos relacionados à VM e conferir que foi registrado o evento de *shutdown* da mesma:
    ```
    ```

28.	Ligar novamente a VM:
    ```
    ```

29.	Criar um snapshot da VM:
    ```
    ```

30.	Listar as imagens:
    ```
    ```

31.	Instanciar o `snapshot`:
    ```
    ```

32.	Listar as VMs usando o comando `virsh`:
    ```
    ```

33.	Mostrar as informações da definição da VM:
    ```
    ```

34.	Deletar as vms:
    ```
    ```

35. Recriar desde o Horizon Dashboard:
    - Criação de `flavor` 
    - Criação de VM
    - Criação de `snapshot`
    - Criação de VM a partir de `snapshot`
    ![](/cld/openstack/img/nova1.png)
    ![](/cld/openstack/img/nova2.png)
    ![](/cld/openstack/img/nova3.png)
