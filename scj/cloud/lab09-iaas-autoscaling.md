# Lab 9 - AWS Autoscaling

Usaremos a imagem oficial `Amazon Linux` para aprender alguns conceitos importantes de *autoscaling*:
 - ***launch configuration templates***
 - ***autoscaling groups***
 - ***scaling policies***
 
## Criando o *launch template*

1. Acessar o serviço **EC2**:
   ![](/scj/cloud/img/ec2-0.png)
   
2. Criar um novo *launch template*:
   ![](/scj/cloud/img/auto0.png)

3. Escolher a **imagem** do `Amazon Linux 2 AMI`:
   ![](/scj/cloud/img/ec2-2.png)
   
4. Escolher o ***flavor*** `t2.micro`:
   ![](/scj/cloud/img/ec2-3.png)
   
5. Assignar um nome para o *launch template*:
   ![](/scj/cloud/img/auto1.png)

6. Revisar e confirmar a criação do *lauch template*:
   ![](/scj/cloud/img/auto2.png)

7. Escolher uma chave previamente criada ou criar uma chave nova para acessar às instâncias do *autoscaling group*:
   ![](/scj/cloud/img/auto3.png)

8. Aguardar a criação do *launch template*:
   ![](/scj/cloud/img/auto4.png)

## Criando o *autoscaling group*

9. Criar um novo *autoscaling group*:
   ![](/scj/cloud/img/auto5.png)

10. Seleccionar o *launch template* criado anteriormente:
   ![](/scj/cloud/img/auto6.png)

11. Assignar um nome e uma subrede para o *autoscaling group*:
   ![](/scj/cloud/img/auto7.png)

12. Definir a política de autoscaling:
    - Uso de CPU como métrica
    - *Scale out* a partir de 40% de uso de CPU
    - *Scale in* a partir de 40% de uso de CPU
    - Número mínimo de instâncias: 1
    - Número máximo de instâncias: 2
    - 300 segundos de *warm up*
   ![](/scj/cloud/img/auto8.png)

13. Revisar e confirmar a criação do *autoscaling group*:
   ![](/scj/cloud/img/auto9.png)
   
14. Aguardar a criação do *autoscaling group*:
   ![](/scj/cloud/img/auto10.png)
   
15. Conferir a criação do *autoscaling group*, a princípio só com uma instância:
   ![](/scj/cloud/img/auto11.png)

16. Conferir as instâncias que fazem parto do *autoscaling group*:
   ![](/scj/cloud/img/auto12.png)

16. Conferir a(s) instância(s) no EC2:
   ![](/scj/cloud/img/auto13.png)

## Testando o *autoscaling group*

17. Accessar via SSH à instância:
    ```
    % ssh -i "fiap.pem" ec2-user@ec2-34-230-22-18.compute-1.amazonaws.com

           __|  __|_  )
           _|  (     /   Amazon Linux 2 AMI
          ___|\___|___|

    https://aws.amazon.com/amazon-linux-2/
    [ec2-user@ip-172-31-44-183 ~]$
    ```
    
18. Confirmar a baixa carga de CPU usando os comandos `uptime` e `top`:
    ```
    $ uptime
    10:01:32 up 7 min,  1 user,  load average: 0.00, 0.02, 0.00
    
    $ top
    top - 10:01:55 up 8 min,  1 user,  load average: 0.00, 0.02, 0.00
    Tasks:  84 total,   1 running,  47 sleeping,   0 stopped,   0 zombie
    %Cpu(s):  0.0 us,  0.0 sy,  0.0 ni,100.0 id,  0.0 wa,  0.0 hi,  0.0 si,  0.0 st
    KiB Mem :  1006940 total,   587088 free,    60240 used,   359612 buff/cache
    KiB Swap:        0 total,        0 free,        0 used.   806496 avail Mem 
    ```
    
19. Criar carga na CPU:
    ```
    $ yes > /dev/null &
    [1] 3533
    ```
    
20. Confirmar a alta carga de CPU usando os comandos `uptime` e `top`:
    ```
    $ top
    top - 10:11:42 up 17 min,  1 user,  load average: 1.00, 0.86, 0.47
    Tasks:  85 total,   2 running,  47 sleeping,   0 stopped,   0 zombie
    %Cpu(s): 98.7 us,  1.3 sy,  0.0 ni,  0.0 id,  0.0 wa,  0.0 hi,  0.0 si,  0.0 st
    KiB Mem :  1006940 total,   584692 free,    61056 used,   361192 buff/cache
    KiB Swap:        0 total,        0 free,        0 used.   805612 avail Mem
   
    $ uptime
    10:44:25 up 36 min,  1 user,  load average: 1.00, 0.94, 0.60
    ```
   
21. Aguardar o ***scale up***.

    Pode demorar ate:
    `5 min (*healt check grace period*) + tempo de criação da instância + tempo de status checks`
   ![](/scj/cloud/img/auto14.png)

22. Parar o processo que consome CPU:
    ```
    $ killall yes
    ```
    
23. Confirmar a baixa carga de CPU usando os comandos `uptime` e `top`:
    ```
    $ uptime
    10:01:32 up 7 min,  1 user,  load average: 0.00, 0.02, 0.00
    
    $ top
    top - 10:01:55 up 8 min,  1 user,  load average: 0.00, 0.02, 0.00
    Tasks:  84 total,   1 running,  47 sleeping,   0 stopped,   0 zombie
    %Cpu(s):  0.0 us,  0.0 sy,  0.0 ni,100.0 id,  0.0 wa,  0.0 hi,  0.0 si,  0.0 st
    KiB Mem :  1006940 total,   587088 free,    60240 used,   359612 buff/cache
    KiB Swap:        0 total,        0 free,        0 used.   806496 avail Mem 
    ```
    
24. Aguardar o ***scale down***. Pode demorar ate: 5 min *warm up* + 5 min (*healt check grace period*) + tempo de deleção da instância.
   ![](/scj/cloud/img/auto11.png)
