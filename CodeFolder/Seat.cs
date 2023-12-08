public class Seat
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
        return $"Letter: {Letter}, Row: {Row}";
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

    // public void ResetBooking()
    // {
    //     Booked = false;
    // }
    public void ResetSeat(){ // used to unselect seat to false since fields are private. 
        Booked = false;
        Console.WriteLine($"Seat {this.Letter}{this.Row} {(Booked ? "booked" : "unbooked")}");
    }
}