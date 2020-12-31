# Lab 7 - AWS CP

Em este lab sobre [**Code Pipeline**](https://aws.amazon.com/pt/codepipeline/) aprenderemos alguns conceitos importantes da criação de *pipelines*:
 - Criação do *pipeline*
 - Automação de *deploy* no Beanstalk

## Pre-reqs

- A aplicação do [lab de Beanstalk](https://github.com/josecastillolema/fiap/blob/master/shift/multicloud/lab06-paas-eb.md) precisa estar no ar:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp00-0.png)
   
- Criação de um repositório no [GitHub](https://github.com/) com [os arquivos do lab de Beanstalk](https://github.com/josecastillolema/fiap/tree/master/shift/multicloud/lab06-paas-eb):
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp00-1.png)


## Criando o *pipeline*
 
1. Acessar o serviço **Code Pipeline**:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp01.png)
   
2. Criar um novo *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp02.png)

3. Nomear o *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp03.png)

4. Escolher `GitHub` como *source provider* e o repositório criado nos pre-reqs:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp04.png)

5. Já que usamos uma linguagem interpretada (Python), pulamos a fase de compilação:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp05.png)

6. Selecionar `AWS Elastic Beanstalk` como *Deploy Provider*, apontando para a aplicação dos pre-reqs:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp06.png)

7. Revisar as configuraçoes e confirmar a criação do *pipeline*:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp07.png)
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp08.png)

8. Acompanhar o *pipeline* pelas diversas fases:
    * Recuperação do código do repositório
    * Deploy no Beanstalk
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp09.png)

9. **No Beanstalk**, confirmar que foi publicada uma nova versão pelo Code Pipeline;
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp10.png)

10. Confirmar que a aplicação está no ar:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp11.png)

11. Publicar uma nova versão da aplicação no repositório do GitHub:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp12.png)
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp13.png)

12. Acompanhar o *deploy* da nova versão no Code Pipeline:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp14.png)

13. **No Beanstalk**, confirmar que foi publicada uma nova versão pelo Code Pipeline;
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp15.png)

14. Confirmar que foi publicada a nova versão:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/cp16.png)
