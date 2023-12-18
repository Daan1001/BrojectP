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
            Text = @$"Flight {flightCode} from {leavingFrom} to {destination} leaving at {departureTime} to arrive at {arrivalTime}. 
Booked by {username} at {DateTime.Now} 
{seatsString}."
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

    public static void SendEditNotification(string username, string emailAddress,string flightCode, string changedSeats)
    {
        var message = new MimeMessage();
        message.From.Add (new MailboxAddress ("Rotterdam Airport", "rotterdamairport.brojectP@outlook.com"));
        message.To.Add (new MailboxAddress ($"{username}", $"{emailAddress}"));
        message.Subject = $"Your reservation on flight {flightCode} has been changed.";

        message.Body = new TextPart ("plain") {
            Text = $"Your reservation on flight {flightCode} has been changed. your new seats are the following: \n{changedSeats}\n\n If do not agree with these changes, please do not hesitate to contact Customer Service."
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