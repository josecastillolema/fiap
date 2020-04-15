# Lab 6 - Kompose

Kompose permite importar templates do Docker Swarm no Kubernetes.
 
1. Instalacao do Kompose:
```
$ curl -L https://github.com/kubernetes/kompose/releases/download/v1.17.0/kompose-linux-amd64 -o kompose
  % Total    % Received % Xferd  Average Speed   Time    Time     Time  Current
                                 Dload  Upload   Total   Spent    Left  Speed
100   630  100   630    0     0   3500      0 --:--:-- --:--:-- --:--:--  3519
100 50.0M  100 50.0M    0     0  31.3M      0  0:00:01  0:00:01 --:--:-- 37.8M
$ chmod +x kompose
$ sudo mv ./kompose /usr/local/bin/
```

2. 
```
$ cd fiap/aso/swarm/v1
$ pwd
/home/ubuntu/fiap/swarm/v1
```

3. 
```
$ kompose up
INFO We are going to create Kubernetes Deployments, Services and PersistentVolumeClaims for your Dockerized application. If you need different kind of resources, use the 'kompose convert' and 'kubectl create -f' commands instead. 
 
INFO Deploying application in "default" namespace 
INFO Successfully created Service: api            
INFO Successfully created Service: mysql          
INFO Successfully created Pod: api                
INFO Successfully created Pod: mysql              
INFO Successfully created PersistentVolumeClaim: db-data of size 100Mi. If your cluster has dynamic storage provisioning, you don't have to do anything. Otherwise you have to create PersistentVolume to make PVC work 

Your application has been deployed to Kubernetes. You can run 'kubectl get deployment,svc,pods,pvc' for details.
```

4.
```

```
