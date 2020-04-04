# Lab 2 - Docker (continuação)
Mini-nfv is a framework for NFV Orchestration with a general purpose VNF Manager to deploy and operate Virtual Network Functions (VNFs) and Network Services on Mininet. It is based on ETSI MANO Architectural Framework.

Use cases
--------------
In the OpenStack world, Tacker is the project implementing a generic VNFM and NFVO. At the input consumes Tosca-based templates, which are then used to spin up VMs on OpenStack. While it is true that today exist various tools that simplify the deployment of an OpenStack cloud (i.e.: devstack), deploying, configuring and managing OpenStack environments is still a time-consuming process with a considerable learning curve.

On the other hand, Mininet has shown itself as a great tool for agile network/SDN/NFV experimentation. The goal of mini-nfv is to alleviate the developers’ tedious task of setting up a whole service chaining environment and let them focus on their own work (e.g., developing a particular VNF, prototyping, implementing an orchestration algorithm or a customized traffic steering).

On top of that, mini-nfv supports [Jinja2](http://jinja.pocoo.org/docs/2.10/), a full featured and designer-friendly template engine for Python, with an integrated sandboxed execution environment. This way, developers can easily automate the scale-out of vNF deployments and NFV orquestration graphs within the TOSCA templates.


```
ubuntu@ip-172-31-47-198:~$ docker pull mysql
Using default tag: latest
latest: Pulling from library/mysql
c499e6d256d6: Pull complete 
22c4cdf4ea75: Pull complete 
6ff5091a5a30: Pull complete 
2fd3d1af9403: Pull complete 
0d9d26127d1d: Pull complete 
54a67d4e7579: Pull complete 
fe989230d866: Pull complete 
3a808704d40c: Pull complete 
826517d07519: Pull complete 
69cd125db928: Pull complete 
b5c43b8c2879: Pull complete 
1811572b5ea5: Pull complete 
Digest: sha256:b69d0b62d02ee1eba8c7aeb32eba1bb678b6cfa4ccfb211a5d7931c7755dc4a8
Status: Downloaded newer image for mysql:latest
docker.io/library/mysql:latest
```

```
ubuntu@ip-172-31-47-198:~$ sudo apt install mysql-client -y
Reading package lists... Done
Building dependency tree       
Reading state information... Done
The following additional packages will be installed:
  libaio1 mysql-client-5.7 mysql-client-core-5.7 mysql-common
The following NEW packages will be installed:
  libaio1 mysql-client mysql-client-5.7 mysql-client-core-5.7 mysql-common
0 upgraded, 5 newly installed, 0 to remove and 34 not upgraded.
Need to get 8607 kB of archives.
After this operation, 61.8 MB of additional disk space will be used.
Get:1 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 libaio1 amd64 0.3.110-5ubuntu0.1 [6476 B]
Get:2 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client-core-5.7 amd64 5.7.29-0ubuntu0.18.04.1 [6642 kB]
Get:3 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 mysql-common all 5.8+1.0.4 [7308 B]
Get:4 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client-5.7 amd64 5.7.29-0ubuntu0.18.04.1 [1942 kB]
Get:5 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 mysql-client all 5.7.29-0ubuntu0.18.04.1 [9828 B]
Fetched 8607 kB in 0s (37.9 MB/s)      
Selecting previously unselected package libaio1:amd64.
(Reading database ... 84272 files and directories currently installed.)
Preparing to unpack .../libaio1_0.3.110-5ubuntu0.1_amd64.deb ...
Unpacking libaio1:amd64 (0.3.110-5ubuntu0.1) ...
Selecting previously unselected package mysql-client-core-5.7.
Preparing to unpack .../mysql-client-core-5.7_5.7.29-0ubuntu0.18.04.1_amd64.deb ...
Unpacking mysql-client-core-5.7 (5.7.29-0ubuntu0.18.04.1) ...
Selecting previously unselected package mysql-common.
Preparing to unpack .../mysql-common_5.8+1.0.4_all.deb ...
Unpacking mysql-common (5.8+1.0.4) ...
Selecting previously unselected package mysql-client-5.7.
Preparing to unpack .../mysql-client-5.7_5.7.29-0ubuntu0.18.04.1_amd64.deb ...
Unpacking mysql-client-5.7 (5.7.29-0ubuntu0.18.04.1) ...
Selecting previously unselected package mysql-client.
Preparing to unpack .../mysql-client_5.7.29-0ubuntu0.18.04.1_all.deb ...
Unpacking mysql-client (5.7.29-0ubuntu0.18.04.1) ...
Setting up mysql-common (5.8+1.0.4) ...
update-alternatives: using /etc/mysql/my.cnf.fallback to provide /etc/mysql/my.cnf (my.cnf) in auto mode
Setting up libaio1:amd64 (0.3.110-5ubuntu0.1) ...
Setting up mysql-client-core-5.7 (5.7.29-0ubuntu0.18.04.1) ...
Setting up mysql-client-5.7 (5.7.29-0ubuntu0.18.04.1) ...
Setting up mysql-client (5.7.29-0ubuntu0.18.04.1) ...
Processing triggers for man-db (2.8.3-2ubuntu0.1) ...
Processing triggers for libc-bin (2.27-3ubuntu1) ...
```

```
ubuntu@ip-172-31-47-198:~$ docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                 NAMES
66696bdd281f        mysql               "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes        3306/tcp, 33060/tcp   funny_yonath
```
