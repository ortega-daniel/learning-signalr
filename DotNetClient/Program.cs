using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Please specify the URL of the SignalR Hub");
var url = Console.ReadLine();

var hubConnection = new HubConnectionBuilder().WithUrl(url).Build();

hubConnection.On<string>("ReceiveMessage", message => Console.WriteLine($"SignalR Hub Message: {message}"));

try
{
    await hubConnection.StartAsync();

    while (true)
    {
        var message = string.Empty;

        Console.WriteLine("Please specify the action:");
        Console.WriteLine("0 - Broadcast to all");
        Console.WriteLine("exit - Exit the program");

        var action = Console.ReadLine();

        if (action == "exit")
            break;

        Console.WriteLine("Please specify the message:");
        message = Console.ReadLine();
    
        await hubConnection.SendAsync("BroadcastMessage", message);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return;
}