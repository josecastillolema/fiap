# Lab 1 - Virtual Machine

## Criando a instancia
Usaremos a imagem oficial `Ubuntu Server` para aprender alguns conceitos importantes de máquinas virtuais:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**
 
1. Acessar o serviço **Virtual machine**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm00.png)

2. Sempre que for criar novos recursos, selecione como ***subscription*** `Azure for students` e como ***resource group*** `19net`. Se o *resource group* ainda não existe, como é o caso, criar ele:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm01.png)
   
3. Configurar um nome para a instãncia, escolher a **imagem** `Ubuntu Server` e o ***flavor*** `Standard_B1`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm03.png)
   
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm02.png)

4. Usaremos chave ssh como método de acesso à VM:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm04.png)

5. A porta 22 no ***security group*** precisa estar aberta para poder acessar a instância via SSH:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm05.png)
   
6. Configurar `Standard HDD` como tipo de disco:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm06.png)

7. Usaremos um *script* de **`cloud-init`** para customizar a instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm07.png)

6. Confirmar criaçao da instância uma vez validada:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm08.png)
   
7. Fazer *download* da **chave** para poder acessar a instância via SSH de forma segura:
   <img src="https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm09.png" width="465" height="342">

8. Aguardar a criacao da instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm10.png)

## Accessando à instancia

9. Note-se o IP público da instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm11.png)

