using Newtonsoft.Json;
public class EditingFlights{
    // code that allows the admin to change flights.
    public static List<string[]> airports = AddingFlights.airports;
    public static List<string> airportstring = new List<string>();
    public static Flight? SelectedFlight;
    public static void EditDestination(Flight selectedFlight){
        // Use LINQ to write the airports into the desired format
        airportstring.Clear();
        airportstring = airports.Select(airport => $"{airport[0]}, {airport[1]}").ToList();
        Console.WriteLine($"Current destination: {selectedFlight.Destination}, {selectedFlight.Country}");
        Console.WriteLine("Choose the new destination (City, Country)(press any key to continue...)");
        Console.ReadKey();
        SelectedFlight = selectedFlight;
        OptionSelection<string>.Start(airportstring);
    }
    public static void EditDestination2(string selectedOption){
        string[] data = selectedOption.Split(',');
        foreach (string[] location in airports){
            if (location[0] == data[0].Trim() && location[1] == data[1].Trim()){
                SelectedFlight!.Destination = data[0].Trim();
                SelectedFlight.Country = data[1].Trim();
                double time = Convert.ToDouble(location[2]);
                DateTime depart = Convert.ToDateTime(SelectedFlight.DepartureTime);
                DateTime arrival = depart.AddHours(time);
                string arrivalstring = arrival.ToString("HH:mm");
                SelectedFlight.ArrivalTime = arrivalstring;
                Console.WriteLine($"Changed destination to {SelectedFlight.Destination}, {SelectedFlight.Country}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                AddingFlights.EditFlight(SelectedFlight);
            }
        }
    }
    public static void EditGate(Flight selectedFlight){
        Console.WriteLine("Current gate: " + selectedFlight.Terminal);
        Console.WriteLine("Enter the new gate");
        string gate = Console.ReadLine()!;
        if (gate != null){
            if (gate.Length == 2){
                gate = "Gate " + gate;
            }
            else if (gate.Length == 1){
                gate = "Gate 0" + gate;
            }
            selectedFlight.Terminal = gate;
            AddingFlights.EditFlight(selectedFlight);
        }
        else{
            Console.WriteLine("Invalid input, press any key to try again");
            Console.ReadKey();
            AddingFlights.EditFlight(selectedFlight);
        }  
    }
    public static void EditTypeAirplane(Flight selectedFlight){
        Console.WriteLine("Current type airplane: " + selectedFlight.AirplaneType);
        Console.Write("Enter the new type Airplane (Boeing 787, Airbus 330, Boeing 737): ");
        string airplaneType = Console.ReadLine()!;
        if (airplaneType != null){
            if (airplaneType.ToLower() == "boeing 787"){
                selectedFlight.AirplaneType = "Boeing 787";
            }
            else if(airplaneType.ToLower() == "boeing 737"){
                selectedFlight.AirplaneType = "Boeing 737";
            }
            else if (airplaneType.ToLower() == "airbus 330"){
                selectedFlight.AirplaneType = "Airbus 330";    
            }
            else{
                Console.WriteLine("Invalid input, press any key to try again");
                Console.ReadKey();
                AddingFlights.EditFlight(selectedFlight);
            }
            int seats = AddingFlights.GetTotalSeats(selectedFlight.AirplaneType!);
            string seats2 = Convert.ToString(seats);
            string filePath = $"DataSources/{selectedFlight.FlightId}.json";
            List<Seat> bookedSeats = new List<Seat>();
            if (File.Exists(filePath)){
                string json = File.ReadAllText(filePath);
                // updates current list of seat or starts a new empty list
                bookedSeats = JsonConvert.DeserializeObject<List<Seat>>(json) ?? new List<Seat>();
            }
            string seats3 = Convert.ToString(seats - bookedSeats.Count());
            selectedFlight.TotalSeats = seats2;
            selectedFlight.SeatsAvailable = seats3;
            AddingFlights.EditFlight(selectedFlight);
        }
        else{
            Console.WriteLine("Invalid input, press any key to try again");
            Console.ReadKey();
            AddingFlights.EditFlight(selectedFlight);
        }
    }
    public static void EditDate(Flight selectedFlight){
        Console.WriteLine("Current flight date: " + selectedFlight.FlightDate);
        Console.Write("enter new flight date (dd-MM-yyyy): ");
        DateTime flightDate;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out flightDate)){
            Console.WriteLine("Invalid date format. Please enter the date in dd-MM-yyyy format.");
        }
        string flightDatestring = flightDate.ToString("dd-MM-yyyy");
        selectedFlight.FlightDate = flightDatestring;
        AddingFlights.EditFlight(selectedFlight);
    }
    public static void EditTime(Flight selectedFlight){
        Console.WriteLine("Current departure time: " + selectedFlight.DepartureTime);
        Console.Write("enter new departure time (HH:mm): ");
        DateTime departureTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, System.Globalization.DateTimeStyles.None, out departureTime)){
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm format.");
        }
        string departureTimestring = departureTime.ToString("HH:mm");
        selectedFlight.DepartureTime = departureTimestring;
        foreach (string[] location in airports){
                if (location[0] == selectedFlight.Destination && location[1] == selectedFlight.Country){
                    double time = Convert.ToDouble(location[2]);
                    DateTime depart = Convert.ToDateTime(selectedFlight.DepartureTime);
                    DateTime arrival = depart.AddHours(time);
                    string arrivalstring = arrival.ToString("HH:mm");
                    selectedFlight.ArrivalTime = arrivalstring;
                    break;
                }
            }
        AddingFlights.EditFlight(selectedFlight);
    }
    public static void EditPrice(Flight selectedFlight){
        Console.WriteLine($"Current price: {selectedFlight.BasePrice}");
        Console.Write("Base Price (in Euro): ");
        string baseprice = Console.ReadLine()!;
        if (baseprice != null){
            baseprice = "€" + baseprice;
            selectedFlight.BasePrice = baseprice;
            AddingFlights.EditFlight(selectedFlight);
        }
        else{
            Console.WriteLine("Invalid input, press any key to try again");
            Console.ReadKey();
            AddingFlights.EditFlight(selectedFlight);
        }  
    }
}