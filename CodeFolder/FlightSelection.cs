public class FlightSelection{
    public static void Selection(string selectedOption){
        Console.Clear();
        string clean = RemoveWhitespace(selectedOption);
        string clean2 = "|";
        string[] stringarray = clean.Split("|");
        for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
            clean2 += " " + stringarray[i]+ "|";
        }
        Console.WriteLine("Booking flight for:");
        Console.WriteLine(clean2);
        Console.ReadKey();
        Booking.StartBooking();
    }
    public static string RemoveWhitespace(string input){
        return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
}

