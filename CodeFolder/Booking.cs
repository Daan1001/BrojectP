using Newtonsoft.Json;

public class Booking{
    [JsonProperty]
    public Flight BookedFlight;
    [JsonProperty]
    public List<Seat> BookedSeats;

    public Booking(Flight BookedFlight, List<Seat> BookedSeats){
        this.BookedFlight = BookedFlight;
        this.BookedSeats = BookedSeats;
    }

    public override string ToString(){
        List<Flight> flights = new List<Flight>();
        flights.Add(BookedFlight);
        String toReturn = "Booked Flight: "+this.BookedFlight.ToString(flights)+"\nBooked Seats: ";
        for(int i = 0; i < BookedSeats.Count(); i++){
            if(i != 0){
                toReturn += ", ";
            }
            toReturn += BookedSeats[i];
        }
        toReturn += ";\n";
        return toReturn;
    }
}