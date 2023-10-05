using System;

class Airplane
{
    public void Boeing737()
    {
        DisplaySeating displaySeating = new('f',33);
        displaySeating.Start();

    }
    public void Airbus330()
    {
        DisplaySeating displaySeating = new('f',50);

    } 

    public void Boeing787()
    {
        DisplaySeating displaySeating = new('f',38);
    }

}
