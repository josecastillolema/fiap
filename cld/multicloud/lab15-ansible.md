# Lab 15 - Ansible

## Ansible

## Pre-reqs

- Uma VM com a imagem `Amazon Linux` que será usada como destino do playbook

- O terminal do AWS Academy Learner Lab será usado como bastion para rodar os playbooks do Ansible
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/multicloud/img/a1.png)


## Instalação (no terminal do AWS Academy Learner Lab)

1. Instalar o `ansible` via `pip`:

    ```
    $ pip install ansible
    Defaulting to user installation because normal site-packages is not writeable
    Collecting ansible
      Downloading ansible-4.5.0.tar.gz (35.5 MB)
         |████████████████████████████████| 35.5 MB 141 kB/s Collecting ansible-core<2.12,>=2.11.4
      Downloading ansible-core-2.11.4.tar.gz (6.8 MB)     |████████████████████████████████| 6.8 MB 55.2 MB/s 
    Requirement already satisfied: jinja2 in /usr/local/lib/python3.6/site-packages (from ansible-core<2.12,>=2.11.4->ansible) (2.10)
    Requirement already satisfied: PyYAML in /usr/local/lib/python3.6/site-packages (from ansible-core<2.12,>=2.11.4->ansible) (3.12)
    Requirement already satisfied: cryptography in /usr/local/lib/python3.6/site-packages (from ansible-core<2.12,>=2.11.4->ansible) (2.8)
    Requirement already satisfied: packaging in /usr/local/lib/python3.6/site-packages (from ansible-
    core<2.12,>=2.11.4->ansible) (17.1)
    Collecting resolvelib<0.6.0,>=0.5.3  Downloading resolvelib-0.5.4-py2.py3-none-any.whl (12 kB)
    Requirement already satisfied: cffi!=1.11.3,>=1.8 in /usr/local/lib/python3.6/site-packages (from cryptography->ansible-core<2.12,>=2.11.4->ansible) (1.13.2)
    Requirement already satisfied: six>=1.4.1 in /usr/local/lib/python3.6/site-packages (from cryptog
    raphy->ansible-core<2.12,>=2.11.4->ansible) (1.13.0)Requirement already satisfied: pycparser in /usr/local/lib/python3.6/site-packages (from cffi!=1.
    11.3,>=1.8->cryptography->ansible-core<2.12,>=2.11.4->ansible) (2.18)Requirement already satisfied: MarkupSafe>=0.23 in /usr/local/lib/python3.6/site-packages (from j
    inja2->ansible-core<2.12,>=2.11.4->ansible) (1.0)
    Requirement already satisfied: pyparsing>=2.0.2 in /usr/local/lib/python3.6/site-packages (from packaging->ansible-core<2.12,>=2.11.4->ansible) (2.2.0)
    Building wheels for collected packages: ansible, ansible-core
      Building wheel for ansible (setup.py) ... done
      Created wheel for ansible: filename=ansible-4.5.0-py3-none-any.whl size=58416593 sha256=cd10026
    1551b7e750f527df65a11e177a47a1bbceb758d2fc7ce4b83cb0d7d3f
      Stored in directory: /mnt/data2/students/sub1/ddd_v1_w_KazC_645304/asn482177_1/asn482178_1/work
    /.cache/pip/wheels/3d/07/88/485d832c1277b3e6c7a8ba90376b474265224c98cdb9897054
      Building wheel for ansible-core (setup.py) ... done
      Created wheel for ansible-core: filename=ansible_core-2.11.4-py3-none-any.whl size=1947298 sha2
    56=a8a14e920bd235e629bb692674593b7cce013f67391ae5882aed6441492dec76
      Stored in directory: /mnt/data2/students/sub1/ddd_v1_w_KazC_645304/asn482177_1/asn482178_1/work
    /.cache/pip/wheels/61/05/a3/cf0a0f377f42ddffae82582e0b563c6fc1bbd7feb0e6fc969a
    Successfully built ansible ansible-core
    Installing collected packages: resolvelib, ansible-core, ansible

    Successfully installed ansible ansible-core resolvelib
    ```
    
