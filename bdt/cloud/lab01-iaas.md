# Lab 1 - AWS EC2

## Criando a instancia
Usaremos a imagem oficial `Amazon Linux` para aprender alguns conceitos importantes do EC2:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**
 
1. Acessar o serviço **EC2**:
   ![](/bdt/cloud/img/ec2-0.png)

2. Lançar o assistente de criaçao de instancias:
   ![](/bdt/cloud/img/ec2-1.png)

3. Escolher a **imagem** do `Amazon Linux 2 AMI`:
   ![](/bdt/cloud/img/ec2-2.png)
   
4. Escolher o ***flavor*** `t2.micro`:
   ![](/bdt/cloud/img/ec2-3.png)

5. Usaremos um *script* de **`cloud-init`** para customizar a instância:
   ![](/bdt/cloud/img/ec2-4.png)

6. Confirmar criaçao da instância:
   ![](/bdt/cloud/img/ec2-5.png)
   
7. Criaçao da **chave** para poder acessar a instância via SSH de forma segura:
   <img src="https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-6.png" width="465" height="342">

8. Validar a criacao da instância:
   ![](/bdt/cloud/img/ec2-7.png)

## Accessando à instancia

9. Seguiremos as próprias indicações do EC2:
   ![](/bdt/cloud/img/ec2-8.png)
   
10. Em um terminal local:
    ```
    $ chmod 400 fiap.pem
    $ ssh -i "fiap.pem" ec2-user@ec2-52-91-146-116.compute-1.amazonaws.com

       __|  __|_  )
       _|  (     /   Amazon Linux 2 AMI
      ___|\___|___|

    https://aws.amazon.com/amazon-linux-2/
    [ec2-user@ip-172-31-50-1 ~]$
    ```
    
11. Confirmar que o script de **cloud-init** rodou com sucesso:
   ```
   $ ls /tmp/
   cloudInitFunciona
   ```

## Instalando um servidor web

