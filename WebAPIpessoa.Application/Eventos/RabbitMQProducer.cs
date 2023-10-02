using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIpessoa.Application.Eventos
{    
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly string _server;
        private readonly string _userName;
        private readonly string _password;
        public RabbitMQProducer(IConfiguration configuration)
        {
            _server = configuration.GetValue<string>("Rabbit:Server");
            _userName = configuration.GetValue<string>("Rabbit:UserName");
            _password = configuration.GetValue<string>("Rabbit:Password");
        }
        public void EnviarMensagem<T>(T message, string queue, string exchange, string routingKey)
        {
            var factory = new ConnectionFactory 
            {
                HostName= _server,
                UserName = _userName,
                Password = _password
            };
            var con = factory.CreateConnection();
            using var channel = con.CreateModel();
            
            channel.QueueDeclare(queue, true, false, true);
            channel.ExchangeDeclare(exchange, "topic", true, true);
            channel.QueueBind(queue, exchange, routingKey);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: body);
        }
    }
}
