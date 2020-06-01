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

3. Escolher a **imagem** do Amazon Linux 2 AMI:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-2.png)
   
4. Escolher o ***flavor*** `t2.micro`:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-3.png)

5. Usaremos um *script* de `*cloud-init*` para customizar a instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-4.png)

6. Confirmar criaçao da instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-5.png)
   
7. Criaçao da chave para poder acessar a instância via SSH de forma segura:
   <img src="https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-6.png" width="465" height="342">

8. Validar a criacao da instância:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-7.png)

## Accessando à instancia

9. Seguiremos as próprias indicações do EC2:
   ![](https://github.com/josecastillolema/fiap/blob/master/bdt/cloud/img/ec2-8.png)
   
10. Em um terminal local:
    ```
    $ chmod 400 fiap.pem
    $ ssh -i "fiap.pem" ec2-user@ec2-52-91-146-116.compute-1.amazonaws.com

       __|  __|_  )
       _|  (     /   Amazon Linux 2 AMI
      ___|\___|___|

    https://aws.amazon.com/amazon-linux-2/
    ```

