

namespace Infrastructure.Adapters.MailService;

public class MailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderFullName { get; set; }
    public string SenderEmail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public MailSettings(int port, string senderFullName, string senderEmail, string userName, string password, string serverName)
    {
        Port = port;
        SenderFullName = senderFullName;
        SenderEmail = senderEmail;
        UserName = userName;
        Password = password;
        Server = serverName;
    }
    public MailSettings()
    {
        Port = 0;
        SenderFullName = string.Empty;
        SenderEmail = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
        Server= string.Empty;
    }


}
