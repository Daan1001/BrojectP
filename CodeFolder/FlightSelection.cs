using System.Runtime.InteropServices;

public class FlightSelection{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static string Selection(string selectedOption){
        Console.Clear();
        Flight SelectedFlight = flights[1];
        foreach (Flight flight in flights){
            if (selectedOption.Substring(1, 6) == flight.FlightId){
                SelectedFlight = flight;
                OptionSelection<string>.selectedFlight2 = SelectedFlight;
            }
        }
        if (SelectedFlight.AirplaneType == "Boeing 737"){
            return "Boeing 737";    
        }
        else if (SelectedFlight.AirplaneType == "Airbus 330"){
            return "Airbus 330";
        }
        else if (SelectedFlight.AirplaneType == "Boeing 787"){
            return "Boeing 787";
        }
        return null!;
    }
    public static string RemoveWhitespace(string input){
        return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
}