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
        Flight SelectedFlight = AddingFlights.FindFlight(selectedOption.Substring(1, 6));
        if (SelectedFlight.AirplaneType == "Boeing 737"){
            Airplane airplane = new();
            airplane.Boeing737(SelectedFlight);
           
        }
        else if (SelectedFlight.AirplaneType == "Airbus 330"){
            Airplane airplane = new();
            airplane.Airbus330(SelectedFlight);
        }
        else if (SelectedFlight.AirplaneType == "Boeing 787"){
            Airplane airplane = new();
            airplane.Boeing787(SelectedFlight);
        }
    }
    public static string RemoveWhitespace(string input){
        return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
}

