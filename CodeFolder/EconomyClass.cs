public class EconomyClass : Seat
{
    public EconomyClass(string typeclass, char letter, int row, bool booked, int price) : base(typeclass, letter, row, booked, price){}

    public override string ShowSeat()
    {
        return $"Economy Class Seat: {Letter}{Row}; Booked: {Booked}; Price: {Price} Euro";
    }
}