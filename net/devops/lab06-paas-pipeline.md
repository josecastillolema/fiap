# Lab 6 - Azure Pipelines

Em este lab sobre [**Azure Pipelines**](https://azure.microsoft.com/pt-br/services/devops/pipelines/) aprenderemos alguns conceitos importantes na criação de *pipelines*:
 - Criação do *pipeline*
 - Automação de deploy no Azure Pipelines
 
## Pre-reqs

- Um *application service* no ar, seguindo os passos do [lab 05 - Application Services](https://github.com/josecastillolema/fiap/blob/master/net/devops/lab05-paas-app.md)

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/app07.png)

## Acessando e criando a conta no Azure Pipelines

1. Acessar o serviço [**Azure Pipelines**](https://azure.microsoft.com/pt-br/services/devops/pipelines/):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap01.png)

2. Logar e autorizar a conta:

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap02.png)
   
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap03.png)

## Criação do *pipeline*

3. Criar um novo projeto:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap04.png)

4. Escolher GitHub como repositório de código:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap05.png)

5. Autorizar o acesso do Azure Pipelines ao GitHub:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap06.png)

6. Selecionar o repositório dos pre-reqs (em este caso `azure-devops`):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap07.png)

7. Autorizar o acesso do Azure Pipelines ao repositório selecionado:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap08.png)
   
8. Configurar um novo *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap09.png)
   
9. Selecionar a *subscription* adecuada:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap10.png)

10. Selecionar o *application service* dos pre-reqs:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap11.png)
   
11. Revisar o template `yaml` que descreve o *pipeline*. **Apontar para a versão 3.6 do Python**. *Template* completo [aqui]().
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap12.png)

12. Confirmar a criação do *template* no repositório do GitHub (automaticamente o *pipeline* será iniciado):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap13.png)

13. Aguardar o término da execução do *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap14.png)

14. Confirmar o correto *deploy* da aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap15.png)

## Invocando o *pipeline*

15. Atualizar o código da aplicaçao no GitHub:

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap16.png)

   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap17.png)

16. Conferir que é criada uma nova execução do *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap18.png)
   
17. Aguardar o término da execução do *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap19.png)
   
18. Confirmar o correto *deploy* da nova versão da aplicação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/net/devops/img/ap20.png)