2. Testar a instalação:
    ```
    $ ansible -h
    usage: ansible [-h] [--version] [-v] [-b] [--become-method BECOME_METHOD]
                   [--become-user BECOME_USER] [-K] [-i INVENTORY] [--list-hosts]
                   [-l SUBSET] [-P POLL_INTERVAL] [-B SECONDS] [-o] [-t TREE] [-k]
                   [--private-key PRIVATE_KEY_FILE] [-u REMOTE_USER]
                   [-c CONNECTION] [-T TIMEOUT]
                   [--ssh-common-args SSH_COMMON_ARGS]
                   [--sftp-extra-args SFTP_EXTRA_ARGS]
                   [--scp-extra-args SCP_EXTRA_ARGS]
                   [--ssh-extra-args SSH_EXTRA_ARGS] [-C] [--syntax-check] [-D]
                   [-e EXTRA_VARS] [--vault-id VAULT_IDS]
                   [--ask-vault-password | --vault-password-file VAULT_PASSWORD_FILES]
                   [-f FORKS] [-M MODULE_PATH] [--playbook-dir BASEDIR]
                   [--task-timeout TASK_TIMEOUT] [-a MODULE_ARGS] [-m MODULE_NAME]
                   pattern

    Define and run a single task 'playbook' against a set of hosts

    positional arguments:
      pattern               host pattern

    optional arguments:
      --ask-vault-password, --ask-vault-pass
                            ask for vault password
      --list-hosts          outputs a list of matching hosts; does not execute
                            anything else
      --playbook-dir BASEDIR
                            Since this tool does not use playbooks, use this as a
                            substitute playbook directory.This sets the relative
                            path for many features including roles/ group_vars/
                            etc.
      --syntax-check        perform a syntax check on the playbook, but do not
                            execute it
      --task-timeout TASK_TIMEOUT
                            set task timeout limit in seconds, must be positive
                            integer.
      --vault-id VAULT_IDS  the vault identity to use
      --vault-password-file VAULT_PASSWORD_FILES, --vault-pass-file VAULT_PASSWORD_FILES
                            vault password file
      --version             show program's version number, config file location,
                            configured module search path, module location,
                            executable location and exit
      -B SECONDS, --background SECONDS
                            run asynchronously, failing after X seconds
                            (default=N/A)
      -C, --check           don't make any changes; instead, try to predict some
                            of the changes that may occur
      -D, --diff            when changing (small) files and templates, show the
                            differences in those files; works great with --check
      -M MODULE_PATH, --module-path MODULE_PATH
                            prepend colon-separated path(s) to module library (def
                            ault=~/.ansible/plugins/modules:/usr/share/ansible/plu
                            gins/modules)
      -P POLL_INTERVAL, --poll POLL_INTERVAL
                            set the poll interval if using -B (default=15)
      -a MODULE_ARGS, --args MODULE_ARGS
                            The action's options in space separated k=v format: -a
                            'opt1=val1 opt2=val2'
      -e EXTRA_VARS, --extra-vars EXTRA_VARS
                            set additional variables as key=value or YAML/JSON, if
                            filename prepend with @
      -f FORKS, --forks FORKS
                            specify number of parallel processes to use
                            (default=5)
      -h, --help            show this help message and exit
      -i INVENTORY, --inventory INVENTORY, --inventory-file INVENTORY
                            specify inventory host path or comma separated host
                            list. --inventory-file is deprecated
      -l SUBSET, --limit SUBSET
                            further limit selected hosts to an additional pattern
      -m MODULE_NAME, --module-name MODULE_NAME
                            Name of the action to execute (default=command)
      -o, --one-line        condense output
      -t TREE, --tree TREE  log output to this directory
      -v, --verbose         verbose mode (-vvv for more, -vvvv to enable
                            connection debugging)

    Privilege Escalation Options:
      control how and which user you become as on target hosts

      --become-method BECOME_METHOD
                            privilege escalation method to use (default=sudo), use
                            `ansible-doc -t become -l` to list valid choices.
      --become-user BECOME_USER
                            run operations as this user (default=root)
      -K, --ask-become-pass
                            ask for privilege escalation password
      -b, --become          run operations with become (does not imply password
                            prompting)

    Connection Options:
      control as whom and how to connect to hosts

      --private-key PRIVATE_KEY_FILE, --key-file PRIVATE_KEY_FILE
                            use this file to authenticate the connection
      --scp-extra-args SCP_EXTRA_ARGS
                            specify extra arguments to pass to scp only (e.g. -l)
      --sftp-extra-args SFTP_EXTRA_ARGS
                            specify extra arguments to pass to sftp only (e.g. -f,
                            -l)
      --ssh-common-args SSH_COMMON_ARGS
                            specify common arguments to pass to sftp/scp/ssh (e.g.
                            ProxyCommand)
      --ssh-extra-args SSH_EXTRA_ARGS
                            specify extra arguments to pass to ssh only (e.g. -R)
      -T TIMEOUT, --timeout TIMEOUT
                            override the connection timeout in seconds
                            (default=10)
      -c CONNECTION, --connection CONNECTION
                            connection type to use (default=smart)
      -k, --ask-pass        ask for connection password
      -u REMOTE_USER, --user REMOTE_USER
                            connect as this user (default=None)

    Some actions do not make sense in Ad-Hoc (include, meta, etc)
    ```

