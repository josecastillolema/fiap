# Lab 3 - Docker Compose

Executando servicos
--------------
Docker Compose permite definir serviços (que a sua vez são formados por containers) e a comunicação entre os mesmos. Esta comunicação é implementada via DNS nos containers. Além disso, no arquivo de configuração do Docker Compose (***docker-compose.yml***) é possível definir:
 - **variáveis de entorno**: comando `environment`
 - **mapeamento de portas**: comando `ports`
 - **persistência de dados**: comando `volumes`
 - **dependências entre os serviços**: comandos `links` e `depends on`
 
Vamos trabalhar com dois terminais abertos (**T1** e **T2**).

1. **[T1]** Instalação do Docker Compose:
    ```
    $ sudo apt  install docker-compose -y
    Reading package lists... Done
    Building dependency tree       
    Reading state information... Done
    The following additional packages will be installed:
      golang-docker-credential-helpers libpython-stdlib libpython2.7-minimal libpython2.7-stdlib libsecret-1-0
      libsecret-common python python-asn1crypto python-backports.ssl-match-hostname python-cached-property python-certifi
      python-cffi-backend python-chardet python-cryptography python-docker python-dockerpty python-dockerpycreds
      python-docopt python-enum34 python-funcsigs python-functools32 python-idna python-ipaddress python-jsonschema
      python-minimal python-mock python-openssl python-pbr python-pkg-resources python-requests python-six
      python-texttable python-urllib3 python-websocket python-yaml python2.7 python2.7-minimal
    Suggested packages:
      python-doc python-tk python-cryptography-doc python-cryptography-vectors python-enum34-doc python-funcsigs-doc
      python-mock-doc python-openssl-doc python-openssl-dbg python-setuptools python-socks python-ntlm python2.7-doc
      binutils binfmt-support
    The following NEW packages will be installed:
      docker-compose golang-docker-credential-helpers libpython-stdlib libpython2.7-minimal libpython2.7-stdlib
      libsecret-1-0 libsecret-common python python-asn1crypto python-backports.ssl-match-hostname python-cached-property
      python-certifi python-cffi-backend python-chardet python-cryptography python-docker python-dockerpty
      python-dockerpycreds python-docopt python-enum34 python-funcsigs python-functools32 python-idna python-ipaddress
      python-jsonschema python-minimal python-mock python-openssl python-pbr python-pkg-resources python-requests
      python-six python-texttable python-urllib3 python-websocket python-yaml python2.7 python2.7-minimal
    0 upgraded, 38 newly installed, 0 to remove and 35 not upgraded.
    Need to get 6014 kB of archives.
    After this operation, 26.6 MB of additional disk space will be used.
    Get:1 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 libpython2.7-minimal amd64 2.7.17-1~18.04 [335 kB]
    Get:2 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 python2.7-minimal amd64 2.7.17-1~18.04 [1294 kB]
    Get:3 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-minimal amd64 2.7.15~rc1-1 [28.1 kB]
    Get:4 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 libpython2.7-stdlib amd64 2.7.17-1~18.04 [1915 kB]
    Get:5 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 python2.7 amd64 2.7.17-1~18.04 [248 kB]
    Get:6 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 libpython-stdlib amd64 2.7.15~rc1-1 [7620 B]
    Get:7 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python amd64 2.7.15~rc1-1 [140 kB]
    Get:8 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-backports.ssl-match-hostname all 3.5.0.1-1 [7024 B]
    Get:9 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-pkg-resources all 39.0.1-2 [128 kB]
    Get:10 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-cached-property all 1.3.1-1 [7568 B]
    Get:11 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-six all 1.11.0-2 [11.3 kB]
    Get:12 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 libsecret-common all 0.18.6-1 [4452 B]
    Get:13 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 libsecret-1-0 amd64 0.18.6-1 [94.6 kB]
    Get:14 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 golang-docker-credential-helpers amd64 0.5.0-2 [444 kB]
    Get:15 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-dockerpycreds all 0.2.1-1 [4138 B]
    Get:16 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-certifi all 2018.1.18-2 [144 kB]
    Get:17 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-chardet all 3.0.4-1 [80.3 kB]
    Get:18 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-idna all 2.6-1 [32.4 kB]
    Get:19 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 python-urllib3 all 1.22-1ubuntu0.18.04.1 [85.9 kB]
    Get:20 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 python-requests all 2.18.4-2ubuntu0.1 [58.5 kB]
    Get:21 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-websocket all 0.44.0-0ubuntu2 [30.7 kB]
    Get:22 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-ipaddress all 1.0.17-1 [18.2 kB]
    Get:23 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-docker all 2.5.1-1 [69.0 kB]
    Get:24 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-dockerpty all 0.4.1-1 [10.8 kB]
    Get:25 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-docopt all 0.6.2-1build1 [25.6 kB]
    Get:26 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-enum34 all 1.1.6-2 [34.8 kB]
    Get:27 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-functools32 all 3.2.3.2-3 [10.8 kB]
    Get:28 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-funcsigs all 1.0.2-4 [13.5 kB]
    Get:29 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-pbr all 3.1.1-3ubuntu3 [53.7 kB]
    Get:30 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-mock all 2.0.0-3 [47.4 kB]
    Get:31 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-jsonschema all 2.6.0-2 [31.5 kB]
    Get:32 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 python-texttable all 0.9.1-1 [8160 B]
    Get:33 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-yaml amd64 3.12-1build2 [115 kB]
    Get:34 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/universe amd64 docker-compose all 1.17.1-2 [76.3 kB]
    Get:35 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-asn1crypto all 0.24.0-1 [72.7 kB]
    Get:36 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-cffi-backend amd64 1.11.5-1 [63.4 kB]
    Get:37 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic-updates/main amd64 python-cryptography amd64 2.1.4-1ubuntu1.3 [221 kB]
    Get:38 http://us-east-1.ec2.archive.ubuntu.com/ubuntu bionic/main amd64 python-openssl all 17.5.0-1ubuntu1 [41.3 kB]
    Fetched 6014 kB in 0s (27.4 MB/s)         
    Extracting templates from packages: 100%
    Selecting previously unselected package libpython2.7-minimal:amd64.
    (Reading database ... 111722 files and directories currently installed.)
    Preparing to unpack .../0-libpython2.7-minimal_2.7.17-1~18.04_amd64.deb ...
    Unpacking libpython2.7-minimal:amd64 (2.7.17-1~18.04) ...
    Selecting previously unselected package python2.7-minimal.
    Preparing to unpack .../1-python2.7-minimal_2.7.17-1~18.04_amd64.deb ...
    Unpacking python2.7-minimal (2.7.17-1~18.04) ...
    Selecting previously unselected package python-minimal.
    Preparing to unpack .../2-python-minimal_2.7.15~rc1-1_amd64.deb ...
    Unpacking python-minimal (2.7.15~rc1-1) ...
    Selecting previously unselected package libpython2.7-stdlib:amd64.
    Preparing to unpack .../3-libpython2.7-stdlib_2.7.17-1~18.04_amd64.deb ...
    Unpacking libpython2.7-stdlib:amd64 (2.7.17-1~18.04) ...
    Selecting previously unselected package python2.7.
    Preparing to unpack .../4-python2.7_2.7.17-1~18.04_amd64.deb ...
    Unpacking python2.7 (2.7.17-1~18.04) ...
    Selecting previously unselected package libpython-stdlib:amd64.
    Preparing to unpack .../5-libpython-stdlib_2.7.15~rc1-1_amd64.deb ...
    Unpacking libpython-stdlib:amd64 (2.7.15~rc1-1) ...
    Setting up libpython2.7-minimal:amd64 (2.7.17-1~18.04) ...
    Setting up python2.7-minimal (2.7.17-1~18.04) ...
    Linking and byte-compiling packages for runtime python2.7...
    Setting up python-minimal (2.7.15~rc1-1) ...
    Selecting previously unselected package python.
    (Reading database ... 112470 files and directories currently installed.)
    Preparing to unpack .../00-python_2.7.15~rc1-1_amd64.deb ...
    Unpacking python (2.7.15~rc1-1) ...
    Selecting previously unselected package python-backports.ssl-match-hostname.
    Preparing to unpack .../01-python-backports.ssl-match-hostname_3.5.0.1-1_all.deb ...
    Unpacking python-backports.ssl-match-hostname (3.5.0.1-1) ...
    Selecting previously unselected package python-pkg-resources.
    Preparing to unpack .../02-python-pkg-resources_39.0.1-2_all.deb ...
    Unpacking python-pkg-resources (39.0.1-2) ...
    Selecting previously unselected package python-cached-property.
    Preparing to unpack .../03-python-cached-property_1.3.1-1_all.deb ...
    Unpacking python-cached-property (1.3.1-1) ...
    Selecting previously unselected package python-six.
    Preparing to unpack .../04-python-six_1.11.0-2_all.deb ...
    Unpacking python-six (1.11.0-2) ...
    Selecting previously unselected package libsecret-common.
    Preparing to unpack .../05-libsecret-common_0.18.6-1_all.deb ...
    Unpacking libsecret-common (0.18.6-1) ...
    Selecting previously unselected package libsecret-1-0:amd64.
    Preparing to unpack .../06-libsecret-1-0_0.18.6-1_amd64.deb ...
    Unpacking libsecret-1-0:amd64 (0.18.6-1) ...
    Selecting previously unselected package golang-docker-credential-helpers.
    Preparing to unpack .../07-golang-docker-credential-helpers_0.5.0-2_amd64.deb ...
    Unpacking golang-docker-credential-helpers (0.5.0-2) ...
    Selecting previously unselected package python-dockerpycreds.
    Preparing to unpack .../08-python-dockerpycreds_0.2.1-1_all.deb ...
    Unpacking python-dockerpycreds (0.2.1-1) ...
    Selecting previously unselected package python-certifi.
    Preparing to unpack .../09-python-certifi_2018.1.18-2_all.deb ...
    Unpacking python-certifi (2018.1.18-2) ...
    Selecting previously unselected package python-chardet.
    Preparing to unpack .../10-python-chardet_3.0.4-1_all.deb ...
    Unpacking python-chardet (3.0.4-1) ...
    Selecting previously unselected package python-idna.
    Preparing to unpack .../11-python-idna_2.6-1_all.deb ...
    Unpacking python-idna (2.6-1) ...
    Selecting previously unselected package python-urllib3.
    Preparing to unpack .../12-python-urllib3_1.22-1ubuntu0.18.04.1_all.deb ...
    Unpacking python-urllib3 (1.22-1ubuntu0.18.04.1) ...
    Selecting previously unselected package python-requests.
    Preparing to unpack .../13-python-requests_2.18.4-2ubuntu0.1_all.deb ...
    Unpacking python-requests (2.18.4-2ubuntu0.1) ...
    Selecting previously unselected package python-websocket.
    Preparing to unpack .../14-python-websocket_0.44.0-0ubuntu2_all.deb ...
    Unpacking python-websocket (0.44.0-0ubuntu2) ...
    Selecting previously unselected package python-ipaddress.
    Preparing to unpack .../15-python-ipaddress_1.0.17-1_all.deb ...
    Unpacking python-ipaddress (1.0.17-1) ...
    Selecting previously unselected package python-docker.
    Preparing to unpack .../16-python-docker_2.5.1-1_all.deb ...
    Unpacking python-docker (2.5.1-1) ...
    Selecting previously unselected package python-dockerpty.
    Preparing to unpack .../17-python-dockerpty_0.4.1-1_all.deb ...
    Unpacking python-dockerpty (0.4.1-1) ...
    Selecting previously unselected package python-docopt.
    Preparing to unpack .../18-python-docopt_0.6.2-1build1_all.deb ...
    Unpacking python-docopt (0.6.2-1build1) ...
    Selecting previously unselected package python-enum34.
    Preparing to unpack .../19-python-enum34_1.1.6-2_all.deb ...
    Unpacking python-enum34 (1.1.6-2) ...
    Selecting previously unselected package python-functools32.
    Preparing to unpack .../20-python-functools32_3.2.3.2-3_all.deb ...
    Unpacking python-functools32 (3.2.3.2-3) ...
    Selecting previously unselected package python-funcsigs.
    Preparing to unpack .../21-python-funcsigs_1.0.2-4_all.deb ...
    Unpacking python-funcsigs (1.0.2-4) ...
    Selecting previously unselected package python-pbr.
    Preparing to unpack .../22-python-pbr_3.1.1-3ubuntu3_all.deb ...
    Unpacking python-pbr (3.1.1-3ubuntu3) ...
    Selecting previously unselected package python-mock.
    Preparing to unpack .../23-python-mock_2.0.0-3_all.deb ...
    Unpacking python-mock (2.0.0-3) ...
    Selecting previously unselected package python-jsonschema.
    Preparing to unpack .../24-python-jsonschema_2.6.0-2_all.deb ...
    Unpacking python-jsonschema (2.6.0-2) ...
    Selecting previously unselected package python-texttable.
    Preparing to unpack .../25-python-texttable_0.9.1-1_all.deb ...
    Unpacking python-texttable (0.9.1-1) ...
    Selecting previously unselected package python-yaml.
    Preparing to unpack .../26-python-yaml_3.12-1build2_amd64.deb ...
    Unpacking python-yaml (3.12-1build2) ...
    Selecting previously unselected package docker-compose.
    Preparing to unpack .../27-docker-compose_1.17.1-2_all.deb ...
    Unpacking docker-compose (1.17.1-2) ...
    Selecting previously unselected package python-asn1crypto.
    Preparing to unpack .../28-python-asn1crypto_0.24.0-1_all.deb ...
    Unpacking python-asn1crypto (0.24.0-1) ...
    Selecting previously unselected package python-cffi-backend.
    Preparing to unpack .../29-python-cffi-backend_1.11.5-1_amd64.deb ...
    Unpacking python-cffi-backend (1.11.5-1) ...
    Selecting previously unselected package python-cryptography.
    Preparing to unpack .../30-python-cryptography_2.1.4-1ubuntu1.3_amd64.deb ...
    Unpacking python-cryptography (2.1.4-1ubuntu1.3) ...
    Selecting previously unselected package python-openssl.
    Preparing to unpack .../31-python-openssl_17.5.0-1ubuntu1_all.deb ...
    Unpacking python-openssl (17.5.0-1ubuntu1) ...
    Setting up libsecret-common (0.18.6-1) ...
    Setting up libsecret-1-0:amd64 (0.18.6-1) ...
    Setting up libpython2.7-stdlib:amd64 (2.7.17-1~18.04) ...
    Setting up python2.7 (2.7.17-1~18.04) ...
    Setting up libpython-stdlib:amd64 (2.7.15~rc1-1) ...
    Setting up golang-docker-credential-helpers (0.5.0-2) ...
    Setting up python (2.7.15~rc1-1) ...
    Setting up python-idna (2.6-1) ...
    Setting up python-texttable (0.9.1-1) ...
    Setting up python-functools32 (3.2.3.2-3) ...
    Setting up python-yaml (3.12-1build2) ...
    Setting up python-asn1crypto (0.24.0-1) ...
    Setting up python-certifi (2018.1.18-2) ...
    Setting up python-pkg-resources (39.0.1-2) ...
    Setting up python-backports.ssl-match-hostname (3.5.0.1-1) ...
    Setting up python-cffi-backend (1.11.5-1) ...
    Setting up python-six (1.11.0-2) ...
    Setting up python-dockerpty (0.4.1-1) ...
    Setting up python-pbr (3.1.1-3ubuntu3) ...
    update-alternatives: using /usr/bin/python2-pbr to provide /usr/bin/pbr (pbr) in auto mode
    Setting up python-enum34 (1.1.6-2) ...
    Setting up python-funcsigs (1.0.2-4) ...
    Setting up python-docopt (0.6.2-1build1) ...
    Setting up python-ipaddress (1.0.17-1) ...
    Setting up python-cached-property (1.3.1-1) ...
    Setting up python-urllib3 (1.22-1ubuntu0.18.04.1) ...
    Setting up python-chardet (3.0.4-1) ...
    Setting up python-dockerpycreds (0.2.1-1) ...
    Setting up python-mock (2.0.0-3) ...
    Setting up python-websocket (0.44.0-0ubuntu2) ...
    update-alternatives: using /usr/bin/python2-wsdump to provide /usr/bin/wsdump (wsdump) in auto mode
    Setting up python-cryptography (2.1.4-1ubuntu1.3) ...
    Setting up python-requests (2.18.4-2ubuntu0.1) ...
    Setting up python-jsonschema (2.6.0-2) ...
    update-alternatives: using /usr/bin/python2-jsonschema to provide /usr/bin/jsonschema (jsonschema) in auto mode
    Setting up python-openssl (17.5.0-1ubuntu1) ...
    Setting up python-docker (2.5.1-1) ...
    Setting up docker-compose (1.17.1-2) ...
    Processing triggers for man-db (2.8.3-2ubuntu0.1) ...
    Processing triggers for mime-support (3.60ubuntu1) ...
    Processing triggers for libc-bin (2.27-3ubuntu1) ...
    ```

