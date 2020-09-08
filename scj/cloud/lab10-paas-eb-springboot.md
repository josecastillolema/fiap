# Lab 10 - AWS Elastic Beanstalk com SpringBoot

Em este lab sobre **Elastic Beanstalk (EB)** aprenderemos alguns conceitos importantes da camada de plataforma da AWS:
 - *Deploy* de aplicações
 - Plataformas/entornos de execução disponíveis
 - *Logging*
 - Monitoramento

## *Deploy* local

1. Clonar o repositório:
    ```
    git clone https://github.com/josecastillolema/fiap
    ```
    
2. Navegar ate o diretorio `fiap/scj/cloud/lab10-paas-eb`. O diretorio contem os [seguintes arquivos](lab10-paas-eb-springboot):
    ```
    $ cd fiap/scj/cloud/lab10-paas-eb-springboot
    $ tree .
    .
    ├── mvnw
    ├── mvnw.cmd
    ├── pom.xml
    └── src
      ├── main
      │   ├── java
      │   │   └── com
      │   │       └── amock
      │   │           └── springboot
      │   │               └── SpringbootApplication.java
      │   └── resources
      │       ├── application.properties
      │       └── templates
      │           └── index.html
      └── test
          └── java
              └── com
                  └── amock
                      └── springboot
                          └── SpringbootApplicationTests.java
    ```
    
