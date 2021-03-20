# Lab 5 - AWS S3

Em este lab sobre [**CloudFormation**](https://aws.amazon.com/pt/cloudformation/) aprenderemos alguns conceitos importantes do paradigma de Infrastructure as Code (IaC):
 - Criação de *stacks*
 - Parametrização
 - Monitoramento

Criaremos um *stack* que configura um servidor WordPress.

## Criação do *stack*
 
1. Accessar o serviço **CloudFormation** e criar um novo *stack*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf01.png)

2. Fazer *upload* do *template* [`lab14-iaas-cloudformation.yaml`](https://github.com/josecastillolema/fiap/blob/master/shift/multicloud/lab14-iaas-cloudformation/lab14-iaas-cloudformation.yaml)
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf02.png)

3. No *designer* podemos visualizar e editar graficamente o conteúdo do *template*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf03.png)
   
4. Parametrização do *stack*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf04.png)

5. Opçoes avançadas (sem mudanças):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf05.png)

6. Revisar e confirmar a criação do *stack*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf06.png)

## Accesso ao *stack*


7. Aguardar o *stack* transicionar ao estado `CREATE_COMPLETE`:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf07.png)


8. Confirmar a URL do WordPress:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf08.png)

9. Accessar o WordPress:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cf09.png)
