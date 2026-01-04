namespace Chat.Scripts;

public class Messaging
{
    public string Time { get; set; }
    public string User { get; set; }
    public string Text { get; set; }

    public Messaging() { }
    
    public Messaging(string user, string text)
    {
        User = user;
        Text = text;
    }
    public override string ToString() => $"{User}: {Text}";
    public string SetUserName()
    {
        while (true)
        {
            Console.Write("Enter your name (no spaces): ");
            var userName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("Need to be a name. Please enter again");
                continue;
            }

            if (!userName.Contains(' ')) return User = userName;

            Console.WriteLine("Names cannot contain spaces. Please try again.");
        }
    }
    
    public async Task ChatUpdateLoop(
        SocketClientManager socketClient, 
        TextMessaging textMessaging)
    {
        while (SocketClientManager.socketClient.Connected)
        {
            Console.Write("Enter your message or type (quit): ");
            var messageInput = Console.ReadLine();
            
            if (string.IsNullOrEmpty(messageInput) || messageInput.Contains(' ')) continue;
            if (messageInput.ToLower() == "quit" || messageInput.ToLower() == "q") break;

            try
            {
                string chatMessage = textMessaging.FormattedMessage(messageInput);
                Console.WriteLine(chatMessage);
             
                var messageObj = new Messaging(User, chatMessage);
                await socketClient.SendMessage(messageObj);
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
        return $"[{Time}] System: {User} {text}";
    }
}