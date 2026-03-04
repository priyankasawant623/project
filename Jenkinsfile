pipeline {

    agent any

    environment {
        AWS_REGION = "ap-south-1"
        ECR_REGISTRY = "410003306855.dkr.ecr.ap-south-1.amazonaws.com"

        BACKEND_REPO = "priyanka623/dotnet-backend"
        FRONTEND_REPO = "priyanka623/static-frontend"

        IMAGE_TAG = "latest"

        BACKEND_IMAGE = "${ECR_REGISTRY}/${BACKEND_REPO}:${IMAGE_TAG}"
        FRONTEND_IMAGE = "${ECR_REGISTRY}/${FRONTEND_REPO}:${IMAGE_TAG}"
    }

    stages {

        
        stage('Clone Code') {
            steps {
                git branch: 'main',
                url: 'https://github.com/priyankasawant623/project.git'
            }
        }

        
        stage('Build Backend Image') {
    steps {
        sh '''
        cd backend/DataWarehouse.API
        docker build -t ${BACKEND_IMAGE} .
        '''
    }
}

        stage('Build Frontend Image') {
            steps {
                sh 'docker build -t ${FRONTEND_IMAGE} ./frontend'
            }
        }

        stage('Login to ECR') {
            steps {
                sh '''
                aws ecr get-login-password --region ${AWS_REGION} | \
                docker login --username AWS --password-stdin ${ECR_REGISTRY}
                '''
            }
        }

        stage('Push Images') {
            steps {
                sh '''
                docker push ${BACKEND_IMAGE}
                docker push ${FRONTEND_IMAGE}
                '''
            }
        }

        stage('Deploy MySQL to EKS') {
    steps {
        sh '''
        kubectl apply -f k8s/mysql-deployment.yaml
        kubectl apply -f k8s/mysql-service.yaml

        kubectl rollout status deployment/mysql
        '''
    }
}

        stage('Deploy to EKS') {
    steps {
        sh '''
        kubectl apply -f k8s/backend-deployment.yaml
        kubectl apply -f k8s/backend-service.yaml

        kubectl apply -f k8s/frontend-deployment.yaml
        kubectl apply -f k8s/frontend-service.yaml

        kubectl set image deployment/dotnet-backend dotnet-backend=${BACKEND_IMAGE}
        kubectl set image deployment/static-frontend static-frontend=${FRONTEND_IMAGE}

        kubectl rollout status deployment/dotnet-backend
        kubectl rollout status deployment/static-frontend
        '''
    }
}

        stage('Verify') {
            steps {
                sh '''
                kubectl get pods
                kubectl get svc
                '''
            }
        }
    }

    post {
        success {
            echo 'Static Frontend + .NET Backend Deployment Successful!'
        }
        failure {
            echo 'Deployment Failed!'
        }
    }
}
