public class BusinessClass : Seat
{
    public BusinessClass(string typeclass, char letter, int row, bool booked, int price) : base(typeclass, letter, row, booked, price){}

    public override string ShowSeat()
    {
        return $"Business Class Seat: {Letter}{Row}; Booked: {Booked}; Price: {Price} Euro";
    }
}