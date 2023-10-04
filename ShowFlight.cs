using Newtonsoft.Json;
using Spectre.Console;

public class ShowFlights{
    // Loads the JSON into a list
    public static List<Flight> LoadFlightsFromJson(string jsonFilePath){
        List<Flight> flights;
        using (StreamReader file = File.OpenText(jsonFilePath)){
            JsonSerializer serializer = new JsonSerializer();
            flights = (List<Flight>)serializer.Deserialize(file, typeof(List<Flight>));
        }
        return flights;
    }

    // Searches the flights list for matching countries
    public static void SearchFlightsByCountry(List<Flight> flights){
        List<string> options = new List<string>();
        foreach (Flight flight in flights){
            if(options.Contains(flight.Country)){
                continue;
            }
            else{
                options.Add(flight.Country);
            }
        }
        options.Add("<-- Go back");
        int selectedIndex = 0;
        Console.CursorVisible = false;
        while (true){
            Console.Clear();
            for (int i = 0; i < options.Count; i++){
                if (i == selectedIndex){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0){
                selectedIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1){
                selectedIndex++;
            }
            else if (key.Key == ConsoleKey.Enter){
                Console.Clear();
                Console.WriteLine("Selected Option: " + options[selectedIndex]);
                if (options[selectedIndex] == "<-- Go back"){
                    SelectingFlights.MainMenu();
                }
                else{
                    string userCountry = options[selectedIndex];
                    var matchingFlights = flights.Where(f => f.Country.Equals(userCountry, StringComparison.OrdinalIgnoreCase)).ToList();
                    DisplayFlights(matchingFlights);
                    break;
                }
            }
        }
    }
    // Searches the flights list for matching cities
    public static void SearchFlghtsByCity(List<Flight>flights){
        List<string> options = new List<string>();
        foreach (Flight flight in flights){
            if(options.Contains(flight.Destination)){
                continue;
            }
            else{
                options.Add(flight.Destination);
            }
        }
        options.Add("<-- Go back");
        int selectedIndex = 0;
        Console.CursorVisible = false;
        while (true){
            Console.Clear();
            for (int i = 0; i < options.Count; i++){
                if (i == selectedIndex){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0){
                selectedIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1){
                selectedIndex++;
            }
            else if (key.Key == ConsoleKey.Enter){
                Console.Clear();
                Console.WriteLine("Selected Option: " + options[selectedIndex]);
                if (options[selectedIndex] == "<-- Go back"){
                    SelectingFlights.MainMenu();
                }
                else{
                    string userCity = options[selectedIndex];
                    var matchingFlights = flights.Where(f => f.Destination.Equals(userCity, StringComparison.OrdinalIgnoreCase)).ToList();
                    DisplayFlights(matchingFlights);
                    break;
                }
            }
        }
    }

    // Method that displays all flights
    public static void ViewAllFlights(List<Flight> flights){
        DisplayFlights(flights);
    }

    // Method that displays the given flights
    public static void DisplayFlights(List<Flight> flights){
        if (flights.Count > 0){
            Console.WriteLine("");
            var table = new Table();
            table.Border = TableBorder.Square;;
            table.AddColumn(new TableColumn("Flight ID").Centered());
            table.AddColumn(new TableColumn("Terminal").Centered());
            table.AddColumn(new TableColumn("Destination").Centered());
            table.AddColumn(new TableColumn("Flight Date").Centered());
            table.AddColumn(new TableColumn("Departure Time").Centered());
            table.AddColumn(new TableColumn("Arrival Time").Centered());
            table.AddColumn(new TableColumn("Seats Available").Centered());
            table.AddColumn(new TableColumn("Base Price").Centered());

            foreach (var flight in flights){
                table.AddRow(
                    flight.FlightId,
                    flight.Terminal,
                    flight.Destination + ", " + flight.Country,
                    flight.FlightDate,
                    flight.DepartureTime,
                    flight.ArrivalTime,
                    flight.SeatsAvailable,
                    flight.BasePrice
                );
            }
            AnsiConsole.Render(table);
            Console.WriteLine("*Base price does not include additional costs for seats");
        }
        else{
            Console.WriteLine("No flights to that destination found.");
        }
    }
}
