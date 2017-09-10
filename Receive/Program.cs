using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Receive
    {
        public static void Main()
        {
            string url = "amqp://cgsbbkwy:_Pxh9bx2ZlnDaIE34O1oJuD4UjmDV2W3@rhino.rmq.cloudamqp.com/cgsbbkwy";

            var factory = new ConnectionFactory() { HostName = "rhino.rmq.cloudamqp.com" };
            factory.Uri = new System.Uri(url.Replace("amqp://", "amqps://"));
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "Hello",
                        autoAck: true,
                        consumer: consumer);
                    Console.WriteLine("Press [Enter] to exit");
                    Console.ReadLine();
                }

            }

        }
    }
}
