// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://jridjagp:9MwWMsFzP64dKN3aapDBrjqoQmOvaiqt@fish.rmq.cloudamqp.com/jridjagp");

//create connection
using var connection = factory.CreateConnection();

//create chanel
var channel = connection.CreateModel();

//create queue
channel.QueueDeclare("hello-queue", true, false, false);

//create message
string message = "hello world";

//convert message to byte
var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);

Console.WriteLine("Message sent successfuly");
Console.ReadLine();