# Lab 1 - Docker

Usaremos a imagem oficial `Ubuntu Linux 18.04` para aprender alguns conceitos importantes do Docker:
 - instalação
 - customização de imagens via Dockerfile
 - upload de imagens no [DockerHub](https://hub.docker.com/)
 
Vamos trabalhar com dois terminais abertos (**T1** e **T2**).

## Instalação
 
1. **[T1]** Instalação do Docker

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

    d. Adicionar o usuário (`ubuntu`) ao grupo `docker`:
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

     g. Rodar um `docker version` para validar a instalação, e conferir que é mostrada tanto a versão do cliente **quanto a do servidor**:
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

## Primeiros passos

2. **[T1]** Listar as imagens do repositório local (o catálogo deveria estar vazio, pois não baixamos nenhuma imagem ainda):
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ```
    
3. **[T1]** Buscar imagens dentro do catálogo do [DockerHub](https://hub.docker.com/):
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
 
4. **[T1]** Fazer o *download* (`pull`) da imagem do Ubuntu no repositório local:
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
    
5. **[T1]** Listar as imagens novamente, conferir que existe a imagem `ubuntu`:
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ubuntu              latest              adafef2e596e        11 days ago         73.9MB
    ```
    
6. **[T1]** Deletar a imagem (opcional):
    ```
    $ docker image rm ubuntu
    ```
    
7. **[T1]** Rodar um comando de exemplo (`hostname`) dentro do container:
    ```
    $ docker run ubuntu hostname
    c293c1989a56
    ```

8. **[T1]** Medir o tempo do comando anterior:
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
    
9. **[T1]** Conferir que tanto container quanto o *host* compartilham o Kernel:
    ```
    $ uname -a
    Linux ip-172-31-60-180 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux

    $ docker run ubuntu uname -a
    Linux de0407ee790f 5.3.0-1023-aws #25~18.04.1-Ubuntu SMP Fri Jun 5 15:18:30 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux
    ```

10. **[T1]** Executar a imagem `ubuntu` em modo interativo. Observe-se que o `prompt` muda quando logamos no container: usuário `root` com hostname `5b83d8b5b521` (o ID do container em este caso).
    ```
    $ docker run -it ubuntu
    root@d8924e5138b3:/#
    ```
    
11. **[T2]** **Sem sair do container no 1o terminal**, listar os containers em execução **no 2o terminal**:
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
    d8924e5138b3        ubuntu              "/bin/bash"         14 seconds ago      Up 13 seconds                           xenodochial_allen
    ```

12. **[T1]** Continuando no 1o terminal, criar um arquivo ainda dentro do container e sair do container:
    ```
    root@d8924e5138b3:/# touch meuArquivo
    
    root@d8924e5138b3:/# ls
    bin   dev  home  lib32  libx32  meuArquivo  opt   root  sbin  sys  usr
    boot  etc  lib   lib64  media   mnt         proc  run   srv   tmp  var

    root@d8924e5138b3:/# exit
    ```
    
13. **[T1]** Conferir que o container não está mais em execução:
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
    ```
    
14. **[T1]** Rodar o container novamente, e confirmar que o arquivo criado não existe mais. **Containers são efêmeros**, não armazenam nenhum tipo de mudança, sejam arquivos, dados, softwares instalados, etc.:
    ```
    $ docker run -it ubuntu
    root@5b83d8b5b521:/# ls
    bin   dev  home  lib32  libx32  mnt  proc  run   srv  tmp  var
    boot  etc  lib   lib64  media   opt  root  sbin  sys  usr

    root@5b83d8b5b521:/# ls meuArquivo
    ls: meuArquivo: No such file or directory
    ```

## Customização de imagens

### Via `docker commit`

15. Customização de imagens via `docker commit`.

    Vamos criar uma imagem customizada instalando algum *software*, por exemplo o `nmap` (um *scanner* de portas).

    a. **[T1]** **Sem sair do container**, atualizar os repositórios:
    ```
    root@5b83d8b5b521:/# apt update
    Get:1 http://security.ubuntu.com/ubuntu focal-security InRelease [107 kB]
    Get:2 http://archive.ubuntu.com/ubuntu focal InRelease [265 kB]
    Get:3 http://security.ubuntu.com/ubuntu focal-security/universe amd64 Packages [45.2 kB]
    Get:4 http://archive.ubuntu.com/ubuntu focal-updates InRelease [111 kB]
    Get:5 http://security.ubuntu.com/ubuntu focal-security/restricted amd64 Packages [33.9 kB]
    Get:6 http://archive.ubuntu.com/ubuntu focal-backports InRelease [98.3 kB]
    Get:7 http://security.ubuntu.com/ubuntu focal-security/main amd64 Packages [165 kB]
    Get:8 http://security.ubuntu.com/ubuntu focal-security/multiverse amd64 Packages [1078 B]
    Get:9 http://archive.ubuntu.com/ubuntu focal/multiverse amd64 Packages [177 kB]
    Get:10 http://archive.ubuntu.com/ubuntu focal/main amd64 Packages [1275 kB]
    Get:11 http://archive.ubuntu.com/ubuntu focal/restricted amd64 Packages [33.4 kB]
    Get:12 http://archive.ubuntu.com/ubuntu focal/universe amd64 Packages [11.3 MB]
    Get:13 http://archive.ubuntu.com/ubuntu focal-updates/universe amd64 Packages [165 kB]
    Get:14 http://archive.ubuntu.com/ubuntu focal-updates/restricted amd64 Packages [33.9 kB]
    Get:15 http://archive.ubuntu.com/ubuntu focal-updates/main amd64 Packages [330 kB]
    Get:16 http://archive.ubuntu.com/ubuntu focal-updates/multiverse amd64 Packages [4202 B]
    Get:17 http://archive.ubuntu.com/ubuntu focal-backports/universe amd64 Packages [3209 B]
    Fetched 14.2 MB in 2s (6179 kB/s)                      
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    1 package can be upgraded. Run 'apt list --upgradable' to see it.
    ```
    
    b. **[T1]** Instalar o pacote `nmap`. O flag `-y` pula a pregunta de confirmação:
    ```
    root@5b83d8b5b521:/# apt install -y nmap
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    The following additional packages will be installed:
      libblas3 liblinear4 liblua5.3-0 libpcap0.8 libssl1.1 lua-lpeg nmap-common
    Suggested packages:
      liblinear-tools liblinear-dev ncat ndiff zenmap
    The following NEW packages will be installed:
      libblas3 liblinear4 liblua5.3-0 libpcap0.8 libssl1.1 lua-lpeg nmap nmap-common
    0 upgraded, 8 newly installed, 0 to remove and 1 not upgraded.
    Need to get 7115 kB of archives.
    After this operation, 31.3 MB of additional disk space will be used.
    Get:1 http://archive.ubuntu.com/ubuntu focal/main amd64 libssl1.1 amd64 1.1.1f-1ubuntu2 [1318 kB]
    Get:2 http://archive.ubuntu.com/ubuntu focal/main amd64 libpcap0.8 amd64 1.9.1-3 [128 kB]
    Get:3 http://archive.ubuntu.com/ubuntu focal/main amd64 libblas3 amd64 3.9.0-1build1 [142 kB]
    Get:4 http://archive.ubuntu.com/ubuntu focal/universe amd64 liblinear4 amd64 2.3.0+dfsg-3build1 [41.7 kB]
    Get:5 http://archive.ubuntu.com/ubuntu focal/main amd64 liblua5.3-0 amd64 5.3.3-1.1ubuntu2 [116 kB]
    Get:6 http://archive.ubuntu.com/ubuntu focal/universe amd64 lua-lpeg amd64 1.0.2-1 [31.4 kB]
    Get:7 http://archive.ubuntu.com/ubuntu focal/universe amd64 nmap-common all 7.80+dfsg1-2build1 [3676 kB]
    Get:8 http://archive.ubuntu.com/ubuntu focal/universe amd64 nmap amd64 7.80+dfsg1-2build1 [1662 kB]
    Fetched 7115 kB in 1s (7120 kB/s)
    debconf: delaying package configuration, since apt-utils is not installed
    Selecting previously unselected package libssl1.1:amd64.
    (Reading database ... 4122 files and directories currently installed.)
    Preparing to unpack .../0-libssl1.1_1.1.1f-1ubuntu2_amd64.deb ...
    Unpacking libssl1.1:amd64 (1.1.1f-1ubuntu2) ...
    Selecting previously unselected package libpcap0.8:amd64.
    Preparing to unpack .../1-libpcap0.8_1.9.1-3_amd64.deb ...
    Unpacking libpcap0.8:amd64 (1.9.1-3) ...
    Selecting previously unselected package libblas3:amd64.
    Preparing to unpack .../2-libblas3_3.9.0-1build1_amd64.deb ...
    Unpacking libblas3:amd64 (3.9.0-1build1) ...
    Selecting previously unselected package liblinear4:amd64.
    Preparing to unpack .../3-liblinear4_2.3.0+dfsg-3build1_amd64.deb ...
    Unpacking liblinear4:amd64 (2.3.0+dfsg-3build1) ...
    Selecting previously unselected package liblua5.3-0:amd64.
    Preparing to unpack .../4-liblua5.3-0_5.3.3-1.1ubuntu2_amd64.deb ...
    Unpacking liblua5.3-0:amd64 (5.3.3-1.1ubuntu2) ...
    Selecting previously unselected package lua-lpeg:amd64.
    Preparing to unpack .../5-lua-lpeg_1.0.2-1_amd64.deb ...
    Unpacking lua-lpeg:amd64 (1.0.2-1) ...
    Selecting previously unselected package nmap-common.
    Preparing to unpack .../6-nmap-common_7.80+dfsg1-2build1_all.deb ...
    Unpacking nmap-common (7.80+dfsg1-2build1) ...
    Selecting previously unselected package nmap.
    Preparing to unpack .../7-nmap_7.80+dfsg1-2build1_amd64.deb ...
    Unpacking nmap (7.80+dfsg1-2build1) ...
    Setting up lua-lpeg:amd64 (1.0.2-1) ...
    Setting up libssl1.1:amd64 (1.1.1f-1ubuntu2) ...
    debconf: unable to initialize frontend: Dialog
    debconf: (No usable dialog-like program is installed, so the dialog based frontend cannot be used. at /usr/share/perl5/Debconf/FrontEnd/Dialog.pm line 76.)
    debconf: falling back to frontend: Readline
    debconf: unable to initialize frontend: Readline
    debconf: (Can't locate Term/ReadLine.pm in @INC (you may need to install the Term::ReadLine module) (@INC contains: /etc/perl /usr/local/lib/x86_64-linux-gnu/perl/5.30.0 /usr/local/share/perl/5.30.0 /usr/lib/x86_64-linux-gnu/perl5/5.30 /usr/share/perl5 /usr/lib/x86_64-linux-gnu/perl/5.30 /usr/share/perl/5.30 /usr/local/lib/site_perl /usr/lib/x86_64-linux-gnu/perl-base) at /usr/share/perl5/Debconf/FrontEnd/Readline.pm line 7.)
    debconf: falling back to frontend: Teletype
    Setting up libblas3:amd64 (3.9.0-1build1) ...
    update-alternatives: using /usr/lib/x86_64-linux-gnu/blas/libblas.so.3 to provide /usr/lib/x86_64-linux-gnu/libblas.so.3 (libblas.so.3-x86_64-linux-gnu) in auto mode
    Setting up libpcap0.8:amd64 (1.9.1-3) ...
    Setting up nmap-common (7.80+dfsg1-2build1) ...
    Setting up liblua5.3-0:amd64 (5.3.3-1.1ubuntu2) ...
    Setting up liblinear4:amd64 (2.3.0+dfsg-3build1) ...
    Setting up nmap (7.80+dfsg1-2build1) ...
    Processing triggers for libc-bin (2.31-0ubuntu9) ...
    ```

    c. **[T1]** Conferir a versão instalada:
    ```
    root@5b83d8b5b521:/# nmap --version
    Nmap version 7.80 ( https://nmap.org )
    Platform: x86_64-pc-linux-gnu
    Compiled with: liblua-5.3.3 openssl-1.1.1d nmap-libssh2-1.8.2 libz-1.2.11 libpcre-8.39 libpcap-1.9.1 nmap-libdnet-1.12 ipv6
    Compiled without:
    Available nsock engines: epoll poll select
    ```
    
    d. **[T2]** No 2o terminal, confirmar o ID do container em execução (no qual acabamos de instalar o `nmap`):
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
    5b83d8b5b521        ubuntu              "/bin/bash"         18 minutes ago      Up 18 minutes                           compassionate_edison
    ```
    
    e. **[T2]** Criar uma nova imagem (`ubuntu_com_nmap`) a partir do container:
    ```
    $ docker commit 5b8 ubuntu_com_nmap
    sha256:287d2c84024a50ba13c9d8304d57df853feea9b3dd9df785313111480a84eecc
    ```
    
    f. Confirmar a criação da imagem (com um tamanho maior a imagem original):
    ```
    $ docker images
    REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
    ubuntu_com_nmap     latest              287d2c84024a        7 seconds ago       127MB
    ubuntu              latest              adafef2e596e        11 days ago         73.9MB
    ```
    
    g. Confirmar que na nova imagem, tem de fato o `nmap` instalado:
    ```
    $ docker run ubuntu_com_nmap nmap --version
    Nmap version 7.80 ( https://nmap.org )
    Platform: x86_64-pc-linux-gnu
    Compiled with: liblua-5.3.3 openssl-1.1.1d nmap-libssh2-1.8.2 libz-1.2.11 libpcre-8.39 libpcap-1.9.1 nmap-libdnet-1.12 ipv6
    Compiled without:
    Available nsock engines: epoll poll select
    ```
    
    h. Confirmar que a imagem original não foi alterada e não tem o `nmap` instalado:
    ```
    $ docker run ubuntu nmap --version
    docker: Error response from daemon: OCI runtime create failed: container_linux.go:349: starting container process caused "exec: \"nmap\": executable file not found in $PATH": unknown.
    ```

### Via `Dockerfile`

16. Customização de imagens via **Dockerfile**. Este é o metodo recomendado para customizar imagens, pois é mais reproduzível que `docker commit`.

    a. Criar o arquivo `Dockerfile` com o seguinte conteúdo:
    ```
    FROM ubuntu
    
    MAINTAINER Jose Castillo <profjose.lema@fiap.com.br>
    
    RUN apt-get update
    RUN apt-get install -y nmap
    ```
    
    b. "Compilar" o `Dockerfile`:
    ```
    $ docker build -t ubuntu-com-nmap-viadockerfile .
    Sending build context to Docker daemon  15.87kB
    Step 1/4 : FROM ubuntu
     ---> adafef2e596e
    Step 2/4 : MAINTAINER Jose Castillo
     ---> Running in d3894b0f5f25
    Removing intermediate container d3894b0f5f25
     ---> 94aab8c83039
    Step 3/4 : RUN apt-get update
     ---> Running in 4ea03e5d7f32
    Get:1 http://security.ubuntu.com/ubuntu focal-security InRelease [107 kB]
    Get:2 http://security.ubuntu.com/ubuntu focal-security/multiverse amd64 Packages [1078 B]
    Get:3 http://security.ubuntu.com/ubuntu focal-security/universe amd64 Packages [45.2 kB]
    Get:4 http://security.ubuntu.com/ubuntu focal-security/restricted amd64 Packages [33.9 kB]
    Get:5 http://security.ubuntu.com/ubuntu focal-security/main amd64 Packages [165 kB]
    Get:6 http://archive.ubuntu.com/ubuntu focal InRelease [265 kB]
    Get:7 http://archive.ubuntu.com/ubuntu focal-updates InRelease [111 kB]
    Get:8 http://archive.ubuntu.com/ubuntu focal-backports InRelease [98.3 kB]
    Get:9 http://archive.ubuntu.com/ubuntu focal/multiverse amd64 Packages [177 kB]
    Get:10 http://archive.ubuntu.com/ubuntu focal/universe amd64 Packages [11.3 MB]
    Get:11 http://archive.ubuntu.com/ubuntu focal/main amd64 Packages [1275 kB]
    Get:12 http://archive.ubuntu.com/ubuntu focal/restricted amd64 Packages [33.4 kB]
    Get:13 http://archive.ubuntu.com/ubuntu focal-updates/universe amd64 Packages [165 kB]
    Get:14 http://archive.ubuntu.com/ubuntu focal-updates/multiverse amd64 Packages [4202 B]
    Get:15 http://archive.ubuntu.com/ubuntu focal-updates/main amd64 Packages [329 kB]
    Get:16 http://archive.ubuntu.com/ubuntu focal-updates/restricted amd64 Packages [33.9 kB]
    Get:17 http://archive.ubuntu.com/ubuntu focal-backports/universe amd64 Packages [3209 B]
    Fetched 14.2 MB in 2s (5775 kB/s)
    Reading package lists...
    Removing intermediate container 4ea03e5d7f32
     ---> 3bf3668ec12d
    Step 4/4 : RUN apt-get install -y nmap
     ---> Running in 057b00bbe74b
    Reading package lists...
    Building dependency tree...
    Reading state information...
    The following additional packages will be installed:
      libblas3 liblinear4 liblua5.3-0 libpcap0.8 libssl1.1 lua-lpeg nmap-common
    Suggested packages:
      liblinear-tools liblinear-dev ncat ndiff zenmap
    The following NEW packages will be installed:
      libblas3 liblinear4 liblua5.3-0 libpcap0.8 libssl1.1 lua-lpeg nmap
      nmap-common
    0 upgraded, 8 newly installed, 0 to remove and 1 not upgraded.
    Need to get 7115 kB of archives.
    After this operation, 31.3 MB of additional disk space will be used.
    Get:1 http://archive.ubuntu.com/ubuntu focal/main amd64 libssl1.1 amd64 1.1.1f-1ubuntu2 [1318 kB]
    Get:2 http://archive.ubuntu.com/ubuntu focal/main amd64 libpcap0.8 amd64 1.9.1-3 [128 kB]
    Get:3 http://archive.ubuntu.com/ubuntu focal/main amd64 libblas3 amd64 3.9.0-1build1 [142 kB]
    Get:4 http://archive.ubuntu.com/ubuntu focal/universe amd64 liblinear4 amd64 2.3.0+dfsg-3build1 [41.7 kB]
    Get:5 http://archive.ubuntu.com/ubuntu focal/main amd64 liblua5.3-0 amd64 5.3.3-1.1ubuntu2 [116 kB]
    Get:6 http://archive.ubuntu.com/ubuntu focal/universe amd64 lua-lpeg amd64 1.0.2-1 [31.4 kB]
    Get:7 http://archive.ubuntu.com/ubuntu focal/universe amd64 nmap-common all 7.80+dfsg1-2build1 [3676 kB]
    Get:8 http://archive.ubuntu.com/ubuntu focal/universe amd64 nmap amd64 7.80+dfsg1-2build1 [1662 kB]
    debconf: delaying package configuration, since apt-utils is not installed
    Fetched 7115 kB in 1s (7096 kB/s)
    Selecting previously unselected package libssl1.1:amd64.
    (Reading database ... 4122 files and directories currently installed.)
    Preparing to unpack .../0-libssl1.1_1.1.1f-1ubuntu2_amd64.deb ...
    Unpacking libssl1.1:amd64 (1.1.1f-1ubuntu2) ...
    Selecting previously unselected package libpcap0.8:amd64.
    Preparing to unpack .../1-libpcap0.8_1.9.1-3_amd64.deb ...
    Unpacking libpcap0.8:amd64 (1.9.1-3) ...
    Selecting previously unselected package libblas3:amd64.
    Preparing to unpack .../2-libblas3_3.9.0-1build1_amd64.deb ...
    Unpacking libblas3:amd64 (3.9.0-1build1) ...
    Selecting previously unselected package liblinear4:amd64.
    Preparing to unpack .../3-liblinear4_2.3.0+dfsg-3build1_amd64.deb ...
    Unpacking liblinear4:amd64 (2.3.0+dfsg-3build1) ...
    Selecting previously unselected package liblua5.3-0:amd64.
    Preparing to unpack .../4-liblua5.3-0_5.3.3-1.1ubuntu2_amd64.deb ...
    Unpacking liblua5.3-0:amd64 (5.3.3-1.1ubuntu2) ...
    Selecting previously unselected package lua-lpeg:amd64.
    Preparing to unpack .../5-lua-lpeg_1.0.2-1_amd64.deb ...
    Unpacking lua-lpeg:amd64 (1.0.2-1) ...
    Selecting previously unselected package nmap-common.
    Preparing to unpack .../6-nmap-common_7.80+dfsg1-2build1_all.deb ...
    Unpacking nmap-common (7.80+dfsg1-2build1) ...
    Selecting previously unselected package nmap.
    Preparing to unpack .../7-nmap_7.80+dfsg1-2build1_amd64.deb ...
    Unpacking nmap (7.80+dfsg1-2build1) ...
    Setting up lua-lpeg:amd64 (1.0.2-1) ...
    Setting up libssl1.1:amd64 (1.1.1f-1ubuntu2) ...
    debconf: unable to initialize frontend: Dialog
    debconf: (TERM is not set, so the dialog frontend is not usable.)
    debconf: falling back to frontend: Readline
    debconf: unable to initialize frontend: Readline
    debconf: (Can't locate Term/ReadLine.pm in @INC (you may need to install the Term::ReadLine module) (@INC contains: /etc/perl /usr/local/lib/x86_64-linux-gnu/perl/5.30.0 /usr/local/share/perl/5.30.0 /usr/lib/x86_64-linux-gnu/perl5/5.30 /usr/share/perl5 /usr/lib/x86_64-linux-gnu/perl/5.30 /usr/share/perl/5.30 /usr/local/lib/site_perl /usr/lib/x86_64-linux-gnu/perl-base) at /usr/share/perl5/Debconf/FrontEnd/Readline.pm line 7.)
    debconf: falling back to frontend: Teletype
    Setting up libblas3:amd64 (3.9.0-1build1) ...
    update-alternatives: using /usr/lib/x86_64-linux-gnu/blas/libblas.so.3 to provide /usr/lib/x86_64-linux-gnu/libblas.so.3 (libblas.so.3-x86_64-linux-gnu) in auto mode
    Setting up libpcap0.8:amd64 (1.9.1-3) ...
    Setting up nmap-common (7.80+dfsg1-2build1) ...
    Setting up liblua5.3-0:amd64 (5.3.3-1.1ubuntu2) ...
    Setting up liblinear4:amd64 (2.3.0+dfsg-3build1) ...
    Setting up nmap (7.80+dfsg1-2build1) ...
    Processing triggers for libc-bin (2.31-0ubuntu9) ...
    Removing intermediate container 057b00bbe74b
     ---> 4647bd58aa0e
    Successfully built 4647bd58aa0e
    Successfully tagged ubuntu-com-nmap-viadockerfile:latest
    ```
    
    c. Conferir que a nova imagem foi criada (e tem o mesmo tamanho que a imagem criada via `docker commit`):
    ```
    $ docker images
    REPOSITORY                      TAG                 IMAGE ID            CREATED             SIZE
    ubuntu-com-nmap-viadockerfile   latest              4647bd58aa0e        13 seconds ago      127MB
    ubuntu_com_nmap                 latest              287d2c84024a        18 minutes ago      127MB
    ubuntu                          latest              adafef2e596e        11 days ago         73.9MB
    ```
    
    d. Testar a nova imagem:
    ```
    $ docker run ubuntu_com_nmap-viadockerfile nmap --version
    Nmap version 7.80 ( https://nmap.org )
    Platform: x86_64-pc-linux-gnu
    Compiled with: liblua-5.3.3 openssl-1.1.1d nmap-libssh2-1.8.2 libz-1.2.11 libpcre-8.39 libpcap-1.9.1 nmap-libdnet-1.12 ipv6
    Compiled without:
    Available nsock engines: epoll poll select
    ```
 
## DockerHub
 
17. *Upload* da nova imagem no [DockerHub](https://hub.docker.com/):

    a. Criar uma conta gratuita
    
    b. Logar na conta desde o terminal com o usuário recém criado:
    ```
    $ docker login
    Login with your Docker ID to push and pull images from Docker Hub. If you don't have a Docker ID, head over to https://hub.docker.com to create one.
    Username: josecastillolema
    Password: 
    WARNING! Your password will be stored unencrypted in /home/ubuntu/.docker/config.json.
    Configure a credential helper to remove this warning. See
    https://docs.docker.com/engine/reference/commandline/login/#credentials-store

    Login Succeeded
    ```
    
    c. Taggear a imagem. O nome da imagem deve ser `username/nome da imagem`:
    ```
    $ docker tag ubuntu_com_nmap-viadockerfile josecastillolema/fiap-bdt
    ```
    
    d. Fazer o *upload* (`push`) da imagem:
    ```
    $ docker push josecastillolema/fiap-bdt
    ```
    
    e. Conferir a imagem no portal do DockerHub:
       ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/mob/cloud/img/docker0.png)
