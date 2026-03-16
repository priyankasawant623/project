
# CI/CD Deployment of .NET Application

## 📌 Project Overview

This project demonstrates a **complete CI/CD pipeline for deploying a .NET Web API** using modern DevOps tools.
The pipeline automates the process of building the application, creating Docker images, pushing them to AWS ECR, and deploying the application to a Kubernetes cluster.

The goal of this project is to implement **automated build, containerization, and deployment** to ensure faster and more reliable software delivery.

---

# 🛠 Tech Stack

* .NET Web API
* Jenkins (CI/CD Pipeline)
* Docker (Containerization)
* Kubernetes (Container Orchestration)
* AWS ECR (Container Registry)
* MySQL (Database)

---

# 📂 Project Structure

```
project/
│
|
├── backend
│   ├── DataWarehouse.API.Tests
│   ├── DataWarehouse.sln
│   ├── DataWarehouse.API/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── Infrastructure/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── DataWarehouse.API.csproj
│
|
├── frontend/
│   ├──Dockerfile
│   ├──index.html
│   ├──script.js
│   └──style.css
|
├── Jenkinsfile
│
├── k8s/
│   ├──backend-deployment.yaml
│   ├──backend-service.yaml
│   ├──frontend-deployment.yaml
│   ├──frontend-service.yaml
│   ├── mysql-deployment.yaml
│   └── mysql-service.yaml
│
└── README.md
```

---

# ⚙️ Prerequisites

Make sure the following tools are installed before running the project:

* Git
* Docker
* Kubernetes
* Jenkins
* AWS CLI

You should also have:

* AWS Account
* AWS ECR repository created
* Kubernetes cluster configured

---

# 🚀 Clone the Repository

```bash
git clone https://github.com/priyankasawant623/project.git
cd project
```

---

# 🐳 Build Docker Image

```bash
docker build -t dotnet-backend .
```

---

# 📦 Push Docker Image to AWS ECR

Login to AWS ECR:

```bash
aws ecr get-login-password --region ap-south-1 \
| docker login --username AWS --password-stdin <ECR_URI>
```

Tag the image:

```bash
docker tag dotnet-backend:latest <ECR_URI>/dotnet-backend:latest
```

Push the image:

```bash
docker push <ECR_URI>/dotnet-backend:latest
```

---

# ☸️ Deploy to Kubernetes

Apply Kubernetes configuration files:

```bash
kubectl apply -f k8s/
```

Check running pods:

```bash
kubectl get pods
```

Check services:

```bash
kubectl get svc
```

---

# 🔄 CI/CD Pipeline Flow

1. Developer pushes code to GitHub repository
2. Jenkins pipeline is triggered automatically
3. Application is built
4. Docker image is created
5. Image is pushed to AWS ECR
6. Kubernetes pulls the image
7. Application is deployed to the cluster

---

# 🌐 API Endpoint

Example endpoint:

```
GET /api/orders
```

Local URL:

```
http://localhost:5000/api/orders
```

---

# ✨ Features

* Automated CI/CD pipeline
* Containerized application using Docker
* Kubernetes deployment for scalability
* Integration with AWS ECR
* MySQL database integration

---

# 👩‍💻 Author

**Priyanka Sawant**

---

# 📜 License

This project is for educational and demonstration purposes.
