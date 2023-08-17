// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://jridjagp:9MwWMsFzP64dKN3aapDBrjqoQmOvaiqt@fish.rmq.cloudamqp.com/jridjagp");

//create connection
using var connection = factory.CreateConnection();

//create chanel
var channel = connection.CreateModel();

var randomQueueName = channel.QueueDeclare().QueueName;


channel.QueueBind(randomQueueName,"log-fanout","",null);

channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(randomQueueName, false, consumer);
Console.WriteLine("Logs observing");
consumer.Received += (sender, args) =>
{
    var message = Encoding.UTF8.GetString(args.Body.ToArray());
    
    Console.WriteLine("Gelen Mesaj: {0}", message);

    channel.BasicAck(args.DeliveryTag, false);
};

Console.ReadLine();