public static class OptionSelection2<T>{
    private static T? selectedOption;
    private static int hoveringOption;
    private static bool ListSelected;
    private static bool ArraySelected;
    public static string? selectedFlight;
    public static Flight? selectedFlight2;
    private static ConsoleKeyInfo keyInfo;
    public static String[] GoBack = {"<-- Go back"};
    public static Boolean stop = false;
    // public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(List<T> list){
        Start(list, null);
    }
    public static void Start(List<T> list, String[]? array){
        ArraySelected = false;
        ListSelected = true;
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
            if(array is not null){
                for (int i = 0; i < array.Length; i++){
                    if (i+list.Count() == hoveringOption){
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(array[i]);
                    Console.ResetColor();
                }
            }
            
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption--;
                    if(hoveringOption <= -1){
                        if(array is not null){
                            if(array.Length > 0){
                                hoveringOption = list.Count() -1 + array.Length;
                            }
                        } else
                        hoveringOption = list.Count() -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption++;
                    if(array is not null){
                        if(array.Length > 0){
                            if(hoveringOption >= list.Count()+array.Length){
                                hoveringOption = 0;
                            }
                        }
                    } else {
                        if(hoveringOption >= list.Count()){
                            hoveringOption = 0;
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    if(hoveringOption >= list.Count()){
                        String selectedOption = array![hoveringOption-list.Count()];
                        Console.WriteLine();
                        Action(selectedOption);
                    } else {
                        selectedOption = list[hoveringOption];
                        Console.WriteLine();
                        Action(selectedOption);
                    }
                    
                    break;
            }
        }
    }

    private static void Action(string selectedOption){
        switch (selectedOption){
                
                case "Save changes": //saving changes to flights
                    AddingFlights.SaveChanges(selectedFlight2!);
                    break;
                case "Price": //editing prices for flights
                    EditingFlights.EditPrice(selectedFlight2!);
                    break;
                case "Type airplane": //editing type airplane for flights
                    EditingFlights.EditTypeAirplane(selectedFlight2!);
                    break;
                case "Gate": //editing gates for flights
                    EditingFlights.EditGate(selectedFlight2!);
                    break;
                case "Date": //editing dates for flights
                    EditingFlights.EditDate(selectedFlight2!);
                    break;
                case "Time": //editing time for flights
                    EditingFlights.EditTime(selectedFlight2!);
                    break;
                case "Destination": //editing destination for flights
                    EditingFlights.EditDestination(selectedFlight2!);
                    break;
                case "Edit flight": //editing flights
                    AddingFlights.EditFlight(selectedFlight2!);
                    break;
                case "Cancel flight": //canceling flights
                    AddingFlights.CancelFlights(selectedFlight!);
                    break;
                case "Add flights": //adding flights
                    AddingFlights.AddFlight();
                    break;
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
                    if(MainMenu.currentUser is not null){
                        if(MainMenu.currentUser.isAdmin){
                            OptionSelection2<String>.Start(new List<string>{"Show flights ","Edit flights", "Add flights", "<-- Go back"});
                        } else {
                        SelectingFlights.Start();
                    }
                    } else {
                        SelectingFlights.Start();
                    }
                    break;
                case "Edit flights":
                    AddingFlights.ChooseFlights();
                    break;
                case "Show flights ":
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
                    // airplane.Boeing737();
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
    }

    public static void Action(T selectedOption){
        if(selectedOption is String){
            Action((selectedOption as String)!);
        } else if(selectedOption is Account){
            JsonFile<Account>.Read("DataSources/Accounts.json");
            for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){

                // Console.ReadKey();
                if(JsonFile<Account>.listOfObjects![i] == (selectedOption as Account)!){
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
            
        // }
    }
}