using MailKit.Net.Smtp;
using MimeKit;

using static System.Net.Mime.MediaTypeNames;

namespace ConsoleUI
{
internal class Program
{
    static void Main(string[] args) {
 
            var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Metin Koyuncu", "metflix@altinbasbys.com.tr"));
                mailMessage.To.Add(new MailboxAddress("Metin Koyuncu", "koyuncu.m@hotmail.com"));
                mailMessage.Subject = "subject";
            mailMessage.Body = new TextPart()
                {
                    Text = "Hello"
        };

        using (var smtpClient = new SmtpClient()){
            smtpClient.Connect("mt-odin-win.guzelhosting.com", 587,false);
            smtpClient.Authenticate("metflix@altinbasbys.com.tr", "3230875aA");
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
                mailMessage.Dispose();
}
  

        }
}
}