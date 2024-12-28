RabbitMQ:

Pull RabbitMQ 3.11 image:

docker pull rabbitmq:3.11-management

Run RabbitMQ 3.11 container:

docker run -d --name rabbitMQ -p 15672:15672 -p 5672:5672 rabbitmq:3.11-management

