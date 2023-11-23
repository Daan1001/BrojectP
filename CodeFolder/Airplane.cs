using System;

public class Airplane
{
    public void Boeing737(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F', 33);
        displaySeating.InitializeSeats(1000, 750, 500); // Call the InitializeSeats method
        displaySeating.Start(CurrentFlight);
    }

    public void Airbus330(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F', 50);
        displaySeating.InitializeSeats(1200, 800, 550); // Call the InitializeSeats method
        displaySeating.Start(CurrentFlight);
    }

    public void Boeing787(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F', 28);
        displaySeating.LoadBookedSeatsFromJson("DataSources/booked_seats.json"); 
        displaySeating.InitializeSeats(1100, 700, 480); // Call the InitializeSeats method
        displaySeating.Start(CurrentFlight);
    }
}