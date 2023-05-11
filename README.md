# .NET-RabbitMQ
RabbitMQ mailing system

## General info
Mail.Consumer, Mail.Producer are example apps built using BrokerLibrary.

## Requirements
* .NET 6
* RabbitMQ
* SMTP Server

## Configuration
* Consumer [Mail](https://github.com/anterani/.NET-RabbitMQ/blob/master/src/Mail.Consumer/Configuration/MailOptions.cs)
* Consumer, Producer [Broker](https://github.com/anterani/.NET-RabbitMQ/blob/master/src/BrokerLibrary/BrokerOptions.cs)

## Useful links
* Docker
  * [MailCatcher](https://hub.docker.com/r/sj26/mailcatcher)
  * [RabbitMQ](https://hub.docker.com/_/rabbitmq)
