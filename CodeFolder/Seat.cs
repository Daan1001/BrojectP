public class Seat: IEquatable<Seat>
{
    public static List<Seat> Seats = new List<Seat>();
    public string TypeClass{get; set;}
    public char Letter { get; private set; }
    public int Row { get; private set; }
    public bool Booked { get;  set; }
    public int Price {get; set;}


    public Seat(string typeclass, char letter, int row, bool booked, int price){
        this.TypeClass = typeclass;
        this.Letter = letter;
        this.Row = row;
        this.Booked = booked;
        this.Price = price;
        Seats.Add(this);
    }

    public virtual string ShowSeat(){
        return $"Seat:    {this.Letter}{this.Row}; Booked: {this.Booked}; Price: {this.Price} Euro.";
        //€
    }

    public override string ToString(){
        // return $"Letter: {Letter}, Row: {Row}";
        return $"Seat: {Letter}-{Row}";
    }
    public void Book(){
        if (!Booked){
            Booked = true;
            Console.WriteLine($" Class: {this.TypeClass}");
            Console.WriteLine($" Seat: {this.Letter}{this.Row}");
            Console.WriteLine($" Price: {this.Price} Euro");
            Console.WriteLine(" Booked successfully!");
            Airplane.bookedSeats.Add(this);
        }
        else{
            Console.WriteLine($"Seat: {this.Letter}{this.Row} is already booked. Please choose another seat.");
        }
         // Console.WriteLine("TEST");
    }

    public void ResetBooking()
    {
        Booked = false;
    }
    public void ResetSeat() // used to unselect seat to false since fields are private.
    {
        Booked = false;
        Console.WriteLine($"Seat {this.Letter}{this.Row} {(Booked ? "booked" : "unbooked")}");
    }

    public bool Equals(Seat? other)
    {
        if(other is null){
            return false;
        } else if(this.TypeClass == other?.TypeClass && this.Row == other?.Row && this.Letter == other?.Letter &&  this.Booked == other?.Booked && this.Price == other?.Price){ // && this.passwordHash == other?.passwordHash && this.isAdmin == other?.isAdmin && this.isSuperAdmin == other.isSuperAdmin
            return true;
        } else {
            return false;
        }
    }
    public static bool operator ==(Seat one, Seat two){
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
    public static bool operator !=(Seat one, Seat two){
       return !(one == two);
    }
}