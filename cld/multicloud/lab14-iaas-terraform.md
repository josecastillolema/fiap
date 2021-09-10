# Lab 14 - Terraform

## AWS Provider

O [Terraform](https://www.terraform.io/) é uma ferramenta para construir, alterar e controlar a infraestrutura de forma segura e eficiente. O Terraform pode gerenciar provedores de serviços existentes e populares como OpenStack, Azure, AWS, Digital Ocean, entre outras, bem como soluções internas personalizadas.

Os arquivos de configuração do Terraform descrevem os componentes necessários para executar um único aplicativo ou todo o *datacenter*, gerando um plano de execução que descreve o que será feito para alcançar o estado desejado e, em seguida, executá-lo para construir a infraestrutura descrita. À medida que a configuração muda, o Terraform é capaz de determinar o que mudou e criar planos de execução incrementais que podem ser aplicados.

A infraestrutura que o Terraform pode gerenciar inclui componentes de baixo nível, como instâncias de computação, armazenamento e redes, bem como componentes de alto nível, como entradas DNS, recursos SaaS, etc.

Com relação a ferramenta podemos comparar o Terraform com o CloudFormation da AWS.

## Pre-reqs

- Uma VM com a imagem `Amazon Linux`

- No painel da Vocareum, accessar as credenciais da conta para acesso programático:
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/d0.png)
   ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/shift/multicloud/img/d1.png)
   
- Copiar as credenciais no arquivo `~/.aws/credentials` dentro da VM:
    ```
    $ cat ~/.aws/credentials 
    [default]
    aws_access_key_id=ASIAY7VWXB2OOVWYLAGN
    aws_secret_access_key=KBIiKTVfM4weELy/fnyo2whIX1CU1rIzRGAuKrIY
    aws_session_token=FwoGZXIvYXdzEHsaDHJaxudSvX0PU3dK+iLBAaNXq+ioeMWV0o10aUbmolUTn/Qipy4YuXeGE4iQPYpLdtLd+djB78dl1PdjD50Hzbr9kr3T7YN2Y9YSG949dIThcvLBgTgJCB008YXTaUSClqKtppKGdhTymdhfUuiYin9m5DDgYDvnQhmt/9ukDWe8lzpFVz6NvjPnfgQrRCfViCs4KPCWz3WqPM6Q7opJM+FPFySWWY57TlzJ4919JpDLsLaE0CBSJQqgj0CWT/rX6zhh1rAQ3gGD8MRGipe6Chwol4qM+QUyLfX8HXTVHQTnTdspoG0ARfrtJglg9imONXKaHIFopyaajJZ12OgQjUKhl3u+WA==
    ```
    
 - Configuramos a região correta (ignorar o resto dos campos):
    ```
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

## Instalação

1. Fazer o *download* da ferramenta:

    ```
    $ wget https://releases.hashicorp.com/terraform/1.0.6/terraform_1.0.6_linux_amd64.zip
    --2021-09-10 13:40:42--  https://releases.hashicorp.com/terraform/1.0.6/terraform_1.0.6_linux_amd64.zip
    Resolving releases.hashicorp.com (releases.hashicorp.com)... 199.232.65.183, 2a04:4e42:50::439
    Connecting to releases.hashicorp.com (releases.hashicorp.com)|199.232.65.183|:443... connected.
    HTTP request sent, awaiting response... 200 OK
    Length: 32677516 (31M) [application/zip]
    Saving to: ‘terraform_1.0.6_linux_amd64.zip’

    100%[============================================================>] 32,677,516   112MB/s   in 0.3s   

    2021-09-10 13:40:42 (112 MB/s) - ‘terraform_1.0.6_linux_amd64.zip’ saved [32677516/32677516]
    ```
    
2. Descomprimir o arquivo baixado:
    ```
    $ unzip terraform_1.0.6_linux_amd64.zip 
    Archive:  terraform_1.0.6_linux_amd64.zip
      inflating: terraform               
    ```
    
3. Movimentar o executável:
    ```
    $ sudo mv terraform /usr/local/bin/
    ```
    
4. Testar a instalação:
    ```
    $ terraform -h
    Usage: terraform [global options] <subcommand> [args]

    The available commands for execution are listed below.
    The primary workflow commands are given first, followed by
    less common or more advanced commands.

    Main commands:
      init          Prepare your working directory for other commands
      validate      Check whether the configuration is valid
      plan          Show changes required by the current configuration
      apply         Create or update infrastructure
      destroy       Destroy previously-created infrastructure

    All other commands:
      console       Try Terraform expressions at an interactive command prompt
      fmt           Reformat your configuration in the standard style
      force-unlock  Release a stuck lock on the current workspace
      get           Install or upgrade remote Terraform modules
      graph         Generate a Graphviz graph of the steps in an operation
      import        Associate existing infrastructure with a Terraform resource
      login         Obtain and save credentials for a remote host
      logout        Remove locally-stored credentials for a remote host
      output        Show output values from your root module
      providers     Show the providers required for this configuration
      refresh       Update the state to match remote systems
      show          Show the current state or a saved plan
      state         Advanced state management
      taint         Mark a resource instance as not fully functional
      test          Experimental support for module integration testing
      untaint       Remove the 'tainted' state from a resource instance
      version       Show the current Terraform version
      workspace     Workspace management

    Global options (use these before the subcommand, if any):
      -chdir=DIR    Switch to a different working directory before executing the
                    given subcommand.
      -help         Show this help output, or the help for a specified subcommand.
      -version      An alias for the "version" subcommand.
    ```

## Uso

5. Baixar os *templates*:
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
    
    $ cd fiap/cld/multicloud/lab14-iaas-terraform/
    ```

