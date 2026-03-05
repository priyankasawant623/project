sudo cat /var/lib/jenkins/secrets/initialAdminPassword
    2  sudo usermod -aG docker jenkins
    3  sudo systemctl restart jenkins
    4  sudo cp -r .aws/ /var/lib/jenkins/
    5  sudo chown -R jenkins:jenkins /var/lib/jenkins/.aws/
    6  sudo cp -r .kube/ /var/lib/jenkins/
    7  sudo chown -R jenkins:jenkins /var/lib/jenkins/.kube/
    8  kubectl get all
    9  kubectl logs deployment/dotnet-backend
   10  kubectl exec -it deployment/mysql -- mysql -u root -p
   11  kubectl exec -it mysql-76799765bb-rjn6x -- bash
   12  kubectl get svc
   13  kubectl port-forward service/backend-service 5000:5000
   14  kubectl edit svc backend-service
   15  kubectl get svc
   16  kubectl rollout restart deployment static-frontend
   17  kubectl get svc
   18  kubectl edit svc backend-service
   19  kubectl get svc
   20  kubectl get pods
   21  kubectl logs deployment/dotnet-backend
   23  kubectl rollout restart deployment static-frontend