12. Instalar o pacote `httpd`:
   ```
   [ec2-user@ip-172-31-50-1 ~]$ sudo yum install -y httpd
   Failed to set locale, defaulting to C
   Loaded plugins: extras_suggestions, langpacks, priorities, update-motd
   amzn2-core                                                                                                                     | 2.4 kB  00:00:00     
   Resolving Dependencies
   --> Running transaction check
   ---> Package httpd.x86_64 0:2.4.43-1.amzn2 will be installed
   --> Processing Dependency: httpd-tools = 2.4.43-1.amzn2 for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: httpd-filesystem = 2.4.43-1.amzn2 for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: system-logos-httpd for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: mod_http2 for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: httpd-filesystem for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: /etc/mime.types for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: libaprutil-1.so.0()(64bit) for package: httpd-2.4.43-1.amzn2.x86_64
   --> Processing Dependency: libapr-1.so.0()(64bit) for package: httpd-2.4.43-1.amzn2.x86_64
   --> Running transaction check
   ---> Package apr.x86_64 0:1.6.3-5.amzn2.0.2 will be installed
   ---> Package apr-util.x86_64 0:1.6.1-5.amzn2.0.2 will be installed
   --> Processing Dependency: apr-util-bdb(x86-64) = 1.6.1-5.amzn2.0.2 for package: apr-util-1.6.1-5.amzn2.0.2.x86_64
   ---> Package generic-logos-httpd.noarch 0:18.0.0-4.amzn2 will be installed
   ---> Package httpd-filesystem.noarch 0:2.4.43-1.amzn2 will be installed
   ---> Package httpd-tools.x86_64 0:2.4.43-1.amzn2 will be installed
   ---> Package mailcap.noarch 0:2.1.41-2.amzn2 will be installed
   ---> Package mod_http2.x86_64 0:1.15.3-2.amzn2 will be installed
   --> Running transaction check
   ---> Package apr-util-bdb.x86_64 0:1.6.1-5.amzn2.0.2 will be installed
   --> Finished Dependency Resolution

   Dependencies Resolved

   ======================================================================================================================================================
    Package                                  Arch                        Version                                   Repository                       Size
   ======================================================================================================================================================
   Installing:
    httpd                                    x86_64                      2.4.43-1.amzn2                            amzn2-core                      1.3 M
   Installing for dependencies:
    apr                                      x86_64                      1.6.3-5.amzn2.0.2                         amzn2-core                      118 k
    apr-util                                 x86_64                      1.6.1-5.amzn2.0.2                         amzn2-core                       99 k
    apr-util-bdb                             x86_64                      1.6.1-5.amzn2.0.2                         amzn2-core                       19 k
    generic-logos-httpd                      noarch                      18.0.0-4.amzn2                            amzn2-core                       19 k
    httpd-filesystem                         noarch                      2.4.43-1.amzn2                            amzn2-core                       23 k
    httpd-tools                              x86_64                      2.4.43-1.amzn2                            amzn2-core                       87 k
    mailcap                                  noarch                      2.1.41-2.amzn2                            amzn2-core                       31 k
    mod_http2                                x86_64                      1.15.3-2.amzn2                            amzn2-core                      146 k

   Transaction Summary
   ======================================================================================================================================================
   Install  1 Package (+8 Dependent packages)

   Total download size: 1.8 M
   Installed size: 5.1 M
   Downloading packages:
   (1/9): apr-util-1.6.1-5.amzn2.0.2.x86_64.rpm                                                                                   |  99 kB  00:00:00     
   (2/9): apr-util-bdb-1.6.1-5.amzn2.0.2.x86_64.rpm                                                                               |  19 kB  00:00:00     
   (3/9): apr-1.6.3-5.amzn2.0.2.x86_64.rpm                                                                                        | 118 kB  00:00:00     
   (4/9): generic-logos-httpd-18.0.0-4.amzn2.noarch.rpm                                                                           |  19 kB  00:00:00     
   (5/9): httpd-filesystem-2.4.43-1.amzn2.noarch.rpm                                                                              |  23 kB  00:00:00     
   (6/9): httpd-2.4.43-1.amzn2.x86_64.rpm                                                                                         | 1.3 MB  00:00:00     
   (7/9): httpd-tools-2.4.43-1.amzn2.x86_64.rpm                                                                                   |  87 kB  00:00:00     
   (8/9): mailcap-2.1.41-2.amzn2.noarch.rpm                                                                                       |  31 kB  00:00:00     
   (9/9): mod_http2-1.15.3-2.amzn2.x86_64.rpm                                                                                     | 146 kB  00:00:00     
   ------------------------------------------------------------------------------------------------------------------------------------------------------
   Total                                                                                                                 6.2 MB/s | 1.8 MB  00:00:00     
   Running transaction check
   Running transaction test
   Transaction test succeeded
   Running transaction
     Installing : apr-1.6.3-5.amzn2.0.2.x86_64                                                                                                       1/9 
     Installing : apr-util-bdb-1.6.1-5.amzn2.0.2.x86_64                                                                                              2/9 
     Installing : apr-util-1.6.1-5.amzn2.0.2.x86_64                                                                                                  3/9 
     Installing : httpd-tools-2.4.43-1.amzn2.x86_64                                                                                                  4/9 
     Installing : generic-logos-httpd-18.0.0-4.amzn2.noarch                                                                                          5/9 
     Installing : mailcap-2.1.41-2.amzn2.noarch                                                                                                      6/9 
     Installing : httpd-filesystem-2.4.43-1.amzn2.noarch                                                                                             7/9 
     Installing : mod_http2-1.15.3-2.amzn2.x86_64                                                                                                    8/9 
     Installing : httpd-2.4.43-1.amzn2.x86_64                                                                                                        9/9 
     Verifying  : apr-util-1.6.1-5.amzn2.0.2.x86_64                                                                                                  1/9 
     Verifying  : apr-util-bdb-1.6.1-5.amzn2.0.2.x86_64                                                                                              2/9 
     Verifying  : httpd-2.4.43-1.amzn2.x86_64                                                                                                        3/9 
     Verifying  : mod_http2-1.15.3-2.amzn2.x86_64                                                                                                    4/9 
     Verifying  : httpd-filesystem-2.4.43-1.amzn2.noarch                                                                                             5/9 
     Verifying  : apr-1.6.3-5.amzn2.0.2.x86_64                                                                                                       6/9 
     Verifying  : mailcap-2.1.41-2.amzn2.noarch                                                                                                      7/9 
     Verifying  : generic-logos-httpd-18.0.0-4.amzn2.noarch                                                                                          8/9 
     Verifying  : httpd-tools-2.4.43-1.amzn2.x86_64                                                                                                  9/9 

   Installed:
     httpd.x86_64 0:2.4.43-1.amzn2                                                                                                                       

   Dependency Installed:
     apr.x86_64 0:1.6.3-5.amzn2.0.2                      apr-util.x86_64 0:1.6.1-5.amzn2.0.2              apr-util-bdb.x86_64 0:1.6.1-5.amzn2.0.2        
     generic-logos-httpd.noarch 0:18.0.0-4.amzn2         httpd-filesystem.noarch 0:2.4.43-1.amzn2         httpd-tools.x86_64 0:2.4.43-1.amzn2            
     mailcap.noarch 0:2.1.41-2.amzn2                     mod_http2.x86_64 0:1.15.3-2.amzn2               

   Complete!
   ```

13. Habilitar o serviço `httpd`:
   ```
   [ec2-user@ip-172-31-50-1 ~]$ sudo service httpd start  
   Redirecting to /bin/systemctl start httpd.service
   ```
   
14. Criar um *site* de teste, no arquivo `/var/www/html/index.html`:
   ```
   <h1>
      FIAP!!!
   </h1>
   ```
   
15. Testar localmente o servidor web:
   ```
   [ec2-user@ip-172-31-50-1 ~]$ curl localhost
   <h1>
           FIAP!!!
   </h1>
   ```
  
16. Obter o IP público da VM:
   ![](/bdt/cloud/img/ec2-9.png)

17. Testar accesso pelo IP público:
   ![](/bdt/cloud/img/ec2-10.png)

18. Como era esperado, o acesso web não funcionou pois a porta HTTP (TCP/80) deve ser liberada nos *security groups*. Incluir uma liberação para esta porta no *security group* associado à instância:
   ![](/bdt/cloud/img/ec2-11.png)

19. Testar novamente o acesso pelo IP público:
   ![](/bdt/cloud/img/ec2-12.png)
