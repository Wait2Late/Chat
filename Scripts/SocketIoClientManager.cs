namespace Chat.Scripts;
using SocketIOClient;

public class SocketClientManager
{
    public static SocketIO socketClient { get; private set; }
    private string EventName { get; set; }

    public async Task Initialize(string serverUrl, string path, string eventName)
    {
        socketClient = new SocketIO(serverUrl, new SocketIOOptions
        {
            Path = path
        });

        EventName = eventName;
        
        socketClient.Options.AutoUpgrade = false;
        
        OnConnection();
        
        await ConnectingAsync();
    }

    private void OnConnection()
    {
        socketClient.OnConnected += (sender, e) =>
        {
            Console.WriteLine("Connected to the server.");
        };
        
        socketClient.OnDisconnected += (sender, e) =>
        {
            Console.WriteLine("Disconnected from the server.");
        };
        
        socketClient.On(EventName, response =>
        {
            try
            {        
                var message = response.GetValue<string>();
                Console.WriteLine($"\n{message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error parsing message: {e.Message}");
                throw;
            }
        });
    }
    
    private async Task ConnectingAsync()
    {
        try
        {
            await socketClient.ConnectAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Connection failed: {e.Message}");
            throw;
        }
        
        int attempts = 0;
        while (!socketClient.Connected && attempts++ < 50)
        {
            await Task.Delay(200);
        }

        if (socketClient.Connected) return;
        
        Console.WriteLine("Failed to establish connection.");
    }

    private void TerminalEventClosure(string userName, SystemMessaging sysMessage)
    {
        Console.CancelKeyPress += async (sender, e) =>
        {
            e.Cancel = true;
            Console.WriteLine("\nShutting down...");
            await ExitChat(userName, sysMessage);
            
            Environment.Exit(0);
        };
    }

    public async Task SendMessage(string message)
    {
        await socketClient.EmitAsync(EventName, message);
    }
    
    public async Task JoinChat(string userName, SystemMessaging sysMessage)
    {
        TerminalEventClosure(userName, sysMessage);
        
        string joinedText = sysMessage.FormattedMessage("has joined the chat");
        Console.WriteLine(joinedText);
        
        await socketClient.EmitAsync(EventName, joinedText);
    }
    
    public async Task ExitChat(string userName, SystemMessaging sysMessage)
    {
        string exitText = sysMessage.FormattedMessage("has exit the chat");
        Console.WriteLine(exitText);
        
        await socketClient.EmitAsync(EventName, exitText);
    }
}