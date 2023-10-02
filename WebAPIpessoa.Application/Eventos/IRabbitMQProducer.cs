using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIpessoa.Application.Eventos
{
    public interface IRabbitMQProducer 
    {
        void EnviarMensagem<T>(T message, string queue, string exchange, string routingKey);
    }
}
