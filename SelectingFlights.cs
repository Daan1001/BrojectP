public class SelectingFlights{
    public static void MainMenu(){
        List<Flight> flights = ShowFlights.LoadFlightsFromJson("flights.json");
        string[] options = { "Search by country", "Search by city", "Show all flights", "Exit" };
        int selectedIndex = 0;

        Console.CursorVisible = false;

        while (true){
            Console.Clear();
            for (int i = 0; i < options.Length; i++){
                if (i == selectedIndex){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0){
                selectedIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1){
                selectedIndex++;
            }
            else if (key.Key == ConsoleKey.Enter){
                Console.Clear();
                Console.WriteLine("Selected Option: " + options[selectedIndex]);
                if (options[selectedIndex] == "Search by country"){
                    ShowFlights.SearchFlightsByCountry(flights);
                }
                else if(options[selectedIndex] == "Search by city"){
                    ShowFlights.SeachFlghtsByCity(flights);
                }
                else if (options[selectedIndex] == "Show all flights"){
                    ShowFlights.ViewAllFlights(flights);
                    Pause();
                }
                else if (options[selectedIndex] == "Exit"){
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }
    }
    public static void Pause(){
        Console.WriteLine("*Base price does not include additional costs for seats");
        string yes = Console.ReadLine();
        MainMenu();
    }
}

