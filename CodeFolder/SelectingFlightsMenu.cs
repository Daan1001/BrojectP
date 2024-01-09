public class SelectingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(){
        List<string> options = new List<string>(){ "Search by country", "Search by city", "Show all flights", "<-- Go back" };
        OptionSelection<String>.Start(options);
    }
}

