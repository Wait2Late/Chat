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

// class Message
// {
//     public string Time { get; set; }
//     public string User { get; set; }
//     public string Text { get; set; }
// }

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
        
        // Console.Write("Enter your name: ");
        // var userName = Console.ReadLine();
        // if (string.IsNullOrEmpty(userName)) userName = "Anonymous";
        string userName = messaging.UserName();
        
        socketClient.TerminalEventClosure(userName);
        
        // await JoinedChat(userName);
        await socketClient.JoinChat(userName);
        
        await messaging.ChatUpdate(userName, socketClient);
        // while (socket.Connected)
        // while (SocketClientManager.socketClient.Connected)
        // {
        //     Console.Write("Enter your message or type (quit): ");
        //     var messageInput = Console.ReadLine();
        //     
        //     if (string.IsNullOrEmpty(messageInput)) continue;
        //     if (messageInput.ToLower() == "quit") break;
        //
        //     string timeOnly = DateTime.Now.ToString("HH:mm:ss");
        //     Messaging message = new Messaging
        //     {
        //         User = userName, 
        //         Text = messageInput,
        //         Time = timeOnly
        //     };
        //
        //     // chatHistory.Add(message);
        //
        //     try
        //     {
        //         string chatMessage = $"\n[{message.Time}] {message.User}: {message.Text}";
        //         // await SendMessage(chatMessage);
        //         await socketClient.SendMessage(chatMessage);
        //         // await socket.EmitAsync("message", message);
        //         
        //         Console.WriteLine("sent.");
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine($"Failed to send message: {e.Message}");
        //         throw;
        //     }
        // }

        await socketClient.ExitChat(userName);
        // await ExitChat(userName);

        //// Save chat history to a JSON file
        // string fileName = $"chat_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        // string jsonOutput = JsonSerializer.Serialize(chatHistory, new JsonSerializerOptions 
        // { 
        //     WriteIndented = true 
        // });
        //
        // await File.WriteAllTextAsync(fileName, jsonOutput);
        // Console.WriteLine($"\nChat history saved to {fileName}");
        // Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");

        // _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        // {
        //     Path = "/sys25d"
        // });

        //_client.On("test", response => Console.WriteLine("Received test event."));

        // _client.OnError += (sender, error) =>
        // {
        //     Console.WriteLine($"Error:  {error}");
        // };
        //
        // _client.On("message", response =>
        // {
        //     // Write the code that will be executed when we get an event called message.
        //     
        //     Console.WriteLine("Received message event.");
        //     try
        //     {
        //         string receivedMessage = response.GetValue<string>();
        //         Console.WriteLine($"Received: {receivedMessage}");
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine($"Error parsing message: {e.Message}");
        //         Console.WriteLine($"Raw response: {response}");
        //     }
        //     
        //     
        //     // response.GetValue<T>() will deserialize the data content to the specified type T
        //     
        //     
        // });
        //
        // _client.OnConnected += async (sender, eventArgs) => 
        // {
        //     Console.WriteLine("Connected to the server.");
        //     _isConnected = true;
        // };
        //
        // _client.OnDisconnected += async (sender, eventArgs) => 
        // {
        //     Console.WriteLine("Disconnected to the server.");
        //     _isConnected = false;
        // };
        //
        // // We will connect to a Socket.IO server
        //
        //   // Will connect to the server
        //
        // try
        // {
        //    
        //     await _client.ConnectAsync();    
        //     
        //     await Task.Delay(1000);
        //
        //     if (!_isConnected)
        //     {
        //         Console.WriteLine("Not connected to the server.");
        //     }
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine($"Connection failed: {e.Message}");
        //     throw;
        // }
        //
        // while (true)
        // {
        //     if (_client.Connected)
        //     {
        //         Console.Write("Enter your message: ");
        //         string? input = Console.ReadLine();
        //         
        //         if(string.IsNullOrEmpty(input)) continue;
        //
        //         if (input.ToLower() == "quit") break;
        //         
        //         try
        //         {
        //             await SendMessage(input);
        //             Console.WriteLine("Message sent.");
        //         }
        //         catch (Exception e)
        //         {
        //             Console.WriteLine($"Failed to send message: {e.Message}");
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine("Not connected to the server. Attempting to reconnect...");
        //         try
        //         {
        //             await _client.ConnectAsync();
        //             await Task.Delay(1000);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($"Reconnection failed: {ex.Message}");
        //             await Task.Delay(5000); // Wait before retry
        //         }
        //     }
        // }
        //
        // await _client.DisconnectAsync();
    }

    // private static async Task SendMessage(string message)
    // {
    //     // if (socket == null || !socket.Connected) return;
    //
    //     // Message messageObj = new Message()
    //     // {
    //     //     Text = message,
    //     //     User = "Shakil"
    //     // };
    //     
    //     await socket.EmitAsync("message", message);
    // }
    //
    // private static async Task JoinedChat(string userName)
    // {
    //     string joinMessage = $"{userName} has joined the chat.";
    //     await socket.EmitAsync("message", joinMessage);
    // }
    //
    // private static async Task ExitChat(string userName)
    // {
    //     string exitMessage = $"{userName} has left the chat.";
    //     await socket.EmitAsync("message", exitMessage);
    //     await socket.DisconnectAsync();
    // }
}