6. Conferir o conteúdo do template:
    ```
    $ cat main.tf 
    terraform {
      required_providers {
        aws = {
          source  = "hashicorp/aws"
          version = "~> 3.27"
        }
      }

      required_version = ">= 0.14.9"
    }

    provider "aws" {
      profile = "default"
      region  = "us-east-1"
    }

    resource "aws_instance" "app_server" {
      ami           = "ami-087c17d1fe0178315"
      instance_type = "t2.micro"

      tags = {
        Name = "fiap-vm"
      }
    }
    ```

7. Inicializar o Terraform e o correspondente *provider* (plugin) de OpenStack:
    ```
    $ terraform init

    Initializing the backend...

    Initializing provider plugins...
    - Finding hashicorp/aws versions matching "~> 3.27"...
    - Installing hashicorp/aws v3.58.0...
    - Installed hashicorp/aws v3.58.0 (signed by HashiCorp)

    Terraform has created a lock file .terraform.lock.hcl to record the provider
    selections it made above. Include this file in your version control repository
    so that Terraform can guarantee to make the same selections by default when
    you run "terraform init" in the future.

    Terraform has been successfully initialized!

    You may now begin working with Terraform. Try running "terraform plan" to see
    any changes that are required for your infrastructure. All Terraform commands
    should now work.

    If you ever set or change modules or backend configuration for Terraform,
    rerun this command to reinitialize your working directory. If you forget, other
    commands will detect it and remind you to do so if necessary.
    ```
    
8. Validar os templates:
    ```
    $ terraform fmt
    $ terraform validate
    Success! The configuration is valid.
    ```
    
