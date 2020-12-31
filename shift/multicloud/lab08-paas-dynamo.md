# Lab 8 - AWS DynamoDB

Em este lab sobre [**DynamoDB**](https://aws.amazon.com/pt/dynamodb/) aprenderemos alguns conceitos importantes na criação de DBaaS NoSQL:
 - Criação de tabelas
 - Inserção/consulta de dados via console
 - Inserção/consulta via código `python`
 
Aproveitaremos para ver alguns conceitos importantes sobre [**Identity and Access Management (IAM)**](https://aws.amazon.com/pt/iam/):
 - Autenticação usando arquivo de credenciais
 - Autenticação usando roles
 
Aproveitaremos também para mostrar as três formas de interação com a AWS:
 - Console WEB
 - Command line (comando `aws`)
 - SDK Python (biblioteca `python3-boto3`)

## Pre-reqs

- Uma VM com a imagem `Ubuntu Linux 18.04`

- No painel da Vocareum, accessar as credenciais da conta para acesso programático:
   ![](/mob/cloud/img/d0.png)
   ![](/mob/cloud/img/d1.png)
   
- Copiar as credenciais no arquivo `~/.aws/credentials` dentro da VM:
    ```
    $ cat ~/.aws/credentials 
    [default]
    aws_access_key_id=ASIAY7VWXB2OOVWYLAGN
    aws_secret_access_key=KBIiKTVfM4weELy/fnyo2whIX1CU1rIzRGAuKrIY
    aws_session_token=FwoGZXIvYXdzEHsaDHJaxudSvX0PU3dK+iLBAaNXq+ioeMWV0o10aUbmolUTn/Qipy4YuXeGE4iQPYpLdtLd+djB78dl1PdjD50Hzbr9kr3T7YN2Y9YSG949dIThcvLBgTgJCB008YXTaUSClqKtppKGdhTymdhfUuiYin9m5DDgYDvnQhmt/9ukDWe8lzpFVz6NvjPnfgQrRCfViCs4KPCWz3WqPM6Q7opJM+FPFySWWY57TlzJ4919JpDLsLaE0CBSJQqgj0CWT/rX6zhh1rAQ3gGD8MRGipe6Chwol4qM+QUyLfX8HXTVHQTnTdspoG0ARfrtJglg9imONXKaHIFopyaajJZ12OgQjUKhl3u+WA==
    ```
- Para testar as credenciais, instalaremos o CLI da AWS:
    ```
    $ sudo apt update
    
    $ sudo apt install -y awscli
    ```
    
 - Configuramos a região correta (ignorar o resto dos campos):
    ````
    $ aws configure
    AWS Access Key ID [****************Q5QG]: 
    AWS Secret Access Key [****************aqWs]: 
    Default region name [None]: us-east-1
    Default output format [None]:
    ```
    
 - Listar VMs (em formato `json`):
    ```
    $ aws ec2 describe-instances
    {
        "Reservations": [
            {
                "Groups": [],
                "Instances": [
                    {
                        "AmiLaunchIndex": 0,
                        "ImageId": "ami-0914bc04e5495b889",
                        "InstanceId": "i-0eea6b50a48d07613",
                        "InstanceType": "t2.micro",
                        "LaunchTime": "2020-09-18T00:17:13.000Z",
                        "Monitoring": {
                            "State": "disabled"
                        },
                        "Placement": {
                            "AvailabilityZone": "us-east-1c",
                            "GroupName": "",
                            "Tenancy": "default"
                        },
                        "PrivateDnsName": "",
                        "ProductCodes": [],
                        "PublicDnsName": "",
                        "State": {
                            "Code": 48,
                            "Name": "terminated"
                        },
                        "StateTransitionReason": "User initiated (2020-09-20 20:10:17 GMT)",
                        "Architecture": "x86_64",
                        "BlockDeviceMappings": [],
                        "ClientToken": "cb65d27a-49cb-d46d-a7ca-1b8368d22330",
                        "EbsOptimized": false,
                        "EnaSupport": true,
                        "Hypervisor": "xen",
                        "NetworkInterfaces": [],
                        "RootDeviceName": "/dev/xvda",
                        "RootDeviceType": "ebs",
                        "SecurityGroups": [],
                        "StateReason": {
                            "Code": "Client.UserInitiatedShutdown",
                            "Message": "Client.UserInitiatedShutdown: User initiated shutdown"
                        },
                        "Tags": [
                            {
                                "Key": "aws:cloudformation:stack-id",
                                "Value": "arn:aws:cloudformation:us-east-1:440730077537:stack/awseb-e-32fei49nnj-stack/35d81f30-f944-11ea-9cb6-0eb23bbe71c5"
                            },
                            {
                                "Key": "elasticbeanstalk:environment-id",
                                "Value": "e-32fei49nnj"
                            },
                            {
                                "Key": "Name",
                                "Value": "Fiapapp-env"
                            },
                            {
                                "Key": "aws:cloudformation:stack-name",
                                "Value": "awseb-e-32fei49nnj-stack"
                            },
                            {
                                "Key": "elasticbeanstalk:environment-name",
                                "Value": "Fiapapp-env"
                            },
                            {
                                "Key": "aws:autoscaling:groupName",
                                "Value": "awseb-e-32fei49nnj-stack-AWSEBAutoScalingGroup-17NY3APZ43NN7"
                            },
                            {
                                "Key": "aws:cloudformation:logical-id",
                                "Value": "AWSEBAutoScalingGroup"
                            }
                        ],
                        "VirtualizationType": "hvm",
                        "CpuOptions": {
                            "CoreCount": 1,
                            "ThreadsPerCore": 1
                        },
                        "CapacityReservationSpecification": {
                            "CapacityReservationPreference": "open"
                        },
                        "HibernationOptions": {
                            "Configured": false
                        },
                        "MetadataOptions": {
                            "State": "pending",
                            "HttpTokens": "optional",
                            "HttpPutResponseHopLimit": 1,
                            "HttpEndpoint": "enabled"
                        }
                    }
                ],
                "OwnerId": "440730077537",
                "RequesterId": "940372691376",
                "ReservationId": "r-04e5222922400810c"
            },
            {
                "Groups": [],
                "Instances": [
                    {
                        "AmiLaunchIndex": 0,
                        "ImageId": "ami-0817d428a6fb68645",
                        "InstanceId": "i-02a7e6dfe99d1f769",
                        "InstanceType": "t2.micro",
                        "KeyName": "fiap",
                        "LaunchTime": "2020-09-20T20:11:49.000Z",
                        "Monitoring": {
                            "State": "disabled"
                        },
                        "Placement": {
                            "AvailabilityZone": "us-east-1a",
                            "GroupName": "",
                            "Tenancy": "default"
                        },
                        "PrivateDnsName": "ip-172-31-40-37.ec2.internal",
                        "PrivateIpAddress": "172.31.40.37",
                        "ProductCodes": [],
                        "PublicDnsName": "ec2-18-232-70-66.compute-1.amazonaws.com",
                        "PublicIpAddress": "18.232.70.66",
                        "State": {
                            "Code": 16,
                            "Name": "running"
                        },
                        "StateTransitionReason": "",
                        "SubnetId": "subnet-17a00f48",
                        "VpcId": "vpc-7166990c",
                        "Architecture": "x86_64",
                        "BlockDeviceMappings": [
                            {
                                "DeviceName": "/dev/sda1",
                                "Ebs": {
                                    "AttachTime": "2020-09-20T20:11:49.000Z",
                                    "DeleteOnTermination": true,
                                    "Status": "attached",
                                    "VolumeId": "vol-0b8b82a6443e712cf"
                                }
                            }
                        ],
                        "ClientToken": "",
                        "EbsOptimized": false,
                        "EnaSupport": true,
                        "Hypervisor": "xen",
                        "NetworkInterfaces": [
                            {
                                "Association": {
                                    "IpOwnerId": "amazon",
                                    "PublicDnsName": "ec2-18-232-70-66.compute-1.amazonaws.com",
                                    "PublicIp": "18.232.70.66"
                                },
                                "Attachment": {
                                    "AttachTime": "2020-09-20T20:11:49.000Z",
                                    "AttachmentId": "eni-attach-00f1dc9a09492b219",
                                    "DeleteOnTermination": true,
                                    "DeviceIndex": 0,
                                    "Status": "attached"
                                },
                                "Description": "",
                                "Groups": [
                                    {
                                        "GroupName": "launch-wizard-3",
                                        "GroupId": "sg-0976be2478d3b8b08"
                                    }
                                ],
                                "Ipv6Addresses": [],
                                "MacAddress": "0e:6a:4f:ca:d5:f1",
                                "NetworkInterfaceId": "eni-09f8ba40f97bbc142",
                                "OwnerId": "440730077537",
                                "PrivateDnsName": "ip-172-31-40-37.ec2.internal",
                                "PrivateIpAddress": "172.31.40.37",
                                "PrivateIpAddresses": [
                                    {
                                        "Association": {
                                            "IpOwnerId": "amazon",
                                            "PublicDnsName": "ec2-18-232-70-66.compute-1.amazonaws.com",
                                            "PublicIp": "18.232.70.66"
                                        },
                                        "Primary": true,
                                        "PrivateDnsName": "ip-172-31-40-37.ec2.internal",
                                        "PrivateIpAddress": "172.31.40.37"
                                    }
                                ],
                                "SourceDestCheck": true,
                                "Status": "in-use",
                                "SubnetId": "subnet-17a00f48",
                                "VpcId": "vpc-7166990c",
                                "InterfaceType": "interface"
                            }
                        ],
                        "RootDeviceName": "/dev/sda1",
                        "RootDeviceType": "ebs",
                        "SecurityGroups": [
                            {
                                "GroupName": "launch-wizard-3",
                                "GroupId": "sg-0976be2478d3b8b08"
                            }
                        ],
                        "SourceDestCheck": true,
                        "VirtualizationType": "hvm",
                        "CpuOptions": {
                            "CoreCount": 1,
                            "ThreadsPerCore": 1
                        },
                        "CapacityReservationSpecification": {
                            "CapacityReservationPreference": "open"
                        },
                        "HibernationOptions": {
                            "Configured": false
                        },
                        "MetadataOptions": {
                            "State": "applied",
                            "HttpTokens": "optional",
                            "HttpPutResponseHopLimit": 1,
                            "HttpEndpoint": "enabled"
                        }
                    }
                ],
                "OwnerId": "440730077537",
                "ReservationId": "r-06e3113829e85cdc6"
            }
        ]
    }
    ```

 - Listar VMs (em formato tabela):
    ```
    $ aws ec2 describe-instances --output table
    ------------------------------------------------------------------------------------------------------------------------------------------------------------------
    |                                                                        DescribeInstances                                                                       |
    +----------------------------------------------------------------------------------------------------------------------------------------------------------------+
    ||                                                                         Reservations                                                                         ||
    |+------------------------------------------------------------------+-------------------------------------------------------------------------------------------+|
    ||  OwnerId                                                         |  440730077537                                                                             ||
    ||  RequesterId                                                     |  940372691376                                                                             ||
    ||  ReservationId                                                   |  r-04e5222922400810c                                                                      ||
    |+------------------------------------------------------------------+-------------------------------------------------------------------------------------------+|
    |||                                                                          Instances                                                                         |||
    ||+-------------------------------------------------------+----------------------------------------------------------------------------------------------------+||
    |||  AmiLaunchIndex                                       |  0                                                                                                 |||
    |||  Architecture                                         |  x86_64                                                                                            |||
    |||  ClientToken                                          |  cb65d27a-49cb-d46d-a7ca-1b8368d22330                                                              |||
    |||  EbsOptimized                                         |  False                                                                                             |||
    |||  EnaSupport                                           |  True                                                                                              |||
    |||  Hypervisor                                           |  xen                                                                                               |||
    |||  ImageId                                              |  ami-0914bc04e5495b889                                                                             |||
    |||  InstanceId                                           |  i-0eea6b50a48d07613                                                                               |||
    |||  InstanceType                                         |  t2.micro                                                                                          |||
    |||  LaunchTime                                           |  2020-09-18T00:17:13.000Z                                                                          |||
    |||  PrivateDnsName                                       |                                                                                                    |||
    |||  PublicDnsName                                        |                                                                                                    |||
    |||  RootDeviceName                                       |  /dev/xvda                                                                                         |||
    |||  RootDeviceType                                       |  ebs                                                                                               |||
    |||  StateTransitionReason                                |  User initiated (2020-09-20 20:10:17 GMT)                                                          |||
    |||  VirtualizationType                                   |  hvm                                                                                               |||
    ||+-------------------------------------------------------+----------------------------------------------------------------------------------------------------+||
    ||||                                                             CapacityReservationSpecification                                                             ||||
    |||+----------------------------------------------------------------------------------------------------------------------------+-----------------------------+|||
    ||||  CapacityReservationPreference                                                                                             |  open                       ||||
    |||+----------------------------------------------------------------------------------------------------------------------------+-----------------------------+|||
    ||||                                                                        CpuOptions                                                                        ||||
    |||+------------------------------------------------------------------------------------------------------------------------+---------------------------------+|||
    ||||  CoreCount                                                                                                             |  1                              ||||
    ||||  ThreadsPerCore                                                                                                        |  1                              ||||
    |||+------------------------------------------------------------------------------------------------------------------------+---------------------------------+|||
    ||||                                                                    HibernationOptions                                                                    ||||
    |||+---------------------------------------------------------------------------------------------+------------------------------------------------------------+|||
    ||||  Configured                                                                                 |  False                                                     ||||
    |||+---------------------------------------------------------------------------------------------+------------------------------------------------------------+|||
    ||||                                                                      MetadataOptions                                                                     ||||
    |||+----------------------------------------------------------------------------------------------------------+-----------------------------------------------+|||
    ||||  HttpEndpoint                                                                                            |  enabled                                      ||||
    ||||  HttpPutResponseHopLimit                                                                                 |  1                                            ||||
    ||||  HttpTokens                                                                                              |  optional                                     ||||
    ||||  State                                                                                                   |  pending                                      ||||
    |||+----------------------------------------------------------------------------------------------------------+-----------------------------------------------+|||
    ||||                                                                        Monitoring                                                                        ||||
    |||+-----------------------------------------------------------------+----------------------------------------------------------------------------------------+|||
    ||||  State                                                          |  disabled                                                                              ||||
    |||+-----------------------------------------------------------------+----------------------------------------------------------------------------------------+|||
    ||||                                                                         Placement                                                                        ||||
    |||+------------------------------------------------------------------------------------------+---------------------------------------------------------------+|||
    ||||  AvailabilityZone                                                                        |  us-east-1c                                                   ||||
    ||||  GroupName                                                                               |                                                               ||||
    ||||  Tenancy                                                                                 |  default                                                      ||||
    |||+------------------------------------------------------------------------------------------+---------------------------------------------------------------+|||
    ||||                                                                           State                                                                          ||||
    |||+-------------------------------------------------------+--------------------------------------------------------------------------------------------------+|||
    ||||  Code                                                 |  48                                                                                              ||||
    ||||  Name                                                 |  terminated                                                                                      ||||
    |||+-------------------------------------------------------+--------------------------------------------------------------------------------------------------+|||
    ||||                                                                        StateReason                                                                       ||||
    |||+-----------------------+----------------------------------------------------------------------------------------------------------------------------------+|||
    ||||  Code                 |  Client.UserInitiatedShutdown                                                                                                    ||||
    ||||  Message              |  Client.UserInitiatedShutdown: User initiated shutdown                                                                           ||||
    |||+-----------------------+----------------------------------------------------------------------------------------------------------------------------------+|||
    ||||                                                                           Tags                                                                           ||||
    |||+-----------------------------------+----------------------------------------------------------------------------------------------------------------------+|||
    ||||                Key                |                                                        Value                                                         ||||
    |||+-----------------------------------+----------------------------------------------------------------------------------------------------------------------+|||
    ||||  aws:cloudformation:stack-id      |  arn:aws:cloudformation:us-east-1:440730077537:stack/awseb-e-32fei49nnj-stack/35d81f30-f944-11ea-9cb6-0eb23bbe71c5   ||||
    ||||  elasticbeanstalk:environment-id  |  e-32fei49nnj                                                                                                        ||||
    ||||  Name                             |  Fiapapp-env                                                                                                         ||||
    ||||  aws:cloudformation:stack-name    |  awseb-e-32fei49nnj-stack                                                                                            ||||
    ||||  elasticbeanstalk:environment-name|  Fiapapp-env                                                                                                         ||||
    ||||  aws:autoscaling:groupName        |  awseb-e-32fei49nnj-stack-AWSEBAutoScalingGroup-17NY3APZ43NN7                                                        ||||
    ||||  aws:cloudformation:logical-id    |  AWSEBAutoScalingGroup                                                                                               ||||
    |||+-----------------------------------+----------------------------------------------------------------------------------------------------------------------+|||
    ||                                                                         Reservations                                                                         ||
    |+------------------------------------------------------------------+-------------------------------------------------------------------------------------------+|
    ||  OwnerId                                                         |  440730077537                                                                             ||
    ||  RequesterId                                                     |                                                                                           ||
    ||  ReservationId                                                   |  r-06e3113829e85cdc6                                                                      ||
    |+------------------------------------------------------------------+-------------------------------------------------------------------------------------------+|
    |||                                                                          Instances                                                                         |||
    ||+-------------------------------------------------------+----------------------------------------------------------------------------------------------------+||
    |||  AmiLaunchIndex                                       |  0                                                                                                 |||
    |||  Architecture                                         |  x86_64                                                                                            |||
    |||  ClientToken                                          |                                                                                                    |||
    |||  EbsOptimized                                         |  False                                                                                             |||
    |||  EnaSupport                                           |  True                                                                                              |||
    |||  Hypervisor                                           |  xen                                                                                               |||
    |||  ImageId                                              |  ami-0817d428a6fb68645                                                                             |||
    |||  InstanceId                                           |  i-02a7e6dfe99d1f769                                                                               |||
    |||  InstanceType                                         |  t2.micro                                                                                          |||
    |||  KeyName                                              |  fiap                                                                                              |||
    |||  LaunchTime                                           |  2020-09-20T20:11:49.000Z                                                                          |||
    |||  PrivateDnsName                                       |  ip-172-31-40-37.ec2.internal                                                                      |||
    |||  PrivateIpAddress                                     |  172.31.40.37                                                                                      |||
    |||  PublicDnsName                                        |  ec2-18-232-70-66.compute-1.amazonaws.com                                                          |||
    |||  PublicIpAddress                                      |  18.232.70.66                                                                                      |||
    |||  RootDeviceName                                       |  /dev/sda1                                                                                         |||
    |||  RootDeviceType                                       |  ebs                                                                                               |||
    |||  SourceDestCheck                                      |  True                                                                                              |||
    |||  StateTransitionReason                                |                                                                                                    |||
    |||  SubnetId                                             |  subnet-17a00f48                                                                                   |||
    |||  VirtualizationType                                   |  hvm                                                                                               |||
    |||  VpcId                                                |  vpc-7166990c                                                                                      |||
    ||+-------------------------------------------------------+----------------------------------------------------------------------------------------------------+||
    ||||                                                                    BlockDeviceMappings                                                                   ||||
    |||+-------------------------------------------------------------------------------+--------------------------------------------------------------------------+|||
    ||||  DeviceName                                                                   |  /dev/sda1                                                               ||||
    |||+-------------------------------------------------------------------------------+--------------------------------------------------------------------------+|||
    |||||                                                                           Ebs                                                                          |||||
    ||||+-------------------------------------------------------------------+------------------------------------------------------------------------------------+||||
    |||||  AttachTime                                                       |  2020-09-20T20:11:49.000Z                                                          |||||
    |||||  DeleteOnTermination                                              |  True                                                                              |||||
    |||||  Status                                                           |  attached                                                                          |||||
    |||||  VolumeId                                                         |  vol-0b8b82a6443e712cf                                                             |||||
    ||||+-------------------------------------------------------------------+------------------------------------------------------------------------------------+||||
    ||||                                                             CapacityReservationSpecification                                                             ||||
    |||+----------------------------------------------------------------------------------------------------------------------------+-----------------------------+|||
    ||||  CapacityReservationPreference                                                                                             |  open                       ||||
    |||+----------------------------------------------------------------------------------------------------------------------------+-----------------------------+|||
    ||||                                                                        CpuOptions                                                                        ||||
    |||+------------------------------------------------------------------------------------------------------------------------+---------------------------------+|||
    ||||  CoreCount                                                                                                             |  1                              ||||
    ||||  ThreadsPerCore                                                                                                        |  1                              ||||
    |||+------------------------------------------------------------------------------------------------------------------------+---------------------------------+|||
    ||||                                                                    HibernationOptions                                                                    ||||
    |||+---------------------------------------------------------------------------------------------+------------------------------------------------------------+|||
    ||||  Configured                                                                                 |  False                                                     ||||
    |||+---------------------------------------------------------------------------------------------+------------------------------------------------------------+|||
    ||||                                                                      MetadataOptions                                                                     ||||
    |||+----------------------------------------------------------------------------------------------------------+-----------------------------------------------+|||
    ||||  HttpEndpoint                                                                                            |  enabled                                      ||||
    ||||  HttpPutResponseHopLimit                                                                                 |  1                                            ||||
    ||||  HttpTokens                                                                                              |  optional                                     ||||
    ||||  State                                                                                                   |  applied                                      ||||
    |||+----------------------------------------------------------------------------------------------------------+-----------------------------------------------+|||
    ||||                                                                        Monitoring                                                                        ||||
    |||+-----------------------------------------------------------------+----------------------------------------------------------------------------------------+|||
    ||||  State                                                          |  disabled                                                                              ||||
    |||+-----------------------------------------------------------------+----------------------------------------------------------------------------------------+|||
    ||||                                                                     NetworkInterfaces                                                                    ||||
    |||+--------------------------------------------------------------+-------------------------------------------------------------------------------------------+|||
    ||||  Description                                                 |                                                                                           ||||
    ||||  InterfaceType                                               |  interface                                                                                ||||
    ||||  MacAddress                                                  |  0e:6a:4f:ca:d5:f1                                                                        ||||
    ||||  NetworkInterfaceId                                          |  eni-09f8ba40f97bbc142                                                                    ||||
    ||||  OwnerId                                                     |  440730077537                                                                             ||||
    ||||  PrivateDnsName                                              |  ip-172-31-40-37.ec2.internal                                                             ||||
    ||||  PrivateIpAddress                                            |  172.31.40.37                                                                             ||||
    ||||  SourceDestCheck                                             |  True                                                                                     ||||
    ||||  Status                                                      |  in-use                                                                                   ||||
    ||||  SubnetId                                                    |  subnet-17a00f48                                                                          ||||
    ||||  VpcId                                                       |  vpc-7166990c                                                                             ||||
    |||+--------------------------------------------------------------+-------------------------------------------------------------------------------------------+|||
    |||||                                                                       Association                                                                      |||||
    ||||+-----------------------------------------+--------------------------------------------------------------------------------------------------------------+||||
    |||||  IpOwnerId                              |  amazon                                                                                                      |||||
    |||||  PublicDnsName                          |  ec2-18-232-70-66.compute-1.amazonaws.com                                                                    |||||
    |||||  PublicIp                               |  18.232.70.66                                                                                                |||||
    ||||+-----------------------------------------+--------------------------------------------------------------------------------------------------------------+||||
    |||||                                                                       Attachment                                                                       |||||
    ||||+--------------------------------------------------------------+-----------------------------------------------------------------------------------------+||||
    |||||  AttachTime                                                  |  2020-09-20T20:11:49.000Z                                                               |||||
    |||||  AttachmentId                                                |  eni-attach-00f1dc9a09492b219                                                           |||||
    |||||  DeleteOnTermination                                         |  True                                                                                   |||||
    |||||  DeviceIndex                                                 |  0                                                                                      |||||
    |||||  Status                                                      |  attached                                                                               |||||
    ||||+--------------------------------------------------------------+-----------------------------------------------------------------------------------------+||||
    |||||                                                                         Groups                                                                         |||||
    ||||+----------------------------------------------------+---------------------------------------------------------------------------------------------------+||||
    |||||  GroupId                                           |  sg-0976be2478d3b8b08                                                                             |||||
    |||||  GroupName                                         |  launch-wizard-3                                                                                  |||||
    ||||+----------------------------------------------------+---------------------------------------------------------------------------------------------------+||||
    |||||                                                                   PrivateIpAddresses                                                                   |||||
    ||||+---------------------------------------------------------+----------------------------------------------------------------------------------------------+||||
    |||||  Primary                                                |  True                                                                                        |||||
    |||||  PrivateDnsName                                         |  ip-172-31-40-37.ec2.internal                                                                |||||
    |||||  PrivateIpAddress                                       |  172.31.40.37                                                                                |||||
    ||||+---------------------------------------------------------+----------------------------------------------------------------------------------------------+||||
    ||||||                                                                      Association                                                                     ||||||
    |||||+----------------------------------------+-------------------------------------------------------------------------------------------------------------+|||||
    ||||||  IpOwnerId                             |  amazon                                                                                                     ||||||
    ||||||  PublicDnsName                         |  ec2-18-232-70-66.compute-1.amazonaws.com                                                                   ||||||
    ||||||  PublicIp                              |  18.232.70.66                                                                                               ||||||
    |||||+----------------------------------------+-------------------------------------------------------------------------------------------------------------+|||||
    ||||                                                                         Placement                                                                        ||||
    |||+------------------------------------------------------------------------------------------+---------------------------------------------------------------+|||
    ||||  AvailabilityZone                                                                        |  us-east-1a                                                   ||||
    ||||  GroupName                                                                               |                                                               ||||
    ||||  Tenancy                                                                                 |  default                                                      ||||
    |||+------------------------------------------------------------------------------------------+---------------------------------------------------------------+|||
    ||||                                                                      SecurityGroups                                                                      ||||
    |||+-----------------------------------------------------+----------------------------------------------------------------------------------------------------+|||
    ||||  GroupId                                            |  sg-0976be2478d3b8b08                                                                              ||||
    ||||  GroupName                                          |  launch-wizard-3                                                                                   ||||
    |||+-----------------------------------------------------+----------------------------------------------------------------------------------------------------+|||
    ||||                                                                           State                                                                          ||||
    |||+----------------------------------------------------------------+-----------------------------------------------------------------------------------------+|||
    ||||  Code                                                          |  16                                                                                     ||||
    ||||  Name                                                          |  running                                                                                ||||
    |||+----------------------------------------------------------------+-----------------------------------------------------------------------------------------+|||
    ```


## Criando o DB no DynamoDB
 
1. Acessar o serviço **DynamoDB**
   
2. Criar uma nova tabela:
   ![](/mob/cloud/img/d2.png)
   
3. Configurar o nome da tabela e a chave primaria da mesma:
   ![](/mob/cloud/img/d3.png)

4. Aguardar a criação da tabela:
   ![](/mob/cloud/img/d4.png)

5. Na aba `Items`, adicionar um novo item:
   ![](/mob/cloud/img/d5.png)

6. Confirmar a criação do item:
   ![](/mob/cloud/img/d6.png)


## Accessando via código `python`

### Usando o arquivo de credenciais

7. Na VM, clonar o repostiorio das aulas:
    ```
    $ git clone https://github.com/josecastillolema/fiap.git
    ```

8. Navegar ate a pasta dos códigos de este lab:
    ```
    $ cd fiap/mob/cloud/lab08-paas-dynamo/
    ```
    
9. Conferir o código:
    * Carrega a tabela `Alunos` da *region* `us-east-1` 
    * Faz um *scan* de todos os dados da tabela
    * Insere um novo aluno
    ```python
    from pprint import pprint
    import boto3

    dynamodb = boto3.resource('dynamodb', region_name='us-east-1')
    table = dynamodb.Table('Alunos')

    def scan(dynamodb, table):
       response = table.scan()
       data = response['Items']
       while 'LastEvaluatedKey' in response:
          response = table.scan(ExclusiveStartKey=response['LastEvaluatedKey'])
          data.extend(response['Items'])
       print (data)

    def put_aluno(dynamodb, table, rm, mail, nome, tlfne):
       response = table.put_item(
          Item={
             'RM': rm,
             'mail': mail,
             'nome': nome,
             'tfne': tlfne
          }
       )
       return response

    if __name__ == '__main__':
       print("\nTestando scan:")
       scan(dynamodb, table)

       resp = put_aluno(dynamodb, table, "RM234472", 'rm234472@fiap.com.br', "Jonas Kahnwald", 11636229987)
       print("\nIserindo aluno:")
       pprint(resp)
     ```
 10. Instalar as dependências:
     ```
     $ sudo apt update
     $ sudo apt install -y python3-boto3
     ```
 
 11. Rodar o código:
     ```
     $ python3 dynamodb.py 

      Testando scan:
      [{'mail': 'rm234472@fiap.com.br', 'nome': 'Jonas Kahnwald', 'RM': 'RM234472', 'tlfne': Decimal('11636229987')}, {'mail': 'rm338132@fiap.com.br', 'nome': 'Joao Lopez', 'RM': 'RM338132', 'tfne': Decimal('11981041293')}]

      Iserindo aluno:
      {'ResponseMetadata': {'HTTPHeaders': {'connection': 'keep-alive',
                                            'content-length': '2',
                                            'content-type': 'application/x-amz-json-1.0',
                                            'date': 'Thu, 30 Jul 2020 17:41:36 GMT',
                                            'server': 'Server',
                                            'x-amz-crc32': '2745614147',
                                            'x-amzn-requestid': 'KDPGDR3S23O19GMPMPOPHCNEO3VV4KQNSO5AEMVJF66Q9ASUAAJG'},
                            'HTTPStatusCode': 200,
                            'RequestId': 'KDPGDR3S23O19GMPMPOPHCNEO3VV4KQNSO5AEMVJF66Q9ASUAAJG',
                            'RetryAttempts': 0}}
     ```

12. No console do DynamoDB, conferir que o novo aluno foi inserido:
   ![](/mob/cloud/img/d7.png)

### Usando IAM *roles* (recomendado)

13. Remover o arquivo de credenciais:
    ```
    $ rm -rf ~.aws
    ```

14. Tentar rodar de novo o código (deberia falhar, pois não estamos mais autenticados):
    ```
    $ python3 dynamodb2.py 

    Testando scan:
    Traceback (most recent call last):
      File "dynamodb2.py", line 28, in <module>
        scan(dynamodb, table)
      File "dynamodb2.py", line 8, in scan
        response = table.scan()
      File "/usr/lib/python3/dist-packages/boto3/resources/factory.py", line 520, in do_action
        response = action(self, *args, **kwargs)
      File "/usr/lib/python3/dist-packages/boto3/resources/action.py", line 83, in __call__
        response = getattr(parent.meta.client, operation_name)(**params)
      File "/usr/lib/python3/dist-packages/botocore/client.py", line 316, in _api_call
        return self._make_api_call(operation_name, kwargs)
      File "/usr/lib/python3/dist-packages/botocore/client.py", line 622, in _make_api_call
        operation_model, request_dict, request_context)
      File "/usr/lib/python3/dist-packages/botocore/client.py", line 641, in _make_request
        return self._endpoint.make_request(operation_model, request_dict)
      File "/usr/lib/python3/dist-packages/botocore/endpoint.py", line 102, in make_request
        return self._send_request(request_dict, operation_model)
      File "/usr/lib/python3/dist-packages/botocore/endpoint.py", line 132, in _send_request
        request = self.create_request(request_dict, operation_model)
      File "/usr/lib/python3/dist-packages/botocore/endpoint.py", line 116, in create_request
        operation_name=operation_model.name)
      File "/usr/lib/python3/dist-packages/botocore/hooks.py", line 356, in emit
        return self._emitter.emit(aliased_event_name, **kwargs)
      File "/usr/lib/python3/dist-packages/botocore/hooks.py", line 228, in emit
        return self._emit(event_name, kwargs)
      File "/usr/lib/python3/dist-packages/botocore/hooks.py", line 211, in _emit
        response = handler(**kwargs)
      File "/usr/lib/python3/dist-packages/botocore/signers.py", line 90, in handler
        return self.sign(operation_name, request)
      File "/usr/lib/python3/dist-packages/botocore/signers.py", line 160, in sign
        auth.add_auth(request)
      File "/usr/lib/python3/dist-packages/botocore/auth.py", line 357, in add_auth
        raise NoCredentialsError
    botocore.exceptions.NoCredentialsError: Unable to locate credentials
    ```
    
15. Acessar o serviço **IAM**
   ![](/mob/cloud/img/iam0.png)

16. Criar um novo *role*:
   ![](/mob/cloud/img/iam1.png)

17. Escolher EC2 como serviço que vai utilizar o novo *role*:
   ![](/mob/cloud/img/iam2.png)

18. Anexar a *policy* `AmazonDynamoDBFullAccess` no novo *role*:
   ![](/mob/cloud/img/iam4.png)

19. Configurar *tags*:
   ![](/mob/cloud/img/iam5.png)

20. Revisar as configurações e confirmar a criação do *role*:
   ![](/mob/cloud/img/iam6.png)

21. No console do EC2, anexar o novo *role* na VM:
   ![](/mob/cloud/img/iam7.png)

22. Seleccionar o *role* que acabamos de criar:
   ![](/mob/cloud/img/iam8.png)

23. Tentar rodar de novo o código (deberia funcionar):
    ```
    $ python3 dynamodb2.py 

    Testando scan:
    [{'mail': 'rm234472@fiap.com.br', 'nome': 'Jonas Kahnwald', 'RM': 'RM234472', 'tfne': Decimal('11636229987')}, {'mail': 'rm338132@fiap.com.br', 'nome': 'Joao Lopez', 'RM': 'RM338132', 'tfne': Decimal('11981041293')}]

    Iserindo aluno:
    {'ResponseMetadata': {'HTTPHeaders': {'connection': 'keep-alive',
                                          'content-length': '2',
                                          'content-type': 'application/x-amz-json-1.0',
                                          'date': 'Wed, 05 Aug 2020 08:39:34 GMT',
                                          'server': 'Server',
                                          'x-amz-crc32': '2745614147',
                                          'x-amzn-requestid': 'ESPGDHO1356RNOVANUH18ASV6NVV4KQNSO5AEMVJF66Q9ASUAAJG'},
                          'HTTPStatusCode': 200,
                          'RequestId': 'ESPGDHO1356RNOVANUH18ASV6NVV4KQNSO5AEMVJF66Q9ASUAAJG',
                          'RetryAttempts': 0}}
     ```
                      
