// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://jridjagp:9MwWMsFzP64dKN3aapDBrjqoQmOvaiqt@fish.rmq.cloudamqp.com/jridjagp");

//create connection
using var connection = factory.CreateConnection();

//create chanel
var channel = connection.CreateModel();

channel.ExchangeDeclare("log-fanout", durable: true, type: ExchangeType.Fanout);


Enumerable.Range(1, 250).ToList().ForEach(x =>
{
    //create message
    string message = $"log {x}";
    //convert message to byte
    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("log-fanout","",null, messageBody);

    Console.WriteLine($"Message sent successfuly {message}");
});

Console.ReadLine();