# Lab 2 - AWS EBS

Em este lab sobre [**Elastic Block Service**](https://aws.amazon.com/pt/ebs/) aprenderemos alguns conceitos importantes do armazenamento em blocos:
 - Criação de volumes
 - Anexar volumes a instâncias
 - Configurar volumes dentro das instâncias
   * Formatação
   * Criação do sistema de arquivos
   * Montar o volume

## Pre-reqs

- Na maquina virtual do [lab 01 - EC2](https://github.com/josecastillolema/fiap/blob/master/shift/multicloud/lab01-iaas-ec2.md), conferir os volumes:
    ```
    [ec2-user@ip-172-31-51-147 ~]$ lsblk
    NAME    MAJ:MIN RM SIZE RO TYPE MOUNTPOINT
    xvda    202:0    0   8G  0 disk 
    `-xvda1 202:1    0   8G  0 part /
    ```
    Veja que só existe um volume (do sistema operacional), com um tamanho de 8 GB e uma partição (`xvda1`). Estas informações podem mudar, o importante e garantir que após a criação de um novo volume e de anexa-lo a instância, novos dispositivos serão listados.

## Criando o volume
 
1. Ainda no serviço **EC2**, navegar ate **Elastic Block Volume** -> **Volumes**. Importante notar a zona de disponibilidade a onde foi criada a maquina virtual do lab 01:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ebs0.png)

2. Criar um novo volume vazio com tamanho de 10 GB, na mesma zona de disponibilidade a onde foi criada a VM do lab 01:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ebs1.png)

3. Uma vez o volume for criado, anexar ele à maquina virtual:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ebs2.png)
   
4. Seleccionar o nome da maquina virtual criada no lab01:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ebs3.png)

5. Apos uns instantes, conferir a informação sobre anexos do volume:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/ebs4.png)

## Configurando o volume dentro da instancia

6. Na maquina virtual, conferir o novo dispositivo, em este caso `xvdf`, com tamanho 10 GB (o nome pode mudar). Observe-se que ainda não possue nenhuma partição:
    ```
    $ lsblk
    NAME    MAJ:MIN RM SIZE RO TYPE MOUNTPOINT
    xvda    202:0    0   8G  0 disk 
    `-xvda1 202:1    0   8G  0 part /
    xvdf    202:80   0  10G  0 disk
    ```
    
7. Usaremos o `fdisk` para criar uma partição no novo disco:
   ```
   $ sudo fdisk /dev/xvdf

   Welcome to fdisk (util-linux 2.30.2).
   Changes will remain in memory only, until you decide to write them.
   Be careful before using the write command.

   Device does not contain a recognized partition table.
   Created a new DOS disklabel with disk identifier 0x8e3e850d.

   Command (m for help): n
   Partition type
      p   primary (0 primary, 0 extended, 4 free)
      e   extended (container for logical partitions)
   Select (default p): p
   Partition number (1-4, default 1): 
   First sector (2048-20971519, default 2048): 
   Last sector, +sectors or +size{K,M,G,T,P} (2048-20971519, default 20971519): 

   Created a new partition 1 of type 'Linux' and of size 10 GiB.

   Command (m for help): w
   The partition table has been altered.
   Calling ioctl() to re-read partition table.
   Syncing disks.

   ```
8. Conferir a nova partição (em este caso, `xvdf1`):
   ```
   $ lsblk 
   NAME    MAJ:MIN RM SIZE RO TYPE MOUNTPOINT
   xvda    202:0    0   8G  0 disk 
   `-xvda1 202:1    0   8G  0 part /
   xvdf    202:80   0  10G  0 disk 
   `-xvdf1 202:81   0  10G  0 part
   ```

9. Criar o sistema de arquivos:
   ```
   $ sudo mkfs /dev/xvdf1
   mke2fs 1.42.9 (28-Dec-2013)
   Filesystem label=
   OS type: Linux
   Block size=4096 (log=2)
   Fragment size=4096 (log=2)
   Stride=0 blocks, Stripe width=0 blocks
   655360 inodes, 2621184 blocks
   131059 blocks (5.00%) reserved for the super user
   First data block=0
   Maximum filesystem blocks=2684354560
   80 block groups
   32768 blocks per group, 32768 fragments per group
   8192 inodes per group
   Superblock backups stored on blocks: 
      32768, 98304, 163840, 229376, 294912, 819200, 884736, 1605632

   Allocating group tables: done                            
   Writing inode tables: done                            
   Writing superblocks and filesystem accounting information: done 
   ```

10. Criar a pasta `/mnt/volumeExterno` para montar o volume:
    ```
    $ sudo mkdir /mnt/volumeExterno
    ```

11. Montar o volume na pasta recem criada:
    ```
    $ sudo mount /dev/xvdf1 /mnt/volumeExterno/
    ```

12. Listar os arquivos do novo volume (é esperado ter uma pasta chamada `lost-found`, mesmo que o volume esteja vazio):
    ```
    $ cd /mnt/volumeExterno/
    $ ls
    lost+found
    ```

13. Criar um arquivo qualquer (como usuário admin):
    ```
    $ cd /mnt/volumeExterno
    $ cat meuArquivo 
    sic mundus creatus est
    ```

14. Se for necessario usar este mesmo volume com o arquivo recem criado em outra instancia, quais dos seguintes pasos seria necessario refazer?
    - Formatação
    - Criação do sistema de arquivos
    - Montar o volume
