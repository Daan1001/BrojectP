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
        String toReturn = "Booked Flight: "+this.BookedFlight+";\nBooked Seats: ";
        for(int i = 0; i < BookedSeats.Count(); i++){
            if(i != 0){
                toReturn += ", ";
            }
            toReturn += BookedSeats[i];
        }
        toReturn += ";";
        return toReturn;
    }
}