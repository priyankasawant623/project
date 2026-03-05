DataWarehouse Orders – Deployment Guide
Project Architecture
4

This project demonstrates deployment of a .NET backend and static frontend on Kubernetes using Docker, AWS ECR, and Jenkins CI/CD, with MySQL as the database.

Architecture components:

Frontend → Static website

Backend → .NET Web API

Database → MySQL

Containerization → Docker

Registry → AWS ECR

Orchestration → Kubernetes

CI/CD → Jenkins

Cloud → AWS EC2

1️⃣ Clone Repository
git clone https://github.com/priyankasawant623/dotnet.git
cd dotnet
2️⃣ Dockerize Applications
Backend Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","DataWarehouse.API.dll"]
Frontend Dockerfile
FROM nginx:alpine
COPY . /usr/share/nginx/html
3️⃣ Build Docker Images
docker build -t dotnet-backend .
docker build -t static-frontend .
4️⃣ Push Images to AWS ECR

Login to ECR:

aws ecr get-login-password --region ap-south-1 \
| docker login --username AWS --password-stdin <ACCOUNT_ID>.dkr.ecr.ap-south-1.amazonaws.com

Tag images:

docker tag dotnet-backend:latest <ACCOUNT_ID>.dkr.ecr.ap-south-1.amazonaws.com/dotnet-backend:latest
docker tag static-frontend:latest <ACCOUNT_ID>.dkr.ecr.ap-south-1.amazonaws.com/static-frontend:latest

Push images:

docker push <ACCOUNT_ID>.dkr.ecr.ap-south-1.amazonaws.com/dotnet-backend:latest
docker push <ACCOUNT_ID>.dkr.ecr.ap-south-1.amazonaws.com/static-frontend:latest
5️⃣ Setup Kubernetes Cluster

On EC2 install:

Docker
kubectl
eksctl (optional)

Verify cluster:

kubectl get nodes
6️⃣ Deploy MySQL in Kubernetes
mysql-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mysql
spec:
  replicas: 1
  selector:
    matchLabels:
      app: mysql
  template:
    metadata:
      labels:
        app: mysql
    spec:
      containers:
      - name: mysql
        image: mysql:8
        env:
        - name: MYSQL_ROOT_PASSWORD
          value: root
        ports:
        - containerPort: 3306
mysql-service.yaml
apiVersion: v1
kind: Service
metadata:
  name: mysql-service
spec:
  selector:
    app: mysql
  ports:
  - port: 3306
  type: ClusterIP

Apply:

kubectl apply -f mysql-deployment.yaml
kubectl apply -f mysql-service.yaml
7️⃣ Create Database and Table

Enter MySQL pod:

kubectl exec -it mysql-pod-name -- bash
mysql -u root -p

Create database:

CREATE DATABASE DataWarehouse;
USE DataWarehouse;

CREATE TABLE Orders (
 Id INT AUTO_INCREMENT PRIMARY KEY,
 OrderDate DATETIME NOT NULL,
 Amount DECIMAL(18,2) NOT NULL
);
8️⃣ Deploy Backend
backend-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: dotnet-backend
spec:
  replicas: 2
  selector:
    matchLabels:
      app: dotnet-backend
  template:
    metadata:
      labels:
        app: dotnet-backend
    spec:
      containers:
      - name: backend
        image: <ECR>/dotnet-backend:latest
        ports:
        - containerPort: 5000
backend-service.yaml
apiVersion: v1
kind: Service
metadata:
  name: backend-service
spec:
  selector:
    app: dotnet-backend
  ports:
  - port: 5000
    targetPort: 5000
  type: LoadBalancer

Apply:

kubectl apply -f backend-deployment.yaml
kubectl apply -f backend-service.yaml
9️⃣ Deploy Frontend
frontend-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: static-frontend
spec:
  replicas: 2
  selector:
    matchLabels:
      app: static-frontend
  template:
    metadata:
      labels:
        app: static-frontend
    spec:
      containers:
      - name: frontend
        image: <ECR>/static-frontend:latest
        ports:
        - containerPort: 80
frontend-service.yaml
apiVersion: v1
kind: Service
metadata:
  name: frontend-service
spec:
  selector:
    app: static-frontend
  ports:
  - port: 80
    targetPort: 80
  type: LoadBalancer

Apply:

kubectl apply -f frontend-deployment.yaml
kubectl apply -f frontend-service.yaml
🔟 Access Application

Get service URLs:

kubectl get svc

Example output:

frontend-service   LoadBalancer   a9ad4833161ba419e84c6524efc32fd1.elb.amazonaws.com
backend-service    LoadBalancer   afda467f70dfb4b68bbd34f9f72ff738.elb.amazonaws.com

Open frontend:

http://<frontend-load-balancer>
1️⃣1️⃣ Jenkins CI/CD Pipeline

Pipeline stages:

1️⃣ Pull code from GitHub
2️⃣ Build Docker images
3️⃣ Push images to AWS ECR
4️⃣ Deploy to Kubernetes

Example stages:

Build Docker Image
Push to ECR
Deploy to Kubernetes
1️⃣2️⃣ Verify Deployment

Check pods:

kubectl get pods

Check services:

kubectl get svc

Check logs:

kubectl logs deployment/dotnet-backend
Final Result

You can:

Add new orders

Fetch orders from database

View results on frontend UI

Data flow:

Frontend → Backend API → MySQL Database
