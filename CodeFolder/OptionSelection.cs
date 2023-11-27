public static class OptionSelection<T>{
    private static T? selectedOption;
    private static int hoveringOption;
    private static bool ListSelected;
    private static bool ArraySelected;
    public static string? selectedFlight;
    public static Flight? selectedFlight2;
    public static Account? selectedAccount;
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
                        ActionString(selectedOption);
                    } else {
                        selectedOption = list[hoveringOption];
                        Console.WriteLine();
                        Action(selectedOption);
                    }
                    
                    break;
            }
        }
    }

    private static void ActionString(string selectedOption){
        List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
        List<Flight> matchingFlights = new List<Flight>();
        foreach(Flight destination in flights){
            if (destination.Destination == selectedOption || destination.Country == selectedOption){
                matchingFlights.Add(destination);
            }
        }
        if(matchingFlights.Count > 0){
            Console.Clear();
            ShowFlights.DisplayFlights(matchingFlights);
        }
        string sub = selectedOption.Substring(0, 1);
        if(sub == "|"){
            FlightSelection.Selection(selectedOption);
        }
        if (sub == "["){ //Admin list with flights start with [
            List<String> option2 = new List<string>();
            selectedFlight = selectedOption;
            selectedFlight2 = AddingFlights.FindFlight(selectedOption.Substring(1, 6));
            option2.Add("Edit flight");
            option2.Add("Cancel flight");
            option2.Add("<-- Go back");
            OptionSelection<String>.Start(option2);
        }
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
                    OptionSelection<String>.Start(new List<string>{"My account","All accounts", "<-- Go back"});
                } else {
                    MainMenu.currentUser.AccountInformation();
                }
                break;
            case "My account":
                MainMenu.currentUser!.AccountInformation();
                break;
            case "All accounts":
                Account.ViewAllAccount();
                break;
            case "Show flights":
                if(MainMenu.currentUser is not null){
                    if(MainMenu.currentUser.isAdmin){
                        OptionSelection<String>.Start(new List<string>{"Show flights ","Edit flights", "Add flights", "<-- Go back"});
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
                OptionSelection<String>.selectedAccount = null;
                OptionSelection<Account>.selectedAccount = null;
                MainMenu.Start();
                break;
            case "Airport contact details":
                MainMenu.AirportDetails();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            case "Book flight -->":
                ShowFlights.Column2(flights);
                break;
            // case "Book a seat":
            //     Airplane airplane = new();
            //     // airplane.Boeing737();
            //     break;
            case "Sort by ...":
                    List<String> option1 = new List<string>();
                    option1.Add("Sort by country");
                    option1.Add("Sort by city");
                    option1.Add("Sort by price");
                    option1.Add("Sort by type airplane");
                    option1.Add("Sort by date");
                    option1.Add("Sort by departure time");
                    OptionSelection<String>.Start(option1);
                    break;
                case "Sort by departure time":
                    List<Flight> SortedTimeList = flights.OrderBy(o=>o.DepartureTime).ToList();
                    ShowFlights.Column2(SortedTimeList);
                    break;
                case "Sort by date":
                    var SortedDateList = flights.OrderBy(flight =>{
                        if (DateTime.TryParse(flight.FlightDate, out DateTime flightDate)){
                            return flightDate;
                        }
                        return DateTime.MinValue;
                    }).ToList();
                    ShowFlights.Column2(SortedDateList);
                    break;
                case "Sort by country":
                    List<Flight> SortedCountryList = flights.OrderBy(o=>o.Country).ToList();
                    ShowFlights.Column2(SortedCountryList);
                    break;
                case "Sort by city":
                    List<Flight> SortedCityList = flights.OrderBy(o=>o.Destination).ToList();
                    ShowFlights.Column2(SortedCityList);
                    break;
                case "Sort by type airplane":
                    List<Flight> SortedTypePlaneList = flights.OrderBy(o=>o.AirplaneType).ToList();
                    ShowFlights.Column2(SortedTypePlaneList);
                    break;
                case "Sort by price":
                    List<Flight> SortedPriceList = flights.OrderBy(o=>o.BasePrice).ToList();
                    ShowFlights.Column2(SortedPriceList);
                    break;
            case "Exit":
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break;
            case "Delete account(!)":
                selectedAccount!.DeleteFromJson();
                Console.WriteLine("Account deleted");
                Console.ReadKey();
                Account.ViewAllAccount();
                break; 
            case "Reset password":
                selectedAccount!.ChangePassword();
                Account.ViewAllAccount();
                break; 
            // default:
            //     Console.WriteLine("Still a W.I.P. (press any key to continue)");
            //     Console.ReadKey(); // alleen tijdens wip nodig
            //     break;
        }
    }

    public static void Action(T selectedOption){
        if(selectedOption is String){
            if(selectedAccount is not null){
                String[] Strings = (selectedOption as String)!.Split(":");
                switch (Strings[0]){ 
                case "Username": //saving changes to flights
                    selectedAccount.changeUsername();
                    selectedAccount.AccountInformation();
                    Console.ReadKey();
                    break;
                case "Is Admin": //editing prices for flights
                    selectedAccount.switchAdminBoolean();
                    selectedAccount.AccountInformation();
                    Console.ReadKey();
                    break;
                case "Is Super Admin": //editing type airplane for flights
                    selectedAccount.switchSuperAdminBoolean();
                    selectedAccount.AccountInformation();
                    Console.ReadKey();
                    break;
                }
            }
            ActionString((selectedOption as String)!);
        } else if(selectedOption is Account){
            JsonFile<Account>.Read("DataSources/Accounts.json");
            for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){
                if(JsonFile<Account>.listOfObjects![i] == (selectedOption as Account)!){
                    selectedAccount = selectedOption as Account;
                    (selectedOption as Account)!.AccountInformation();
                }
            }
        }
    }
}