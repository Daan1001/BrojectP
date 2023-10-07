public static class OptionSelection{
    private static String? selectedOption;
    private static int hoveringOption;
    public static ConsoleKeyInfo keyInfo;
    public static void Start(string[] array){
        hoveringOption = 0;
        selectedOption = "";
        Console.CursorVisible = false;
        Boolean stop = false;
        while(!stop){
            Console.Clear();
            for (int i = 0; i < array.Length; i++){
                if (i == hoveringOption){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(array[i]);
                Console.ResetColor();
            }
            // Read key press
            keyInfo = Console.ReadKey();
            // Process arrow keys
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption = Math.Max(0, hoveringOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption = Math.Min(array.Length - 1, hoveringOption + 1);
                    break;
                case ConsoleKey.Enter:
                    selectedOption = array[hoveringOption];
                    Console.WriteLine();
                    Action(selectedOption);
                    Console.ReadKey(); // alleen tijdens wip nodig
                    break;
            }
            stop = selectedOption == "Exit";
        }
    }
    // public static void Start(List<String> list){

    // }
        
    public static void Action(String selectedOption){
        switch (selectedOption){
            case "Log in": // log in
                Console.WriteLine("Still a W.I.P.");
                break;
            case "Sign in": // sign in
                User.NewUser();
                break;
            case "Account information": // account information
                Console.WriteLine("Still a W.I.P.");
                break;
            case "Book a flight": // booking flight
                SelectingFlights.MainMenu();
                break;
            case "Leave a review": // leave a review
                Console.WriteLine("Still a W.I.P.");
                break;
            case "Search by country":
                ShowFlights.SearchFlightsByCountry(SelectingFlights.flights);
                break;
            case "Search by city":
                ShowFlights.SearchFlghtsByCity(SelectingFlights.flights);
                break;
            case "Show all flights":
                ShowFlights.ViewAllFlights(SelectingFlights.flights);
                break;
            case "<-- Go back":
                // Console.WriteLine(hoveringOption);
                // Console.WriteLine(selectedOption);
                // Console.ReadKey();
                Menu.Start();
                break;
        }
    }
}