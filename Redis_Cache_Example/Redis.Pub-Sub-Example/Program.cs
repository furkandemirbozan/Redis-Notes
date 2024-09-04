using StackExchange.Redis;




ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");// Connect to Redis Server

ISubscriber subscriber = connection.GetSubscriber();

while (true)
{
    Console.WriteLine("mesaj : ");
    string message = Console.ReadLine();

    await subscriber.PublishAsync("mychannel", message); // Publish message to channel
}

