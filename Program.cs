using System.Net.Sockets;
using System.Threading.Channels;

using Chat.Scripts;
using System.Text.Json;
namespace Chat;
// using SocketIOClient;

// ws (websocket) is similar as http, without encryption
// wss is similar as https, with TLS/SSL encryption activated

/*
 * Events are fired when the server sends data to the client
 * Each event has an eventName and data content
 */

class Program
{
    // private static SocketIO socket;
    // private static List<Messaging> chatHistory = new List<Messaging>();
    // private static bool isTyping = false;
    // private static CancellationTokenSource typingCancellation;
    const string URL = "wss://api.leetcode.se";
    const string PATH = "/sys25d";
    
    static async Task Main(string[] args)
    {
        SocketClientManager socketClient = new SocketClientManager();
        Messaging messaging = new Messaging();
        
        await socketClient.Initialize(URL, PATH);
        
        string userName = messaging.SetUserName();
        
        TextMessaging textMessaging = new TextMessaging(userName);
        SystemMessaging systemMessaging = new SystemMessaging(userName);
        
        await socketClient.JoinChat(userName, systemMessaging);
        
        await messaging.ChatUpdateLoop(socketClient, textMessaging);

        await socketClient.ExitChat(userName, systemMessaging);
    }
}