2. **[T1]** Navegar ate a pasta ***/fiap/aso/compose*** de este repositório *git*:
    ```
    $ cd fiap/aso/compose/
    $ pwd
    /home/ubuntu/fiap/aso/compose
    $ ls
    api_v1  api_v2  api_v3  docker-compose.yml  mongo  mysql

    ```

3. **[T1]** Mostrar o conteúdo do arquivo ***docker-compose.yml***. São definidos dois serviços:
    - **api**: a API escrita em Python, que tem dependência (consulta) o serviço *mysql*
    - **mysql**: o servidor MySQL, com mapeamento de portas (porta 3306), persistência de dados (pasta */var/lib/mysql*) e algumas variáveis de entorno
    ```
    $ cat docker-compose.yml 
    version: '2'
    services:

      api:
        build: api_v2/.
        ports:
          - "4000:5000"
        links:
          - mysql
        depends_on:
          - mysql

      mysql:
        build: mysql/.
        ports:
          - "3306:3306"
        volumes:
          - /var/lib/mysql
        environment:
           MYSQL_ROOT_PASSWORD: senhaFiap
           MYSQL_USER: root
           MYSQL_DATABASE: fiapdb
    ```
  
4. **[T1]** Criar os serviços definidos no arquivo ***docker-compose.yml***:
    ```
    $ docker-compose up
    Creating network "compose_default" with the default driver
    Building mysql
    Step 1/3 : FROM mysql
     ---> 9228ee8bac7a
    Step 2/3 : MAINTAINER Jose Castillo <profjose.lema@fiap.com.br>
     ---> Running in 5f85965fa52d
    Removing intermediate container 5f85965fa52d
     ---> 1035c57e599d
    Step 3/3 : ADD ./aso.sql /docker-entrypoint-initdb.d
     ---> a809a9c5ca56
    Successfully built a809a9c5ca56
    Successfully tagged compose_mysql:latest
    WARNING: Image for service mysql was built because it did not already exist. To rebuild this image you must use `docker-compose build` or `docker-compose up --build`.
    Building api
    Step 1/8 : FROM python:2
     ---> 154f4db0c875
    Step 2/8 : MAINTAINER Jose Castillo <profjose.lema@fiap.com.br>
     ---> Using cache
     ---> defda7283ddf
    Step 3/8 : ADD api.py requirements.txt /
     ---> abb5e8e662ca
    Step 4/8 : RUN pip install -r ./requirements.txt
     ---> Running in ebd156421186
    DEPRECATION: Python 2.7 reached the end of its life on January 1st, 2020. Please upgrade your Python as Python 2.7 is no longer maintained. A future version of pip will drop support for Python 2.7. More details about Python 2 support in pip, can be found at https://pip.pypa.io/en/latest/development/release-process/#python-2-support
    Collecting flask
      Downloading Flask-1.1.2-py2.py3-none-any.whl (94 kB)
    Collecting flask-mysql
      Downloading Flask_MySQL-1.5.1-py2.py3-none-any.whl (3.8 kB)
    Collecting flask-cors
      Downloading Flask_Cors-3.0.8-py2.py3-none-any.whl (14 kB)
    Collecting cryptography
      Downloading cryptography-2.9-cp27-cp27mu-manylinux2010_x86_64.whl (2.7 MB)
    Collecting click>=5.1
      Downloading click-7.1.1-py2.py3-none-any.whl (82 kB)
    Collecting itsdangerous>=0.24
      Downloading itsdangerous-1.1.0-py2.py3-none-any.whl (16 kB)
    Collecting Werkzeug>=0.15
      Downloading Werkzeug-1.0.1-py2.py3-none-any.whl (298 kB)
    Collecting Jinja2>=2.10.1
      Downloading Jinja2-2.11.1-py2.py3-none-any.whl (126 kB)
    Collecting PyMySQL
      Downloading PyMySQL-0.9.3-py2.py3-none-any.whl (47 kB)
    Requirement already satisfied: Six in /usr/local/lib/python2.7/site-packages (from flask-cors->-r ./requirements.txt (line 3)) (1.14.0)
    Collecting ipaddress; python_version < "3"
      Downloading ipaddress-1.0.23-py2.py3-none-any.whl (18 kB)
    Collecting cffi!=1.11.3,>=1.8
      Downloading cffi-1.14.0-cp27-cp27mu-manylinux1_x86_64.whl (387 kB)
    Collecting enum34; python_version < "3"
      Downloading enum34-1.1.10-py2-none-any.whl (11 kB)
    Collecting MarkupSafe>=0.23
      Downloading MarkupSafe-1.1.1-cp27-cp27mu-manylinux1_x86_64.whl (24 kB)
    Collecting pycparser
      Downloading pycparser-2.20-py2.py3-none-any.whl (112 kB)
    Installing collected packages: click, itsdangerous, Werkzeug, MarkupSafe, Jinja2, flask, PyMySQL, flask-mysql, flask-cors, ipaddress, pycparser, cffi, enum34, cryptography
    Successfully installed Jinja2-2.11.1 MarkupSafe-1.1.1 PyMySQL-0.9.3 Werkzeug-1.0.1 cffi-1.14.0 click-7.1.1 cryptography-2.9 enum34-1.1.10 flask-1.1.2 flask-cors-3.0.8 flask-mysql-1.5.1 ipaddress-1.0.23 itsdangerous-1.1.0 pycparser-2.20
    Removing intermediate container ebd156421186
     ---> 1897edcddbb5
    Step 5/8 : ENV PORT=5000
     ---> Running in 8dd8bb035819
    Removing intermediate container 8dd8bb035819
     ---> 3b92b6fb59a5
    Step 6/8 : EXPOSE $PORT
     ---> Running in 99d862666779
    Removing intermediate container 99d862666779
     ---> 05c77804ed9d
    Step 7/8 : HEALTHCHECK CMD curl --fail http://localhost:$PORT || exit 1
     ---> Running in ecbcbde1d83c
    Removing intermediate container ecbcbde1d83c
     ---> 178843362cde
    Step 8/8 : CMD [ "./api.py" ]
     ---> Running in bfe14e880a4b
    Removing intermediate container bfe14e880a4b
     ---> e401dc1035d7
    Successfully built e401dc1035d7
    Successfully tagged compose_api:latest
    WARNING: Image for service api was built because it did not already exist. To rebuild this image you must use `docker-compose build` or `docker-compose up --build`.
    Creating compose_mysql_1 ... 
    Creating compose_mysql_1 ... done
    Creating compose_api_1 ... 
    Creating compose_api_1 ... done
    Attaching to compose_mysql_1, compose_api_1
    mysql_1  | 2020-04-08 19:52:56+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    mysql_1  | 2020-04-08 19:52:57+00:00 [Note] [Entrypoint]: Switching to dedicated user 'mysql'
    mysql_1  | 2020-04-08 19:52:57+00:00 [Note] [Entrypoint]: Entrypoint script for MySQL Server 8.0.19-1debian10 started.
    mysql_1  | 2020-04-08 19:52:57+00:00 [Note] [Entrypoint]: Initializing database files
    mysql_1  | 2020-04-08T19:52:57.639740Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    mysql_1  | 2020-04-08T19:52:57.639830Z 0 [System] [MY-013169] [Server] /usr/sbin/mysqld (mysqld 8.0.19) initializing of server in progress as process 41
    api_1    |  * Serving Flask app "api" (lazy loading)
    api_1    |  * Environment: production
    api_1    |    WARNING: This is a development server. Do not use it in a production deployment.
    api_1    |    Use a production WSGI server instead.
    api_1    |  * Debug mode: on
    api_1    |  * Running on http://0.0.0.0:5000/ (Press CTRL+C to quit)
    api_1    |  * Restarting with stat
    api_1    |  * Debugger is active!
    api_1    |  * Debugger PIN: 463-518-758
    mysql_1  | 2020-04-08T19:53:03.024229Z 5 [Warning] [MY-010453] [Server] root@localhost is created with an empty password ! Please consider switching off the --initialize-insecure option.
    mysql_1  | 2020-04-08 19:53:08+00:00 [Note] [Entrypoint]: Database files initialized
    mysql_1  | 2020-04-08 19:53:08+00:00 [Note] [Entrypoint]: Starting temporary server
    mysql_1  | 2020-04-08T19:53:08.721880Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    mysql_1  | 2020-04-08T19:53:08.722007Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 91
    mysql_1  | 2020-04-08T19:53:09.446603Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    mysql_1  | 2020-04-08T19:53:09.457240Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    mysql_1  | 2020-04-08T19:53:09.478577Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 0  MySQL Community Server - GPL.
    mysql_1  | 2020-04-08 19:53:09+00:00 [Note] [Entrypoint]: Temporary server started.
    mysql_1  | 2020-04-08T19:53:09.527830Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock'
    mysql_1  | Warning: Unable to load '/usr/share/zoneinfo/iso3166.tab' as time zone. Skipping it.
    mysql_1  | Warning: Unable to load '/usr/share/zoneinfo/leap-seconds.list' as time zone. Skipping it.
    mysql_1  | Warning: Unable to load '/usr/share/zoneinfo/zone.tab' as time zone. Skipping it.
    mysql_1  | Warning: Unable to load '/usr/share/zoneinfo/zone1970.tab' as time zone. Skipping it.
    mysql_1  | 2020-04-08 19:53:13+00:00 [Note] [Entrypoint]: Creating database fiapdb
    mysql_1  | 
    mysql_1  | 2020-04-08 19:53:13+00:00 [Note] [Entrypoint]: /usr/local/bin/docker-entrypoint.sh: running /docker-entrypoint-initdb.d/aso.sql
    mysql_1  | 
    mysql_1  | 
    mysql_1  | 2020-04-08 19:53:14+00:00 [Note] [Entrypoint]: Stopping temporary server
    mysql_1  | 2020-04-08T19:53:14.140552Z 12 [System] [MY-013172] [Server] Received SHUTDOWN from user root. Shutting down mysqld (Version: 8.0.19).
    mysql_1  | 2020-04-08T19:53:15.777586Z 0 [System] [MY-010910] [Server] /usr/sbin/mysqld: Shutdown complete (mysqld 8.0.19)  MySQL Community Server - GPL.
    mysql_1  | 2020-04-08 19:53:16+00:00 [Note] [Entrypoint]: Temporary server stopped
    mysql_1  | 
    mysql_1  | 2020-04-08 19:53:16+00:00 [Note] [Entrypoint]: MySQL init process done. Ready for start up.
    mysql_1  | 
    mysql_1  | 2020-04-08T19:53:16.482021Z 0 [Warning] [MY-011070] [Server] 'Disabling symbolic links using --skip-symbolic-links (or equivalent) is the default. Consider not using this option as it' is deprecated and will be removed in a future release.
    mysql_1  | 2020-04-08T19:53:16.482148Z 0 [System] [MY-010116] [Server] /usr/sbin/mysqld (mysqld 8.0.19) starting as process 1
    mysql_1  | 2020-04-08T19:53:17.150498Z 0 [Warning] [MY-010068] [Server] CA certificate ca.pem is self signed.
    mysql_1  | 2020-04-08T19:53:17.159715Z 0 [Warning] [MY-011810] [Server] Insecure configuration for --pid-file: Location '/var/run/mysqld' in the path is accessible to all OS users. Consider choosing a different directory.
    mysql_1  | 2020-04-08T19:53:17.180954Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.19'  socket: '/var/run/mysqld/mysqld.sock'  port: 3306  MySQL Community Server - GPL.
    mysql_1  | 2020-04-08T19:53:17.291602Z 0 [System] [MY-011323] [Server] X Plugin ready for connections. Socket: '/var/run/mysqld/mysqlx.sock' bind-address: '::' port: 33060
    ```

