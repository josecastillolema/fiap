# Lab 15 - AWS EKS

## Criando o cluster
Vamos criar um cluster Kubernetes gerenciado para aprender alguns conceitos importantes do [**Elastic Kubernetes Service**](https://aws.amazon.com/pt/eks/):
 - Geração do arquivo `kubeconfig`
 - Uso do cliente `kubectl`
 
1. Acessar o serviço **EKS**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks1.png)
   
2. Escolher a opção de criação de um novo cluster:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks2.png)
   
3. Definir um nome para o cluster, e se tiver usando as **contas do AWS Academy** selecionar `LabRole` como *Cluster Service Role*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks3.png)
   
4. Na aba de *Networking*, deixar só selecionadas as subredes correspondes as zonas de disponibilidade `us-east-1a`, `us-east-1a` e `us-east-1c`.
Remover as subredes correspondentes a `us-east-1d`, `us-east-1e` e `us-east-1f`.
Para saber em qual zona de disponibilidade foram provisionadas as subredes consultar o módulo VPC.
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks4.png)

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks5.png)
   
6. Manter o *default* na aba de *logging*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks6.png)

7. Aguardar varios minutos até o cluster ficar em estado *Active*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks7.png)

8. Vamos a abrir um console **CloudShell** para acessar o cluster:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/eks8.png)

9. Gerar o `kubeconfig` do cluster (é um arquivo com as credencias e a URL do cluster):
    ```
    $ aws eks --region us-east-1 update-kubeconfig --name fiapCluster
    ```
    
10. Instalar o `kubectl` (CLI para interagir com o cluster):
    ```
    $ curl -LO https://storage.googleapis.com/kubernetes-release/release/v1.18.13/bin/linux/amd64/kubectl
    $ chmod +x kubectl
    ```

11. Testar o acesso:
    ```
    $ ./kubectl get svc
    ```
