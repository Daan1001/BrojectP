public class SelectingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    private static List<String> options = new List<string>(){ "Search by country", "Search by city", "Show all flights", "<-- Go back" };
    public static void Start(){
        OptionSelection.Start(SelectingFlights.options);
    }
}

