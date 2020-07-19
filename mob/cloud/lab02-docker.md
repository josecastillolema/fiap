# Lab 2 - Docker

## Criando a instancia
Usaremos a imagem oficial `Ubuntu Linux 18.04` para aprender alguns conceitos importantes do Docker:
 - instalação
 - customização de imagens via Dockerfile
 - upload de imagens no [Dockerhub](https://hub.docker.com/)
 
1. Instalação do Docker

    a. Atualização dos repositórios
    ```
    $ sudo apt update
    Hit:1 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic InRelease
    Get:2 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates InRelease [88.7 kB]
    Get:3 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-backports InRelease [74.6 kB]
    Get:4 http://security.ubuntu.com/ubuntu bionic-security InRelease [88.7 kB]              
    Get:5 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 Packages [8570 kB]
    Get:6 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe Translation-en [4941 kB]     
    Get:7 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/multiverse amd64 Packages [151 kB]
    Get:8 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/multiverse Translation-en [108 kB]
    Get:9 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 Packages [1011 kB]
    Get:10 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main Translation-en [341 kB]
    Get:11 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/restricted amd64 Packages [76.7 kB]
    Get:12 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/restricted Translation-en [17.1 kB]
    Get:13 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/universe amd64 Packages [1092 kB]
    Get:14 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/universe Translation-en [340 kB]
    Get:15 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/multiverse amd64 Packages [11.5 kB]
    Get:16 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/multiverse Translation-en [4832 B]
    Get:17 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-backports/main amd64 Packages [7516 B]
    Get:18 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-backports/main Translation-en [4764 B]
    Get:19 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-backports/universe amd64 Packages [7736 B]
    Get:20 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-backports/universe Translation-en [4588 B]
    Get:21 http://security.ubuntu.com/ubuntu bionic-security/main amd64 Packages [783 kB] 
    Get:22 http://security.ubuntu.com/ubuntu bionic-security/main Translation-en [247 kB]
    Get:23 http://security.ubuntu.com/ubuntu bionic-security/restricted amd64 Packages [67.8 kB]
    Get:24 http://security.ubuntu.com/ubuntu bionic-security/restricted Translation-en [15.0 kB]
    Get:25 http://security.ubuntu.com/ubuntu bionic-security/universe amd64 Packages [679 kB]
    Get:26 http://security.ubuntu.com/ubuntu bionic-security/universe Translation-en [225 kB]
    Get:27 http://security.ubuntu.com/ubuntu bionic-security/multiverse amd64 Packages [7908 B]
    Get:28 http://security.ubuntu.com/ubuntu bionic-security/multiverse Translation-en [2816 B]
    Fetched 19.0 MB in 4s (4815 kB/s)                               
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    29 packages can be upgraded. Run 'apt list --upgradable' to see them.
    ```
    
    b. Instalação dos pacotes
    ```
    $ sudo apt install docker.io
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    The following additional packages will be installed:
      bridge-utils cgroupfs-mount containerd pigz runc ubuntu-fan
    Suggested packages:
      ifupdown aufs-tools debootstrap docker-doc rinse zfs-fuse | zfsutils
    The following NEW packages will be installed:
      bridge-utils cgroupfs-mount containerd docker.io pigz runc ubuntu-fan
    0 upgraded, 7 newly installed, 0 to remove and 29 not upgraded.
    Need to get 63.8 MB of archives.
    After this operation, 319 MB of additional disk space will be used.
    Do you want to continue? [Y/n] y
    Get:1 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 pigz amd64 2.4-1 [57.4 kB]
    Get:2 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 bridge-utils amd64 1.5-15ubuntu1 [30.1 kB]
    Get:3 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 cgroupfs-mount all 1.4 [6320 B]
    Get:4 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/universe amd64 runc amd64 1.0.0~rc10-0ubuntu1~18.04.2 [2000 kB]
    Get:5 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/universe amd64 containerd amd64 1.3.3-0ubuntu1~18.04.2 [21.7 MB]
    Get:6 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/universe amd64 docker.io amd64 19.03.6-0ubuntu1~18.04.1 [39.9 MB]
    Get:7 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 ubuntu-fan all 0.12.10 [34.7 kB]
    Fetched 63.8 MB in 2s (41.7 MB/s)
    Preconfiguring packages ...
    Selecting previously unselected package pigz.
    (Reading database ... 57065 files and directories currently installed.)
    Preparing to unpack .../0-pigz_2.4-1_amd64.deb ...
    Unpacking pigz (2.4-1) ...
    Selecting previously unselected package bridge-utils.
    Preparing to unpack .../1-bridge-utils_1.5-15ubuntu1_amd64.deb ...
    Unpacking bridge-utils (1.5-15ubuntu1) ...
    Selecting previously unselected package cgroupfs-mount.
    Preparing to unpack .../2-cgroupfs-mount_1.4_all.deb ...
    Unpacking cgroupfs-mount (1.4) ...
    Selecting previously unselected package runc.
    Preparing to unpack .../3-runc_1.0.0~rc10-0ubuntu1~18.04.2_amd64.deb ...
    Unpacking runc (1.0.0~rc10-0ubuntu1~18.04.2) ...
    Selecting previously unselected package containerd.
    Preparing to unpack .../4-containerd_1.3.3-0ubuntu1~18.04.2_amd64.deb ...
    Unpacking containerd (1.3.3-0ubuntu1~18.04.2) ...
    Selecting previously unselected package docker.io.
    Preparing to unpack .../5-docker.io_19.03.6-0ubuntu1~18.04.1_amd64.deb ...
    Unpacking docker.io (19.03.6-0ubuntu1~18.04.1) ...
    Selecting previously unselected package ubuntu-fan.
    Preparing to unpack .../6-ubuntu-fan_0.12.10_all.deb ...
    Unpacking ubuntu-fan (0.12.10) ...
    Setting up runc (1.0.0~rc10-0ubuntu1~18.04.2) ...
    Setting up cgroupfs-mount (1.4) ...
    Setting up containerd (1.3.3-0ubuntu1~18.04.2) ...
    Created symlink /etc/systemd/system/multi-user.target.wants/containerd.service → /lib/systemd/system/containerd.service.
    Setting up bridge-utils (1.5-15ubuntu1) ...
    Setting up ubuntu-fan (0.12.10) ...
    Created symlink /etc/systemd/system/multi-user.target.wants/ubuntu-fan.service → /lib/systemd/system/ubuntu-fan.service.
    Setting up pigz (2.4-1) ...
    Setting up docker.io (19.03.6-0ubuntu1~18.04.1) ...
    Adding group `docker' (GID 115) ...
    Done.
    Created symlink /etc/systemd/system/sockets.target.wants/docker.socket → /lib/systemd/system/docker.socket.
    docker.service is a disabled or a static unit, not starting it.
    Processing triggers for systemd (237-3ubuntu10.41) ...
    Processing triggers for man-db (2.8.3-2ubuntu0.1) ...
    Processing triggers for ureadahead (0.100.0-21) ...
    ```
    
    c. Conferir que o usuário não faz parte do grupo `docker`, e consecuentemente nao tem permissão para rodar comandos `docker`:
    ```
    $ groups
    ubuntu adm dialout cdrom floppy sudo audio dip video plugdev lxd netdev
    ```

    d. Adicionar o usuário (`ubuntu`) ao grupo `docker`"
    ```
    $ sudo usermod -aG docker ubuntu
    ```

    e. Reiniciar a VM para que as mudanças de grupo sejam aplicadas:
    ```
    $ sudo reboot
    Connection to ec2-18-210-19-170.compute-1.amazonaws.com closed by remote host.
    Connection to ec2-18-210-19-170.compute-1.amazonaws.com closed.
    ```

    f. Após o reboot, confirmar que o usuário pertence ao grupo `docker`:
    ```
    $ groups
    ubuntu adm dialout cdrom floppy sudo audio dip video plugdev lxd netdev docker
    ```

     g. Rodar um docker version para validar a instalação, e conferir que é mostrada tanto a versão do cliente quanto a do servidor:
     ```
     $ docker version
     Client:
      Version:           19.03.6
      API version:       1.40
      Go version:        go1.12.17
      Git commit:        369ce74a3c
      Built:             Fri Feb 28 23:45:43 2020
      OS/Arch:           linux/amd64
      Experimental:      false

     Server:
      Engine:
       Version:          19.03.6
       API version:      1.40 (minimum version 1.12)
       Go version:       go1.12.17
       Git commit:       369ce74a3c
       Built:            Wed Feb 19 01:06:16 2020
       OS/Arch:          linux/amd64
       Experimental:     false
      containerd:
       Version:          1.3.3-0ubuntu1~18.04.2
       GitCommit:        
      runc:
       Version:          spec: 1.0.1-dev
       GitCommit:        
      docker-init:
       Version:          0.18.0
       GitCommit:
     ```


2. Listar as imagens do repositório local (o catálogo deveria estar vazio, pois não baixamos nenhuma imagem ainda):
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ```
    
3. Buscar imagens dentro do catálogo do [Dockerhub](https://hub.docker.com/):
    ```
    $ docker search mongodb
    NAME                                DESCRIPTION                                     STARS               OFFICIAL            AUTOMATED
    mongo                               MongoDB document databases provide high avai…   7041                [OK]                
    mongo-express                       Web-based MongoDB admin interface, written w…   735                 [OK]                
    tutum/mongodb                       MongoDB Docker image – listens in port 27017…   229                                     [OK]
    bitnami/mongodb                     Bitnami MongoDB Docker Image                    123                                     [OK]
    frodenas/mongodb                    A Docker Image for MongoDB                      18                                      [OK]
    centos/mongodb-32-centos7           MongoDB NoSQL database server                   8                                       
    webhippie/mongodb                   Docker images for MongoDB                       7                                       [OK]
    centos/mongodb-26-centos7           MongoDB NoSQL database server                   5                                       
    centos/mongodb-36-centos7           MongoDB NoSQL database server                   5                                       
    eses/mongodb_exporter               mongodb exporter for prometheus                 4                                       [OK]
    neowaylabs/mongodb-mms-agent        This Docker image with MongoDB Monitoring Ag…   4                                       [OK]
    centos/mongodb-34-centos7           MongoDB NoSQL database server                   3                                       
    quadstingray/mongodb                MongoDB with Memory and User Settings           3                                       [OK]
    tozd/mongodb                        Base image for MongoDB server.                  2                                       [OK]
    mongodbsap/mongodbdocker                                                            2                                       
    zadki3l/mongodb-oplog               Simple mongodb image with single-node replic…   2                                       [OK]
    ssalaues/mongodb-exporter           MongoDB Replicaset Prometheus Compatible Met…   2                                       
    xogroup/mongodb_backup_gdrive       Docker image to create a MongoDB database ba…   1                                       [OK]
    bitnami/mongodb-exporter                                                            1                                       
    openshift/mongodb-24-centos7        DEPRECATED: A Centos7 based MongoDB v2.4 ima…   1                                       
    ansibleplaybookbundle/mongodb-apb   An APB to deploy MongoDB.                       1                                       [OK]
    targetprocess/mongodb_exporter      MongoDB exporter for prometheus                 0                                       [OK]
    gebele/mongodb                      mongodb                                         0                                       [OK]
    phenompeople/mongodb                 MongoDB is an open-source, document databas…   0                                       [OK]
    astronomerio/mongodb-source         Mongodb source.                                 0                                       [OK]
    ```
 
4. Fazer o download (`pull`) da imagem do Ubuntu no repositório local:
    ```
    $ docker pull ubuntu
    Using default tag: latest
    latest: Pulling from library/ubuntu
    692c352adcf2: Pull complete 
    97058a342707: Pull complete 
    2821b8e766f4: Pull complete 
    4e643cc37772: Pull complete 
    Digest: sha256:55cd38b70425947db71112eb5dddfa3aa3e3ce307754a3df2269069d2278ce47
    Status: Downloaded newer image for ubuntu:latest
    docker.io/library/ubuntu:latest
    ```
    
5. Listar as imagens novamente, conferir que existe a imagem `ubuntu`:
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ubuntu              latest              adafef2e596e        11 days ago         73.9MB
    ```
    
6. Deletar a imagem (opcional):
    ```
    $ docker image rm ubuntu
    ```
    
7. Rodar um comando de exemplo (`hostname`) dentro do container:
    ```
    $ docker run ubuntu hostname
    c293c1989a56
    ```

8. Medir o tempo do comando anterior:
    ```
    $ time docker run ubuntu hostname
    7aa02808ccfc

    real	0m0.812s
    user	0m0.023s
    sys	0m0.027s
    ```
    Note-se que em menos de um segundo:
    - Docker criou o container
    - Rodou o comando `hostname` nele
    - Printou a sainda
    - Deletou o container
    
9. Conferir que tanto container quanto o *host* compartilham o Kernel:
    ```
    $ uname -a
    Linux ip-172-31-60-180 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux

    $ docker run ubuntu uname -a
    Linux de0407ee790f 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux
    ```