9. Criar a infraestrutura virtual:
    ```
    $ terraform apply

    Terraform used the selected providers to generate the following execution plan. Resource actions are
    indicated with the following symbols:
      + create

    Terraform will perform the following actions:

      # aws_instance.app_server will be created
      + resource "aws_instance" "app_server" {
          + ami                                  = "ami-087c17d1fe0178315"
          + arn                                  = (known after apply)
          + associate_public_ip_address          = (known after apply)
          + availability_zone                    = (known after apply)
          + cpu_core_count                       = (known after apply)
          + cpu_threads_per_core                 = (known after apply)
          + disable_api_termination              = (known after apply)
          + ebs_optimized                        = (known after apply)
          + get_password_data                    = false
          + host_id                              = (known after apply)
          + id                                   = (known after apply)
          + instance_initiated_shutdown_behavior = (known after apply)
          + instance_state                       = (known after apply)
          + instance_type                        = "t2.micro"
          + ipv6_address_count                   = (known after apply)
          + ipv6_addresses                       = (known after apply)
          + key_name                             = (known after apply)
          + monitoring                           = (known after apply)
          + outpost_arn                          = (known after apply)
          + password_data                        = (known after apply)
          + placement_group                      = (known after apply)
          + primary_network_interface_id         = (known after apply)
          + private_dns                          = (known after apply)
          + private_ip                           = (known after apply)
          + public_dns                           = (known after apply)
          + public_ip                            = (known after apply)
          + secondary_private_ips                = (known after apply)
          + security_groups                      = (known after apply)
          + source_dest_check                    = true
          + subnet_id                            = (known after apply)
          + tags                                 = {
              + "Name" = "fiap-vm"
            }
          + tags_all                             = {
              + "Name" = "fiap-vm"
            }
          + tenancy                              = (known after apply)
          + user_data                            = (known after apply)
          + user_data_base64                     = (known after apply)
          + vpc_security_group_ids               = (known after apply)

          + capacity_reservation_specification {
              + capacity_reservation_preference = (known after apply)

              + capacity_reservation_target {
                  + capacity_reservation_id = (known after apply)
                }
            }

          + ebs_block_device {
              + delete_on_termination = (known after apply)
              + device_name           = (known after apply)
              + encrypted             = (known after apply)
              + iops                  = (known after apply)
              + kms_key_id            = (known after apply)
              + snapshot_id           = (known after apply)
              + tags                  = (known after apply)
              + throughput            = (known after apply)
              + volume_id             = (known after apply)
              + volume_size           = (known after apply)
              + volume_type           = (known after apply)
            }

          + enclave_options {
              + enabled = (known after apply)
            }

          + ephemeral_block_device {
              + device_name  = (known after apply)
              + no_device    = (known after apply)
              + virtual_name = (known after apply)
            }

          + metadata_options {
              + http_endpoint               = (known after apply)
              + http_put_response_hop_limit = (known after apply)
              + http_tokens                 = (known after apply)
            }

          + network_interface {
              + delete_on_termination = (known after apply)
              + device_index          = (known after apply)
              + network_interface_id  = (known after apply)
            }

          + root_block_device {
              + delete_on_termination = (known after apply)
              + device_name           = (known after apply)
              + encrypted             = (known after apply)
              + iops                  = (known after apply)
              + kms_key_id            = (known after apply)
              + tags                  = (known after apply)
              + throughput            = (known after apply)
              + volume_id             = (known after apply)
              + volume_size           = (known after apply)
              + volume_type           = (known after apply)
            }
        }

    Plan: 1 to add, 0 to change, 0 to destroy.

    Do you want to perform these actions?
      Terraform will perform the actions described above.
      Only 'yes' will be accepted to approve.

      Enter a value: yes

    aws_instance.app_server: Creating...
    aws_instance.app_server: Still creating... [10s elapsed]
    aws_instance.app_server: Still creating... [20s elapsed]
    aws_instance.app_server: Still creating... [30s elapsed]
    aws_instance.app_server: Creation complete after 32s [id=i-0581e7619465fe0bb]

    Apply complete! Resources: 1 added, 0 changed, 0 destroyed.
    ```
    
8. Mostrar os recursos criados:
    ```
    $ terraform show
    # aws_instance.app_server:
    resource "aws_instance" "app_server" {
        ami                                  = "ami-087c17d1fe0178315"
        arn                                  = "arn:aws:ec2:us-east-1:376713914115:instance/i-0581e7619465
    fe0bb"
        associate_public_ip_address          = true
        availability_zone                    = "us-east-1a"
        cpu_core_count                       = 1
        cpu_threads_per_core                 = 1
        disable_api_termination              = false
        ebs_optimized                        = false
        get_password_data                    = false
        hibernation                          = false
        id                                   = "i-0581e7619465fe0bb"
        instance_initiated_shutdown_behavior = "stop"
        instance_state                       = "running"
        instance_type                        = "t2.micro"
        ipv6_address_count                   = 0
        ipv6_addresses                       = []
        monitoring                           = false
        primary_network_interface_id         = "eni-0a879183697c40c22"
        private_dns                          = "ip-172-31-93-139.ec2.internal"
        private_ip                           = "172.31.93.139"
        public_dns                           = "ec2-18-212-49-179.compute-1.amazonaws.com"
        public_ip                            = "18.212.49.179"
        secondary_private_ips                = []
        security_groups                      = [
            "default",
        ]
        source_dest_check                    = true
        subnet_id                            = "subnet-2a31160b"
        tags                                 = {
            "Name" = "fiap-vm"
        }
        tags_all                             = {
            "Name" = "fiap-vm"
        }
        tenancy                              = "default"
        vpc_security_group_ids               = [
            "sg-fa4580e5",
        ]

        capacity_reservation_specification {
            capacity_reservation_preference = "open"
        }

        credit_specification {
            cpu_credits = "standard"
        }

        enclave_options {
            enabled = false
        }

        metadata_options {
            http_endpoint               = "enabled"
            http_put_response_hop_limit = 1
            http_tokens                 = "optional"
        }

        root_block_device {
            delete_on_termination = true
            device_name           = "/dev/xvda"
            encrypted             = false
            iops                  = 100
            tags                  = {}
            throughput            = 0
            volume_id             = "vol-0faa94c59e6dfcc09"
            volume_size           = 8
            volume_type           = "gp2"
        }
    }
    ```

