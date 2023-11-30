using Newtonsoft.Json;

// Flight class that converts JSON file to list
public class Flight
{
    [JsonProperty("Flight id")]
    public string? FlightId { get; set; }
    
    [JsonProperty("Type Airplane")]
    public string? AirplaneType { get; set; }

    [JsonProperty("Terminal")]
    public string? Terminal { get; set; }
    
    [JsonProperty("Country")]
    public string? Country { get; set; }
    
    [JsonProperty("Destination")]
    public string? Destination { get; set; }
    
    [JsonProperty("Flight date")]
    public string? FlightDate { get; set; }
    
    [JsonProperty("Departure time")]
    public string? DepartureTime { get; set; }
    
    [JsonProperty("Arrival time")]
    public string? ArrivalTime { get; set; }
    
    [JsonProperty("Seats Available")]
    public string? SeatsAvailable { get; set; }
    
    [JsonProperty("Total Seats")]
    public string? TotalSeats { get; set; }
    
    [JsonProperty("Base Price")]
    public string? BasePrice { get; set; }

    public override string ToString()
    {
        List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
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
        string FlightID = $"{this.SeatsAvailable}/{this.TotalSeats}";
        string paddedDestination = this.Destination!.PadRight(LenDes);
        string paddedCountry = this.Country!.PadRight(LenCountry);
        string data = $"{this.FlightId, -6} | {this.Terminal, -7} | {paddedDestination} | {paddedCountry} | {this.FlightDate, -10} | {this.DepartureTime, -8} | {this.ArrivalTime, -8} | {this.AirplaneType, -10} |{FlightID, -7} | {this.BasePrice, -3:C} ";
        return data;
    }
}
