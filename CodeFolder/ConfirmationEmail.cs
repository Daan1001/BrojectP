using MimeKit;

namespace CodeFolder;
using MailKit;

public class ConfirmationEmail
{
    public static void SendConfirmation(string username, string emailAddress, string flightCode,string leavingFrom, string destination, string departureTime, string arrivalTime)
    {
        var message = new MimeMessage();
        message.From.Add (new MailboxAddress ("Rotterdam Airport", "rotterdamairport.brojectP@gmail.com"));
        message.To.Add (new MailboxAddress (username, emailAddress));
        message.Subject = $"Confirmation flight {flightCode}";

        message.Body = new TextPart ("plain") {
            Text = $"flight {flightCode} from {leavingFrom} to {destination} leaving at {departureTime} to arrive at {arrivalTime} booked by {username} at {DateTime.Now}"
        };
        Console.WriteLine(message);
    }
}