10. Validar a criação da instância:
    ```
    $ aws ec2 describe-instances --filters Name=tag-key,Values=Name --query "Reservations[*].Instances[*].{Instance:InstanceId,AZ:Placement.AvailabilityZone,Name:Tags[?Key=='Name']|[0].Value}" --output table
    --------------------------------------------------
    |                DescribeInstances               |
    +-------------+-----------------------+----------+
    |     AZ      |       Instance        |  Name    |
    +-------------+-----------------------+----------+
    |  us-east-1a |  i-0581e7619465fe0bb  |  fiap-vm |
    +-------------+-----------------------+----------+
    ```
    ![](https://raw.githubusercontent.com/josecastillolema/fiap/master/cld/multicloud/img/t1.png)

    
## *Clean-up*

11. Deletar o plano:
    ```
    $ terraform destroy
    aws_instance.app_server: Refreshing state... [id=i-0581e7619465fe0bb]

    Terraform used the selected providers to generate the following execution plan. Resource actions are
    indicated with the following symbols:
      - destroy

    Terraform will perform the following actions:

      # aws_instance.app_server will be destroyed
      - resource "aws_instance" "app_server" {
          - ami                                  = "ami-087c17d1fe0178315" -> null
          - arn                                  = "arn:aws:ec2:us-east-1:376713914115:instance/i-0581e7619465fe0bb" -> null
          - associate_public_ip_address          = true -> null
          - availability_zone                    = "us-east-1a" -> null
          - cpu_core_count                       = 1 -> null
          - cpu_threads_per_core                 = 1 -> null
          - disable_api_termination              = false -> null
          - ebs_optimized                        = false -> null
          - get_password_data                    = false -> null
          - hibernation                          = false -> null
          - id                                   = "i-0581e7619465fe0bb" -> null
          - instance_initiated_shutdown_behavior = "stop" -> null
          - instance_state                       = "running" -> null
          - instance_type                        = "t2.micro" -> null
          - ipv6_address_count                   = 0 -> null
          - ipv6_addresses                       = [] -> null
          - monitoring                           = false -> null
          - primary_network_interface_id         = "eni-0a879183697c40c22" -> null
          - private_dns                          = "ip-172-31-93-139.ec2.internal" -> null
          - private_ip                           = "172.31.93.139" -> null
          - public_dns                           = "ec2-18-212-49-179.compute-1.amazonaws.com" -> null
          - public_ip                            = "18.212.49.179" -> null
          - secondary_private_ips                = [] -> null
          - security_groups                      = [
              - "default",
            ] -> null
          - source_dest_check                    = true -> null
          - subnet_id                            = "subnet-2a31160b" -> null
          - tags                                 = {
              - "Name" = "fiap-vm"
            } -> null
          - tags_all                             = {
              - "Name" = "fiap-vm"
            } -> null
          - tenancy                              = "default" -> null
          - vpc_security_group_ids               = [
              - "sg-fa4580e5",
            ] -> null

          - capacity_reservation_specification {
              - capacity_reservation_preference = "open" -> null
            }

          - credit_specification {
              - cpu_credits = "standard" -> null
            }

          - enclave_options {
              - enabled = false -> null
            }

          - metadata_options {
              - http_endpoint               = "enabled" -> null
              - http_put_response_hop_limit = 1 -> null
              - http_tokens                 = "optional" -> null
            }

          - root_block_device {
              - delete_on_termination = true -> null
              - device_name           = "/dev/xvda" -> null
              - encrypted             = false -> null
              - iops                  = 100 -> null
              - tags                  = {} -> null
              - throughput            = 0 -> null
              - volume_id             = "vol-0faa94c59e6dfcc09" -> null
              - volume_size           = 8 -> null
              - volume_type           = "gp2" -> null
            }
        }

    Plan: 0 to add, 0 to change, 1 to destroy.

    Do you really want to destroy all resources?
      Terraform will destroy all your managed infrastructure, as shown above.
      There is no undo. Only 'yes' will be accepted to confirm.

      Enter a value: yes

    aws_instance.app_server: Destroying... [id=i-0581e7619465fe0bb]
    aws_instance.app_server: Still destroying... [id=i-0581e7619465fe0bb, 10s elapsed]
    aws_instance.app_server: Still destroying... [id=i-0581e7619465fe0bb, 20s elapsed]
    aws_instance.app_server: Still destroying... [id=i-0581e7619465fe0bb, 30s elapsed]
    aws_instance.app_server: Destruction complete after 31s

    Destroy complete! Resources: 1 destroyed.
    ```
