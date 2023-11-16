using Newtonsoft.Json;
public class AddingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void AddFlight(){
        Console.WriteLine("Enter the following details for the new flight:");
        // Automatically generate a unique ID
        string flightId = GetRandomNumber();
        
        Console.Write("Type Airplane (Boeing 787, Airbus 330, Boeing 737): ");
        string airplaneType = Console.ReadLine()!;

        Console.WriteLine("Enter the gate");
        string gate = Console.ReadLine()!;
        if (gate.Length == 2){
            gate = "Gate " + gate;
        }
        else if (gate.Length == 1){
            gate = "Gate 0" + gate;
        }

        Console.Write("Country: ");
        string country = Console.ReadLine()!;

        Console.Write("City: ");
        string city = Console.ReadLine()!;

        Console.Write("Flight Date (dd-MM-yyyy): ");
        DateTime flightDate;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out flightDate)){
            Console.WriteLine("Invalid date format. Please enter the date in dd-MM-yyyy format.");
        }

        Console.Write("Departure Time (HH:mm:ss): ");
        DateTime departureTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out departureTime)){
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm:ss format.");
        }

        Console.Write("Arrival Time (HH:mm:ss): ");
        DateTime arrivalTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out arrivalTime)){
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm:ss format.");
        }

        int totalSeats = GetTotalSeats(airplaneType);
        int seatsAvailable = totalSeats;

        Console.Write("Base Price (in Euro): ");
        string baseprice = Console.ReadLine()!;
        baseprice = "€" + baseprice;

        // Create a new Flight object
        var newFlight = new Flight
        {
            FlightId = flightId,
            AirplaneType = airplaneType,
            Terminal = gate,
            Country = country!,
            Destination = city!,
            FlightDate = flightDate.ToString("dd-MM-yyyy"),
            DepartureTime = departureTime.ToString("HH:mm:ss"),
            ArrivalTime = arrivalTime.ToString("HH:mm:ss"),
            SeatsAvailable = seatsAvailable.ToString(),
            TotalSeats = totalSeats.ToString(),
            BasePrice = baseprice
        };

        var existingFlights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText("DataSources/Flights.json")) ?? new List<Flight>();
        existingFlights.Add(newFlight);
        File.WriteAllText("DataSources/Flights.json", JsonConvert.SerializeObject(existingFlights, Formatting.Indented));
        Console.WriteLine("New flight added successfully!");
    }

    private static int GetTotalSeats(string airplaneType)
    {
        switch (airplaneType)
        {
            case "Boeing 787":
                return 189;
            case "Airbus 330":
                return 150;
            case "Boeing 737":
                return 170;
            default:
                return 0;
        }
    }

    private static string GetRandomNumber(){
        var chars = "0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        string flightId = result.ToString();
        foreach (Flight flight in flights){
            if (flight.FlightId == flightId){
                GetRandomNumber();
            }
        }
        return flightId;
    }

    public static void ChooseFlights(){
        List<String> option1 = new List<string>();
        Console.WriteLine("Choose a flight to edit");
        int LenCountry = 0;
        int LenDes = 0;
        foreach (Flight flight in flights){
            if (flight.Country!.Length > LenCountry){
                LenCountry = flight.Country.Length;
            }
            if (flight.Destination!.Length > LenDes){
                LenDes = flight.Destination.Length;
            }
        }
        foreach (Flight flight in flights){
            string FlightID = $"{flight.SeatsAvailable}/{flight.TotalSeats}";
            string paddedDestination = flight.Destination!.PadRight(LenDes);
            string paddedCountry = flight.Country!.PadRight(LenCountry);
            string data = $"[{flight.FlightId, -6} | {flight.Terminal, -7} | {paddedDestination} | {paddedCountry} | {flight.FlightDate, -10} | {flight.DepartureTime, -8} | {flight.ArrivalTime, -8} | {flight.AirplaneType, -10} |{FlightID, -7} | {flight.BasePrice, -3:C} ]";
            option1.Add(data);
        }
        Console.ReadKey();
        OptionSelection.Start(option1);
    }
    public static void EditFlight(string selectedOption){
        Console.Clear();
        string clean = FlightSelection.RemoveWhitespace(selectedOption);
        string clean2 = "|";
        string[] stringarray = clean.Split("|");
        for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
            clean2 += " " + stringarray[i] + " |";
        }
        Console.WriteLine("Editing flight for:");
        Console.WriteLine(clean2);
        Console.ReadKey();
        Flight selectedFlight = flights[1];
        foreach (Flight flight in flights){
            if (selectedOption.Substring(1, 6) == flight.FlightId){
                selectedFlight = flight;
            }
        }
        bool selection = true;
        while (selection){
            Console.WriteLine("Enter the destination (City, Country): ");
            string destination = Console.ReadLine();
            if (destination != null && destination.Contains(",")){
                string[] data = destination.Split(',');
                selectedFlight.Destination = data[0].Trim();
                selectedFlight.Country = data[1].Trim();
                selection = false;
            }
            else{
                Console.WriteLine("Invalid input, press any key to try again");
                Console.ReadKey();
            }
        }
        foreach(Flight flight in flights){
            if (flight == selectedFlight){
                flights.Remove(flight);
                flights.Add(selectedFlight);
                string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
                File.WriteAllText("DataSources/Flights.json", updatedJson);
                MainMenu.Start();
            }
        }
    }
}
