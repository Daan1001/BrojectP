public class Seat
{
    public static List<Seat> Seats = new List<Seat>();

    public char Letter;
    public  int Row;

    public  bool Booked;


    public Seat(char letter, int row, bool booked)
    {
        this.Letter = letter;
        this.Row = row;
        this.Booked = booked;
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

}