5. **[T1]** (Opcional) O comando anterior pode ser executado com a opção **-d**. De esta forma os containers são executados no *background*, e não será necessário usar dois terminais. Porém, temos menos visibilidade no que está acontecendo nos containers:
    ```
    $ docker-compose up -d
    Creating network "compose_default" with the default driver
    Creating compose_mysql_1 ... 
    Creating compose_mysql_1 ... done
    Creating compose_api_1 ... 
    Creating compose_api_1 ... done
    ```

6. **[T2]** Em um segundo terminal, confirmar que os serviços do Docker Compose foram criados corretamente. E necessário navegar ate pasta que contém o arquivo ***docker-compose.yml***.
    ```
    $ cd fiap/aso/compose/
    $ pwd
    /home/ubuntu/fiap/aso/compose
    $ docker-compose ps
         Name                   Command             State                 Ports              
    -----------------------------------------------------------------------------------------
    compose_api_1     ./api.py                      Up      0.0.0.0:4000->5000/tcp           
    compose_mysql_1   docker-entrypoint.sh mysqld   Up      0.0.0.0:3306->3306/tcp, 33060/tcp
    ```

7. **[T2]** Confirmar que os containers foram criados corretamente:
    ```
    $ docker ps
    CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS                        PORTS                               NAMES
    6d7277694bc3        compose_api         "./api.py"               2 minutes ago       Up About a minute (healthy)   0.0.0.0:4000->5000/tcp              compose_api_1
    349228f10355        compose_mysql       "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes                  0.0.0.0:3306->3306/tcp, 33060/tcp   compose_mysql_1
    ```  

