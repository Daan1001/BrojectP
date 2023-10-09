public class Seat
{
    public static List<Seat> Seats = new List<Seat>();

    public char Letter { get; private set; }
    public int Row { get; private set; }
    public bool Booked { get; private set; }
    

    public Seat(char letter, int row, bool booked)
    {
        Letter = letter;
        Row = row;
        Booked = booked;

        Seats.Add(this);
    }

    public string ShowSeat()
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
        }
        else
        {
            Console.WriteLine($"Seat {this} is already booked. Please choose another seat.");
        }
    }

    public void ResetBooking()
    {
        Booked = false;
    }

    
    public void ResetSeat() // used to unselect seat to false since fields are private.
    {
        Booked = !Booked;
        Console.WriteLine($"Seat {this} {(Booked ? "booked" : "unbooked")}");
    }

}