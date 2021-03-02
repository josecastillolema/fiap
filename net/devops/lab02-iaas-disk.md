# Lab 2 - Azure Disks

Em este lab sobre **disks** aprenderemos alguns conceitos importantes do armazenamento em bloco:
 - Criação de volumes
 - Anexar volumes a instâncias
 - Configurar volumes dentro das instâncias
   * Formatação
   * Criação do sistema de arquivos
   * Montar o volume

## Pre-reqs

- Na maquina virtual do [lab 01 - EC2](https://github.com/josecastillolema/fiap/blob/master/net/devops/lab01-iaas-vm.md), conferir os volumes:
    ```
    aula1:~$ lsblk
    NAME    MAJ:MIN RM  SIZE RO TYPE MOUNTPOINT
    sda       8:0    0   30G  0 disk 
    ├─sda1    8:1    0 29.9G  0 part /
    ├─sda14   8:14   0    4M  0 part 
    └─sda15   8:15   0  106M  0 part /boot/efi
    sdb       8:16   0    4G  0 disk 
    └─sdb1    8:17   0    4G  0 part /mnt
    sr0      11:0    1  628K  0 rom
    ```
    Veja que só existem dois volumes, `sda` (do sistema operacional) e `sdb`, com uns tamanhos de 30 GB e 4 GB. Estas informações podem mudar, o importante e garantir que após a criação de um novo volume e de anexa-lo a instância, novos dispositivos serão listados.

## Criando o volume
 
1. Na aba `Disks` da descrição da instância, criar um novo volume vazio de tipo HDD com tamanho de 50 GB:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/disk01.png)

## Configurando o volume dentro da instancia

2. Na maquina virtual, conferir o novo dispositivo, em este caso `sdc`, com tamanho 50 GB (o nome pode mudar). Observe-se que ainda não possue nenhuma partição:
    ```
    aula1:~$ lsblk
    NAME    MAJ:MIN RM  SIZE RO TYPE MOUNTPOINT
    sda       8:0    0   30G  0 disk 
    ├─sda1    8:1    0 29.9G  0 part /
    ├─sda14   8:14   0    4M  0 part 
    └─sda15   8:15   0  106M  0 part /boot/efi
    sdb       8:16   0    4G  0 disk 
    └─sdb1    8:17   0    4G  0 part /mnt
    sdc       8:32   0   50G  0 disk 
    sr0      11:0    1  628K  0 rom 
    ```
    
3. Usaremos o `fdisk` para criar uma partição no novo disco:
   ```
   aula1$ sudo fdisk /dev/sdc

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
   First sector (2048-104857599, default 2048): 
   Last sector, +sectors or +size{K,M,G,T,P} (2048-104857599, default 104857599): 

   Created a new partition 1 of type 'Linux' and of size 50 GiB.

   Command (m for help): w
   The partition table has been altered.
   Calling ioctl() to re-read partition table.
   Syncing disks.
   ```
   
4. Conferir a nova partição (em este caso, `sdc1`):
   ```
   aula1:~$ lsblk
   NAME    MAJ:MIN RM  SIZE RO TYPE MOUNTPOINT
   sda       8:0    0   30G  0 disk 
   ├─sda1    8:1    0 29.9G  0 part /
   ├─sda14   8:14   0    4M  0 part 
   └─sda15   8:15   0  106M  0 part /boot/efi
   sdb       8:16   0    4G  0 disk 
   └─sdb1    8:17   0    4G  0 part /mnt
   sdc       8:32   0   50G  0 disk 
   └─sdc1    8:33   0   50G  0 part 
   sr0      11:0    1  628K  0 rom
   ```

5. Criar o sistema de arquivos:
    ```
    aula1$ sudo mkfs /dev/sdc1
    mke2fs 1.44.1 (24-Mar-2018)
    Discarding device blocks: done                            
    Creating filesystem with 13106944 4k blocks and 3276800 inodes
    Filesystem UUID: 4d4abdc9-0b70-4b2e-83aa-a2e280c0540a
    Superblock backups stored on blocks: 
      32768, 98304, 163840, 229376, 294912, 819200, 884736, 1605632, 2654208, 
      4096000, 7962624, 11239424

    Allocating group tables: done                            
    Writing inode tables: done                            
    Writing superblocks and filesystem accounting information: done   
    ```
 
6. Criar a pasta `/mnt/volumeExterno` para montar o volume:
    ```
    aula1$ sudo mkdir /mnt/volumeExterno
    ```

7. Montar o volume na pasta recem criada:
    ```
    aula1$ sudo mount /dev/sdc1 /mnt/volumeExterno/
    ```

8. Listar os arquivos do novo volume (é esperado ter uma pasta chamada `lost-found`, mesmo que o volume esteja vazio):
    ```
    aula1$ cd /mnt/volumeExterno/
    aula1$ ls
    lost+found
    ```

9. Criar um arquivo qualquer (como usuário admin):
    ```
    aula1$ cd /mnt/volumeExterno
    aula1$ cat meuArquivo 
    sic mundus creatus est
    ```

10. Se for necessario usar este mesmo volume com o arquivo recem criado em outra instancia, quais dos seguintes pasos seria necessario refazer?
    - Formatação
    - Criação do sistema de arquivos
    - Montar o volume