10. [**Linux/MAC**] Seguiremos as próprias indicações do Azure:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm12.png)
   
    [**Windows**] Usaremos o [PuTTY](https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html), seguindo as seguintes [instruções](https://docs.microsoft.com/en-us/azure/virtual-machines/linux/ssh-from-windows). Como alternativa ao PuTTy, o [MobaXterm](https://mobaxterm.mobatek.net/) é uma excelente opçao.
   
11. [**Linux/MAC**] Em um terminal local:
    ```
    $ chmod 400 aula1_key.pem 
    $ ssh -i aula1_key.pem  azureuser@168.61.32.71
    The authenticity of host '168.61.32.71 (168.61.32.71)' can't be established.
    ECDSA key fingerprint is SHA256:KASB7hYoy0WvdiE+zDEBlyFQ7eoO3bwco32davnUxCA.
    Are you sure you want to continue connecting (yes/no/[fingerprint])? yes
    Warning: Permanently added '168.61.32.71' (ECDSA) to the list of known hosts.
    Welcome to Ubuntu 18.04.5 LTS (GNU/Linux 5.4.0-1039-azure x86_64)

     * Documentation:  https://help.ubuntu.com
     * Management:     https://landscape.canonical.com
     * Support:        https://ubuntu.com/advantage

      System information as of Thu Feb 18 13:57:15 UTC 2021

      System load:  0.0               Processes:           107
      Usage of /:   4.5% of 28.90GB   Users logged in:     0
      Memory usage: 44%               IP address for eth0: 10.0.0.4
      Swap usage:   0%

    0 packages can be updated.
    0 of these updates are security updates.

    The programs included with the Ubuntu system are free software;
    the exact distribution terms for each program are described in the
    individual files in /usr/share/doc/*/copyright.

    Ubuntu comes with ABSOLUTELY NO WARRANTY, to the extent permitted by
    applicable law.

    To run a command as administrator (user "root"), use "sudo <command>".
    See "man sudo_root" for details.

    azureuser@aula1:~$ 
    ```
    
    [**Windows**] Seguir as instruções do PuTTY: https://docs.microsoft.com/en-us/azure/virtual-machines/linux/ssh-from-windows
    
12. Uma vez logado na maquina virtual, confirmar que o script de **cloud-init** rodou com sucesso:
    ```
    $ ls /tmp/
    CloudInitFunciona
    ```

## Instalando um servidor web

13. Instalar o pacote `apache2`:
    ```
    azureuser@aula1:~$ sudo apt install apache2
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    The following package was automatically installed and is no longer required:
      linux-headers-4.15.0-135
    Use 'sudo apt autoremove' to remove it.
    The following additional packages will be installed:
      apache2-bin apache2-data apache2-utils libapr1 libaprutil1 libaprutil1-dbd-sqlite3 libaprutil1-ldap
      liblua5.2-0 ssl-cert
    Suggested packages:
      www-browser apache2-doc apache2-suexec-pristine | apache2-suexec-custom openssl-blacklist
    The following NEW packages will be installed:
      apache2 apache2-bin apache2-data apache2-utils libapr1 libaprutil1 libaprutil1-dbd-sqlite3
      libaprutil1-ldap liblua5.2-0 ssl-cert
    0 upgraded, 10 newly installed, 0 to remove and 0 not upgraded.
    Need to get 1729 kB of archives.
    After this operation, 6985 kB of additional disk space will be used.
    Do you want to continue? [Y/n] y
    Get:1 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 libapr1 amd64 1.6.3-2 [90.9 kB]
    Get:2 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 libaprutil1 amd64 1.6.1-2 [84.4 kB]
    Get:3 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 libaprutil1-dbd-sqlite3 amd64 1.6.1-2 [10.6 kB]
    Get:4 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 libaprutil1-ldap amd64 1.6.1-2 [8764 B]
    Get:5 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 liblua5.2-0 amd64 5.2.4-1.1build1 [108 kB]
    Get:6 http://azure.archive.ubuntu.com/ubuntu bionic-updates/main amd64 apache2-bin amd64 2.4.29-1ubuntu4.14 [1070 kB]
    Get:7 http://azure.archive.ubuntu.com/ubuntu bionic-updates/main amd64 apache2-utils amd64 2.4.29-1ubuntu4.14 [83.9 kB]
    Get:8 http://azure.archive.ubuntu.com/ubuntu bionic-updates/main amd64 apache2-data all 2.4.29-1ubuntu4.14 [160 kB]
    Get:9 http://azure.archive.ubuntu.com/ubuntu bionic-updates/main amd64 apache2 amd64 2.4.29-1ubuntu4.14 [95.1 kB]
    Get:10 http://azure.archive.ubuntu.com/ubuntu bionic/main amd64 ssl-cert all 1.0.39 [17.0 kB]
    Fetched 1729 kB in 0s (28.4 MB/s)
    Preconfiguring packages ...
    Selecting previously unselected package libapr1:amd64.
    (Reading database ... 76706 files and directories currently installed.)
    Preparing to unpack .../0-libapr1_1.6.3-2_amd64.deb ...
    Unpacking libapr1:amd64 (1.6.3-2) ...
    Selecting previously unselected package libaprutil1:amd64.
    Preparing to unpack .../1-libaprutil1_1.6.1-2_amd64.deb ...
    Unpacking libaprutil1:amd64 (1.6.1-2) ...
    Selecting previously unselected package libaprutil1-dbd-sqlite3:amd64.
    Preparing to unpack .../2-libaprutil1-dbd-sqlite3_1.6.1-2_amd64.deb ...
    Unpacking libaprutil1-dbd-sqlite3:amd64 (1.6.1-2) ...
    Selecting previously unselected package libaprutil1-ldap:amd64.
    Preparing to unpack .../3-libaprutil1-ldap_1.6.1-2_amd64.deb ...
    Unpacking libaprutil1-ldap:amd64 (1.6.1-2) ...
    Selecting previously unselected package liblua5.2-0:amd64.
    Preparing to unpack .../4-liblua5.2-0_5.2.4-1.1build1_amd64.deb ...
    Unpacking liblua5.2-0:amd64 (5.2.4-1.1build1) ...
    Selecting previously unselected package apache2-bin.
    Preparing to unpack .../5-apache2-bin_2.4.29-1ubuntu4.14_amd64.deb ...
    Unpacking apache2-bin (2.4.29-1ubuntu4.14) ...
    Selecting previously unselected package apache2-utils.
    Preparing to unpack .../6-apache2-utils_2.4.29-1ubuntu4.14_amd64.deb ...
    Unpacking apache2-utils (2.4.29-1ubuntu4.14) ...
    Selecting previously unselected package apache2-data.
    Preparing to unpack .../7-apache2-data_2.4.29-1ubuntu4.14_all.deb ...
    Unpacking apache2-data (2.4.29-1ubuntu4.14) ...
    Selecting previously unselected package apache2.
    Preparing to unpack .../8-apache2_2.4.29-1ubuntu4.14_amd64.deb ...
    Unpacking apache2 (2.4.29-1ubuntu4.14) ...
    Selecting previously unselected package ssl-cert.
    Preparing to unpack .../9-ssl-cert_1.0.39_all.deb ...
    Unpacking ssl-cert (1.0.39) ...
    Setting up libapr1:amd64 (1.6.3-2) ...
    Setting up apache2-data (2.4.29-1ubuntu4.14) ...
    Setting up ssl-cert (1.0.39) ...
    Setting up libaprutil1:amd64 (1.6.1-2) ...
    Setting up liblua5.2-0:amd64 (5.2.4-1.1build1) ...
    Setting up libaprutil1-ldap:amd64 (1.6.1-2) ...
    Setting up libaprutil1-dbd-sqlite3:amd64 (1.6.1-2) ...
    Setting up apache2-utils (2.4.29-1ubuntu4.14) ...
    Setting up apache2-bin (2.4.29-1ubuntu4.14) ...
    Setting up apache2 (2.4.29-1ubuntu4.14) ...
    Enabling module mpm_event.
    Enabling module authz_core.
    Enabling module authz_host.
    Enabling module authn_core.
    Enabling module auth_basic.
    Enabling module access_compat.
    Enabling module authn_file.
    Enabling module authz_user.
    Enabling module alias.
    Enabling module dir.
    Enabling module autoindex.
    Enabling module env.
    Enabling module mime.
    Enabling module negotiation.
    Enabling module setenvif.
    Enabling module filter.
    Enabling module deflate.
    Enabling module status.
    Enabling module reqtimeout.
    Enabling conf charset.
    Enabling conf localized-error-pages.
    Enabling conf other-vhosts-access-log.
    Enabling conf security.
    Enabling conf serve-cgi-bin.
    Enabling site 000-default.
    Created symlink /etc/systemd/system/multi-user.target.wants/apache2.service → /lib/systemd/system/apache2.service.
    Created symlink /etc/systemd/system/multi-user.target.wants/apache-htcacheclean.service → /lib/systemd/system/apache-htcacheclean.service.
    Processing triggers for libc-bin (2.27-3ubuntu1.4) ...
    Processing triggers for systemd (237-3ubuntu10.44) ...
    Processing triggers for man-db (2.8.3-2ubuntu0.1) ...
    Processing triggers for ufw (0.36-0ubuntu0.18.04.1) ...
    Processing triggers for ureadahead (0.100.0-21) ...
    ```
   
14. Criar um *site* de teste, no arquivo `/var/www/html/index.html` (como usuário admin):
    ```
    <h1>
       FIAP!!!
    </h1>
    ```
   
15. Testar localmente o servidor web:
    ```
    azureuser@aula1:~$ curl localhost
    <h1>
       FIAP!!!
    </h1>
    ```
  
16. Obter o IP público da VM:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm11.png)

17. Testar accesso pelo IP público:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm13.png)

18. Como era esperado, o acesso web não funcionou pois a porta HTTP (TCP/80) deve ser liberada nos *security groups*. Incluir uma liberação para esta porta no *security group* associado à instância:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm14.png)

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm15.png)

19. Validar a criação da regra: 
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm16.png)


19. Testar novamente o acesso pelo IP público:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/vm17.png)
