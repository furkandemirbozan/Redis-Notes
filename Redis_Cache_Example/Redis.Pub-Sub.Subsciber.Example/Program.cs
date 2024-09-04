using StackExchange.Redis;




ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");// Connect to Redis Server

ISubscriber subscriber = connection.GetSubscriber();

await subscriber.SubscribeAsync("mychannel", (channel, message) =>
{
    Console.WriteLine("Message : " + message);
});


Console.Read();