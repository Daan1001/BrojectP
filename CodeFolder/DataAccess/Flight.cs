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

    public string ToString(List<Flight> flights){
        int LenCountry = 0;
        int LenDes = 0;
        foreach (Flight flight in flights){
            if (flight.Country != null && flight.Country.Length > LenCountry){
                LenCountry = flight.Country.Length;
            }
            if (flight.Destination != null && flight.Destination.Length > LenDes){
                LenDes = flight.Destination.Length;
            }
        }
        string FlightID = $"{this.SeatsAvailable}/{this.TotalSeats}";
        string paddedDestination = this.Destination != null ? this.Destination.PadRight(LenDes) : string.Empty;
        string paddedCountry = this.Country != null ? this.Country.PadRight(LenCountry) : string.Empty;
        string data = $"{this.FlightId, -6} | {this.Terminal, -7} | {paddedDestination} | {paddedCountry} | {this.FlightDate, -10} | {this.DepartureTime, -5} | {this.ArrivalTime, -5} | {this.AirplaneType, -10} |{FlightID, -7} | {this.BasePrice, -3:C} ";
        return data;
    }
    public override string ToString(){
        string FlightID = $"{this.SeatsAvailable}/{this.TotalSeats}";
        string paddedDestination = this.Destination != null ? this.Destination.PadRight(this.Destination.Length) : string.Empty;
        string paddedCountry = this.Country != null ? this.Country.PadRight(this.Country.Length) : string.Empty;
        string data = $"{this.FlightId, -6} | {this.Terminal, -7} | {paddedDestination} | {paddedCountry} | {this.FlightDate, -10} | {this.DepartureTime, -5} | {this.ArrivalTime, -5} | {this.AirplaneType, -10} |{FlightID, -7} | {this.BasePrice, -3:C} ";
        return data;
    }

    public bool Equals(Flight? other)
    {
        if(other is null){
            return false;
        } else if(this.FlightId == other?.FlightId && this.AirplaneType == other?.AirplaneType && this.Terminal == other?.Terminal &&  this.Country == other?.Country && this.Destination == other?.Destination && this.FlightDate == other?.FlightDate && this.DepartureTime == other?.DepartureTime && this.ArrivalTime == other?.ArrivalTime && this.SeatsAvailable == other?.SeatsAvailable && this.TotalSeats == other?.TotalSeats && this.BasePrice == other?.BasePrice){
            return true;
        } else {
            return false;
        }
    }
    public static bool operator ==(Flight one, Flight two){
        if(one is null || two is null){
            if (one is null){
                return two is null;
            } else{
                return false;
            }
        } else {
            return one.Equals(two);
        }
    }
    public static bool operator !=(Flight one, Flight two){
       return !(one == two);
    }
}
