# Lab 1 - AWS EC2

## Criando a instancia
Usaremos a imagem oficial `Amazon Linux` para aprender alguns conceitos importantes do EC2:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**
 
1. Acessar o serviço EC2:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-0.png)

2. Lançar o assistente de criaçao de instancias:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-1.png)

3. Escolher a imagem do Amazon Linux 2 AMI:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-2.png)
   
4. Escolher o *flavor* `t2.micro`:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-3.png)

5. Usaremos um *script* de `cloud-init` para customizar a instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-4.png)

6. Confirmar criaçao da instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-5.png)
   
7. Criaçao da chave para poder acessar a instância via SSH de forma segura:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-6.png){ width=50% }

8. Validar a criacao da instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-7.png)

## Accessando à instancia

9. **[T2]** Conseguimos executar o container, vamos tentar acessar o banco desde o outro terminal. Para isso, precisamos instalar o cliente do MySQL:
    ```
    $ sudo apt install mysql-client -y
    Reading package lists... Done
    Building dependency tree          
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

