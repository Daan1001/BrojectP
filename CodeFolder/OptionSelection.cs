using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Diagnostics;
public static class OptionSelection{
    private static String? selectedOption;
    private static int hoveringOption;
    public static ConsoleKeyInfo keyInfo;
    public static Boolean stop = false;
    public static List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    public static void Start(List<String> array){
        hoveringOption = 0;
        selectedOption = "";
        stop = false;
        Console.CursorVisible = false;
        while(!stop){
            Console.Clear();
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
                case "Book a flight":
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
                    Console.Clear();
                    Console.WriteLine("For help:");
                    Console.WriteLine("-Call: 112 or 911 (We advise 112 for quicker arrival of help)");
                    Console.WriteLine("-Email: INeedHelp.PleaseHelpMe@gmail.com");
                    Console.WriteLine();
                    Console.WriteLine("Some tips for a more enjoyable stay:");
                    Console.WriteLine("-Avoid the restaurants completely (they will empty your wallet before you can no)");
                    Console.WriteLine("-Keep a 2m distance from everyone (to avoid any kind of bacteria from others)");
                    Console.WriteLine("-Come prepared for your flight");
                    Console.WriteLine("-Pay for your ticket (if we find out you skipped this part, you won't make it on the flight)");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "Book flight -->":
                    Booking.StartBooking();
                    break;
                case "Book a seat":
                    Airplane airplane = new();
                    airplane.Boeing737();
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