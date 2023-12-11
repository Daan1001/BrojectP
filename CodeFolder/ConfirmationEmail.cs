using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace CodeFolder;

public class ConfirmationEmail
{
    public static void SendConfirmation(string username, string emailAddress, string flightCode,string leavingFrom, string destination, string departureTime, string arrivalTime)
    {
        var message = new MimeMessage();
        message.From.Add (new MailboxAddress ("Rotterdam Airport", "rotterdamairport.brojectP@outlook.com"));
        message.To.Add (new MailboxAddress (username, $"{emailAddress}"));
        message.Subject = $"Confirmation flight {flightCode}";

        message.Body = new TextPart ("plain") {
            Text = $"flight {flightCode} from {leavingFrom} to {destination} leaving at {departureTime} to arrive at {arrivalTime} booked by {username} at {DateTime.Now}"
        };
        
        // Set up the SMTP client for Gmail
        using (var client = new SmtpClient())
        {
            // Connect to the outlook SMTP server
            client.Connect("outlook.office365.com", 993, SecureSocketOptions.StartTls);
            //logging into outlook account
            client.Authenticate("rotterdamairport.projectb@outlook.com", "ehlbomdiprnlxffh");
            client.Send(message);
            client.Disconnect(true);
        }
    }
    
}