using System.Net;

public class GreetingService
{
    public MessageModel GetGreeting(string? name = "")
    {
        return new MessageModel(name);
    }
}

public class MessageModel
{
    public string message;
    public MessageModel(string? name = "")
    {
        message = "Hello";
        if (name == "" || name == null || name == " ")
        {
            message += " World";
        }
        else
        {
            message += $" {name}";
        }
        message += $" from {Dns.GetHostName()} v2";
    }
}