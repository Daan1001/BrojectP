using System;
using Newtonsoft.Json;

public class Booking: IEquatable<Booking>{
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
    public bool Equals(Booking? other)
    {
        if(other is null){
            return false;
        } else if(this.BookedFlight == other?.BookedFlight){
            return true;
        } else {
            return false;
        }
    }
    public static bool operator ==(Booking one, Booking two){
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
    public static bool operator !=(Booking one, Booking two){
       return !(one == two);
    }

    public static Booking? operator +(Booking one, Booking two){
        if(one is null){
            return two;
        } else if(two is null){
            return one;
        } else if(one == two){
            one.BookedSeats.AddRange(two.BookedSeats);
            return new Booking(one.BookedFlight, one.BookedSeats);
        } else {
            return null;
        }
    }
}