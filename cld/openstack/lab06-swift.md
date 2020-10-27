# Lab 6 - OpenStack Swift

## Object Storage Service
Usaremos o serviço Swift para aprender alguns conceitos importantes sobre armazenamento de objeto:
 - criação de containers
 - objetos
 - acesso via URL

1. Carregar as credenciais de administrador e conferir que foram carregadas no ambiente:
    ```
    
    ```

2. Conferir que o Swift foi instalado no OpenStack:
    ```
    
    ```
 
3. Mostrar informação sobre o endpoint:
    ```
    
    ```
 
4. Conferir que a porta 8080 está aberta e associada ao Swift:
    ```
    
    ```
 
5. Listar os serviços Linux que compõem o Swift:
    ```
    
    ```
 
6. Conferir a saúde dos serviços:
    ```
    
    ```
 
7. Mostrar os logs do serviço:
    ```
    
    ```
 
8. Mostrar os arquivos de configuração:
    ```
    
    ```
 
9. Mostrar os arquivos de configuração de uma forma mais clara (sem comentários nem linhas vazias):
    ```
    
    ```
 
10.	Conferir o endpoint do Keystone:
    ```
    
    ```
 
11.	Mostrar estatísticas de uso gerais (passando o endpoint do Keystone + `/v3`):
    ```
    
    ```
 
12.	Listar os containers (não deveria ter nenhum ainda):
    ```
    
    ```
 

13.	Criar um arquivo de teste:
    ```
    
    ```
 
14.	Subir o arquivo de teste a um container chamado `fiap`:
    ```
    
    ```
 
15.	Listar os containers:
    ```
    
    ```
 
16.	Listar o container `fiap`:
    ```
    
    ```
 
17.	Mostrar estatísticas de uso gerais:
    ```
    
    ```
 
18.	Mostrar estatísticas de uso do container `fiap`:
    ```
    
    ```
 
19.	Mostrar estatísticas de uso do arquivo de teste:
    ```
    
    ```
 
20.	Criar uma nova pasta e fazer o *download* do arquivo:
    ```
    
    ```
 
21.	Conferir o conteúdo do arquivo baixado:
    ```
    
    ```
 
22.	Configurar permissões de leitura para o usuário demo do projeto demo:
    ```
    
    ```

23.	Configurar permissões de escrita para o usuário demo do projeto demo:
    ```
    
    ```
 
24.	Deletar objeto e container
    ```
    
    ```
    
25.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de container
    -  Upload de arquivo
    - Download de arquivo
    - Configuração de permissões
    - Abrir objeto via URL
    
    ![](/cld/openstack/img/swift1.png)
