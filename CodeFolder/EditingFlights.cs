public class EditingFlights{
    public static void EditDestination(Flight selectedFlight){
        Console.WriteLine($"Current destination: {selectedFlight.Destination}, {selectedFlight.Country}");
        Console.WriteLine("Enter the new destination (City, Country): ");
        string destination = Console.ReadLine()!;
        if (destination != null && destination.Contains(",")){
            string[] data = destination.Split(',');
            selectedFlight.Destination = data[0].Trim();
            selectedFlight.Country = data[1].Trim();
            AddingFlights.EditFlight(selectedFlight);
        }
        else{
            Console.WriteLine("Invalid input, press any key to try again");
            Console.ReadKey();
            AddingFlights.EditFlight(selectedFlight);
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
            selectedFlight.TotalSeats = seats2;
            selectedFlight.SeatsAvailable = seats2;
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
        Console.Write("enter new departure time (HH:mm:ss): ");
        DateTime departureTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out departureTime)){
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm:ss format.");
        }
        string departureTimestring = departureTime.ToString("HH:mm:ss");
        Console.WriteLine("Current arrival time: " + selectedFlight.ArrivalTime);
        Console.Write("enter new arrival time (HH:mm:ss): ");
        DateTime arrivalTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out arrivalTime)){
            Console.WriteLine("Invalid time format. Please enter the time in HH:mm:ss format.");
        }
        string arrivalTimestring = arrivalTime.ToString("HH:mm:ss");
        selectedFlight.DepartureTime = departureTimestring;
        selectedFlight.ArrivalTime = arrivalTimestring;
        AddingFlights.EditFlight(selectedFlight);
    }
}