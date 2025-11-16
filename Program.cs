using Chat.Scripts;
namespace Chat;

class Program
{
    //TODO if I had more time, I would implement more features for VG.
    // private static SocketIO socket;
    // private static List<Messaging> chatHistory = new List<Messaging>();
    // private static bool isTyping = false;
    // private static CancellationTokenSource typingCancellation;
    const string URL = "wss://api.leetcode.se";
    const string PATH = "/sys25d";
    const string EVENT_NAME = "message";
    
    static async Task Main(string[] args)
    {
        SocketClientManager socketClient = new SocketClientManager();
        Messaging messaging = new Messaging();
        
        await socketClient.Initialize(URL, PATH, EVENT_NAME);
        
        string userName = messaging.SetUserName();
        
        TextMessaging textMessaging = new TextMessaging(userName);
        SystemMessaging systemMessaging = new SystemMessaging(userName);
        
        await socketClient.JoinChat(userName, systemMessaging);
        
        await messaging.ChatUpdateLoop(socketClient, textMessaging);

        await socketClient.ExitChat(userName, systemMessaging);
    }
}