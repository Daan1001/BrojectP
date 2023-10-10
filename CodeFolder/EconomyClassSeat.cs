public class EconomyClassSeat : Seat
{
    public int Price {get; private set;}
    public EconomyClassSeat(char letter, int row, bool booked, int price) : base(letter,row,booked)
    {
        this.Price = price;
        Seats.Add(this);
    }
     public override string ShowSeat()
    {
        return $"base.ShowSeat(); {this.Price}";
    }


}