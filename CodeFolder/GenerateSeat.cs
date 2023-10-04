public class GenerateSeat 
{
    // public GenerateSeat(char letter, int row, bool booked ) 
    // {}

    public void FullPlane()
    {

        List<Seat> SeatsList = Seat.Seats;
        Seat seat1 = new Seat('A', 1, true);
        Seat seat2 = new Seat('A', 2, false);
        Seat seat3 = new Seat('A', 3, true );

        // Seat.Seats.Add(seat1);
        // Seat.Seats.Add(seat2);
        // Seat.Seats.Add(seat3);

        foreach (Seat Seat in Seat.Seats)
        {
            Console.WriteLine(Seat.ShowSeat());
        }
    }
    
}