## Uso do `ansible`

3. Baixar os *templates*:
    ```
    $ git clone https://github.com/josecastillolema/fiap
    Cloning into 'fiap'...
    remote: Enumerating objects: 10, done.
    remote: Counting objects: 100% (10/10), done.
    remote: Compressing objects: 100% (10/10), done.
    remote: Total 3716 (delta 4), reused 0 (delta 0), pack-reused 3706
    Receiving objects: 100% (3716/3716), 44.63 MiB | 3.88 MiB/s, done.
    Resolving deltas: 100% (1862/1862), done.
    Checking connectivity... done.
    
    $ cd fiap/cld/multicloud/lab15-ansible/
    ```
    
4. Atualizar o conteúdo do arquivo [hosts](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/multicloud/lab15-ansible/hosts) com o endereço da máquina virtual `Amazon Linux` e testar o acesso a máquina virtual:
    ```
    $ ansible all -m  ping -i hosts --key-file ~/.ssh/labsuser.pem
    ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com | SUCCESS => {
        "ansible_facts": {
            "discovered_interpreter_python": "/usr/bin/python"
        },
        "changed": false,
        "ping": "pong"
    }
    ```

## Uso do `ansible-playbook`


5. Invocar o playbook [template](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/multicloud/lab15-ansible/deploy-flask.yaml):
    ```
    $ ansible-playbook deploy-flask.yaml -i hosts --key-file ~/.ssh/labsuser.pem
    PLAY [webservers] *******************************************************************************

    TASK [Gathering Facts] **************************************************************************
    ok: [ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com]

    TASK [install pip] ******************************************************************************
    changed: [ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com]

    PLAY RECAP **************************************************************************************
    ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com : ok=2    changed=1    unreachable=0    failed=0    skipped=0    rescued=0    ignored=0  
    ```

6. Na 1a execução do playbook, veja que o pip foi instalado (`changed=1`). Se rodar-mos o playbook outra vez, não haberá mudanças na VM (`changed=0`):
    ```
    $ ansible-playbook deploy-flask.yaml -i hosts --key-file ~/.ssh/labsuser.pem
    PLAY [webservers] *******************************************************************************

    TASK [Gathering Facts] **************************************************************************
    ok: [ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com]

    TASK [install pip] ******************************************************************************
    ok: [ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com]

    PLAY RECAP **************************************************************************************
    ec2-user@ec2-54-145-72-182.compute-1.amazonaws.com : ok=2    changed=0    unreachable=0    failed=0    skipped=0    rescued=0    ignored=0   
    ```
    
8. Concluir o playbook com os restantes passos para o correto deploy da aplicação (consultar o [lab de Beanstalk - deploy em uma VM do EC2](https://github.com/josecastillolema/fiap/blob/master/scj/cloud/lab06-paas-eb.md#deploy-em-uma-vm-no-ec2)):
    - Copiar os arquivos da aplicação (ou clonar este repositório git)
    - Instalação das dependencias Python usando o `pip` (arquivo `requirements`)
    - Execução da aplicação
