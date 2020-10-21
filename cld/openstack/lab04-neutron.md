# Lab 4 - Neutron

## Network Service
Usaremos o serviço Neutron para aprender alguns conceitos importantes sobre virtualização de redes:
 - criação de redes/subredes virtuais
 - *virtual routers*
 - *security groups*
 - *floating IPs*
 

1.	Conferir que o Neutron foi instalado no OpenStack:
 
2.	Mostrar informação sobre o endpoint:
 
3.	Conferir que a porta 9696 está aberta e associada ao Neutron:
 
4.	Conferir a saúde dos agentes que compõem o Neutron:
 
5.	Listar os serviços Linux que compõem o Neutron:
 
 
6.	Conferir a saúde dos serviços:
 
 
7.	Mostrar os logs do serviço:
 
8.	Mostrar os arquivos de configuração:
 
 

9.	Mostrar os arquivos de configuração de uma forma mais clara (sem comentários nem linhas vazias):
 
 
 
10.	Criar uma rede:
 
11.	Criar uma subrede:
 
12.	Subir duas vms via Horizon associadas à rede/subrede que acabamos de criar no passos anteriores. Acessar a uma delas via console e tentar pingar a outra vm pelo IP interno. Depois tenta pingar para a Internet (p.ex. 8.8.8.8, o servidor DNS da Google, não vai conseguir):
 
 
13.	Criar um roteador:
 
14.	Associar o roteador a rede externa (pública) do OpenStack:
 
15.	Adicionar uma interface do roteador à subrede previamente criada:
 
16.	Repetir os testes de ping via console na vm previamente criada:
 
17.	Reservar um IP público (floating IP):
 
18.	Associar o IP público à VM previamente criada:
 
 
19.	Criar um security group:
 
20.	Liberar o tráfego ICMP (para poder pingar à VM):
 
21.	Liberar o tráfego TCP (para conseguir acessar via SSH à VM):
 
22.	Associar o security group à VM via Horizon (e retirar o default)

23.	Tentar pingar a VM pelo IP interno:
 
24.	Conferir o ID do roteador para saber a qual namespace temos que acessar:
 
25.	Listar os namespaces:
 
26.	Acessar ao namespace do router:
 

27.	Conferir os IPs (do gateway e floating IPs):
 
28.	Pingar à VM e acessar via SSH:
 
29.	Listar as portas:
 
30.	Mostar informação sobre uma porta determinada:
 
31.	Encontrar a interface associada a porta:

32.	Mostrar a porta no openvswitch:
 
33.	Mostrar os bridges e as interfaces configuradas no OpenvSwitch:
 
34.	Deletar vms, rede, subrede e roteador
 
35.	Refazer o mesmo processo via Horizon Dashboard:
    - Criação de rede
    - Criação de subrede
    - Criação de rotaedor
    - Assignar interfaces ao roteador
    - Reservar floating IP e associar a instância
    - Criação de security group
    - Liberar regras no security group

