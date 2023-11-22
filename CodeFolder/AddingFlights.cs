using Newtonsoft.Json;
public class AddingFlights{
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static List<string[]> airports = new List<string[]>
        {
            new string[] { "Istanbul", "Turkey", "3" },
            new string[] { "Madrid", "Spain", "2" },
            new string[] { "Frankfurt", "Germany", "1,5" },
            new string[] { "Barcelona", "Spain", "2" },
            new string[] { "Munich", "Germany", "1,5" },
            new string[] { "Rome", "Italy", "2" },
            new string[] { "Paris", "France", "1,5" },
            new string[] { "Mallorca", "Spain", "2" },
            new string[] { "Moscow", "Russia", "3" },
            new string[] { "Lisbon", "Portugal", "2" },
            new string[] { "Dublin", "Ireland", "1,5" },
            new string[] { "Vienna", "Austria", "2" },
            new string[] { "Manchester", "United Kingdom", "1" },
            new string[] { "London", "United Kingdom", "1" },
            new string[] { "Athens", "Greece", "3" },
            new string[] { "Zurich", "Switzerland", "1,5" },
            new string[] { "Berlin", "Germany", "1,5" }
        };
    public static void AddFlight(){
        // Code that allows the admin to add flights
        Console.WriteLine("Enter the following details for the new flight:");
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

        string country = "";
        string city = "";
        string arrivalstring = "";
        bool desselection = true;
        while (desselection){
            Console.WriteLine("Enter the new destination (City, Country): ");
            string destination = Console.ReadLine()!;
            if (destination != null && destination.Contains(",")){
                string[] data = destination.Split(',');
                foreach (string[] location in airports){
                    if (location[0] == data[0].Trim() && location[1] == data[1].Trim()){
                        double time = Convert.ToDouble(location[2]);
                        DateTime arrival = departureTime.AddHours(time);
                        arrivalstring = arrival.ToString("HH:mm:ss");
                        country = data[0].Trim();
                        city = data[1].Trim();
                        desselection = false;
                        break;
                    }
                }
            }
        }

        int totalSeats = GetTotalSeats(airplaneType);
        int seatsAvailable = totalSeats;

        Console.Write("Base Price (in Euro): ");
        string baseprice = Console.ReadLine()!;
        baseprice = "€" + baseprice;

        // Create a new Flight object
        var newFlight = new Flight
        {
            FlightId = GetRandomNumber(),
            AirplaneType = airplaneType,
            Terminal = gate,
            Country = city,
            Destination = country,
            FlightDate = flightDate.ToString("dd-MM-yyyy"),
            DepartureTime = departureTime.ToString("HH:mm:ss"),
            ArrivalTime = arrivalstring,
            SeatsAvailable = seatsAvailable.ToString(),
            TotalSeats = totalSeats.ToString(),
            BasePrice = baseprice
        };

        var existingFlights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText("DataSources/Flights.json")) ?? new List<Flight>();
        existingFlights.Add(newFlight);
        File.WriteAllText("DataSources/Flights.json", JsonConvert.SerializeObject(existingFlights, Formatting.Indented));
        Console.WriteLine("New flight added successfully!");
    }

    public static int GetTotalSeats(string airplaneType){//gets the total seats of the plane based on type airplane
        switch (airplaneType){
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

    private static string GetRandomNumber(){//get random flight id
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
        List<string> option1 = new List<string>();
        Console.WriteLine("Choose a flight to change");
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
    public static void EditFlight(Flight selectedFlight){
        Console.Clear();
        Console.WriteLine("Editing flight for:");
        string FlightID = $"{selectedFlight.SeatsAvailable}/{selectedFlight.TotalSeats}";
        string data = $"[{selectedFlight.FlightId, -6} | {selectedFlight.Terminal, -7} | {selectedFlight.Destination, -10} | {selectedFlight.Country, -10} | {selectedFlight.FlightDate, -10} | {selectedFlight.DepartureTime, -8} | {selectedFlight.ArrivalTime, -8} | {selectedFlight.AirplaneType, -10} |{FlightID, -7} | {selectedFlight.BasePrice, -3:C} ]";
        string clean = FlightSelection.RemoveWhitespace(data);
        string clean2 = "|";
        string[] stringarray = clean.Split("|");
        for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
            clean2 += " " + stringarray[i] + " |";
        }
        Console.WriteLine(clean2);
        Console.ReadKey();
        List<string> option2 = new List<string>();
        option2.Add("Destination");
        option2.Add("Type airplane");
        option2.Add("Gate");
        option2.Add("Date");
        option2.Add("Time");
        option2.Add("Price");
        option2.Add("Save changes");
        option2.Add("<-- Go back");
        OptionSelection.Start(option2);
    }
    public static void CancelFlights(string selectedOption){
        // code that allows the admin to delete flights
        Console.Clear();
        string clean = FlightSelection.RemoveWhitespace(selectedOption);
        string clean2 = "|";
        string[] stringarray = clean.Split("|");
        for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
            clean2 += " " + stringarray[i] + " |";
        }
        Console.WriteLine("Cancelling flight:");
        Console.WriteLine(clean2);
        Console.WriteLine("Are you sure?(Y/N)");
        string answer = Console.ReadLine()!;
        answer.ToLower();
        switch (answer.ToLower()){
            case "y":
                Console.WriteLine($"Deleting flight {selectedOption}...");
                Flight selectedFlight = FindFlight(selectedOption.Substring(1, 6));
                DeletingFlight(selectedFlight);
                break;
            case "n":
                break;
            default:
                Console.WriteLine("Wrong input try again");
                CancelFlights(selectedOption);
                break;
        }
    }
    public static Flight FindFlight(string flightid){ //finds the matching flight based on id
        foreach (Flight flight in flights){
            if (flightid == flight.FlightId){
                return flight;
            }
        }
        return null!;
    }
    public static void DeletingFlight(Flight selectedFlight){
        foreach(Flight flight in flights){
            if (flight == selectedFlight){
                flights.Remove(flight);
                string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
                File.WriteAllText("DataSources/Flights.json", updatedJson);
                MainMenu.Start();
            }
        }
    }
    public static void SaveChanges(Flight selectedFlight){ //saves the changes made to selectedFlight
        List<Flight> flightsCopy = new List<Flight>(flights);
        for (int i = 0; i < flightsCopy.Count; i++){
            Flight flight = flightsCopy[i];
            if (flight.FlightId == selectedFlight.FlightId){
                flights.Remove(flight);
                flights.Add(selectedFlight);
                string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
                File.WriteAllText("DataSources/Flights.json", updatedJson);
                break;
            }
        }

        Console.WriteLine("Saved changes, press any key to continue");
        Console.ReadKey();
        ChooseFlights();
    }
}
