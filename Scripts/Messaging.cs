namespace Chat.Scripts;

public class Messaging
{
    public string Time { get; set; }
    public string User { get; set; }
    public string Text { get; set; }

    // public Messaging(string user)
    // {
    //     User = user;
    //     Time = DateTime.Now.ToString("HH:mm:ss");
    // }

    public string SetUserName()
    {
        Console.Write("Enter your name: ");
        var userName = Console.ReadLine();
        if (string.IsNullOrEmpty(userName)) userName = "No Name";

        return User = userName;
    }
    
    public async Task ChatUpdateLoop(
        SocketClientManager socketClient, 
        TextMessaging textMessaging)
    {
        while (SocketClientManager.socketClient.Connected)
        {
            Console.Write("Enter your message or type (quit): ");
            var messageInput = Console.ReadLine();
            
            if (string.IsNullOrEmpty(messageInput)) continue;
            if (messageInput.ToLower() == "quit") break;

            // Text = messageInput;
            // Time = DateTime.Now.ToString("HH:mm:ss");

            try
            {
                // string chatMessage = $"[{Time}] {User}: {Text}";
                string chatMessage = textMessaging.FormattedMessage(messageInput);
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

    public virtual string FormattedMessage(string text)
    {
        return User;
    }
}

public class TextMessaging : Messaging
{
    public TextMessaging(string user)
    {
        User = user;
        Time = DateTime.Now.ToString("HH:mm:ss");
    }
    
    public override string FormattedMessage(string text)
    {
        return $"[{Time}] {User}: {text}";
    }
}

public class SystemMessaging : Messaging
{
    public SystemMessaging(string user)
    {
        User = user;
        Time = DateTime.Now.ToString("HH:mm:ss");
    }

    public override string FormattedMessage(string text)
    {
        // [time] System: User has left the chat
        return $"[{Time}] System: {User} {text}";
    }

}