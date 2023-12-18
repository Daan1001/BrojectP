using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace CodeFolder;

public class ConfirmationEmail
{
    public static void SendConfirmation(string username, string emailAddress, string flightCode,string leavingFrom, string destination, string departureTime, string arrivalTime, string seatsString)
    {
        var message = new MimeMessage();
        message.From.Add (new MailboxAddress ("Rotterdam Airport", "rotterdamairport.brojectP@outlook.com"));
        message.To.Add (new MailboxAddress ($"{username}", $"{emailAddress}"));
        message.Subject = $"Confirmation flight {flightCode}";

        message.Body = new TextPart ("plain") {
            Text = $"flight {flightCode} from {leavingFrom} to {destination} leaving at {departureTime} to arrive at {arrivalTime} booked by {username} at {DateTime.Now} \n{seatsString} "
        };
        
        // Set up the SMTP client for Gmail
        using (var client = new SmtpClient())
        {
            // Connect to the outlook SMTP server
            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
            //logging into outlook account
            client.Authenticate("rotterdam.airport.hr@gmail.com", "kfdq itji zevu pxex");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}