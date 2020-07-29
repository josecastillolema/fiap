# Lab 7 - AWS CP

Em este lab sobre **Code Pipeline** aprenderemos alguns conceitos importantes da criação de *pipelines*:
 - Criação de volumes
 - Anexar volumes a instâncias
 - Configurar volumes dentro das instâncias
   * Formatação
   * Criação do sistema de arquivos
   * Montar o volume

## Pre-reqs

- A aplicação do [lab de Beanstalk](/mob/cloud/lab06-paas-eb.md) precisa estar no ar:
   ![](/mob/cloud/img/cp00-0.png)
   
- Criação de um repositório no [GitHub](https://github.com/) com [os arquivos do lab de Beanstalk](/mob/cloud/lab06-paas-eb):
   ![](/mob/cloud/img/cp00-1.png)


## Criando o volume
 
1. Acessar o serviço **Code Pipeline**:
   ![](/mob/cloud/img/cp01.png)
   
2. Criar um novo *pipeline*:
   ![](/mob/cloud/img/cp02.png)

3. Nomear o *pipeline*:
   ![](/mob/cloud/img/cp03.png)

4. Escolher `GitHub` como *source provider* e o repositório criado nos pre-reqs:
   ![](/mob/cloud/img/cp04.png)

5. Já que usamos uma linguagem interpretada (Python), pulamos a fase de compilação:
   ![](/mob/cloud/img/cp05.png)

6. Selecionar `AWS Elastic Beanstalk` como *Deploy Provider*, apontando para a aplicação dos pre-reqs:
   ![](/mob/cloud/img/cp06.png)

7. Revisar as configuraçoes e confirmar a criação do *pipeline*:
   ![](/mob/cloud/img/cp07.png)
   ![](/mob/cloud/img/cp08.png)

8. Acompanhar o *pipeline* pelas diversas fases:
    * Recuperação do código do repositório
    * Deploy no Beanstalk
   ![](/mob/cloud/img/cp09.png)

9. **No Beanstalk**, confirmar que foi publicada uma nova versão pelo Code Pipeline;
   ![](/mob/cloud/img/cp10.png)

10. Confirmar que a aplicação está no ar:
   ![](/mob/cloud/img/cp11.png)

11. Publicar uma nova versão da aplicação no repositório do GitHub:
   ![](/mob/cloud/img/cp12.png)
   ![](/mob/cloud/img/cp13.png)

12. Acompanhar o *deploy* da nova versão no Code Pipeline:
   ![](/mob/cloud/img/cp14.png)

13. **No Beanstalk**, confirmar que foi publicada uma nova versão pelo Code Pipeline;
   ![](/mob/cloud/img/cp15.png)

14. Confirmar que foi publicada a nova versão:
   ![](/mob/cloud/img/cp16.png)
