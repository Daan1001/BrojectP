using System.Runtime.InteropServices;

public class FlightSelection{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Selection(string selectedOption){
        Console.Clear();
        string clean = RemoveWhitespace(selectedOption);
        string clean2 = "|";
        string[] stringarray = clean.Split("|");
        for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
            clean2 += " " + stringarray[i] + " |";
        }
        Console.WriteLine("Booking flight for:");
        Console.WriteLine(clean2);
        Console.ReadKey();
        Flight SelectedFlight = flights[1];
        foreach (Flight flight in flights){
            if (selectedOption.Substring(1, 6) == flight.FlightId){
                SelectedFlight = flight;
            }
        }
        if (SelectedFlight.AirplaneType == "Boeing 737"){
            
            Boeing737 boeing737 = new('F', 33);
            boeing737.Start(SelectedFlight);
           
        }
        else if (SelectedFlight.AirplaneType == "Airbus 330"){
            // Airplane airplane = new();
            Airbus330 airbus330 = new('I',44);
            airbus330.Start(SelectedFlight);
        }
        else if (SelectedFlight.AirplaneType == "Boeing 787"){
            Boeing787 boeing787 = new('I',28);
            boeing787.Start(SelectedFlight);
        }
    }
    public static string RemoveWhitespace(string input){
        return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
}