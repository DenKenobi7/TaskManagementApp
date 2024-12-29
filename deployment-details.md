# RabbitMQ:

## Pull RabbitMQ 3.11 image:

docker pull rabbitmq:3.11-management

## Run RabbitMQ 3.11 container:

docker run -d --name rabbitMQ -p 15672:15672 -p 5672:5672 rabbitmq:3.11-management



# MS SQL Server:

## Pull MS SQL Server container:

docker pull mcr.microsoft.com/mssql/server:2019-latest

## Run MS SQL Server:

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrongPassword1!1" -p 1533:1433 -d mcr.microsoft.com/mss
ql/server:2019-latest

## Update MS SQL database:

In Nuget Package Manager Console run command: 
update-database

# Launch project

Select "Docker" launch configuration
