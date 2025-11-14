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
        // socket = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        // {
        //     Path = "/sys25d"
        // });
        //
        // socket.OnConnected += (sender, e) =>
        // {
        //     Console.WriteLine("Connected to the server.");
        // };
        //
        // socket.OnDisconnected += (sender, e) =>
        // {
        //     Console.WriteLine("Disconnected from the server.");
        // };
        //
        // socket.On("typing", response =>
        // {
        //     try
        //     {
        //         string userName = response.GetValue<Message>().User;
        //         Console.WriteLine($"\n{userName} is typing...");
        //         Console.Write("Enter your message or type (quit): ");
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // });
        //
        // // socket.On("stopTyping", response =>
        // // {
        // //     try
        // //     {
        // //         string userName = response.GetValue<string>();
        // //         Console.WriteLine($"\n{userName} stopped typing.");
        // //         Console.Write("Enter your message or type (quit): ");
        // //     }
        // //     catch (Exception e)
        // //     {
        // //         Console.WriteLine($"Error parsing stopTyping event: {e.Message}");
        // //     }
        // // });
        //
        // socket.On("message", response =>
        // {
        //     try
        //     {
        //         Message message = response.GetValue<Message>();
        //         chatHistory.Add(message);
        //         // Console.WriteLine($"\n[{message.Time}] {message.User}: {message.Text}");
        //         // Console.Write("Enter your message or type (quit): ");
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine($"Error parsing message: {e.Message}");
        //         throw;
        //     }
        // });
        //
        // try
        // {
        //     await socket.ConnectAsync();
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine($"Connection failed: {e.Message}");
        //     throw;
        // }
        //
        // int attempts = 0;
        // while (!socket.Connected && attempts++ < 50)
        // {
        //     await Task.Delay(200);
        // }
        //
        // if (!socket.Connected)
        // {
        //     Console.WriteLine("Failed to establish connection.");
        //     return;
        // }
        //
     
        SocketClientManager socketClient = new SocketClientManager();
        Messaging messaging = new Messaging();
        
        await socketClient.Initialize(URL, PATH);
        
        string userName = messaging.SetUserName();
        
        socketClient.TerminalEventClosure(userName);
        
        await socketClient.JoinChat(userName);
        
        await messaging.ChatUpdate(userName, socketClient);

        await socketClient.ExitChat(userName);
    }
}