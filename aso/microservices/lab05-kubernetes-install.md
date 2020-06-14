# Lab 5 - Kubernetes - Instalação

Orquestrando containers
--------------
Kubernetes (k8s), da mesma forma que o Docker Swarm, permite orquestrar containers em um cluster formado por vários servidores. De esta forma conseguimos garantir as seguintes propriedades nos containers gerenciados pelo orquestrador:
 - **tolerância a falhas**: se um dos servidores do cluster cair, o container automaticamente será iniciado em outro servidor do cluster
 - **alta disponibilidade**: várias réplicas de cada container podem ser executadas em vários servidores do cluster
 - **escalabilidade**: o número de réplicas de cada container pode ser aumentado a qualquer momento em funçao da demanda
 
 **Microk8s** é um Kubernetes pequeno, rápido, seguro e com um único nó (*all-in-one*) que é instalado em praticamente qualquer computador com Linux. Usado para desenvolvimento *off-line*, criação de protótipos, testes ou uso em uma VM como um k8s pequeno, barato e confiável para CI/CD.
 
1. Instalação do **microk8s**:
    ```
    $ sudo snap install microk8s --classic
    microk8s v1.18.0 from Canonical✓ installed
    ```
2. Ajuste de permissões do usuário ***ubuntu*** no grupo ***microk8s***:
    ```
    $ sudo usermod -a -G microk8s ubuntu
    $ sudo reboot
    ```

3. Após o *reboot*, confirmar que o usuário pertence ao grupo `microk8s`:
   ```
   $ groups
   ubuntu adm dialout cdrom floppy sudo audio dip video plugdev lxd netdev docker microk8s
   ```

4. Conferir instalação:
    ```yaml
    $ microk8s.status
    microk8s is running
    addons:
    cilium: disabled
    dashboard: disabled
    dns: disabled
    fluentd: disabled
    gpu: disabled
    helm: disabled
    helm3: disabled
    ingress: disabled
    istio: disabled
    jaeger: disabled
    knative: disabled
    kubeflow: disabled
    linkerd: disabled
    metallb: disabled
    metrics-server: disabled
    prometheus: disabled
    rbac: disabled
    registry: disabled
    storage: disabled
    ```

5. Usar o cliente interno do microk8s (***kubectl***):
    ```
    $ microk8s.kubectl get all
    NAME                 TYPE        CLUSTER-IP     EXTERNAL-IP   PORT(S)   AGE
    service/kubernetes   ClusterIP   10.152.183.1   <none>        443/TCP   4m5s
    ```

6. Instalação do cliente externo:
    ```
    $ sudo snap install kubectl --classic
    kubectl 1.18.0 from Canonical✓ installed
    ```

7. Configuracão dos parámetros de acesso (URL, credenciais, etc.) do cliente externo:
    ```yaml
    $ microk8s.config > .kube/config
    $ cat .kube/config 
    apiVersion: v1
    clusters:
    - cluster:
        certificate-authority-data: LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSURBVENDQWVtZ0F3SUJBZ0lKQUx5UmJTeUd2NjRLTUEwR0NTcUdTSWIzRFFFQkN3VUFNQmN4RlRBVEJnTlYKQkFNTURERXdMakUxTWk0eE9ETXVNVEFlRncweU1EQTBNVFV5TURBek1URmFGdzB5TURBMU1UVXlNREF6TVRGYQpNQmN4RlRBVEJnTlZCQU1NRERFd0xqRTFNaTR4T0RNdU1UQ0NBU0l3RFFZSktvWklodmNOQVFFQkJRQURnZ0VQCkFEQ0NBUW9DZ2dFQkFNODM5REltV1VLN2JNMWQ1OVR1SThITnRBbVlOQ2hqSStFa0txa2g5T1lPaWV0c0xjbDIKS2p5Ym5nRTJyOHp4M21YeEtGSmhnZGJxVzNmVHpGOGtxNGNKbVdFc0UxKzFtdFRaRUNFU0lrVmFlK3NUbjMvMwo0VWRYckV3SW5tQjJsMDlCRFNXblRBTHBaVUhnSFRvcUhOQmVzTlVlQ3h6Z0dzUWgranRGSlEzMWp4QnNqQnFNCk1EM1ZNRlFCK2FMUW5pSTM0UTZrNm8zZmR5dTArQlhJNHVYR3hldWFBVzViZStidWlPUlQ3a3hhdnZ4bmNsOE4KTFd0aG4wK2hjKzNEUE1DdXhxT2dra0UvRFZCV2trdklsbHBMeG5RLzU3b1BxUHVFVXRzd1hJeGZRMi9EUTBNdwpzZ1pLT0trNzYrby9nakhmaTNtRjArYTJWaUZPcm9FVTVBTUNBd0VBQWFOUU1FNHdIUVlEVlIwT0JCWUVGUE1LCkMyaXl0QkZxVTM3VU1uRXJHZEYrZnoyeU1COEdBMVVkSXdRWU1CYUFGUE1LQzJpeXRCRnFVMzdVTW5FckdkRisKZnoyeU1Bd0dBMVVkRXdRRk1BTUJBZjh3RFFZSktvWklodmNOQVFFTEJRQURnZ0VCQUpRdG5HbGZPVTk5VGdyZgpUNDkxc3hCSUUydVdEVG9Pc3BMVC9hcEFjK1JSdGtlWk1tS1NQNjYzNTFhQnJEamFCem9nblZ6WVNDQWxEU3Y1Cm95bVN3MTBQUms2SGt0QW44YzlFOGFFTHpyOHowZytOVE1sazRDRHNtQTcrN3hpMm1UaEM3bitzMWpXd1Ztd08KUzdRWG05dTIyS2g4MFk4aU1qVHRDdDBTY2I1TFdFVmwyLzRzWHhyQnZiSFBpbllRdjZSdFhvQWxlODFGMmZZLwpjVDg0L0dHNHRJdXd0VlB2b09hS0lKTzZLT1l6a0VzcU81SFdiTnltZ2xpVGxlMEFBLzdEUUprd1c0M0h4clF1CmdoMThrVURtQXpJa2Z0dDV0M1pKVnNJR2xGUnprZXRJK25TVnVYbjhHN0l5NEd5aWdiNmlZcE1EeE9CblhXWG0KbG43L09YZz0KLS0tLS1FTkQgQ0VSVElGSUNBVEUtLS0tLQo=
        server: https://172.31.47.198:16443
      name: microk8s-cluster
    contexts:
    - context:
        cluster: microk8s-cluster
        user: admin
      name: microk8s
    current-context: microk8s
    kind: Config
    preferences: {}
    users:
    - name: admin
      user:
        username: admin
        password: NldvWW5rZGFzY3I4Tkw4SVY4Y25kV3YrR1lXcnZNek1tc2Z2a2prVStEQT0K\
    ```

