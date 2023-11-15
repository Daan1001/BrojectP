using Newtonsoft.Json;
using System.Diagnostics;
public static class OptionSelection{
    private static String? selectedOption;
    private static int hoveringOption;
    public static ConsoleKeyInfo keyInfo;
    public static Boolean stop = false;
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(List<String> array){
        // MainMenu.AirportName();
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
            // Read key press
            keyInfo = Console.ReadKey();
            // Process arrow keys
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption = Math.Max(0, hoveringOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption = Math.Min(array.Count() - 1, hoveringOption + 1);
                    break;
                case ConsoleKey.Enter:
                    selectedOption = array[hoveringOption];
                    Console.WriteLine();
                    Action(selectedOption);
                    break;
            }
        }
    }
    // public static void Start(List<String> list){

    // }
   
    public static void Action(String selectedOption){
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
        if (sub == "["){
            AddingFlights.EditFlight(selectedOption);
        }
        else{
            switch (selectedOption){
                case "Log in":
                    User.LogInInput();
                    MainMenu.Start();
                    break;
                case "Sign in": // sign in
                    User.NewUserInput();
                    MainMenu.Start();

                    break;
                case "Log out":
                    MainMenu.user = null;
                    MainMenu.Start();
                    break;
                case "Account information":
                    Console.WriteLine("Still a W.I.P. (press any key to continue)");
                    Console.ReadKey(); // alleen tijdens wip nodig
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