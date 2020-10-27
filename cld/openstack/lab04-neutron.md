# Lab 4 - OpenStack Neutron

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
 
2.	Mostrar informação sobre o *endpoint*:
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
    $ sudo netstat -punlt | grep 9696
    tcp        0      0 0.0.0.0:9696            0.0.0.0:*               LISTEN                    831/python
    
    $ ps aux  | grep 831
    os          831  0.2  0.9 279916 119168 ?       Ss   04:54   0:04 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
    os         6963  0.0  0.0  14224   972 pts/2    S+   05:29   0:00 grep --color=auto 831
    ```

4.	Conferir a saúde dos agentes que compõem o Neutron:
    ```
    $ openstack network agent list
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+---------------------------+
    | ID                                   | Agent Type         | Host   | Availability Zone | Alive | State | Binary                    |
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+---------------------------+
    | 1714e6c2-5907-4550-ba3b-cffeeabe6600 | L3 agent           | ubuntu | nova              | :-)   | UP    | neutron-l3-agent          |
    | 6194150e-7771-4a73-8963-5cb0f7a2a10f | Metadata agent     | ubuntu | None              | :-)   | UP    | neutron-metadata-agent    |
    | cd64f22e-e148-48ea-b3a8-29880934cb18 | DHCP agent         | ubuntu | nova              | :-)   | UP    | neutron-dhcp-agent        |
    | cfc5d9ac-c59a-4e07-b740-8cbda8b6ef50 | Open vSwitch agent | ubuntu | None              | :-)   | UP    | neutron-openvswitch-agent |
    +--------------------------------------+--------------------+--------+-------------------+-------+-------+---------------------------+
    ```
 
5.	Listar os serviços Linux que compõem o Neutron:
    ```
    $ systemctl | grep devstack@q
    devstack@q-agt.service                                                     loaded active running   Devstack devstack@q-agt.service
    devstack@q-dhcp.service                                                    loaded active running   Devstack devstack@q-dhcp.service
    devstack@q-l3.service                                                      loaded active running   Devstack devstack@q-l3.service
    devstack@q-meta.service                                                    loaded active running   Devstack devstack@q-meta.service
    devstack@q-svc.service                                                     loaded active running   Devstack devstack@q-svc.service
    
    $ systemctl | grep openvswitch
    openvswitch-nonetwork.service                                              loaded active exited    Open vSwitch Internal Unit
    openvswitch-switch.service                                                 loaded active exited    Open vSwitch
    ```
 
6.	Conferir a saúde dos serviços:
    ```
    $ systemctl status devstack@q*
    ● devstack@q-dhcp.service - Devstack devstack@q-dhcp.service
       Loaded: loaded (/etc/systemd/system/devstack@q-dhcp.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 1 day 18h ago
     Main PID: 867 (neutron-dhcp-ag)
       CGroup: /system.slice/system-devstack.slice/devstack@q-dhcp.service
               ├─ 867 /usr/bin/python /usr/local/bin/neutron-dhcp-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/dhcp_agent.ini
               ├─2453 sudo /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               ├─2455 /usr/bin/python /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               └─6523 dnsmasq --no-hosts  --strict-order --except-interface=lo --pid-file=/opt/stack/data/neutron/dhcp/4e05e1bd-50f4-494b-aacf-07d43a37d1a1/pid --dhcp-hostsfile=/opt/stack/data/neu

    ● devstack@q-meta.service - Devstack devstack@q-meta.service
       Loaded: loaded (/etc/systemd/system/devstack@q-meta.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 1 day 18h ago
     Main PID: 732 (neutron-metadat)
       CGroup: /system.slice/system-devstack.slice/devstack@q-meta.service
               ├─ 732 /usr/bin/python /usr/local/bin/neutron-metadata-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/metadata_agent.ini
               ├─2331 /usr/bin/python /usr/local/bin/neutron-metadata-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/metadata_agent.ini
               └─2338 /usr/bin/python /usr/local/bin/neutron-metadata-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/metadata_agent.ini

    ● devstack@q-l3.service - Devstack devstack@q-l3.service
       Loaded: loaded (/etc/systemd/system/devstack@q-l3.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 1 day 18h ago
     Main PID: 733 (neutron-l3-agen)
       CGroup: /system.slice/system-devstack.slice/devstack@q-l3.service
               ├─ 733 /usr/bin/python /usr/local/bin/neutron-l3-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/l3_agent.ini
               ├─3196 sudo /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               ├─3197 /usr/bin/python /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               ├─3365 radvd -C /opt/stack/data/neutron/ra/19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7.radvd.conf -p /opt/stack/data/neutron/external/pids/19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7.pid.radvd -m
               ├─3366 radvd -C /opt/stack/data/neutron/ra/19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7.radvd.conf -p /opt/stack/data/neutron/external/pids/19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7.pid.radvd -m
               └─3488 haproxy -f /opt/stack/data/neutron/ns-metadata-proxy/19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7.conf

    ● devstack@q-agt.service - Devstack devstack@q-agt.service
       Loaded: loaded (/etc/systemd/system/devstack@q-agt.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 1 day 18h ago
     Main PID: 790 (neutron-openvsw)
       CGroup: /system.slice/system-devstack.slice/devstack@q-agt.service
               ├─ 790 /usr/bin/python /usr/local/bin/neutron-openvswitch-agent --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
               ├─2451 sudo /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               ├─2454 /usr/bin/python /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
               └─2503 ovsdb-client monitor tcp:127.0.0.1:6640 Interface name,ofport,external_ids --format=json

    ● devstack@q-svc.service - Devstack devstack@q-svc.service
       Loaded: loaded (/etc/systemd/system/devstack@q-svc.service; enabled; vendor preset: enabled)
       Active: active (running) since Mon 2020-10-19 11:22:41 PDT; 1 day 18h ago
     Main PID: 831 (neutron-server)
       CGroup: /system.slice/system-devstack.slice/devstack@q-svc.service
               ├─ 831 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
               ├─2360 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
               ├─2361 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
               ├─2373 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini
               └─2378 /usr/bin/python /usr/local/bin/neutron-server --config-file /etc/neutron/neutron.conf --config-file /etc/neutron/plugins/ml2/ml2_conf.ini

    $ systemctl status openvswitch-switch
    ● openvswitch-switch.service - Open vSwitch
       Loaded: loaded (/lib/systemd/system/openvswitch-switch.service; enabled; vendor preset: enabled)
       Active: active (exited) since Mon 2020-10-19 11:23:01 PDT; 1 day 18h ago
      Process: 1518 ExecStart=/bin/true (code=exited, status=0/SUCCESS)
     Main PID: 1518 (code=exited, status=0/SUCCESS)
       CGroup: /system.slice/openvswitch-switch.service
    ```
 
7.	Mostrar os logs do serviço:
    ```
    $ journalctl -u devstack@q-svc
    ```
    
8.	Mostrar os arquivos de configuração:
    ```
    $ less /etc/neutron/neutron.conf
    
    $ less /etc/neutron/plugins/ml2/ml2_conf.ini
    
    $ less /etc/neutron/policy.json
    ```

9.	Mostrar os arquivos de configuração de uma forma mais clara (sem comentários nem linhas vazias):
    ```
    $ grep -vE "^#|^$" /etc/neutron/plugins/ml2/ml2_conf.ini
    [DEFAULT]
    [l2pop]
    [ml2]
    tenant_network_types = vxlan
    extension_drivers = port_security
    mechanism_drivers = openvswitch,linuxbridge
    [ml2_type_flat]
    flat_networks = public,
    [ml2_type_geneve]
    vni_ranges = 1:1000
    [ml2_type_gre]
    tunnel_id_ranges = 1:1000
    [ml2_type_vlan]
    network_vlan_ranges = public
    [ml2_type_vxlan]
    vni_ranges = 1:1000
    [securitygroup]
    firewall_driver = iptables_hybrid
    [agent]
    tunnel_types = vxlan
    root_helper_daemon = sudo /usr/local/bin/neutron-rootwrap-daemon /etc/neutron/rootwrap.conf
    root_helper = sudo /usr/local/bin/neutron-rootwrap /etc/neutron/rootwrap.conf
    [ovs]
    datapath_type = system
    bridge_mappings = public:br-ex
    tunnel_bridge = br-tun
    local_ip = 192.168.17.131
    ```
 
10.	Criar uma rede:
    ```
    $ openstack network create rede-privada
    +---------------------------+--------------------------------------+
    | Field                     | Value                                |
    +---------------------------+--------------------------------------+
    | admin_state_up            | UP                                   |
    | availability_zone_hints   |                                      |
    | availability_zones        |                                      |
    | created_at                | 2020-10-21T12:33:59Z                 |
    | description               |                                      |
    | dns_domain                | None                                 |
    | id                        | f7476929-ffcd-4451-a4b0-3e109d1d782c |
    | ipv4_address_scope        | None                                 |
    | ipv6_address_scope        | None                                 |
    | is_default                | False                                |
    | is_vlan_transparent       | None                                 |
    | mtu                       | 1450                                 |
    | name                      | rede-privada                         |
    | port_security_enabled     | True                                 |
    | project_id                | faac34f01fb2464295bcea501b18b741     |
    | provider:network_type     | vxlan                                |
    | provider:physical_network | None                                 |
    | provider:segmentation_id  | 93                                   |
    | qos_policy_id             | None                                 |
    | revision_number           | 2                                    |
    | router:external           | Internal                             |
    | segments                  | None                                 |
    | shared                    | False                                |
    | status                    | ACTIVE                               |
    | subnets                   |                                      |
    | tags                      |                                      |
    | updated_at                | 2020-10-21T12:33:59Z                 |
    +---------------------------+--------------------------------------+
    ```
 
11.	Criar uma subrede:
    ```
    $ openstack subnet create --network rede-privada --subnet-range 10.20.20.0/24 --ip-version 4 --dns-nameserver 8.8.8.8 subrede-privada
    +-------------------------+--------------------------------------+
    | Field                   | Value                                |
    +-------------------------+--------------------------------------+
    | allocation_pools        | 10.20.20.2-10.20.20.254              |
    | cidr                    | 10.20.20.0/24                        |
    | created_at              | 2020-10-21T12:34:56Z                 |
    | description             |                                      |
    | dns_nameservers         | 8.8.8.8                              |
    | enable_dhcp             | True                                 |
    | gateway_ip              | 10.20.20.1                           |
    | host_routes             |                                      |
    | id                      | 97ae9655-263d-4d17-8156-f030bd34367e |
    | ip_version              | 4                                    |
    | ipv6_address_mode       | None                                 |
    | ipv6_ra_mode            | None                                 |
    | name                    | subrede-privada                      |
    | network_id              | f7476929-ffcd-4451-a4b0-3e109d1d782c |
    | project_id              | faac34f01fb2464295bcea501b18b741     |
    | revision_number         | 0                                    |
    | segment_id              | None                                 |
    | service_types           |                                      |
    | subnetpool_id           | None                                 |
    | tags                    |                                      |
    | updated_at              | 2020-10-21T12:34:56Z                 |
    | use_default_subnet_pool | None                                 |
    +-------------------------+--------------------------------------+
    ```
 
12.	Subir duas vms via Horizon associadas à rede/subrede que acabamos de criar no passos anteriores. Acessar a uma delas via console e tentar pingar a outra vm pelo IP interno. Depois tenta pingar para a Internet (p.ex. 8.8.8.8, o servidor DNS da Google, não vai conseguir):
    ```
    $ virsh list
     Id    Name                           State
    ----------------------------------------------------
     5     instance-00000007              running
     6     instance-00000008              running

    $ virsh  console 5
    Connected to domain instance-00000007
    Escape character is ^]

    login as 'cirros' user. default password: 'cubswin:)'. use 'sudo' for root.
    vm-1 login: cirros
    Password:
    $ ip a
    1: lo: <LOOPBACK,UP,LOWER_UP> mtu 16436 qdisc noqueue
        link/loopback 00:00:00:00:00:00 brd 00:00:00:00:00:00
        inet 127.0.0.1/8 scope host lo
        inet6 ::1/128 scope host
           valid_lft forever preferred_lft forever
    2: eth0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1450 qdisc pfifo_fast qlen 1000
        link/ether fa:16:3e:12:b4:dc brd ff:ff:ff:ff:ff:ff
        inet 10.20.20.10/24 brd 10.20.20.255 scope global eth0
        inet6 fe80::f816:3eff:fe12:b4dc/64 scope link
           valid_lft forever preferred_lft forever
           
    $ ping 10.20.20.7
    PING 10.20.20.7 (10.20.20.7) 56(84) bytes of data.
    64 bytes from 10.20.20.7: icmp_seq=1 ttl=64 time=4.24 ms
    --- 10.20.20.7 ping statistics ---
    2 packets transmitted, 2 received, 0% packet loss, time 1002ms
    
    $ traceroute 8.8.8.8
    traceroute to 8.8.8.8 (8.8.8.8), 30 hops max, 46 byte packets
     1  *  *  10.20.20.10 (10.20.20.10)  3008.291 ms !H
     2  10.20.20.10 (10.20.20.10)  2996.692 ms !H  2999.085 ms !H  2998.905 ms !H
    ```

13.	Criar um roteador:
    ```
    $ openstack router create router-fiap
    +-------------------------+--------------------------------------+
    | Field                   | Value                                |
    +-------------------------+--------------------------------------+
    | admin_state_up          | UP                                   |
    | availability_zone_hints |                                      |
    | availability_zones      |                                      |
    | created_at              | 2020-10-21T12:35:21Z                 |
    | description             |                                      |
    | distributed             | False                                |
    | external_gateway_info   | None                                 |
    | flavor_id               | None                                 |
    | ha                      | False                                |
    | id                      | d4ea769a-6403-436d-9f8c-c9330597a247 |
    | name                    | router-fiap                          |
    | project_id              | faac34f01fb2464295bcea501b18b741     |
    | revision_number         | None                                 |
    | routes                  |                                      |
    | status                  | ACTIVE                               |
    | tags                    |                                      |
    | updated_at              | 2020-10-21T12:35:21Z                 |
    +-------------------------+--------------------------------------+
    ```

14.	Associar o roteador a rede externa (pública) do OpenStack:
    ```
    $ openstack router set --external-gateway public router-fiap
    ```
 
15.	Adicionar uma interface do roteador à subrede previamente criada:
    ```
    $ openstack router add subnet router-fiap subrede-privada
    ```

16.	Repetir os testes de ping **via console na vm previamente criada** para ver se a VM sae para "a Internet" (IP 172.24.4.6):
    ```
    $ ping 10.20.20.1
    PING 10.20.20.1 (10.20.20.1) 56(84) bytes of data.
    64 bytes from 10.20.20.1: icmp_seq=1 ttl=64 time=4.24 ms
    --- 10.20.20.1 ping statistics ---
    2 packets transmitted, 2 received, 0% packet loss, time 1002ms
    
    $ traceroute 8.8.8.8
    traceroute to 8.8.8.8 (8.8.8.8), 30 hops max, 46 byte packets
     1  10.20.20.1 (10.20.20.1)  1.010 ms  1.529 ms  0.609 ms
     2  172.24.4.6 (172.24.4.6)  3001.284 ms !H  3004.897 ms !H  3001.027 ms !H
    ```

17.	Reservar um IP público (*floating* IP):
    ```
    $ openstack floating ip create public
    +---------------------+--------------------------------------+
    | Field               | Value                                |
    +---------------------+--------------------------------------+
    | created_at          | 2020-10-21T12:36:18Z                 |
    | description         |                                      |
    | fixed_ip_address    | None                                 |
    | floating_ip_address | 172.24.4.13                           |
    | floating_network_id | 85eba4fa-9b69-44cf-9e69-4f41d8b37286 |
    | id                  | edbfe304-03b0-4a1b-a268-dd71bf389f1e |
    | name                | 172.24.4.3                           |
    | port_id             | None                                 |
    | project_id          | faac34f01fb2464295bcea501b18b741     |
    | revision_number     | 0                                    |
    | router_id           | None                                 |
    | status              | DOWN                                 |
    | updated_at          | 2020-10-21T12:36:18Z                 |
    +---------------------+--------------------------------------+
    ```

18.	Associar o IP público à VM previamente criada:
    ```
    $ openstack server add floating ip vm-1 172.24.4.13
    ```

19.	Criar um *security group*:
    ```
    $ openstack security group create fiap
    +-----------------+-------------------------------------------------------------------------------------------------------------------------------------------------------+
    | Field           | Value                                                                                                                                                 |
    +-----------------+-------------------------------------------------------------------------------------------------------------------------------------------------------+
    | created_at      | 2020-10-21T12:36:49Z                                                                                                                                  |
    | description     | fiap                                                                                                                                                  |
    | id              | b139dbf9-20b2-44cd-b39b-8de218047453                                                                                                                  |
    | name            | fiap                                                                                                                                                  |
    | project_id      | faac34f01fb2464295bcea501b18b741                                                                                                                      |
    | revision_number | 2                                                                                                                                                     |
    | rules           | created_at='2020-10-21T12:36:49Z', direction='egress', ethertype='IPv4', id='d212931e-290d-4527-a769-74ede1ce30c4', updated_at='2020-10-21T12:36:49Z' |
    |                 | created_at='2020-10-21T12:36:49Z', direction='egress', ethertype='IPv6', id='ea4e356c-d97c-42b7-ad7c-9a0e0b0c326f', updated_at='2020-10-21T12:36:49Z' |
    | updated_at      | 2020-10-21T12:36:49Z                                                                                                                                  |
    +-----------------+-------------------------------------------------------------------------------------------------------------------------------------------------------+
    ```

20.	Liberar o tráfego ICMP (para poder pingar à VM):
    ```
    $ openstack security group rule create --ingress --protocol icmp --remote-ip 0.0.0.0/0 fiap
    +-------------------+--------------------------------------+
    | Field             | Value                                |
    +-------------------+--------------------------------------+
    | created_at        | 2020-10-21T12:37:39Z                 |
    | description       |                                      |
    | direction         | ingress                              |
    | ether_type        | IPv4                                 |
    | id                | 4b690df3-5736-47c5-a401-c0cae4ce0614 |
    | name              | None                                 |
    | port_range_max    | None                                 |
    | port_range_min    | None                                 |
    | project_id        | faac34f01fb2464295bcea501b18b741     |
    | protocol          | icmp                                 |
    | remote_group_id   | None                                 |
    | remote_ip_prefix  | 0.0.0.0/0                            |
    | revision_number   | 0                                    |
    | security_group_id | b139dbf9-20b2-44cd-b39b-8de218047453 |
    | updated_at        | 2020-10-21T12:37:39Z                 |
    +-------------------+--------------------------------------+
    ```

21.	Liberar o tráfego TCP (para conseguir acessar via SSH à VM):
    ```
    $ openstack security group rule create --ingress --protocol tcp --remote-ip 0.0.0.0/0 fiap
    +-------------------+--------------------------------------+
    | Field             | Value                                |
    +-------------------+--------------------------------------+
    | created_at        | 2020-10-21T12:38:09Z                 |
    | description       |                                      |
    | direction         | ingress                              |
    | ether_type        | IPv4                                 |
    | id                | 83eb8a45-4a3e-4580-8227-b679d9d1b0fd |
    | name              | None                                 |
    | port_range_max    | None                                 |
    | port_range_min    | None                                 |
    | project_id        | faac34f01fb2464295bcea501b18b741     |
    | protocol          | tcp                                  |
    | remote_group_id   | None                                 |
    | remote_ip_prefix  | 0.0.0.0/0                            |
    | revision_number   | 0                                    |
    | security_group_id | b139dbf9-20b2-44cd-b39b-8de218047453 |
    | updated_at        | 2020-10-21T12:38:09Z                 |
    +-------------------+--------------------------------------+
    ```

22.	Associar o *security group* à VM:
    ```
    $ openstack server list
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+
    | ID                                   | Name | Status | Networks                              | Image                    | Flavor |
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+
    | bf522bf5-3e86-4f6c-ab02-d36c2de4ee5b | vm-2 | ACTIVE | rede-privada=10.20.20.7               | cirros-0.3.5-x86_64-disk | m.fiap |
    | 5342ee28-9516-43d4-825f-a4547a8c7cdf | vm-1 | ACTIVE | rede-privada=10.20.20.10, 172.24.4.13 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+

    $ openstack server add security group vm-1 fiap
    ```

23.	Tentar pingar a VM pelo IP interno (não é possível):
    ```
    $ openstack server list
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+
    | ID                                   | Name | Status | Networks                              | Image                    | Flavor |
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+
    | bf522bf5-3e86-4f6c-ab02-d36c2de4ee5b | vm-2 | ACTIVE | rede-privada=10.20.20.7               | cirros-0.3.5-x86_64-disk | m.fiap |
    | 5342ee28-9516-43d4-825f-a4547a8c7cdf | vm-1 | ACTIVE | rede-privada=10.20.20.10, 172.24.4.13 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+------+--------+---------------------------------------+--------------------------+--------+

    $ ping 10.20.20.10
    PING 10.20.20.10 (10.20.20.10) 56(84) bytes of data.
    --- 10.20.20.10 ping statistics ---
    10 packets transmitted, 0 received, 100% packet loss, time 8999ms
    ```

24.	Conferir o ID do roteador para saber a qual *network namespace* temos que acessar:
    ```
    $ openstack router list
    +--------------------------------------+-------------+--------+-------+-------------+-------+----------------------------------+
    | ID                                   | Name        | Status | State | Distributed | HA    | Project                          |
    +--------------------------------------+-------------+--------+-------+-------------+-------+----------------------------------+
    | 19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7 | router1     | ACTIVE | UP    | False       | False | faac34f01fb2464295bcea501b18b741 |
    | d4ea769a-6403-436d-9f8c-c9330597a247 | router-fiap | ACTIVE | UP    | False       | False | faac34f01fb2464295bcea501b18b741 |
    +--------------------------------------+-------------+--------+-------+-------------+-------+----------------------------------+
    ``` 
 
25.	Listar os *network namespaces*:
    ```
    $ sudo ip netns ls
    qrouter-d4ea769a-6403-436d-9f8c-c9330597a247
    qdhcp-f7476929-ffcd-4451-a4b0-3e109d1d782c
    qrouter-19d71b70-0c7b-4b5b-8fe8-b11a1d3a7bf7
    qdhcp-4e05e1bd-50f4-494b-aacf-07d43a37d1a1
    ```

26.	Acessar ao *network namespace* do *virtual router* (veja que o *prompt* muda):
    ```
    $ sudo ip netns exec qrouter-d4ea769a-6403-436d-9f8c-c9330597a247 bash
    root@ubuntu:~#
    ``` 

27.	Conferir os IPs (do *gateway* e *floating* IPs):
    ```
    # ip a
    1: lo: <LOOPBACK,UP,LOWER_UP> mtu 65536 qdisc noqueue state UNKNOWN group default qlen 1
        link/loopback 00:00:00:00:00:00 brd 00:00:00:00:00:00
        inet 127.0.0.1/8 scope host lo
           valid_lft forever preferred_lft forever
        inet6 ::1/128 scope host
           valid_lft forever preferred_lft forever
    31: qg-215adae6-b3: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc noqueue state UNKNOWN group default qlen 1
        link/ether fa:16:3e:1f:96:43 brd ff:ff:ff:ff:ff:ff
        inet 172.24.4.6/24 brd 172.24.4.255 scope global qg-215adae6-b3
           valid_lft forever preferred_lft forever
        inet 172.24.4.13/32 brd 172.24.4.13 scope global qg-215adae6-b3
           valid_lft forever preferred_lft forever
        inet6 2001:db8::9/64 scope global
           valid_lft forever preferred_lft forever
        inet6 fe80::f816:3eff:fe1f:9643/64 scope link
           valid_lft forever preferred_lft forever
    32: qr-609c323c-1c: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1450 qdisc noqueue state UNKNOWN group default qlen 1
        link/ether fa:16:3e:83:e1:94 brd ff:ff:ff:ff:ff:ff
        inet 10.20.20.1/24 brd 10.20.20.255 scope global qr-609c323c-1c
           valid_lft forever preferred_lft forever
        inet6 fe80::f816:3eff:fe83:e194/64 scope link
           valid_lft forever preferred_lft forever
    ```

28.	Pingar à VM e acessar via SSH:
    ```
    root@ubuntu:~# ping 10.20.20.10
    PING 10.20.20.10 (10.20.20.10) 56(84) bytes of data.
    64 bytes from 10.20.20.10: icmp_seq=1 ttl=64 time=4.24 ms
    --- 10.20.20.10 ping statistics ---
    2 packets transmitted, 2 received, 0% packet loss, time 1002ms
    
    root@ubuntu:~# ssh cirros@10.20.20.10
    The authenticity of host '10.20.20.10 (10.20.20.10)' can't be established.
    RSA key fingerprint is SHA256:OmSUneCH0niBARHfqLBT9pjvRPs7DHwuGZfcHEbIT4I.
    Are you sure you want to continue connecting (yes/no)? yes
    Warning: Permanently added '10.20.20.10' (RSA) to the list of known hosts.
    cirros@10.20.20.10's password:
    $
    ```

29.	Listar as portas (**fora do *prompt* do *namespace***):
    ```
    $ openstack port list
    +--------------------------------------+------+-------------------+-----------------------------------------------------------------------------------------------------+--------+
    | ID                                   | Name | MAC Address       | Fixed IP Addresses                                                                                  | Status |
    +--------------------------------------+------+-------------------+-----------------------------------------------------------------------------------------------------+--------+
    | 215adae6-b393-4f0e-bafb-0a0df58654ea |      | fa:16:3e:1f:96:43 | ip_address='172.24.4.6', subnet_id='da0570da-acff-4f82-a811-1b2fe4770d46'                           | ACTIVE |
    |                                      |      |                   | ip_address='2001:db8::9', subnet_id='b505a0fc-87aa-4317-9a58-a03d501b3fb1'                          |        |
    | 47a2ebc7-b0a8-4ec9-9275-6f8a2426a320 |      | fa:16:3e:28:a2:2b | ip_address='10.20.20.2', subnet_id='97ae9655-263d-4d17-8156-f030bd34367e'                           | ACTIVE |
    | 5921542f-9e21-4841-a0a0-d0f9520376b1 |      | fa:16:3e:80:11:55 | ip_address='172.24.4.13', subnet_id='da0570da-acff-4f82-a811-1b2fe4770d46'                          | N/A    |
    |                                      |      |                   | ip_address='2001:db8::c', subnet_id='b505a0fc-87aa-4317-9a58-a03d501b3fb1'                          |        |
    | 609c323c-1cdc-4cb5-b113-6658c589df67 |      | fa:16:3e:83:e1:94 | ip_address='10.20.20.1', subnet_id='97ae9655-263d-4d17-8156-f030bd34367e'                           | ACTIVE |
    | 71f41315-e1d8-4716-810e-a8532e7cefdc |      | fa:16:3e:45:ed:93 | ip_address='10.0.0.2', subnet_id='d569c273-81c2-488b-9422-d1d4ee85eee7'                             | ACTIVE |
    |                                      |      |                   | ip_address='fdb5:7432:9bc4:0:f816:3eff:fe45:ed93', subnet_id='3932e372-f808-4263-9850-e82be84a32c7' |        |
    | 7b272383-9238-429e-9a8f-b06a3824538b |      | fa:16:3e:12:b4:dc | ip_address='10.20.20.10', subnet_id='97ae9655-263d-4d17-8156-f030bd34367e'                          | ACTIVE |
    | aa0f0802-9d48-4647-a186-f9dacf80e772 |      | fa:16:3e:aa:12:dd | ip_address='10.0.0.1', subnet_id='d569c273-81c2-488b-9422-d1d4ee85eee7'                             | ACTIVE |
    | c9016286-df6f-4d09-8fa9-f63921dc812d |      | fa:16:3e:9b:0d:c8 | ip_address='10.20.20.7', subnet_id='97ae9655-263d-4d17-8156-f030bd34367e'                           | ACTIVE |
    | e3982d0f-57e2-43a2-8133-65e5c92244d2 |      | fa:16:3e:15:8e:34 | ip_address='172.24.4.3', subnet_id='da0570da-acff-4f82-a811-1b2fe4770d46'                           | N/A    |
    |                                      |      |                   | ip_address='2001:db8::4', subnet_id='b505a0fc-87aa-4317-9a58-a03d501b3fb1'                          |        |
    | ed7b4f9d-fbfc-45c4-a2bb-b3ec61b265fa |      | fa:16:3e:4c:b8:b0 | ip_address='fdb5:7432:9bc4::1', subnet_id='3932e372-f808-4263-9850-e82be84a32c7'                    | ACTIVE |
    | f0d36643-4cee-4026-bc48-ea43e5ede266 |      | fa:16:3e:cf:20:b5 | ip_address='172.24.4.4', subnet_id='da0570da-acff-4f82-a811-1b2fe4770d46'                           | ACTIVE |
    |                                      |      |                   | ip_address='2001:db8::1', subnet_id='b505a0fc-87aa-4317-9a58-a03d501b3fb1'                          |        |
    +--------------------------------------+------+-------------------+-----------------------------------------------------------------------------------------------------+--------+
    ```

30.	Mostar informação sobre uma porta virtual determinada:
    ```
    $ openstack port show 7b272383-9238-429e-9a8f-b06a3824538b
    +-----------------------+----------------------------------------------------------------------------+
    | Field                 | Value                                                                      |
    +-----------------------+----------------------------------------------------------------------------+
    | admin_state_up        | UP                                                                         |
    | allowed_address_pairs |                                                                            |
    | binding_host_id       | ubuntu                                                                     |
    | binding_profile       |                                                                            |
    | binding_vif_details   | datapath_type='system', ovs_hybrid_plug='True', port_filter='True'         |
    | binding_vif_type      | ovs                                                                        |
    | binding_vnic_type     | normal                                                                     |
    | created_at            | 2020-10-21T12:39:40Z                                                       |
    | data_plane_status     | None                                                                       |
    | description           |                                                                            |
    | device_id             | 5342ee28-9516-43d4-825f-a4547a8c7cdf                                       |
    | device_owner          | compute:nova                                                               |
    | dns_assignment        | None                                                                       |
    | dns_name              | None                                                                       |
    | extra_dhcp_opts       |                                                                            |
    | fixed_ips             | ip_address='10.20.20.10', subnet_id='97ae9655-263d-4d17-8156-f030bd34367e' |
    | id                    | 7b272383-9238-429e-9a8f-b06a3824538b                                       |
    | ip_address            | None                                                                       |
    | mac_address           | fa:16:3e:12:b4:dc                                                          |
    | name                  |                                                                            |
    | network_id            | f7476929-ffcd-4451-a4b0-3e109d1d782c                                       |
    | option_name           | None                                                                       |
    | option_value          | None                                                                       |
    | port_security_enabled | True                                                                       |
    | project_id            | faac34f01fb2464295bcea501b18b741                                           |
    | qos_policy_id         | None                                                                       |
    | revision_number       | 7                                                                          |
    | security_group_ids    | b139dbf9-20b2-44cd-b39b-8de218047453, b1dfa17e-2f36-492a-bf28-fbd4b123bdd4 |
    | status                | ACTIVE                                                                     |
    | subnet_id             | None                                                                       |
    | tags                  |                                                                            |
    | trunk_details         | None                                                                       |
    | updated_at            | 2020-10-21T13:00:48Z                                                       |
    +-----------------------+----------------------------------------------------------------------------+
    ```

31.	Encontrar a interface do OpenvSwitch associada a porta:
    ```
    $ sudo ovs-vsctl show | grep 7b27
        Port "qvo7b272383-92"
            Interface "qvo7b272383-92"
    ```
    
32.	Mostrar a porta no OpenvSwitch:
    ```
    $ sudo ovs-vsctl list interface qvo7b272383-92
    _uuid               : 59fb7296-d229-43ab-b57e-577eebfa8faf
    admin_state         : up
    bfd                 : {}
    bfd_status          : {}
    cfm_fault           : []
    cfm_fault_status    : []
    cfm_flap_count      : []
    cfm_health          : []
    cfm_mpid            : []
    cfm_remote_mpids    : []
    cfm_remote_opstate  : []
    duplex              : full
    error               : []
    external_ids        : {attached-mac="fa:16:3e:12:b4:dc", iface-id="7b272383-9238-429e-9a8f-b06a3824538b", iface-status=active, vm-uuid="5342ee28-9516-43d4-825f-a4547a8c7cdf"}
    ifindex             : 34
    ingress_policing_burst: 0
    ingress_policing_rate: 0
    lacp_current        : []
    link_resets         : 0
    link_speed          : 10000000000
    link_state          : up
    lldp                : {}
    mac                 : []
    mac_in_use          : "da:07:e5:c7:d7:a2"
    mtu                 : 1450
    mtu_request         : []
    name                : "qvo7b272383-92"
    ofport              : 17
    ofport_request      : []
    options             : {}
    other_config        : {}
    statistics          : {collisions=0, rx_bytes=17411, rx_crc_err=0, rx_dropped=0, rx_errors=0, rx_frame_err=0, rx_over_err=0, rx_packets=176, tx_bytes=18682, tx_dropped=0, tx_errors=0, tx_packets=159}
    status              : {driver_name=veth, driver_version="1.0", firmware_version=""}
    type                : ""
    ```

33.	Mostrar os *bridges* e as interfaces configuradas no OpenvSwitch:
    ```
    $ sudo ovs-vsctl show
    c4198c58-dfa5-4d11-8cac-49b7565354b0
        Manager "ptcp:6640:127.0.0.1"
            is_connected: true
        Bridge br-ex
            Controller "tcp:127.0.0.1:6633"
                is_connected: true
            fail_mode: secure
            Port phy-br-ex
                Interface phy-br-ex
                    type: patch
                    options: {peer=int-br-ex}
            Port br-ex
                Interface br-ex
                    type: internal
        Bridge br-tun
            Controller "tcp:127.0.0.1:6633"
                is_connected: true
            fail_mode: secure
            Port br-tun
                Interface br-tun
                    type: internal
            Port patch-int
                Interface patch-int
                    type: patch
                    options: {peer=patch-tun}
        Bridge br-int
            Controller "tcp:127.0.0.1:6633"
                is_connected: true
            fail_mode: secure
            Port patch-tun
                Interface patch-tun
                    type: patch
                    options: {peer=patch-int}
            Port "tap71f41315-e1"
                tag: 1
                Interface "tap71f41315-e1"
                    type: internal
            Port br-int
                Interface br-int
                    type: internal
            Port "qg-215adae6-b3"
                tag: 2
                Interface "qg-215adae6-b3"
                    type: internal
            Port int-br-ex
                Interface int-br-ex
                    type: patch
                    options: {peer=phy-br-ex}
            Port "qr-aa0f0802-9d"
                tag: 1
                Interface "qr-aa0f0802-9d"
                    type: internal
            Port "qr-609c323c-1c"
                tag: 3
                Interface "qr-609c323c-1c"
                    type: internal
            Port "qvo7b272383-92"
                tag: 3
                Interface "qvo7b272383-92"
            Port "qvoc9016286-df"
                tag: 3
                Interface "qvoc9016286-df"
            Port "tap47a2ebc7-b0"
                tag: 3
                Interface "tap47a2ebc7-b0"
                    type: internal
            Port "qr-ed7b4f9d-fb"
                tag: 1
                Interface "qr-ed7b4f9d-fb"
                    type: internal
            Port "qg-f0d36643-4c"
                tag: 2
                Interface "qg-f0d36643-4c"
                    type: internal
        ovs_version: "2.6.1"
    ```
    
34.	Deletar VMs, rede, subrede e roteador
    ```
    $ openstack server remove floating ip vm-1   172.24.4.13
    $ openstack router remove subnet router-fiap subrede-privada
    $ openstack router delete router-fiap
    $ openstack server delete vm-1 vm-2
    $ openstack subnet delete subrede-privada
    ```

35.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de rede
    ![](/cld/openstack/img/neutron1.png)
    ![](/cld/openstack/img/neutron2.png)

    - Criação de subrede
    - Criação de roteador
    ![](/cld/openstack/img/neutron3.png)

    - Assignar interfaces ao roteador
    ![](/cld/openstack/img/neutron4.png)

    - Reservar *floating* IP e associar a instância
    ![](/cld/openstack/img/neutron5.png)

    - Criação de *security group*
    ![](/cld/openstack/img/neutron6.png)

    - Liberar regras no security group

