namespace Chat.Scripts;
using SocketIOClient;

public class SocketClientManager
{
    private SocketIO socketClient;
    
    public void Initialize(string serverUrl, string path)
    {
        socketClient = new SocketIO(serverUrl, new SocketIOOptions
        {
            Path = path
        });
        
        OnConnection();
        
        ConnectingAsync();
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
    }
    
    private async void ConnectingAsync()
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

    public void OnEvent(string eventName)
    {
        socketClient.On(eventName, response =>
        {
            try
            {
                var message = response.GetValue<Message>();
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error parsing message: {e.Message}");
                throw;
            }
        });
    }
    
    public async Task SendMessage(string message)
    {
        await socketClient.EmitAsync("message", message);
    }

    public async Task JoinChat(string userName)
    {
        string joinMessage = $"{userName} has joined the chat.";
        await socketClient.EmitAsync("message", joinMessage);
    }
    
    public async Task ExitChat(string userName)
    {
        string exitMessage = $"{userName} has left the chat.";
        await socketClient.EmitAsync("message", exitMessage);
    }
}