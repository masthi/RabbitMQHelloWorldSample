using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQHelloWorld
{

    class Send
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
                    string message = "Hello Masthan";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                        routingKey: "Hello",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("[x] sent {0}", message);
                }
            }
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }

   
}
