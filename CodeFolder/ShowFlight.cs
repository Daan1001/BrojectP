using System.Diagnostics;
using Newtonsoft.Json;
using Spectre.Console;

public class ShowFlights{
    // Loads the JSON into a list
    private static List<string> options = new List<string>();
    public static List<Flight> LoadFlightsFromJson(string jsonFilePath){   
        List<Flight> flights;
        using (StreamReader file = File.OpenText(jsonFilePath)){
            JsonSerializer serializer = new JsonSerializer();
            flights = (List<Flight>)serializer.Deserialize(file, typeof(List<Flight>))!;
        }
        return flights;
    }

    // Searches the flights list for matching countries
    // public static void SearchFlightsByCountry(List<Flight> flights){ //can refactor this woth searchbycity
    //     flights = LoadFlightsFromJson("DataSources/flights.json");
    //     options.Clear();
    //     foreach (Flight flight in flights){
    //         if(!options.Contains(flight.Country!)){
    //             options.Add(flight.Country!);
    //         }
    //     }
    //     options.Add("<-- Go back");
    //     OptionSelection<String>.Start(options);
    // }

    public static void SearchFlightsBy(List<Flight> flights, string sortingType){ //can refactor this woth searchbycity
        flights = LoadFlightsFromJson("DataSources/flights.json");
        options.Clear();
        switch(sortingType){
            case "Country":
                foreach (Flight flight in flights){
                    if(!options.Contains(flight.Country!)){
                        options.Add(flight.Country!);
                    }
                }
                options.Add("<-- Go back");
                OptionSelection<String>.Start(options);
                break;
            case "City":
                foreach (Flight flight in flights){
                    if(!options.Contains(flight.Destination!)){
                        options.Add(flight.Destination!);
                    }
                }
                options.Add("<-- Go back");
                OptionSelection<String>.Start(options);
                break;
        }
    }
    // Searches the flights list for matching cities
    // public static void SearchFlghtsByCity(List<Flight>flights){
    //     flights = LoadFlightsFromJson("DataSources/flights.json");
    //     options.Clear();
    //     foreach (Flight flight in flights){
    //         if(!options.Contains(flight.Destination!)){
    //             options.Add(flight.Destination!);
    //         }
    //     }
    //     options.Add("<-- Go back");
    //     OptionSelection<String>.Start(options);
    // }

    // Method that displays all flights
    public static void ViewAllFlights(List<Flight> flights){
        flights = LoadFlightsFromJson("DataSources/flights.json");
        Console.Clear();
        DisplayFlights(flights);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(); // alleen tijdens wip nodig
    }

    // Method that displays the given flights
    public static void DisplayFlights(List<Flight> flights){
        if (flights.Count > 0){
            var table = new Table();
            table.Border = TableBorder.Square;;
            table.AddColumn(new TableColumn("Flight ID").Centered());
            table.AddColumn(new TableColumn("Gate").Centered());
            table.AddColumn(new TableColumn("Destination").Centered());
            table.AddColumn(new TableColumn("Flight Date").Centered());
            table.AddColumn(new TableColumn("Departure Time").Centered());
            table.AddColumn(new TableColumn("Arrival Time").Centered());
            table.AddColumn(new TableColumn("Type plane").Centered());
            table.AddColumn(new TableColumn("Seats Available").Centered());
            table.AddColumn(new TableColumn("Base Price").Centered());

            foreach (var flight in flights){
                string FlightID = $"{flight.SeatsAvailable}/{flight.TotalSeats}";
                table.AddRow(
                    flight.FlightId!,
                    flight.Terminal!,
                    flight.Destination + ", " + flight.Country,
                    flight.FlightDate!,
                    flight.DepartureTime!,
                    flight.ArrivalTime!,
                    flight.AirplaneType!,
                    FlightID,
                    flight.BasePrice!
                );
            }
            AnsiConsole.Write(table);
        }
        else{
            Console.WriteLine("No flights to that destination found.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Column2(flights);
    }
    public static void Column2(List<Flight> flights){
        List<String> option1 = new List<string>();
        foreach (Flight flight in flights){
            string data = $"|{flight.ToString(flights)}|";
            option1.Add(data);
        }
        if (option1.Count > 3){
            option1.Add("Sort by ...");
        }
        option1.Add("<-- Go back");
        OptionSelection<String>.Start(option1);
    }
}
