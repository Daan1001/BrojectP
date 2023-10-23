public class Seat
{
    public static List<Seat> Seats = new List<Seat>();

    public char Letter { get; private set; }
    public int Row { get; private set; }
    public bool Booked { get;  set; }


    public Seat(char letter, int row, bool booked)
    {
        Letter = letter;
        Row = row;
        Booked = booked;

        Seats.Add(this);
    }

    public virtual string ShowSeat()
    {
        return $"Letter: {this.Letter}; Row: {this.Row}; Booked: {this.Booked}";
    }

    public override string ToString()
    {
        return $"Letter: {Letter}, Row: {Row}";
    }
    public void Book()
    {
        if (!Booked)
        {
            Booked = true;
            Console.WriteLine($"Seat {this} booked successfully!");
            DisplaySeating.bookedSeats.Add(this);
        }
        else
        {
            Console.WriteLine($"Seat {this} is already booked. Please choose another seat.");
        }
        // Console.WriteLine("TEST");
    }

    // public void ResetBooking()
    // {
    //     Booked = false;
    // }
    public void ResetSeat() // used to unselect seat to false since fields are private.
    {
        Booked = false;
        Console.WriteLine($"Seat {this} {(Booked ? "booked" : "unbooked")}");
    }

}