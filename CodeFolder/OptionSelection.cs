using Newtonsoft.Json;
using System.Diagnostics;
public static class OptionSelection{
    private static String? selectedOption;
    private static int hoveringOption;
    public static ConsoleKeyInfo keyInfo;
    public static Boolean stop = false;
    public static string? selectedFlight;
    public static Flight? selectedFlight2;
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(List<String> array){
        hoveringOption = 0;
        selectedOption = "";
        stop = false;
        Console.CursorVisible = false;
        while(!stop){
            Console.Clear();
            MainMenu.AirportName();
            for (int i = 0; i < array.Count(); i++){
                if (i == hoveringOption){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(array[i]);
                Console.ResetColor();
            }
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption--;
                    if(hoveringOption <= -1){
                        hoveringOption = array.Count() -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption++;
                    if(hoveringOption >= array.Count()){
                        hoveringOption = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = array[hoveringOption];
                    Console.WriteLine();
                    Action(selectedOption);
                    break;
            }
        }
    }
    public static void Start2(List<Account> array){
        hoveringOption = 0;
        selectedOption = "";
        stop = false;
        Console.CursorVisible = false;
        while(!stop){
            Console.Clear();
            MainMenu.AirportName();
            for (int i = 0; i < array.Count(); i++){
                if (i == hoveringOption){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(array[i]);
                Console.ResetColor();
            }
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption--;
                    if(hoveringOption <= -1){
                        hoveringOption = array.Count() -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption++;
                    if(hoveringOption >= array.Count()){
                        hoveringOption = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    // selectedOption = array[hoveringOption];
                    Console.WriteLine();
                    // Action(selectedOption);
                    break;
            }
        }
    }
    // public static void Start(List<String> list){

    // }
   
    public static void Action(String selectedOption){
        flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
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
        if(sub == "|"){ //customer list with flights start with |
            FlightSelection.Selection(selectedOption);
        }
        if (sub == "["){ //Admin list with flights start with [
            List<String> option2 = new List<string>();
            selectedFlight = selectedOption;
            selectedFlight2 = AddingFlights.FindFlight(selectedOption.Substring(1, 6));
            option2.Add("Edit");
            option2.Add("Cancel");
            option2.Add("<-- Go back");
            Start(option2);
        }
        else{
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
                case "Edit": //editing flights
                    AddingFlights.EditFlight(selectedFlight2!);
                    break;
                case "Cancel": //canceling flights
                    AddingFlights.CancelFlights(selectedFlight!);
                    break;
                case "Add": //adding flights
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
                case "Airport contact details": // dit moet nog in een aparte methode maar ik weet nog niet waar ik die methode neer ga zetten
                    MainMenu.AirportDetails();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "Book flight -->":
                    ShowFlights.Column2(flights);
                    break;
                case "Sort by ...":
                    List<String> option1 = new List<string>();
                    option1.Add("Sort by country");
                    option1.Add("Sort by city");
                    option1.Add("Sort by price");
                    option1.Add("Sort by type airplane");
                    option1.Add("Sort by date");
                    option1.Add("Sort by departure time");
                    OptionSelection.Start(option1);
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
                default:
                    Console.WriteLine("Still a W.I.P. (press any key to continue)");
                    Console.ReadKey(); // alleen tijdens wip nodig
                    break;
            }
        }
        
    }
}