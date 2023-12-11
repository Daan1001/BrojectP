using Newtonsoft.Json;
public class CheckingFlights{
    public static void UpdateDates(){
        string json = File.ReadAllText("DataSources/Flights.json");
        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
        foreach (Flight flight in flights!){
            DateTime flightDate = DateTime.ParseExact(flight.FlightDate!, "dd-MM-yyyy", null);
            DateTime lessflightDate = flightDate.AddDays(-1);
            if (lessflightDate <= DateTime.Now){
                Random r = new Random();
                int rInt = r.Next(0, 5);
                flightDate = DateTime.Now.AddMonths(rInt);
                flight.FlightDate = flightDate.ToString("dd-MM-yyyy");
            }
        }
        string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
        File.WriteAllText("DataSources/Flights.json", updatedJson);
    }
}
