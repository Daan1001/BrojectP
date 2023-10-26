using Newtonsoft.Json;
public class CheckingFlights{
    public static void UpdateDates(){
        string json = File.ReadAllText("DataSources/Flights.json");
        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
        foreach (var flight in flights!){
            DateTime flightDate = DateTime.ParseExact(flight.FlightDate, "dd-MM-yyyy", null);
            if (flightDate < DateTime.Now){
                flightDate = flightDate.AddMonths(1);
                flight.FlightDate = flightDate.ToString("dd-MM-yyyy");
            }
        }
        string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
        File.WriteAllText("DataSources/Flights.json", updatedJson);
    }
}
