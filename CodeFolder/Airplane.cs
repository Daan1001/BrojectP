public class Airplane
{
    public void Boeing737()
    {
        DisplaySeating displaySeating = new('F', 33);
        displaySeating.InitializeSeats(1000, 750, 500); // Call the InitializeSeats method
        displaySeating.Start();
    }

    public void Airbus330()
    {
        DisplaySeating displaySeating = new('F', 50);
        displaySeating.InitializeSeats(1200, 800, 550); // Call the InitializeSeats method
        displaySeating.Start();
    }

    public void Boeing787()
    {
        DisplaySeating displaySeating = new('F', 28);
        displaySeating.LoadBookedSeatsFromJson("DataSources/booked_seats.json"); 
        displaySeating.InitializeSeats(1100, 700, 480); // Call the InitializeSeats method
        displaySeating.Start();
    }
}
