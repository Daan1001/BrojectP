using System;

public class Airplane
{
    public void Boeing737(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F',33);
        displaySeating.Start(CurrentFlight);
        
    }
    public void Airbus330(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F',50);
        displaySeating.Start(CurrentFlight);
    } 
    public void Boeing787(Flight CurrentFlight)
    {
        DisplaySeating displaySeating = new('F',38);
        displaySeating.Start(CurrentFlight);
    }

}