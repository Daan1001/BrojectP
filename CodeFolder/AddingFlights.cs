using CodeFolder;
using Newtonsoft.Json;
public class AddingFlights{
    private static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static List<string[]> airports = new List<string[]> //list of available flights
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
            new string[] { "Berlin", "Germany", "1,5" },
            new string[] { "Brussels", "Belgium", "1" },
            new string[] { "Stockholm", "Sweden", "2,5" }
        };
    public static List<string> airportstring = new List<string>();
    public static void AddFlight(){
        airportstring = airports.Select(airport =>{
            string airporttime = Convert.ToString(airport[2]);
            return $"{airport[0]}, {airport[1]}, {airporttime}";
        }).ToList();
        OptionSelection<string>.Start(airportstring);
    }

    public static void AddFlight2(string selectedoption){    
        Console.WriteLine("Type \"Cancel\" or \"Quit\" anytime when asked for input to cancel.");    
        Console.CursorVisible = true;
        // Code that allows the admin to add flights
        Console.WriteLine("Enter the following details for the new flight:");
        Console.Write("Flight Date (dd-MM-yyyy): ");
        DateTime flightDate;
        String input = Console.ReadLine()!;
        while (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out flightDate)){
            MainMenu.Return(input);
            Console.WriteLine("Invalid date format. Please enter the date in dd-MM-yyyy format.");
            input = Console.ReadLine()!;
        }
        Console.Write("Departure Time (HH:mm): ");
        DateTime departureTime;
        String input2 = Console.ReadLine()!;
        while (!DateTime.TryParseExact(input2, "HH:mm", null, System.Globalization.DateTimeStyles.None, out departureTime)){
            MainMenu.Return(input2);
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm format.");
            input2 = Console.ReadLine()!;
        }
        string country = "";
        string city = "";
        string arrivalstring = "";
        string Baseprice = "";
        string[] data = selectedoption.Split(',');
        foreach (string[] location in airports){
            if (location[0] == data[0].Trim() && location[1] == data[1].Trim()){
                double time = Convert.ToDouble(location[2]);
                DateTime arrival = departureTime.AddHours(time);
                arrivalstring = arrival.ToString("HH:mm");
                country = data[0].Trim();
                city = data[1].Trim();
                double price = Convert.ToDouble(data[2].Trim());
                int Basepriceint = Convert.ToInt32(Math.Ceiling(price));
                Basepriceint = Basepriceint * 30;
                // 30 euro per uur naar boven afronden
                Baseprice = $"€{Convert.ToString(Basepriceint)}";
                break;
            }
        }
        string airplaneType = "";
        bool select = true;
        while(select){
            Console.Write("Type Airplane (Boeing 787, Airbus 330, Boeing 737): ");
            airplaneType = Console.ReadLine()!;
            MainMenu.Return(airplaneType);
            if (airplaneType.ToLower() == "boeing 787" || airplaneType.ToLower() == "boeing 737" || airplaneType.ToLower() == "airbus 330"){
                select = false;
            }
            else{
                Console.WriteLine("Invalid input try again");
            }
        }
        string gate = "";
        bool select2 = true;
        while(select2){
            Console.WriteLine("Enter the gate(1-20)");
            gate = Console.ReadLine()!;
            MainMenu.Return(gate);
            if(int.TryParse(gate, out _)){
                int gateint = Convert.ToInt32(gate);
                if (gateint < 20 && gateint > 0){
                    select2 = false;
                }
                else{
                    Console.WriteLine("Invalid input try again");
                }
            }
        }
        if (gate.Length == 2){
            gate = "Gate " + gate;
        }
        else if (gate.Length == 1){
            gate = "Gate 0" + gate;
        }
        int totalSeats = GetTotalSeats(airplaneType);
        int seatsAvailable = totalSeats;

        Console.CursorVisible = false;
        Flight newFlight = CreateFlight(GetRandomNumber(), airplaneType, gate, city, country, flightDate.ToString("dd-MM-yyyy"), departureTime.ToString("HH:mm"), arrivalstring, seatsAvailable.ToString(), totalSeats.ToString(), Baseprice);
        var existingFlights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText("DataSources/Flights.json")) ?? new List<Flight>();
        existingFlights.Add(newFlight);
        File.WriteAllText("DataSources/Flights.json", JsonConvert.SerializeObject(existingFlights, Formatting.Indented));
        Console.WriteLine("New flight added successfully!(Press any key to continue)");
        Console.ReadLine();
        Program.Main();
    }
    public static Flight CreateFlight(string id, string airplaneType, string Gate, string city, string country, string FlightDate, string DepartureTime, string ArrivalTime, string seatsAvailable, string totalSeats, string baseprice){
        var newFlight = new Flight
        {
            FlightId = id,
            AirplaneType = airplaneType,
            Terminal = Gate,
            Country = city,
            Destination = country,
            FlightDate = FlightDate,
            DepartureTime = DepartureTime,
            ArrivalTime = ArrivalTime,
            SeatsAvailable = seatsAvailable,
            TotalSeats = totalSeats,
            BasePrice = baseprice
        };
        return newFlight;
    }

    public static int GetTotalSeats(string airplaneType){//gets the total seats of the plane based on type airplane
        switch (airplaneType){
            case "Boeing 787":
                return 234;
            case "Airbus 330":
                return 378;
            case "Boeing 737":
                return 198;
            default:
                return 0;
        }
    }

    public static string GetRandomNumber(){//get random flight id
        List<Flight> flights2 = flights;
        string filePath = "DataSources/Accounts.json";
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(filePath))!;
        foreach (Account account in accounts){
            foreach (Booking booking in account.AccountBookings){
                flights2.Add(booking.BookedFlight);
            }
        }
        var chars = "0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        string flightId = result.ToString();
        foreach (Flight flight in flights2){
            if (flight.FlightId == flightId){
                GetRandomNumber();
            }
        }
        return flightId;
    }

    public static void ChooseFlights(){
        flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
        List<string> option1 = new List<string>();
        Console.WriteLine("Choose a flight to change (Press any key to continue)");
        foreach (Flight flight in flights){
            string data = $"[{flight.ToString(flights)}]";
            option1.Add(data);
        }
        Console.ReadKey();
        OptionSelection<String>.Start(option1, OptionSelection<String>.GoBack);
    }
    public static void EditFlight(Flight selectedFlight){
        Console.Clear();
        List<Flight> flights1 = new List<Flight>();
        Console.WriteLine("Editing flight for:");
        string data = selectedFlight.ToString(flights1);
        Console.WriteLine(data);
        Console.ReadKey();
        List<string> option2 = new List<string>(){"Destination","Type airplane","Gate","Time","Price","Save changes","<-- Go back"};
        OptionSelection<String>.Start(option2);
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
            case "yes":
                Console.WriteLine($"Deleting flight {FlightSelection.RemoveWhitespace(selectedOption)}...");
                Flight selectedFlight = FindFlight(selectedOption.Substring(1, 6))!;
                DeletingFlight(selectedFlight);
                break;
            case "no":
            case "n":
                Console.WriteLine("Aborting process, press any key to continue");
                ChooseFlights();
                break;
            default:
                Console.WriteLine("Wrong input try again");
                CancelFlights(selectedOption);
                break;
        }
    }

    public static Flight? FindFlight(string flightid){
        List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
        return flights.FirstOrDefault(flight => flight.FlightId == flightid);
    }

    public static void DeletingFlight(Flight selectedFlight){
        string filePath = "DataSources/Accounts.json";
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(filePath))!;
        // Remove bookings for the selected flight
        foreach (Account account in accounts){
            // send email to every account with this flight
            var bookingsToUpdate = account.AccountBookings.Where(booking => booking.BookedFlight.FlightId == selectedFlight.FlightId).ToList();
            foreach (Booking booking in bookingsToUpdate){
                List<Flight> flights2 = new List<Flight>();
                flights2.Add(selectedFlight);
                string text = $@"Your reservation of the following flight has been canceled:
{selectedFlight.ToString(flights2)}
We apologize for any inconvenience.";
                ConfirmationEmail.SendFlightEditNotification(account.username, account.email, selectedFlight, text);
            }
            account.AccountBookings.RemoveAll(booking => booking.BookedFlight.FlightId == selectedFlight.FlightId);
        }
        // Write the modified list of accounts back to the JSON file
        string updatedJson1 = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson1);
        // Remove flight from flights
        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText("DataSources/Flights.json"))!;
        flights.RemoveAll(flight => flight == selectedFlight);
        // Write the modified list of flights back to the JSON file
        string updatedJson2 = JsonConvert.SerializeObject(flights, Formatting.Indented);
        File.WriteAllText("DataSources/Flights.json", updatedJson2);
        Console.WriteLine("Flight deleted (press any key to continue...)");
        Console.ReadKey();
        MainMenu.Start();
    }

    public static void SaveChanges(Flight selectedFlight){
        string filePath = "DataSources/Accounts.json";
        string jsonContent = File.ReadAllText(filePath);
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(jsonContent)!;
        foreach (Account account in accounts){
            var bookingsToUpdate = account.AccountBookings.Where(booking => booking.BookedFlight.FlightId == selectedFlight.FlightId).ToList();
            foreach (Booking booking in bookingsToUpdate){
                // send email to every account with this flight
                List<Flight> flights2 = new List<Flight>();
                flights2.Add(selectedFlight);
                string text = $@"Your reservation on flight {selectedFlight.FlightId} has been changed. your new flight is the following:
{selectedFlight.ToString(flights2)}";
                ConfirmationEmail.SendFlightEditNotification(account.username, account.email, selectedFlight, text);
                booking.BookedFlight = selectedFlight;
            }
            account.AccountBookings.RemoveAll(bookingsToUpdate.Contains);
            account.AccountBookings.AddRange(bookingsToUpdate);
        }
        string updatedJsonAccounts = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        File.WriteAllText(filePath, updatedJsonAccounts);
        // Update flights.json using LINQ
        Flight matchingFlight = flights.FirstOrDefault(flight => flight.FlightId == selectedFlight.FlightId)!;
        if (matchingFlight != null!){
            flights.Remove(matchingFlight);
            flights.Add(selectedFlight);
            string updatedJsonFlights = JsonConvert.SerializeObject(flights, Formatting.Indented);
            File.WriteAllText("DataSources/Flights.json", updatedJsonFlights);
        }
        Console.WriteLine("Saved changes, press any key to continue");
        Console.ReadKey();
    }
}