3. Compilar a aplicação:
    ```
    $ mvn package
    [INFO] Scanning for projects...
    [INFO] 
    [INFO] ------------------------< com.amock:springboot >------------------------
    [INFO] Building springboot 0.0.1-SNAPSHOT
    [INFO] --------------------------------[ jar ]---------------------------------
    [INFO] 
    [INFO] --- maven-resources-plugin:3.1.0:resources (default-resources) @ springboot ---
    [INFO] Using 'UTF-8' encoding to copy filtered resources.
    [INFO] Copying 1 resource
    [INFO] Copying 1 resource
    [INFO] 
    [INFO] --- maven-compiler-plugin:3.8.0:compile (default-compile) @ springboot ---
    [INFO] Changes detected - recompiling the module!
    [INFO] Compiling 1 source file to /Users/jlema/fiap/scj/java/springboot/target/classes
    [INFO] 
    [INFO] --- maven-resources-plugin:3.1.0:testResources (default-testResources) @ springboot ---
    [INFO] Using 'UTF-8' encoding to copy filtered resources.
    [INFO] skip non existing resourceDirectory /Users/jlema/fiap/scj/java/springboot/src/test/resources
    [INFO] 
    [INFO] --- maven-compiler-plugin:3.8.0:testCompile (default-testCompile) @ springboot ---
    [INFO] Changes detected - recompiling the module!
    [INFO] Compiling 1 source file to /Users/jlema/fiap/scj/java/springboot/target/test-classes
    [INFO] 
    [INFO] --- maven-surefire-plugin:2.22.1:test (default-test) @ springboot ---
    [INFO] 
    [INFO] -------------------------------------------------------
    [INFO]  T E S T S
    [INFO] -------------------------------------------------------
    [INFO] Running com.amock.springboot.SpringbootApplicationTests
    16:15:52.395 [main] DEBUG org.springframework.test.context.junit4.SpringJUnit4ClassRunner - SpringJUnit4ClassRunner constructor called with [class com.amock.springboot.SpringbootApplicationTests]
    16:15:52.402 [main] DEBUG org.springframework.test.context.BootstrapUtils - Instantiating CacheAwareContextLoaderDelegate from class [org.springframework.test.context.cache.DefaultCacheAwareContextLoaderDelegate]
    16:15:52.412 [main] DEBUG org.springframework.test.context.BootstrapUtils - Instantiating BootstrapContext using constructor [public org.springframework.test.context.support.DefaultBootstrapContext(java.lang.Class,org.springframework.test.context.CacheAwareContextLoaderDelegate)]
    16:15:52.437 [main] DEBUG org.springframework.test.context.BootstrapUtils - Instantiating TestContextBootstrapper for test class [com.amock.springboot.SpringbootApplicationTests] from class [org.springframework.boot.test.context.SpringBootTestContextBootstrapper]
    16:15:52.469 [main] INFO org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Neither @ContextConfiguration nor @ContextHierarchy found for test class [com.amock.springboot.SpringbootApplicationTests], using SpringBootContextLoader
    16:15:52.474 [main] DEBUG org.springframework.test.context.support.AbstractContextLoader - Did not detect default resource location for test class [com.amock.springboot.SpringbootApplicationTests]: class path resource [com/amock/springboot/SpringbootApplicationTests-context.xml] does not exist
    16:15:52.474 [main] DEBUG org.springframework.test.context.support.AbstractContextLoader - Did not detect default resource location for test class [com.amock.springboot.SpringbootApplicationTests]: class path resource [com/amock/springboot/SpringbootApplicationTestsContext.groovy] does not exist
    16:15:52.475 [main] INFO org.springframework.test.context.support.AbstractContextLoader - Could not detect default resource locations for test class [com.amock.springboot.SpringbootApplicationTests]: no resource found for suffixes {-context.xml, Context.groovy}.
    16:15:52.477 [main] INFO org.springframework.test.context.support.AnnotationConfigContextLoaderUtils - Could not detect default configuration classes for test class [com.amock.springboot.SpringbootApplicationTests]: SpringbootApplicationTests does not declare any static, non-private, non-final, nested classes annotated with @Configuration.
    16:15:52.551 [main] DEBUG org.springframework.test.context.support.ActiveProfilesUtils - Could not find an 'annotation declaring class' for annotation type [org.springframework.test.context.ActiveProfiles] and class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.669 [main] DEBUG org.springframework.context.annotation.ClassPathScanningCandidateComponentProvider - Identified candidate component class: file [/Users/jlema/fiap/scj/java/springboot/target/classes/com/amock/springboot/SpringbootApplication.class]
    16:15:52.670 [main] INFO org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Found @SpringBootConfiguration com.amock.springboot.SpringbootApplication for test class com.amock.springboot.SpringbootApplicationTests
    16:15:52.931 [main] DEBUG org.springframework.boot.test.context.SpringBootTestContextBootstrapper - @TestExecutionListeners is not present for class [com.amock.springboot.SpringbootApplicationTests]: using defaults.
    16:15:52.931 [main] INFO org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Loaded default TestExecutionListener class names from location [META-INF/spring.factories]: [org.springframework.boot.test.mock.mockito.MockitoTestExecutionListener, org.springframework.boot.test.mock.mockito.ResetMocksTestExecutionListener, org.springframework.boot.test.autoconfigure.restdocs.RestDocsTestExecutionListener, org.springframework.boot.test.autoconfigure.web.client.MockRestServiceServerResetTestExecutionListener, org.springframework.boot.test.autoconfigure.web.servlet.MockMvcPrintOnlyOnFailureTestExecutionListener, org.springframework.boot.test.autoconfigure.web.servlet.WebDriverTestExecutionListener, org.springframework.test.context.web.ServletTestExecutionListener, org.springframework.test.context.support.DirtiesContextBeforeModesTestExecutionListener, org.springframework.test.context.support.DependencyInjectionTestExecutionListener, org.springframework.test.context.support.DirtiesContextTestExecutionListener, org.springframework.test.context.transaction.TransactionalTestExecutionListener, org.springframework.test.context.jdbc.SqlScriptsTestExecutionListener]
    16:15:52.952 [main] DEBUG org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Skipping candidate TestExecutionListener [org.springframework.test.context.transaction.TransactionalTestExecutionListener] due to a missing dependency. Specify custom listener classes or make the default listener classes and their required dependencies available. Offending class: [org/springframework/transaction/interceptor/TransactionAttributeSource]
    16:15:52.952 [main] DEBUG org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Skipping candidate TestExecutionListener [org.springframework.test.context.jdbc.SqlScriptsTestExecutionListener] due to a missing dependency. Specify custom listener classes or make the default listener classes and their required dependencies available. Offending class: [org/springframework/transaction/interceptor/TransactionAttribute]
    16:15:52.953 [main] INFO org.springframework.boot.test.context.SpringBootTestContextBootstrapper - Using TestExecutionListeners: [org.springframework.test.context.web.ServletTestExecutionListener@41e36e46, org.springframework.test.context.support.DirtiesContextBeforeModesTestExecutionListener@15c43bd9, org.springframework.boot.test.mock.mockito.MockitoTestExecutionListener@3d74bf60, org.springframework.boot.test.autoconfigure.SpringBootDependencyInjectionTestExecutionListener@4f209819, org.springframework.test.context.support.DirtiesContextTestExecutionListener@15eb5ee5, org.springframework.boot.test.mock.mockito.ResetMocksTestExecutionListener@2145b572, org.springframework.boot.test.autoconfigure.restdocs.RestDocsTestExecutionListener@39529185, org.springframework.boot.test.autoconfigure.web.client.MockRestServiceServerResetTestExecutionListener@72f926e6, org.springframework.boot.test.autoconfigure.web.servlet.MockMvcPrintOnlyOnFailureTestExecutionListener@3daa422a, org.springframework.boot.test.autoconfigure.web.servlet.WebDriverTestExecutionListener@31c88ec8]
    16:15:52.955 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved @ProfileValueSourceConfiguration [null] for test class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.956 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved ProfileValueSource type [class org.springframework.test.annotation.SystemProfileValueSource] for class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.958 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved @ProfileValueSourceConfiguration [null] for test class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.958 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved ProfileValueSource type [class org.springframework.test.annotation.SystemProfileValueSource] for class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.960 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved @ProfileValueSourceConfiguration [null] for test class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.960 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved ProfileValueSource type [class org.springframework.test.annotation.SystemProfileValueSource] for class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.967 [main] DEBUG org.springframework.test.context.support.AbstractDirtiesContextTestExecutionListener - Before test class: context [DefaultTestContext@6eda5c9 testClass = SpringbootApplicationTests, testInstance = [null], testMethod = [null], testException = [null], mergedContextConfiguration = [WebMergedContextConfiguration@55b7a4e0 testClass = SpringbootApplicationTests, locations = '{}', classes = '{class com.amock.springboot.SpringbootApplication}', contextInitializerClasses = '[]', activeProfiles = '{}', propertySourceLocations = '{}', propertySourceProperties = '{org.springframework.boot.test.context.SpringBootTestContextBootstrapper=true}', contextCustomizers = set[org.springframework.boot.test.context.filter.ExcludeFilterContextCustomizer@76707e36, org.springframework.boot.test.json.DuplicateJsonObjectContextCustomizerFactory$DuplicateJsonObjectContextCustomizer@694e1548, org.springframework.boot.test.mock.mockito.MockitoContextCustomizer@0, org.springframework.boot.test.web.client.TestRestTemplateContextCustomizer@15d0c81b, org.springframework.boot.test.autoconfigure.properties.PropertyMappingContextCustomizer@0, org.springframework.boot.test.autoconfigure.web.servlet.WebDriverContextCustomizerFactory$Customizer@7a1ebcd8], resourceBasePath = 'src/main/webapp', contextLoader = 'org.springframework.boot.test.context.SpringBootContextLoader', parent = [null]], attributes = map['org.springframework.test.context.web.ServletTestExecutionListener.activateListener' -> true]], class annotated with @DirtiesContext [false] with mode [null].
    16:15:52.969 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved @ProfileValueSourceConfiguration [null] for test class [com.amock.springboot.SpringbootApplicationTests]
    16:15:52.969 [main] DEBUG org.springframework.test.annotation.ProfileValueUtils - Retrieved ProfileValueSource type [class org.springframework.test.annotation.SystemProfileValueSource] for class [com.amock.springboot.SpringbootApplicationTests]
    16:15:53.019 [main] DEBUG org.springframework.test.context.support.TestPropertySourceUtils - Adding inlined properties to environment: {spring.jmx.enabled=false, org.springframework.boot.test.context.SpringBootTestContextBootstrapper=true, server.port=-1}

      .   ____          _            __ _ _
     /\\ / ___'_ __ _ _(_)_ __  __ _ \ \ \ \
    ( ( )\___ | '_ | '_| | '_ \/ _` | \ \ \ \
     \\/  ___)| |_)| | | | | || (_| |  ) ) ) )
      '  |____| .__|_| |_|_| |_\__, | / / / /
     =========|_|==============|___/=/_/_/_/
     :: Spring Boot ::        (v2.1.4.RELEASE)

    2020-09-08 16:15:53.420  INFO 67459 --- [           main] c.a.s.SpringbootApplicationTests         : Starting SpringbootApplicationTests on jlema-mac with PID 67459 (started by jlema in /Users/jlema/fiap/scj/java/springboot)
    2020-09-08 16:15:53.422  INFO 67459 --- [           main] c.a.s.SpringbootApplicationTests         : No active profile set, falling back to default profiles: default
    2020-09-08 16:15:55.512  INFO 67459 --- [           main] o.s.s.concurrent.ThreadPoolTaskExecutor  : Initializing ExecutorService 'applicationTaskExecutor'
    2020-09-08 16:15:55.886  INFO 67459 --- [           main] o.s.b.a.w.s.WelcomePageHandlerMapping    : Adding welcome page template: index
    2020-09-08 16:15:56.088  INFO 67459 --- [           main] c.a.s.SpringbootApplicationTests         : Started SpringbootApplicationTests in 3.043 seconds (JVM running for 4.122)
    [INFO] Tests run: 1, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 4.186 s - in com.amock.springboot.SpringbootApplicationTests
    2020-09-08 16:15:56.460  INFO 67459 --- [       Thread-2] o.s.s.concurrent.ThreadPoolTaskExecutor  : Shutting down ExecutorService 'applicationTaskExecutor'
    [INFO] 
    [INFO] Results:
    [INFO] 
    [INFO] Tests run: 1, Failures: 0, Errors: 0, Skipped: 0
    [INFO] 
    [INFO] 
    [INFO] --- maven-jar-plugin:3.1.1:jar (default-jar) @ springboot ---
    [INFO] Building jar: /Users/jlema/fiap/scj/java/springboot/target/springboot-0.0.1-SNAPSHOT.jar
    [INFO] 
    [INFO] --- spring-boot-maven-plugin:2.1.4.RELEASE:repackage (repackage) @ springboot ---
    [INFO] Replacing main artifact with repackaged archive
    [INFO] ------------------------------------------------------------------------
    [INFO] BUILD SUCCESS
    [INFO] ------------------------------------------------------------------------
    [INFO] Total time:  9.471 s
    [INFO] Finished at: 2020-09-08T16:15:58-03:00
    [INFO] ------------------------------------------------------------------------
    ```
    
4. Ejecutar a aplicação localmente:
    ```
    $ mvn spring-boot:run
    [INFO] Scanning for projects...
    [INFO] 
    [INFO] ------------------------< com.amock:springboot >------------------------
    [INFO] Building springboot 0.0.1-SNAPSHOT
    [INFO] --------------------------------[ jar ]---------------------------------
    [INFO] 
    [INFO] >>> spring-boot-maven-plugin:2.1.4.RELEASE:run (default-cli) > test-compile @ springboot >>>
    [INFO] 
    [INFO] --- maven-resources-plugin:3.1.0:resources (default-resources) @ springboot ---
    [INFO] Using 'UTF-8' encoding to copy filtered resources.
    [INFO] Copying 1 resource
    [INFO] Copying 1 resource
    [INFO] 
    [INFO] --- maven-compiler-plugin:3.8.0:compile (default-compile) @ springboot ---
    [INFO] Nothing to compile - all classes are up to date
    [INFO] 
    [INFO] --- maven-resources-plugin:3.1.0:testResources (default-testResources) @ springboot ---
    [INFO] Using 'UTF-8' encoding to copy filtered resources.
    [INFO] skip non existing resourceDirectory /Users/jlema/fiap/scj/java/springboot/src/test/resources
    [INFO] 
    [INFO] --- maven-compiler-plugin:3.8.0:testCompile (default-testCompile) @ springboot ---
    [INFO] Nothing to compile - all classes are up to date
    [INFO] 
    [INFO] <<< spring-boot-maven-plugin:2.1.4.RELEASE:run (default-cli) < test-compile @ springboot <<<
    [INFO] 
    [INFO] 
    [INFO] --- spring-boot-maven-plugin:2.1.4.RELEASE:run (default-cli) @ springboot ---

      .   ____          _            __ _ _
     /\\ / ___'_ __ _ _(_)_ __  __ _ \ \ \ \
    ( ( )\___ | '_ | '_| | '_ \/ _` | \ \ \ \
     \\/  ___)| |_)| | | | | || (_| |  ) ) ) )
      '  |____| .__|_| |_|_| |_\__, | / / / /
     =========|_|==============|___/=/_/_/_/
     :: Spring Boot ::        (v2.1.4.RELEASE)

    2020-09-08 16:16:13.871  INFO 67475 --- [           main] c.a.springboot.SpringbootApplication     : Starting SpringbootApplication on jlema-mac with PID 67475 (/Users/jlema/fiap/scj/java/springboot/target/classes started by jlema in /Users/jlema/fiap/scj/java/springboot)
    2020-09-08 16:16:13.874  INFO 67475 --- [           main] c.a.springboot.SpringbootApplication     : No active profile set, falling back to default profiles: default
    2020-09-08 16:16:15.179  INFO 67475 --- [           main] o.s.b.w.embedded.tomcat.TomcatWebServer  : Tomcat initialized with port(s): 9093 (http)
    2020-09-08 16:16:15.213  INFO 67475 --- [           main] o.apache.catalina.core.StandardService   : Starting service [Tomcat]
    2020-09-08 16:16:15.214  INFO 67475 --- [           main] org.apache.catalina.core.StandardEngine  : Starting Servlet engine: [Apache Tomcat/9.0.17]
    2020-09-08 16:16:15.357  INFO 67475 --- [           main] o.a.c.c.C.[Tomcat].[localhost].[/]       : Initializing Spring embedded WebApplicationContext
    2020-09-08 16:16:15.358  INFO 67475 --- [           main] o.s.web.context.ContextLoader            : Root WebApplicationContext: initialization completed in 1430 ms
    2020-09-08 16:16:15.633  INFO 67475 --- [           main] o.s.s.concurrent.ThreadPoolTaskExecutor  : Initializing ExecutorService 'applicationTaskExecutor'
    2020-09-08 16:16:15.842  INFO 67475 --- [           main] o.s.b.a.w.s.WelcomePageHandlerMapping    : Adding welcome page template: index
    2020-09-08 16:16:15.972  INFO 67475 --- [           main] o.s.b.w.embedded.tomcat.TomcatWebServer  : Tomcat started on port(s): 9093 (http) with context path ''
    2020-09-08 16:16:15.976  INFO 67475 --- [           main] c.a.springboot.SpringbootApplication     : Started SpringbootApplication in 2.476 seconds (JVM running for 5.511)
    2020-09-08 16:16:33.667  INFO 67475 --- [nio-9093-exec-1] o.a.c.c.C.[Tomcat].[localhost].[/]       : Initializing Spring DispatcherServlet 'dispatcherServlet'
    2020-09-08 16:16:33.667  INFO 67475 --- [nio-9093-exec-1] o.s.web.servlet.DispatcherServlet        : Initializing Servlet 'dispatcherServlet'
    2020-09-08 16:16:33.673  INFO 67475 --- [nio-9093-exec-1] o.s.web.servlet.DispatcherServlet        : Completed initialization in 6 ms
    ```
    
