using System;
using CodeFolder;

public class Prices{
    public static int Korting;
    public static double TotalpriceDouble;
    public static bool TicketPrices(Flight currentflight)
    {
        if (MainMenu.currentUser! != null!){
            Korting = CalculateDiscount(MainMenu.currentUser);
        }
        Console.WriteLine("Confirmation Screen:");
        Console.WriteLine($"Selected flight: {currentflight.AirplaneType} to {currentflight.Destination}, {currentflight.Country}");
        Console.WriteLine($"Departure time: {currentflight.FlightDate} at {currentflight.DepartureTime}");
        Console.WriteLine($"Price P.P: {currentflight.BasePrice}");
        Console.WriteLine("Selected Seats:");
        int totalprice = 0;
        string Basepricestring = currentflight.BasePrice!.Substring(1);
        int BasePriceInt = Convert.ToInt32(Basepricestring);
        int count = 1;
        string seatsstring = $@"Price P.P: {currentflight.BasePrice}.
Selected seats:";
        foreach (var seat in Airplane.TemporarlySeat){
            if(seat.Booked == true){
                string seatsstringlist = $"{count}. Class: {seat.TypeClass} Seat: {seat.Letter}{seat.Row} Price: €{seat.Price}";
                Console.WriteLine(seatsstringlist);
                count++;
                totalprice = totalprice + seat.Price + BasePriceInt;
                seatsstring = seatsstring + $@"
                {seatsstringlist}"; //adds seat to own line
            }  
        }
        double percentage = (double)Korting/100;
        double percentagekorting = 1.0 - percentage;
        double totalpricedouble = CalculatePrice(Convert.ToDouble(totalprice), percentagekorting);
        TotalpriceDouble = totalpricedouble;
        TotalpriceDouble = Math.Round(TotalpriceDouble, 2);
        seatsstring = seatsstring + $@"
        Price before discount: €{totalprice}
        Current discount: {Korting}%
        Total price: €{TotalpriceDouble}
        Have a great flight!";
        Console.WriteLine($"Price before discount: €{totalprice}");
        Console.WriteLine($"Current discount: {Korting}%");
        Console.WriteLine($"Total price: €{TotalpriceDouble}");
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Y){
            if (Airplane.TemporarlySeat.Count() > 0){
        //     Console.WriteLine("TESTING 3");
        // Console.ReadKey();
                if (MainMenu.currentUser is not null){
                    if (AccountBookings.editing){
                        ConfirmationEmail.SendEditNotification(MainMenu.currentUser.username, MainMenu.currentUser.email, currentflight.FlightId!, seatsstring);
                    }
                    else
                    {
                        ConfirmationEmail.SendConfirmation($"{MainMenu.currentUser.username}", $"{MainMenu.currentUser.email}", $"{currentflight.FlightId}", $"Rotterdam", $"{currentflight.Destination}", $"{currentflight.DepartureTime}", $"{currentflight.ArrivalTime}", seatsstring);
                    }
                    // Console.WriteLine(MainMenu.currentUser);
                    // Console.WriteLine("currenUser");
                    // Console.ReadKey();
                    // Console.WriteLine("TESTING 1");
                    // Console.ReadKey();

                    // Flight accountFlight = currentflight;
                    // List<Seat> seats = Airplane.TemporarlySeat;
                    // Booking accountbookings = new Booking(accountFlight, seats);

                    if(OptionSelection<Account>.selectedAccount is not null){
                        OptionSelection<Account>.selectedAccount.DeleteFromJson();
                        AddBooking(currentflight, OptionSelection<Account>.selectedAccount);
                    } else {
                        MainMenu.currentUser.DeleteFromJson();
                        AddBooking(currentflight, MainMenu.currentUser);
                    }

                    AccountBookings.editing = false;
                    JsonFile<Account>.Read("DataSources/Accounts.json");
                    if(OptionSelection<Account>.selectedAccount is null){
                        JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
                    } else {
                        JsonFile<Account>.Write("DataSources/Accounts.json", OptionSelection<Account>.selectedAccount);
                    }
                }
            }
        }
        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
    }

    public static int GetCalcDiscount(Account account){
        return CalculateDiscount(account);
    }
    private static int CalculateDiscount(Account account){
        int korting = 0;
        //decides the discount based on how many flights user has booked
        if(account.AccountBookings.Count == 0){
            korting = 0;
        }
        else{
            int modulo = account.AccountBookings.Count() % 3;
            if (modulo == 1){
                korting = 5;
            }
            if (modulo == 2){
                korting = 10;
            }
            if (modulo == 0){
                korting = 15;
            }
        }
        return korting;
    }
    public static double GetCalcPrice(double totalprice, double percentagekorting){
        return CalculatePrice(totalprice, percentagekorting);
    }
    private static double CalculatePrice(double totalprice, double percentagekorting){
        return totalprice * percentagekorting;
    }

    private static void AddBooking(Flight currentflight, Account account){
        Flight accountFlight = currentflight;
        List<Seat> seats = Airplane.TemporarlySeat;
        Booking accountbookings = new Booking(accountFlight, seats);
        if(account.AccountBookings.Any(b => b == accountbookings)){
            Booking existingBooking = account.AccountBookings.Where(a => a == accountbookings).ToList()[0];
            if(!AccountBookings.editing){
                for(int i = 0; i < accountbookings.BookedSeats.Count(); i++){
                    if(existingBooking.BookedSeats.Any(s => s == accountbookings.BookedSeats[i])){
                        accountbookings.BookedSeats.Remove(accountbookings.BookedSeats[i]);
                        i--;
                    }
                }
                accountbookings = (existingBooking + accountbookings)!;
            } else {
                accountbookings.BookedSeats = seats;
            }
            account.AccountBookings.Remove(existingBooking);
        }
        account.AccountBookings.Add(accountbookings);
    }
}