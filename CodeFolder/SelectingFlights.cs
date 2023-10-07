public class SelectingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static string[] options = { "Search by country", "Search by city", "Show all flights", "<-- Go back" };
    public static void MainMenu(){
        OptionSelection.Start(SelectingFlights.options);
    }
}

