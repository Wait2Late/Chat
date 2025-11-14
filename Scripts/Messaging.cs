namespace Chat.Scripts;

public class Messaging
{
    public string Time { get; set; }
    public string User { get; set; }
    public string Text { get; set; }

    public string SetUserName()
    {
        Console.Write("Enter your name: ");
        var userName = Console.ReadLine();
        if (string.IsNullOrEmpty(userName)) userName = "No Name";

        return User = userName;
    }
    public async Task ChatUpdate(string userName, SocketClientManager socketClient)
    {
        while (SocketClientManager.socketClient.Connected)
        {
            Console.Write("Enter your message or type (quit): ");
            var messageInput = Console.ReadLine();
            
            if (string.IsNullOrEmpty(messageInput)) continue;
            if (messageInput.ToLower() == "quit") break;

            Text = messageInput;
            Time = DateTime.Now.ToString("HH:mm:ss");
            // Messaging message = new Messaging
            // {
            //     User = userName, 
            //     Text = messageInput,
            //     Time = timeOnly
            // };

            // chatHistory.Add(message);

            try
            {
                string chatMessage = $"[{Time}] {User}: {Text}";
                Console.WriteLine(chatMessage);
                await socketClient.SendMessage(chatMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to send message: {e.Message}");
                throw;
            }
        }
    }
}