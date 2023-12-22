using Newtonsoft.Json;
public class CheckingFlights{
    public static void UpdateDates(){
        string json = File.ReadAllText("DataSources/Flights.json");
        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
        foreach (Flight flight in flights!){
            DateTime flightDate = DateTime.ParseExact(flight.FlightDate!, "dd-MM-yyyy", null);
            DateTime lessflightDate = flightDate.AddDays(-1);
            if (lessflightDate <= DateTime.Now){
                if (Convert.ToInt32(flight.SeatsAvailable) < Convert.ToInt32(flight.TotalSeats)){
                    Random r = new Random();
                    int rInt = r.Next(0, 5);
                    flightDate = DateTime.Now.AddMonths(rInt);
                    flight.FlightId = AddingFlights.GetRandomNumber();
                    flight.SeatsAvailable = flight.TotalSeats;
                    flight.FlightDate = flightDate.ToString("dd-MM-yyyy");
                }
            }
        }
        string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
        File.WriteAllText("DataSources/Flights.json", updatedJson);
        // moet accounts updaten, seats clearen, outdated flights fixen
    }
    public static void OrderingFlightsInJson(){ //sorts flights by id
        string filePath = "DataSources/Flights.json";
        string json = File.ReadAllText(filePath);
        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
        List<Flight> sortedFlights = flights.OrderBy(f => f.FlightId).ToList();
        string sortedJson = JsonConvert.SerializeObject(sortedFlights, Formatting.Indented);
        File.WriteAllText(filePath, sortedJson);
    }

    public static void UpdateAccountFlights(){ // checks if flights in accounts are outdated
        string filePath = "DataSources/Accounts.json";
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(filePath))!;
        foreach (Account account in accounts){
            foreach (Booking booking in account.AccountBookings){
                if (!booking.Outdated){
                    DateTime flightDate = DateTime.ParseExact(booking.BookedFlight.FlightDate!, "dd-MM-yyyy", null);
                    DateTime lessflightDate = flightDate.AddDays(-1);
                    if (lessflightDate <= DateTime.Now){
                        booking.Outdated = true;
                    }
                }
            }
        }
        string updatedJson1 = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson1);
    }
}
