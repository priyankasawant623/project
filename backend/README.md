STEP 1> Install .NET Runtime on EC2
sudo dnf update -y
sudo dnf install -y dotnet-sdk-9.0
STEP 2> Install git Runtime on EC2
sudo yum install git -y
STEP 3> Upload Your Application to EC2
git clone <your-repo-url>
STEP 4> Create Database & Table
CREATE DATABASE DataWarehouse;
USE DataWarehouse;

CREATE TABLE Orders (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  OrderDate DATETIME NOT NULL,
  Amount DECIMAL(18,2) NOT NULL
);

STEP 5> Configure appsettings.json
nano appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<RDS-ENDPOINT>;Port=3306;Database=DataWarehouse;User=dbuser;Password=StrongPassword;"
  }
}
STEP 6> Run the Application
dotnet restore
dotnet build
dotnet run --urls "http://0.0.0.0:5000"
STEP 6> edit sg and allow all trafic


