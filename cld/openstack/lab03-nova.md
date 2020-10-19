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
    ```

4.	Mostrar os *logs* do serviço:
    ```
    
    ```

5. Carregar as credenciais de OpenStack:
    ```
    ```

6.	Listar os módulos do OpenStack:
    ```
    ```

7.	Mostrar o arquivo de configuração:
    ```
    ```

8.	Mostrar os hypervisors disponíveis:
    ```
    ```

9.	Mostrar a descrição do hypervisor:
    ```
    ```

10.	Mostrar estatísticas de uso dos hypervisors:
    ```
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
