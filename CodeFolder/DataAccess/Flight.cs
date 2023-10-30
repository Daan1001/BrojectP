using Newtonsoft.Json;

// Flight class that converts JSON file to list
public class Flight
{
    [JsonProperty("Flight id")]
    public string FlightId { get; set; }
    
    [JsonProperty("Type Airplane")]
    public string AirplaneType { get; set; }

    [JsonProperty("Terminal")]
    public string Terminal { get; set; }
    
    [JsonProperty("Country")]
    public string Country { get; set; }
    
    [JsonProperty("Destination")]
    public string Destination { get; set; }
    
    [JsonProperty("Flight date")]
    public string FlightDate { get; set; }
    
    [JsonProperty("Departure time")]
    public string DepartureTime { get; set; }
    
    [JsonProperty("Arrival time")]
    public string ArrivalTime { get; set; }
    
    [JsonProperty("Seats Available")]
    public string SeatsAvailable { get; set; }
    
    [JsonProperty("Total Seats")]
    public string TotalSeats { get; set; }
    
    [JsonProperty("Base Price")]
    public string BasePrice { get; set; }
}
