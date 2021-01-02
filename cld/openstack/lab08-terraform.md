# Lab 8 - Terraform

## OpenStack Provider

O [Terraform](https://www.terraform.io/) é uma ferramenta para construir, alterar e controlar a infraestrutura de forma segura e eficiente. O Terraform pode gerenciar provedores de serviços existentes e populares como OpenStack, Azure, AWS, Digital Ocean, entre outras, bem como soluções internas personalizadas.

Os arquivos de configuração do Terraform descrevem os componentes necessários para executar um único aplicativo ou todo o *datacenter*, gerando um plano de execução que descreve o que será feito para alcançar o estado desejado e, em seguida, executá-lo para construir a infraestrutura descrita. À medida que a configuração muda, o Terraform é capaz de determinar o que mudou e criar planos de execução incrementais que podem ser aplicados.

A infraestrutura que o Terraform pode gerenciar inclui componentes de baixo nível, como instâncias de computação, armazenamento e redes, bem como componentes de alto nível, como entradas DNS, recursos SaaS, etc.

Com relação a ferramenta podemos comparar o Terraform com o CloudFormation da AWS.

## Instalação

1. Fazer o *download* da ferramenta:

    ```
    $ wget https://releases.hashicorp.com/terraform/0.13.5/terraform_0.13.5_linux_amd64.zip
    --2020-10-28 12:37:26--  https://releases.hashicorp.com/terraform/0.13.5/terraform_0.13.5_linux_amd64.zip
    Resolving releases.hashicorp.com (releases.hashicorp.com)... 151.101.1.183, 151.101.65.183, 151.101.129.183, ...
    Connecting to releases.hashicorp.com (releases.hashicorp.com)|151.101.1.183|:443... connected.
    HTTP request sent, awaiting response... 200 OK
    Length: 34880173 (33M) [application/zip]
    Saving to: ‘terraform_0.13.5_linux_amd64.zip’

    terraform_0.13.5_linux_amd64.zip            100%[========================================================================================>]  33.26M  32.0MB/s    in 1.0s

    2020-10-28 12:37:27 (32.0 MB/s) - ‘terraform_0.13.5_linux_amd64.zip’ saved [34880173/34880173]
    ```
    
2. Descomprimir o arquivo baixado:
    ```
    $ unzip terraform_0.13.5_linux_amd64.zip
    Archive:  terraform_0.13.5_linux_amd64.zip
      inflating: terraform
    ```
    
3. Movimentar o executável:
    ```
    $ sudo mv terraform  /usr/local/bin/
    ```

## Uso

4. Baixar os *templates*:
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
    
    $ cd fiap/cld/openstack/lab08-terraform/
    ```

5. Inicializar o Terraform e o correspondente *provider* (plugin) de OpenStack:
    ```
    $ terraform init

    Initializing the backend...

    Initializing provider plugins...
    - Finding latest version of terraform-provider-openstack/openstack...
    - Installing terraform-provider-openstack/openstack v1.32.0...
    - Installed terraform-provider-openstack/openstack v1.32.0 (self-signed, key ID 4F80527A391BEFD2)

    Partner and community providers are signed by their developers.
    If you'd like to know more about provider signing, you can read about it here:
    https://www.terraform.io/docs/plugins/signing.html

    The following providers do not have any version constraints in configuration,
    so the latest version was installed.

    To prevent automatic upgrades to new major versions that may contain breaking
    changes, we recommend adding version constraints in a required_providers block
    in your configuration, with the constraint strings suggested below.

    * terraform-provider-openstack/openstack: version = "~> 1.32.0"

    Terraform has been successfully initialized!

    You may now begin working with Terraform. Try running "terraform plan" to see
    any changes that are required for your infrastructure. All Terraform commands
    should now work.

    If you ever set or change modules or backend configuration for Terraform,
    rerun this command to reinitialize your working directory. If you forget, other
    commands will detect it and remind you to do so if necessary.
    os@ubuntu:~/fiap/cld/openstack$ terraform plan
    Refreshing Terraform state in-memory prior to plan...
    The refreshed state will be used to calculate this plan, but will not be
    persisted to local or remote state storage.


    ------------------------------------------------------------------------
    ```
    
6. Criar o plano:
    ```
    $ terraform plan
    Refreshing Terraform state in-memory prior to plan...
    The refreshed state will be used to calculate this plan, but will not be
    persisted to local or remote state storage.


    ------------------------------------------------------------------------

    An execution plan has been generated and is shown below.
    Resource actions are indicated with the following symbols:
      + create

    Terraform will perform the following actions:

      # openstack_compute_floatingip_associate_v2.asoc-ip-publica will be created
      + resource "openstack_compute_floatingip_associate_v2" "asoc-ip-publica" {
          + floating_ip = (known after apply)
          + id          = (known after apply)
          + instance_id = (known after apply)
          + region      = (known after apply)
        }

      # openstack_compute_instance_v2.web will be created
      + resource "openstack_compute_instance_v2" "web" {
          + access_ip_v4        = (known after apply)
          + access_ip_v6        = (known after apply)
          + all_metadata        = (known after apply)
          + all_tags            = (known after apply)
          + availability_zone   = "nova"
          + flavor_id           = (known after apply)
          + flavor_name         = "m.fiap"
          + force_delete        = false
          + id                  = (known after apply)
          + image_id            = (known after apply)
          + image_name          = "cirros-0.3.5-x86_64-disk"
          + name                = "web"
          + power_state         = "active"
          + region              = (known after apply)
          + security_groups     = [
              + "default",
            ]
          + stop_before_destroy = false

          + network {
              + access_network = false
              + fixed_ip_v4    = (known after apply)
              + fixed_ip_v6    = (known after apply)
              + floating_ip    = (known after apply)
              + mac            = (known after apply)
              + name           = "private"
              + port           = (known after apply)
              + uuid           = (known after apply)
            }
        }

      # openstack_networking_floatingip_v2.ip-publica will be created
      + resource "openstack_networking_floatingip_v2" "ip-publica" {
          + address    = (known after apply)
          + all_tags   = (known after apply)
          + dns_domain = (known after apply)
          + dns_name   = (known after apply)
          + fixed_ip   = (known after apply)
          + id         = (known after apply)
          + pool       = "public"
          + port_id    = (known after apply)
          + region     = (known after apply)
          + tenant_id  = (known after apply)
        }

    Plan: 3 to add, 0 to change, 0 to destroy.

    ------------------------------------------------------------------------

    Note: You didn't specify an "-out" parameter to save this plan, so Terraform
    can't guarantee that exactly these actions will be performed if
    "terraform apply" is subsequently run.
    ```
    
7. Criar a infraestrutura virtual:
    ```
    $ terraform apply

    An execution plan has been generated and is shown below.
    Resource actions are indicated with the following symbols:
      + create

    Terraform will perform the following actions:

      # openstack_compute_floatingip_associate_v2.asoc-ip-publica will be created
      + resource "openstack_compute_floatingip_associate_v2" "asoc-ip-publica" {
          + floating_ip = (known after apply)
          + id          = (known after apply)
          + instance_id = (known after apply)
          + region      = (known after apply)
        }

      # openstack_compute_instance_v2.web will be created
      + resource "openstack_compute_instance_v2" "web" {
          + access_ip_v4        = (known after apply)
          + access_ip_v6        = (known after apply)
          + all_metadata        = (known after apply)
          + all_tags            = (known after apply)
          + availability_zone   = "nova"
          + flavor_id           = (known after apply)
          + flavor_name         = "m.fiap"
          + force_delete        = false
          + id                  = (known after apply)
          + image_id            = (known after apply)
          + image_name          = "cirros-0.3.5-x86_64-disk"
          + name                = "web"
          + power_state         = "active"
          + region              = (known after apply)
          + security_groups     = [
              + "default",
            ]
          + stop_before_destroy = false

          + network {
              + access_network = false
              + fixed_ip_v4    = (known after apply)
              + fixed_ip_v6    = (known after apply)
              + floating_ip    = (known after apply)
              + mac            = (known after apply)
              + name           = "private"
              + port           = (known after apply)
              + uuid           = (known after apply)
            }
        }

      # openstack_networking_floatingip_v2.ip-publica will be created
      + resource "openstack_networking_floatingip_v2" "ip-publica" {
          + address    = (known after apply)
          + all_tags   = (known after apply)
          + dns_domain = (known after apply)
          + dns_name   = (known after apply)
          + fixed_ip   = (known after apply)
          + id         = (known after apply)
          + pool       = "public"
          + port_id    = (known after apply)
          + region     = (known after apply)
          + tenant_id  = (known after apply)
        }

    Plan: 3 to add, 0 to change, 0 to destroy.

    Do you want to perform these actions?
      Terraform will perform the actions described above.
      Only 'yes' will be accepted to approve.

      Enter a value: yes

    openstack_networking_floatingip_v2.ip-publica: Creating...
    openstack_compute_instance_v2.web: Creating...
    openstack_networking_floatingip_v2.ip-publica: Creation complete after 6s [id=9ee8f0ab-1c12-4ca3-a357-2cec7843bde6]
    openstack_compute_instance_v2.web: Still creating... [10s elapsed]
    openstack_compute_instance_v2.web: Creation complete after 13s [id=1bb24176-55be-4156-913a-af062a0237df]
    openstack_compute_floatingip_associate_v2.asoc-ip-publica: Creating...
    openstack_compute_floatingip_associate_v2.asoc-ip-publica: Creation complete after 2s [id=172.24.4.5/1bb24176-55be-4156-913a-af062a0237df/]

    Apply complete! Resources: 3 added, 0 changed, 0 destroyed.

    Outputs:

    ip = 172.24.4.5
    ```
    
8. Mostrar os recursos criados:
    ```
    $ terraform show
    # openstack_compute_floatingip_associate_v2.asoc-ip-publica:
    resource "openstack_compute_floatingip_associate_v2" "asoc-ip-publica" {
        floating_ip = "172.24.4.5"
        id          = "172.24.4.5/1bb24176-55be-4156-913a-af062a0237df/"
        instance_id = "1bb24176-55be-4156-913a-af062a0237df"
        region      = "RegionOne"
    }

    # openstack_compute_instance_v2.web:
    resource "openstack_compute_instance_v2" "web" {
        access_ip_v4        = "10.0.0.6"
        access_ip_v6        = "[fdb5:7432:9bc4:0:f816:3eff:feaa:4949]"
        all_metadata        = {}
        all_tags            = []
        availability_zone   = "nova"
        flavor_id           = "a0683bcb-b937-4c75-be19-7641eceeff78"
        flavor_name         = "m.fiap"
        force_delete        = false
        id                  = "1bb24176-55be-4156-913a-af062a0237df"
        image_id            = "cd992dd3-2197-49fe-9f0e-43d783d18a5c"
        image_name          = "cirros-0.3.5-x86_64-disk"
        name                = "web"
        power_state         = "active"
        region              = "RegionOne"
        security_groups     = [
            "default",
        ]
        stop_before_destroy = false

        network {
            access_network = false
            fixed_ip_v4    = "10.0.0.6"
            fixed_ip_v6    = "[fdb5:7432:9bc4:0:f816:3eff:feaa:4949]"
            mac            = "fa:16:3e:aa:49:49"
            name           = "private"
            uuid           = "4e05e1bd-50f4-494b-aacf-07d43a37d1a1"
        }
    }

    # openstack_networking_floatingip_v2.ip-publica:
    resource "openstack_networking_floatingip_v2" "ip-publica" {
        address   = "172.24.4.5"
        all_tags  = []
        id        = "9ee8f0ab-1c12-4ca3-a357-2cec7843bde6"
        pool      = "public"
        region    = "RegionOne"
        tenant_id = "faac34f01fb2464295bcea501b18b741"
    }


    Outputs:

    ip = "172.24.4.5"
    ```

9. Mostrar o grafo dos recursos criados:
    ```
    $ terraform graph
    digraph {
            compound = "true"
            newrank = "true"
            subgraph "root" {
                    "[root] openstack_compute_floatingip_associate_v2.asoc-ip-publica (expand)" [label = "openstack_compute_floatingip_associate_v2.asoc-ip-publica", shape = "box"]
                    "[root] openstack_compute_instance_v2.web (expand)" [label = "openstack_compute_instance_v2.web", shape = "box"]
                    "[root] openstack_networking_floatingip_v2.ip-publica (expand)" [label = "openstack_networking_floatingip_v2.ip-publica", shape = "box"]
                    "[root] provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"]" [label = "provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"]", shape = "diamond"]
                    "[root] var.defaults" [label = "var.defaults", shape = "note"]
                    "[root] meta.count-boundary (EachMode fixup)" -> "[root] openstack_compute_floatingip_associate_v2.asoc-ip-publica (expand)"
                    "[root] meta.count-boundary (EachMode fixup)" -> "[root] output.ip (expand)"
                    "[root] openstack_compute_floatingip_associate_v2.asoc-ip-publica (expand)" -> "[root] openstack_compute_instance_v2.web (expand)"
                    "[root] openstack_compute_floatingip_associate_v2.asoc-ip-publica (expand)" -> "[root] openstack_networking_floatingip_v2.ip-publica (expand)"
                    "[root] openstack_compute_instance_v2.web (expand)" -> "[root] provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"]"
                    "[root] openstack_compute_instance_v2.web (expand)" -> "[root] var.defaults"
                    "[root] openstack_networking_floatingip_v2.ip-publica (expand)" -> "[root] provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"]"
                    "[root] output.ip (expand)" -> "[root] openstack_networking_floatingip_v2.ip-publica (expand)"
                    "[root] provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"] (close)" -> "[root] openstack_compute_floatingip_associate_v2.asoc-ip-publica (expand)"
                    "[root] root" -> "[root] meta.count-boundary (EachMode fixup)"
                    "[root] root" -> "[root] provider[\"registry.terraform.io/terraform-provider-openstack/openstack\"] (close)"
            }
    }
    ```

10. Validar a criação da instância:
    ```
    $ source devstack/openrc admin
    WARNING: setting legacy OS_TENANT_NAME to support cli tools.
    
    $ openstack server list
    +--------------------------------------+------+--------+--------------------------------------------------------------------+--------------------------+--------+
    | ID                                   | Name | Status | Networks                                                           | Image                    | Flavor |
    +--------------------------------------+------+--------+--------------------------------------------------------------------+--------------------------+--------+
    | 1bb24176-55be-4156-913a-af062a0237df | web  | ACTIVE | private=fdb5:7432:9bc4:0:f816:3eff:feaa:4949, 10.0.0.6, 172.24.4.5 | cirros-0.3.5-x86_64-disk | m.fiap |
    +--------------------------------------+------+--------+--------------------------------------------------------------------+--------------------------+--------+
    ```
    
## *Clean-up*

11. Deletar o plano:
    ```
    $ terraform destroy
    openstack_networking_floatingip_v2.ip-publica: Refreshing state... [id=9ee8f0ab-1c12-4ca3-a357-2cec7843bde6]
    openstack_compute_instance_v2.web: Refreshing state... [id=1bb24176-55be-4156-913a-af062a0237df]
    openstack_compute_floatingip_associate_v2.asoc-ip-publica: Refreshing state... [id=172.24.4.5/1bb24176-55be-4156-913a-af062a0237df/]

    An execution plan has been generated and is shown below.
    Resource actions are indicated with the following symbols:
      - destroy

    Terraform will perform the following actions:

      # openstack_compute_floatingip_associate_v2.asoc-ip-publica will be destroyed
      - resource "openstack_compute_floatingip_associate_v2" "asoc-ip-publica" {
          - floating_ip = "172.24.4.5" -> null
          - id          = "172.24.4.5/1bb24176-55be-4156-913a-af062a0237df/" -> null
          - instance_id = "1bb24176-55be-4156-913a-af062a0237df" -> null
          - region      = "RegionOne" -> null
        }

      # openstack_compute_instance_v2.web will be destroyed
      - resource "openstack_compute_instance_v2" "web" {
          - access_ip_v4        = "10.0.0.6" -> null
          - access_ip_v6        = "[fdb5:7432:9bc4:0:f816:3eff:feaa:4949]" -> null
          - all_metadata        = {} -> null
          - all_tags            = [] -> null
          - availability_zone   = "nova" -> null
          - flavor_id           = "a0683bcb-b937-4c75-be19-7641eceeff78" -> null
          - flavor_name         = "m.fiap" -> null
          - force_delete        = false -> null
          - id                  = "1bb24176-55be-4156-913a-af062a0237df" -> null
          - image_id            = "cd992dd3-2197-49fe-9f0e-43d783d18a5c" -> null
          - image_name          = "cirros-0.3.5-x86_64-disk" -> null
          - name                = "web" -> null
          - power_state         = "active" -> null
          - region              = "RegionOne" -> null
          - security_groups     = [
              - "default",
            ] -> null
          - stop_before_destroy = false -> null
          - tags                = [] -> null

          - network {
              - access_network = false -> null
              - fixed_ip_v4    = "10.0.0.6" -> null
              - fixed_ip_v6    = "[fdb5:7432:9bc4:0:f816:3eff:feaa:4949]" -> null
              - mac            = "fa:16:3e:aa:49:49" -> null
              - name           = "private" -> null
              - uuid           = "4e05e1bd-50f4-494b-aacf-07d43a37d1a1" -> null
            }
        }

      # openstack_networking_floatingip_v2.ip-publica will be destroyed
      - resource "openstack_networking_floatingip_v2" "ip-publica" {
          - address   = "172.24.4.5" -> null
          - all_tags  = [] -> null
          - fixed_ip  = "10.0.0.6" -> null
          - id        = "9ee8f0ab-1c12-4ca3-a357-2cec7843bde6" -> null
          - pool      = "public" -> null
          - port_id   = "d8349f6e-8663-4bd0-b92e-04beed2b58b1" -> null
          - region    = "RegionOne" -> null
          - tags      = [] -> null
          - tenant_id = "faac34f01fb2464295bcea501b18b741" -> null
        }

    Plan: 0 to add, 0 to change, 3 to destroy.

    Changes to Outputs:
      - ip = "172.24.4.5" -> null

    Do you really want to destroy all resources?
      Terraform will destroy all your managed infrastructure, as shown above.
      There is no undo. Only 'yes' will be accepted to confirm.

      Enter a value: yes

    openstack_compute_floatingip_associate_v2.asoc-ip-publica: Destroying... [id=172.24.4.5/1bb24176-55be-4156-913a-af062a0237df/]
    openstack_compute_floatingip_associate_v2.asoc-ip-publica: Destruction complete after 2s
    openstack_networking_floatingip_v2.ip-publica: Destroying... [id=9ee8f0ab-1c12-4ca3-a357-2cec7843bde6]
    openstack_compute_instance_v2.web: Destroying... [id=1bb24176-55be-4156-913a-af062a0237df]
    openstack_networking_floatingip_v2.ip-publica: Destruction complete after 6s
    openstack_compute_instance_v2.web: Still destroying... [id=1bb24176-55be-4156-913a-af062a0237df, 10s elapsed]
    openstack_compute_instance_v2.web: Destruction complete after 10s

    Destroy complete! Resources: 3 destroyed.
    ```

12. Conferir que a VM foi deletada:
    ```
    $ openstack server list

    $
    ```