5. Testar o acesso local:
   ![](img/ebspring0.png)

## *Deploy* na AWS
 
1. Acessar o serviço **Elastic Beanstalk**:
   ![](img/eb0.png)

2. Criar um novo *environment*:
   ![](/mob/cloud/img/eb1.png)

3. A aplicação é um serviço web:
   ![](/mob/cloud/img/eb2.png)
   
4. Configurar o nome da apliação:
   ![](/mob/cloud/img/ebspring1.png)

5. Escolher o entorno de execução:
   ![](/mob/cloud/img/ebspring2.png)
   
6. Fazer o *upload* da aplicação. O arquivo `.jar` se encontra na pasta: `fiap/scj/cloud/lab10-paas-eb/target/springboot-0.0.1-SNAPSHOT.jar` 
   ![](/mob/cloud/img/ebspring3.png)

7. Após uns minutos, conferir o estado da aplicação:
   ![](/mob/cloud/img/eb6.png)

8. Accessar a URL da aplicação:
   ![](/mob/cloud/img/eb7.png)

## *Logging* e monitoramento

9. Se for necessário fazer *troubleshooting* da aplicação, fazer *download* dos logs:
   ![](/mob/cloud/img/eb8.png)

10. Para monitorar a aplicação:
   ![](/mob/cloud/img/eb9.png)
