public static class OptionSelection2<T>{
    private static T? selectedOption;
    private static int hoveringOption;
    private static ConsoleKeyInfo keyInfo;
    public static Boolean stop = false;
    // public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(List<T> list){
        hoveringOption = 0;
        selectedOption = default;
        stop = false;
        Console.CursorVisible = false;
        while(!stop){
            Console.Clear();
            MainMenu.AirportName();
            for (int i = 0; i < list.Count(); i++){
                if (i == hoveringOption){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(list[i]);
                Console.ResetColor();
            }
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption--;
                    if(hoveringOption <= -1){
                        hoveringOption = list.Count() -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption++;
                    if(hoveringOption >= list.Count()){
                        hoveringOption = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = list[hoveringOption];
                    Console.WriteLine();
                    Action(selectedOption);
                    break;
            }
        }
    }
   
    public static void Action(T selectedOption){
        if(selectedOption is Account){
            // Console.WriteLine("test3");
                    // Console.ReadKey();
            JsonFile<Account>.Read("DataSources/Accounts.json");
            for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){
                // Console.WriteLine("test2");
                // Console.WriteLine("\ngeselecteerd:");
                // Console.WriteLine(selectedOption);
                // Console.WriteLine("\ngecheked voor:");
                // Console.WriteLine(JsonFile<Account>.listOfObjects![i]);
                // Boolean testing = JsonFile<Account>.listOfObjects![i].AccountInformation == (selectedOption as Account).AccountInformation;
                // Boolean testing2 = JsonFile<Account>.listOfObjects![i].Equals(selectedOption as Account);
                // Console.WriteLine(testing);
                // Console.WriteLine(testing2);

                // Console.ReadKey();
                if(JsonFile<Account>.listOfObjects![i] == (selectedOption as Account)){
                    (selectedOption as Account)!.AccountInformation();
                    // Console.WriteLine("test1");
                    // Console.ReadKey();
                }
            }
        }
        // List<Flight> matchingFlights = new List<Flight>();
        // foreach(Flight destination in flights){
        //     if (destination.Destination == selectedOption || destination.Country == selectedOption){
        //         matchingFlights.Add(destination);
        //     }
        // }
        // if(matchingFlights.Count > 0){
        //     Console.Clear();
        //     ShowFlights.DisplayFlights(matchingFlights);
        // }
        // string sub = selectedOption.Substring(0, 1);
        // if(sub == "|"){
        //     FlightSelection.Selection(selectedOption);
        // }
        // else{
            switch (selectedOption){
                case "Log in":
                    Login.LogInInput();
                    MainMenu.Start();
                    break;
                case "Sign up":
                    NewAccount.MakeInput();
                    MainMenu.Start();
                    break;
                case "Log out":
                    MainMenu.currentUser = null;
                    MainMenu.Start();
                    break;
                case "Account information":
                    if(MainMenu.currentUser!.isAdmin){
                        OptionSelection.Start(new List<string>{"My account","All accounts", "<-- Go back"});
                    } else {
                        MainMenu.currentUser.AccountInformation();
                    }
                    break;
                case "My account":
                    MainMenu.currentUser!.AccountInformation();
                    break;
                case "All accounts":
                    AdminOptions.ViewAllAccount();
                    break;
                case "Show flights":
                    SelectingFlights.Start();
                    break;
                case "Leave a review":
                    Review.CreateNewReviewInput();
                    ReviewMenu.Start();
                    break;
                case "Reviews":
                    ReviewMenu.Start();
                    break;
                case "Show reviews":
                    Review.ShowAllReviews();
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
                    MainMenu.Start();
                    break;
                case "Airport contact details":
                    MainMenu.AirportDetails();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "Book flight -->":
                    // ShowFlights.Column2(flights);
                    break;
                case "Book a seat":
                    Airplane airplane = new();
                    airplane.Boeing737();
                    break;
                case "Exit":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                // default:
                //     Console.WriteLine("Still a W.I.P. (press any key to continue)");
                //     Console.ReadKey(); // alleen tijdens wip nodig
                //     break;
            }
        // }
    }
}