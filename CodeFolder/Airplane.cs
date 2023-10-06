using System;

class Airplane
{
    public void Boeing737()
    {
        DisplaySeating displaySeating = new('F',33);
        displaySeating.Start();

    }
    public void Airbus330()
    {
        DisplaySeating displaySeating = new('F',50);

    } 

    public void Boeing787()
    {
        DisplaySeating displaySeating = new('F',38);
    }

}
