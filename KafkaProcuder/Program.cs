
using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
};

using var procuder = new ProducerBuilder<Null, string>(config).Build();

try
{
    bool state = true;
    while (true)
    {
        Console.WriteLine("Enter the Message");
        var message = Console.ReadLine();
        if (message == null)
            state = false;
        var response = await procuder.ProduceAsync("Deneme", new Message<Null, string> { Value = JsonSerializer.Serialize(new Message() { Id = 10, Value = message }) });
        Console.WriteLine($"{message} sent to kafka");
    }
    
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

Console.ReadKey();

public class Message
{
    public int Id { get; set; }
    public string Value { get; set; }
}