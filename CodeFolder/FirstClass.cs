public class FirstClass : Seat
{
    public FirstClass(string typeclass,char letter, int row, bool booked, int price) : base(typeclass,letter, row, booked, price){}

    public override string ShowSeat()
    {
        return $"First Class Seat: {Letter}{Row}; Booked: {Booked}; Price: {Price} Euro";
    }
}