8. Testar o cliente externo:
    ```
    $ kubectl get all --all-namespaces
    NAMESPACE   NAME                 TYPE        CLUSTER-IP     EXTERNAL-IP   PORT(S)   AGE
    default     service/kubernetes   ClusterIP   10.152.183.1   <none>        443/TCP   6m46s
    ```

9. Habilitar os *plugins* de ***dns*** e ***dashboard***
    ```
    $ microk8s.enable dns dashboard
    Enabling DNS
    Applying manifest
    serviceaccount/coredns created
    configmap/coredns created
    deployment.apps/coredns created
    service/kube-dns created
    clusterrole.rbac.authorization.k8s.io/coredns created
    clusterrolebinding.rbac.authorization.k8s.io/coredns created
    Restarting kubelet
    DNS is enabled
    Applying manifest
    serviceaccount/kubernetes-dashboard created
    service/kubernetes-dashboard created
    secret/kubernetes-dashboard-certs created
    secret/kubernetes-dashboard-csrf created
    secret/kubernetes-dashboard-key-holder created
    configmap/kubernetes-dashboard-settings created
    role.rbac.authorization.k8s.io/kubernetes-dashboard created
    clusterrole.rbac.authorization.k8s.io/kubernetes-dashboard created
    rolebinding.rbac.authorization.k8s.io/kubernetes-dashboard created
    clusterrolebinding.rbac.authorization.k8s.io/kubernetes-dashboard created
    deployment.apps/kubernetes-dashboard created
    service/dashboard-metrics-scraper created
    deployment.apps/dashboard-metrics-scraper created
    service/monitoring-grafana created
    service/monitoring-influxdb created
    service/heapster created
    deployment.apps/monitoring-influxdb-grafana-v4 created
    serviceaccount/heapster created
    clusterrolebinding.rbac.authorization.k8s.io/heapster created
    configmap/heapster-config created
    configmap/eventer-config created
    deployment.apps/heapster-v1.5.2 created

    If RBAC is not enabled access the dashboard using the default token retrieved with:

    token=$(microk8s kubectl -n kube-system get secret | grep default-token | cut -d " " -f1)
    microk8s kubectl -n kube-system describe secret $token

    In an RBAC enabled setup (microk8s enable RBAC) you need to create a user with restricted
    permissions as shown in:
    https://github.com/kubernetes/dashboard/blob/master/docs/user/access-control/creating-sample-user.md
    ```

10. Confirmar que os *plugins* estão habilitados:
    ```yaml
    $ microk8s.status
    microk8s is running
    addons:
    dashboard: enabled
    dns: enabled
    cilium: disabled
    fluentd: disabled
    gpu: disabled
    helm: disabled
    helm3: disabled
    ingress: disabled
    istio: disabled
    jaeger: disabled
    knative: disabled
    kubeflow: disabled
    linkerd: disabled
    metallb: disabled
    metrics-server: disabled
    prometheus: disabled
    rbac: disabled
    registry: disabled
    storage: disabled
    ```

