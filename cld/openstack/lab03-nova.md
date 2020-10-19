# Lab 3 - Nova

## Compute Service
Usaremos o serviço Nova para aprender alguns conceitos importantes sobre máquinas virtuais:
 - ***flavors***
 - ***security groups***
 - **[cloud-init](https://cloud-init.io/)**

1. Conferir se as extensões de virtualizações estão presentes no processador:

2.	Listar os serviços Linux que compõem o Nova:

3.	Conferir a saúde dos serviços:

4.	Mostrar os *logs* do serviço:

5. Carregar as credenciais de OpenStack:

6.	Listar os módulos do OpenStack:

7.	Mostrar o arquivo de configuração:

8.	Mostrar os hypervisors disponíveis:

9.	Mostrar a descrição do hypervisor:

10.	Mostrar estatísticas de uso dos hypervisors:

11.	Listar os *flavors*:

12.	Mostrar informações sobre um *flavor*:

13.	Criar um *flavor*: 

14.	Criar uma chave:

15.	Conferir o conteúdo da chave:

16.	Listar as chaves disponíveis:

17.	Assignar as permissões certas na chave e conferir que foram aplicadas:

18.	Listar as imagens:

19.	Criar a VM: 

20.	Listar as VMs:

21.	Mostrar a url do console:

22.	Acessar por console a vm e criar uma pasta ou um arquivo qualquer:
    - Pela URL do console
    - Directo pelo hypervisor:
    
23.	Mostrar o *log* da VM:

24.	Mostrar os eventos relacionados à VM:

25.	Desligar a VM:

26.	Listar as VMs e conferir que foi desligada:

27.	Listar novamente os eventos relacionados à VM e conferir que foi registrado o evento de *shutdown* da mesma:

28.	Ligar novamente a VM:

29.	Criar um snapshot da VM:

30.	Listar as imagens:

31.	Instanciar o snapshot:

32.	Listar as VMs usando o comando `virsh`:

33.	Mostrar as informações da definição da VM:

34.	Deletar as vms:

35. Recriar desde o Horizon Dashboard:
    - Criação de flavor 
    - Criação de vm
    - Criação de snapshot
    - Criação de vm a partir de snapshot
    ![](/cld/openstack/img/nova1.png)
    ![](/cld/openstack/img/nova2.png)
    ![](/cld/openstack/img/nova3.png)
