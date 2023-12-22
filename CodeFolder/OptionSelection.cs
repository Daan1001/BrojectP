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
    public static void Start(List<T> list){
        Start(list, null);
    }
    public static void Start(List<T> list, String[]? array){
        CheckingFlights.OrderingFlightsInJson();
        if(MainMenu.currentUser is not null){
            Account.UpdateUser();
        }
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
                Boolean isString = list[i] is String;
                if(isString && (list[i] as String)!.ToUpper().Contains("DELETE") || isString && (list[i] as String)!.ToUpper().Contains("REMOVE") || isString && (list[i] as String)!.ToUpper().Contains("EXIT") || isString && (list[i] as String)!.ToUpper().Contains("CANCEL")){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(list[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                } else if(isString && (list[i] as String)!.ToUpper().Contains("ADD")){
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(list[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    Console.WriteLine(list[i]);
                }
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
        if(matchingFlights.Count > 0){ //checks if any flights match with the selected option if so shows those flights
            Console.Clear();
            ShowFlights.DisplayFlights(matchingFlights);
        }
        string sub = selectedOption.Substring(0, 1);
        if(sub == "|"){ //Standard list with flights start with |
            string clean = FlightSelection.RemoveWhitespace(selectedOption);
            string clean2 = "|";
            string[] stringarray = clean.Split("|");
            for(int i = 0 + 1; i < stringarray.Count() - 1; i++){
                clean2 += " " + stringarray[i] + " |";
            }
            Console.WriteLine("Booking flight for:");
            Console.WriteLine(clean2);
            string plane = FlightSelection.Selection(selectedOption);
            switch (plane){
                case "Boeing 787":
                    Airplane.boeing787.Start(selectedFlight2!);
                    break;
                case "Boeing 737":
                    Airplane.boeing737.Start(selectedFlight2!);
                    break;
                case "Airbus 330":
                    Airplane.airbus330.Start(selectedFlight2!);
                    break;
            }
        }
        if (sub == "["){ //Admin list with flights start with [
            List<String> option2 = new List<string>();
            selectedFlight = selectedOption;
            selectedFlight2 = AddingFlights.FindFlight(selectedOption.Substring(1, 6));
            option2.Add("Edit flight");
            option2.Add("Cancel flight");
            option2.Add("Change class seat prices");
            option2.Add("<-- Go back");
            OptionSelection<String>.Start(option2);
        }
        if (sub == "("){
            // if(OptionSelection<Account>.selectedAccount is null){
            //     AccountBookings.DeleteBooking(selectedOption, MainMenu.currentUser!);
            // } else {
            //     AccountBookings.DeleteBooking(selectedOption, OptionSelection<Account>.selectedAccount);
            // }
            //##########################################################################
            AccountBookings.DeleteBooking(selectedOption, SelectedOrCurrentAccount());
            //##########################################################################
        }
        if (EditingFlights.airportstring.Contains(selectedOption)){ //Sends the admin back to editingflights after choosing a location to fly to
            EditingFlights.EditDestination2(selectedOption);
        }
        if (AddingFlights.airportstring.Contains(selectedOption)){ //Sends the admin back to addingflights after choosing a location to fly to
            AddingFlights.AddFlight2(selectedOption);
        }
        switch (selectedOption){
            case "Bookings":
                ActionString("My bookings");
                break;
            case "My bookings":
                Account.UpdateUser();
                if (MainMenu.currentUser!.AccountBookings.Count() > 0 || OptionSelection<Account>.selectedAccount is not null){
                    List<string> option = new List<string>();
                    if(OptionSelection<Account>.selectedAccount is null){
                        option.Add("See my bookings");
                    } else {
                        option.Add("Book flight");
                        option.Add("See bookings");
                    }
                    option.Add("Edit booking");
                    option.Add("Cancel booking");
                    option.Add("<-- Go back");
                    OptionSelection<string>.Start(option);
                    break;
                }
                else{
                    if(OptionSelection<Account>.selectedAccount is null){
                        Console.WriteLine("This account doesnt have any bookings yet (Press any key to continue)");
                        Console.ReadKey();
                    }
                    break;
                }
            case "Book flight":
                ActionString("Book a flight ");
                break;
            case "See my bookings":
                AccountBookings.ShowBooking();
                break;
            case "Cancel booking":
                // if(OptionSelection<Account>.selectedAccount is null){
                //     AccountBookings.CancelBooking(MainMenu.currentUser!);
                // } else {
                //     AccountBookings.CancelBooking(OptionSelection<Account>.selectedAccount);
                // }
                //##########################################################################
                AccountBookings.CancelBooking(SelectedOrCurrentAccount());
                //##########################################################################
                break;
            case "Edit booking":
                AccountBookings.EditBooking();
                break;
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
            case "Change class seat prices": // change class seat price all of this kind of plane.
                if(selectedFlight2!.AirplaneType == "Boeing 737"){
                    Airplane.boeing737.SetClassPrices();
                }
                if(selectedFlight2.AirplaneType == "Boeing 787"){
                    Airplane.boeing787.SetClassPrices();
                }
                if(selectedFlight2.AirplaneType == "Airbus 330"){
                    Airplane.airbus330.SetClassPrices();
                }
                
                break;
            case "Add flights": //adding flights
                AddingFlights.AddFlight();
                break;
            case "Log in": //Logs user in with existing account
                Login.LogInInput();
                MainMenu.Start();
                break;
            case "Sign up": //Makes the user sign up with a new account
                NewAccount.MakeInput();
                MainMenu.Start();
                break;
            case "Log out": //logs user out of current account
                MainMenu.currentUser = null;
                MainMenu.Start();
                break;
            case "Account information": //Send user to its account information, admin can see all accounts
                if(MainMenu.currentUser!.isAdmin){
                    OptionSelection<String>.Start(new List<string>{"My account","All accounts", "<-- Go back"});
                } else {
                    MainMenu.currentUser.AccountInformation();
                }
                break;
            case "My account": //Shows the admin ist own account
                MainMenu.currentUser!.AccountInformation();
                break;
            case "All accounts": //shows the admin all the accounts.
                Account.ViewAllAccount();
                break;
            case "Book a flight":
                if(MainMenu.currentUser is not null){
                    if(MainMenu.currentUser.isAdmin){
                        OptionSelection<String>.Start(new List<string>{"Book a flight ","Edit flights", "Add flights","<-- Go back"});
                    } else {
                    SelectingFlights.Start();
                }
                } else {
                    SelectingFlights.Start();
                }
                break;
            case "Edit flights": //allows the admin choose a flight to edit
                AddingFlights.ChooseFlights();
                break;
            case "Book a flight ":
                SelectingFlights.Start();
                break;
            case "Leave a review": //allows user to leave a review
                Review.CreateNewReviewInput();
                ReviewMenu.Start();
                break;
            case "Reviews": // sends user to the review menu
                ReviewMenu.Start();
                break;
            case "Show reviews": //shows all reviews made by users
                Review.ShowAllReviews();
                break;
            case "Search by country": // allows the user to sort by a specific country
                ShowFlights.SearchFlightsBy(SelectingFlights.flights, "Country");
                break;
            case "Search by city": // allows the user to sort by a specific city
                ShowFlights.SearchFlightsBy(SelectingFlights.flights, "City");
                break;
            case "Show all flights": //shows all flights currently available
                ShowFlights.ViewAllFlights(SelectingFlights.flights);
                break;
            case "<-- Go back":
                MainMenu.Start();
                break;
            case "Airport contact details": // shows airport contact information
                MainMenu.AirportDetails();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            case "Book flight -->": // Not sure if this is still required
                ShowFlights.Column2(flights);
                break;
            case "Sort by ...": //gives the user options to sort the flights based on diffrent data
                    List<String> option1 = new List<string>();
                    option1.Add("Sort by country");
                    option1.Add("Sort by city");
                    option1.Add("Sort by price");
                    option1.Add("Sort by type airplane");
                    option1.Add("Sort by date");
                    option1.Add("Sort by departure time");
                    OptionSelection<String>.Start(option1);
                    break;
            case "Sort by departure time": //sort by departure time
                List<Flight> SortedTimeList = flights.OrderBy(o=>o.DepartureTime).ToList();
                ShowFlights.Column2(SortedTimeList);
                break;
            case "Sort by date": //sort by date
                var SortedDateList = flights.OrderBy(flight =>{
                    if (DateTime.TryParse(flight.FlightDate, out DateTime flightDate)){
                        return flightDate;
                    }
                    return DateTime.MinValue;
                }).ToList();
                ShowFlights.Column2(SortedDateList);
                break;
            case "Sort by country": //sort by country
                List<Flight> SortedCountryList = flights.OrderBy(o=>o.Country).ToList();
                ShowFlights.Column2(SortedCountryList);
                break;
            case "Sort by city": //sort by city
                List<Flight> SortedCityList = flights.OrderBy(o=>o.Destination).ToList();
                ShowFlights.Column2(SortedCityList);
                break;
            case "Sort by type airplane": //sort by airplane
                List<Flight> SortedTypePlaneList = flights.OrderBy(o=>o.AirplaneType).ToList();
                ShowFlights.Column2(SortedTypePlaneList);
                break;
            case "Sort by price": //sort by price
                List<Flight> SortedPriceList = flights.OrderBy(o=>o.BasePrice).ToList();
                ShowFlights.Column2(SortedPriceList);
                break;
            case "Exit": // Closes the application
                Color.Green("G", false);
                Color.Red("o", false);
                Color.Yellow("o", false);
                Color.Magenta("d", false);
                Color.Blue("b", false);
                Color.Black("y", false);
                Color.Cyan("e", false);
                Color.Red("!", false);
                Environment.Exit(0);
                break;
            case "Delete account(!)": // deletes the users account
                if(selectedAccount is not null && selectedAccount != MainMenu.currentUser!){
                    selectedAccount!.DeleteFromJson();
                    Console.WriteLine("Account deleted");
                    Console.ReadKey();
                    Account.ViewAllAccount();
                } else {
                    MainMenu.currentUser!.DeleteFromJson();
                    MainMenu.currentUser = null;
                    Console.WriteLine("Account deleted");
                    Console.ReadKey();
                    MainMenu.Start();
                }
                break; 
            case "Reset password": // allows the user to reset its password
                if(selectedAccount is not null){
                    selectedAccount!.ChangePasswordInput();
                    Account.ViewAllAccount();
                } else {
                    MainMenu.currentUser!.ChangePasswordInput();
                }
                break; 
            case "Change username":
                MainMenu.currentUser!.changeUsernameInput();
                break; 
            case "See bookings":
                AccountBookings.ShowBooking(OptionSelection<Account>.selectedAccount!);
                break;
            case "See food selection":
                List<string> option3 = new List<string>();
                option3.Add("Business class menu");
                option3.Add("Economy class menu");
                option3.Add("First class menu");
                option3.Add("<-- Go back");
                OptionSelection<String>.Start(option3);
                break;
            case "Business class menu":
                FoodMenu.PrintMenuDescriptions<BusinessMenu>();
                break;
            case "Economy class menu":
                FoodMenu.PrintMenuDescriptions<EconomyMenu>();
                break;
            case "First class menu":
                FoodMenu.PrintMenuDescriptions<FirstClassMenu>();
                break;
            default:
                Console.WriteLine("Still a W.I.P. (press any key to continue)");
                Console.ReadKey(); // alleen tijdens wip nodig
                break;
        }
    }

    public static void Action(T selectedOption){
        if(selectedOption is String){
            if(selectedAccount is not null){
                String[] Strings = (selectedOption as String)!.Split(":");
                switch (Strings[0]){ 
                case "Username": //saving changes to flights
                    selectedAccount.changeUsernameInput();
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
        } else if(selectedOption is Booking){
            if((selectedOption as Booking) is not null){
                Airplane.TemporarlySeat = (selectedOption as Booking)!.BookedSeats;
                if ((selectedOption as Booking)!.BookedFlight.AirplaneType == "Boeing 737"){
                    Boeing737 boeing737 = new('F', 33);
                    boeing737.Start((selectedOption as Booking)!.BookedFlight);
                }
                else if ((selectedOption as Booking)!.BookedFlight.AirplaneType == "Airbus 330"){
                    Airbus330 airbus330 = new('I',44);
                    airbus330.Start((selectedOption as Booking)!.BookedFlight);
                }
                else if ((selectedOption as Booking)!.BookedFlight.AirplaneType == "Boeing 787"){
                    Boeing787 boeing787 = new('I',28);
                    boeing787.Start((selectedOption as Booking)!.BookedFlight);
                }
            }
        }
    }

    public static Account SelectedOrCurrentAccount(){
        if(OptionSelection<Account>.selectedAccount is not null){
            return OptionSelection<Account>.selectedAccount;
        } else {
            return MainMenu.currentUser!;
        }
    }
}