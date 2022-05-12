# BasicApp

A basic .NET Core out-of-the-box console application use for simple deployments in Kubernetes. 

## dotnet CLI

dotnet CLI used to create this project:

```ps1: In C:\src\github.com\ongzhixian\BasicApp
dotnet new sln -n BasicApp
dotnet new console -n BasicApp.ConsoleApp
dotnet sln .\BasicApp.sln add .\BasicApp.ConsoleApp\
```

## Minikube CLI

minikube image build .\BasicApp.ConsoleApp\ -t basic-app:1

Run iteractive (and terminate when done with `--rm` flag)
minikube kubectl -- run -it app1 --image=basic-app:1 --rm

Run iteractive (and continue running after exit without `--rm` flag) 
(Not recommend 👎👎👎 -- this approach has the unintended side-effect of terminating pod when we <CTRL-C> to exit)
minikube kubectl -- run -it app1 --image=basic-app:1

Just run (and view output by attaching later)
minikube kubectl -- run app1 --image=basic-app:1

Attach to running pod without interactivity
minikube kubectl -- attach app1 

Attach to running pod with interactivity
minikube kubectl -- attach app1 -i -t


Attache and spawn a shell
minikube kubectl -- exec app1 --stdin --tty -- /bin/bash

