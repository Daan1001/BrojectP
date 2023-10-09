using Newtonsoft.Json;
using Spectre.Console;

public class ShowFlights{
    // Loads the JSON into a list
    private static List<string> options = new List<string>();
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
        options.Clear();
        foreach (Flight flight in flights){
            if(!options.Contains(flight.Country)){
                options.Add(flight.Country);
            }
        }
        options.Add("<-- Go back");
        OptionSelection.Start(options);
    }
    // Searches the flights list for matching cities
    public static void SearchFlghtsByCity(List<Flight>flights){
        options.Clear();
        foreach (Flight flight in flights){
            if(!options.Contains(flight.Destination)){
                options.Add(flight.Destination);
            }
        }
        options.Add("<-- Go back");
        OptionSelection.Start(options);
    }

    // Method that displays all flights
    public static void ViewAllFlights(List<Flight> flights){
        DisplayFlights(flights);
        Console.ReadKey(); // alleen tijdens wip nodig
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
        }
        else{
            Console.WriteLine("No flights to that destination found.");
        }
        Console.ReadKey();
        List<String> option1 = new List<string>();
        option1.Add("<-- Go back");
        option1.Add("Book flight -->");
        OptionSelection.Start(option1);
    }
}