8. **[T2]** Testar a API:
    ``` 
    $ curl localhost:4000
    Benvido a API FIAP!
    ``` 

9. **[T2]** Testar a conexão da API com o banco de dados:
    ``` 
    $ curl localhost:4000/getDados
    [{"id": 1234, "name": "Jose Castillo Lema"}]
    ```

10. **[T2]** Conferir a configuração de DNS, usando o ID do container *compose_api* obtido no passo 6:
    ``` 
    $ docker exec 6d7277694bc3 ping -c 3 mysql
    PING mysql (172.18.0.2) 56(84) bytes of data.
    64 bytes from compose_mysql_1.compose_default (172.18.0.2): icmp_seq=1 ttl=64 time=0.073 ms
    64 bytes from compose_mysql_1.compose_default (172.18.0.2): icmp_seq=2 ttl=64 time=0.070 ms
    64 bytes from compose_mysql_1.compose_default (172.18.0.2): icmp_seq=3 ttl=64 time=0.072 ms

    --- mysql ping statistics ---
    3 packets transmitted, 3 received, 0% packet loss, time 35ms
    rtt min/avg/max/mdev = 0.070/0.071/0.073/0.009 ms
    ```

11. **[T2]** Remover os serviços (e os containers):
    ```
    $ docker-compose down
    Stopping compose_api_1   ... done
    Stopping compose_mysql_1 ... done
    Removing compose_api_1   ... done
    Removing compose_mysql_1 ... done
    Removing network compose_default
    ```
