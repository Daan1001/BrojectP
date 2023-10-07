public class SelectingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static string[] options = { "Search by country", "Search by city", "Show all flights", "<-- Go back" };
    public static void MainMenu(){
        OptionSelection.Start(SelectingFlights.options);
            // else if (key.Key == ConsoleKey.Enter){
            //     Console.Clear();
            //     Console.WriteLine("Selected Option: " + options[selectedIndex]);
            //     if (options[selectedIndex] == "Search by country"){
                    // ShowFlights.SearchFlightsByCountry(flights);
            //     }
            //     else if(options[selectedIndex] == "Search by city"){
            //         ShowFlights.SearchFlghtsByCity(flights);
            //     }
            //     else if (options[selectedIndex] == "Show all flights"){
            //         ShowFlights.ViewAllFlights(flights);
            //         Pause();
            //     }
            //     else if (options[selectedIndex] == "<-- Go back"){
            //         Menu.Start();
            //     }
    }
}

