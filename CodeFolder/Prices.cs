using CodeFolder;

public class Prices{
    public static int Korting;
    public static double TotalpriceDouble;
    public static bool TicketPrices(Flight currentflight)
    {
        int korting = 0;
        if (MainMenu.currentUser! != null!){
            if (MainMenu.currentUser!.AccountFlights.Count() == 1){
                korting = 5;
            }
            if (MainMenu.currentUser.AccountFlights.Count() == 2){
                korting = 10;
            }
            if (MainMenu.currentUser.AccountFlights.Count() >= 3){
                korting = 15;
            }
        }
        Korting = korting;
        // Console.Clear();
        Console.WriteLine("Confirmation Screen:");
        Console.WriteLine($"Selected flight: {currentflight.AirplaneType} to {currentflight.Destination}, {currentflight.Country}");
        Console.WriteLine($"Departure time: {currentflight.FlightDate} at {currentflight.DepartureTime}");
        Console.WriteLine($"Price P.P: {currentflight.BasePrice}");
        Console.WriteLine("Selected Seats:");
        int totalprice = 0;
        string Basepricestring = currentflight.BasePrice!.Substring(1);
        int BasePriceInt = Convert.ToInt32(Basepricestring);
        int count = 1;
        foreach (var seat in Airplane.TemporarlySeat)
        {
            if(seat.Booked == true){
                Console.WriteLine($"{count}. Class: {seat.TypeClass} Seat: {seat.Letter}{seat.Row} Price: €{seat.Price}");
                count++;
                totalprice = totalprice + seat.Price + BasePriceInt;
            }   
            // gotta include the price but, have to change the Seat class constructor also the inittializedseat methode 
            // switch layout around and add total price
        }
        double percentage = (double)korting/100;
        double percentagekorting = 1.0 - percentage;
        double totalpricedouble = CalculatePrice(Convert.ToDouble(totalprice), percentagekorting);
        TotalpriceDouble = totalpricedouble;
        Console.WriteLine($"Price before discount: {totalprice}");
        Console.WriteLine($"Current discount: {korting}%");
        Console.WriteLine($"Total price: €{totalpricedouble}");
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Y){
            ConfirmationEmail.SendConfirmation($"{MainMenu.currentUser.username}", $"{MainMenu.currentUser.email}", $"{currentflight.FlightId}", $"Rotterdam", $"{currentflight.Destination}", $"{currentflight.DepartureTime}", $"{currentflight.ArrivalTime}");
            if (Airplane.TemporarlySeat.Count() > 0){
                
                if (MainMenu.currentUser! != null!){
                    MainMenu.currentUser.DeleteFromJson();
                    MainMenu.currentUser.AccountFlights.Add(currentflight);
                    JsonFile<Account>.Read("DataSources/Accounts.json");
                    JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
                }
            }
        }
        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
    }
    // public void Test(){
    //     Account account = new Account("Sander5", "Sander123!", false, false);
    //     MainMenu.currentUser = account;
    //     List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    //     account.AccountFlights.Add(flights[1]);
    //     Seat seat= new Seat("First Class", 'B', 1, true, 500);
    //     DisplaySeating.TemporarlySeat.Add(seat);
    //     Prices.TicketPrices(flights[2]);

    //     Assert.AreEqual(Prices.Korting, 5);
    // }
    public static double CalculatePrice(double totalprice, double percentagekorting){
        return totalprice * percentagekorting;
    }
}