public class Program
{
    static void Main()
    {
        bool booking = true;

        while (booking)
        {
            Console.WriteLine("What would u like to do?");
            Console.WriteLine("[1] Book a Seat?");
            Console.WriteLine("[2] Return to menu");
            Console.WriteLine("[3] Exit");

            string? user = Console.ReadLine();
            if (user == "1")
            {
                Airplane airplane = new();
                airplane.Boeing737();
            } 
            else if (user == "2")
            {
                // return to menu before this. 
                booking = false;
                break;
            }
            else if (user == "3")
            {
                break;
            }

        }
          

    }

            
    
}