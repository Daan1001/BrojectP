public class Prices{
    public static int Korting;
    public static double TotalpriceDouble;
    public static bool TicketPrices(Flight currentflight)
    {
        int korting = 0;
        if (MainMenu.currentUser! != null!){
            if (MainMenu.currentUser!.AccountBookings.Count() == 1){
                korting = 5;
            }
            if (MainMenu.currentUser.AccountBookings.Count() == 2){
                korting = 10;
            }
            if (MainMenu.currentUser.AccountBookings.Count() >= 3){
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
        TotalpriceDouble = Math.Round(TotalpriceDouble, 2);
        Console.WriteLine($"Price before discount: {totalprice}");
        Console.WriteLine($"Current discount: {korting}%");
        Console.WriteLine($"Total price: €{TotalpriceDouble}");
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();
        // Console.WriteLine("TESTING");
        // Console.ReadKey();
        if (key.Key == ConsoleKey.Y){
        //     Console.WriteLine("TESTING 2");
        // Console.ReadKey();
            if (Airplane.TemporarlySeat.Count() > 0){
        //         Console.WriteLine("TESTING 3");
        // Console.ReadKey();
                if (MainMenu.currentUser is not null){
                    // Console.WriteLine(MainMenu.currentUser);
                    // Console.WriteLine("currenUser");
                    // Console.ReadKey();
                    // Console.WriteLine("TESTING 1");
                    // Console.ReadKey();

                    // Flight accountFlight = currentflight;
                    // List<Seat> seats = Airplane.TemporarlySeat;
                    // Booking accountbookings = new Booking(accountFlight, seats);

                    if(OptionSelection<Account>.selectedAccount is not null){
                        // Console.WriteLine(OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine("Selected account 1");
                        // Console.ReadKey();
                        // Console.WriteLine("TESTING 2");
                        // Console.ReadKey();
                        // AccountReservation.UpdateUser(OptionSelection<Account>.selectedAccount);
                        OptionSelection<Account>.selectedAccount.DeleteFromJson();
                        AddBooking(currentflight, OptionSelection<Account>.selectedAccount);
                        // OptionSelection<Account>.selectedAccount = AccountReservation.UpdateAccount(OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine(OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine("Selected account 2");
                        // Console.ReadKey();
                    } else {
                        // Console.WriteLine(MainMenu.currentUser);
                        // Console.WriteLine("current user 2");
                        // Console.ReadKey();
                        // Console.WriteLine("TESTING 3");
                        // Console.ReadKey();
                        // AccountReservation.UpdateUser();
                        MainMenu.currentUser.DeleteFromJson();
                        AddBooking(currentflight, MainMenu.currentUser);
                    }
                    // Console.WriteLine(OptionSelection<Account>.selectedAccount);
                    //     Console.WriteLine("Selected account 3");
                    //     Console.ReadKey();

                    AccountReservation.editing = false;
                    JsonFile<Account>.Read("DataSources/Accounts.json");
                    if(OptionSelection<Account>.selectedAccount is null){
                        // Console.WriteLine(MainMenu.currentUser);
                        // Console.WriteLine(OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine("TESTING 1");
                        // Console.ReadKey();
                        JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
                    } else {
                        JsonFile<Account>.Write("DataSources/Accounts.json", OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine(MainMenu.currentUser);
                        // Console.WriteLine(OptionSelection<Account>.selectedAccount);
                        // Console.WriteLine("TESTING 2");
                        // Console.ReadKey();
                    }
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
    //     account.AccountBooking.Add(flights[1]);
    //     Seat seat= new Seat("First Class", 'B', 1, true, 500);
    //     DisplaySeating.TemporarlySeat.Add(seat);
    //     Prices.TicketPrices(flights[2]);

    //     Assert.AreEqual(Prices.Korting, 5);
    // }
    public static double CalculatePrice(double totalprice, double percentagekorting){
        return totalprice * percentagekorting;
    }

    private static void AddBooking(Flight currentflight, Account account){
        Flight accountFlight = currentflight;
        List<Seat> seats = Airplane.TemporarlySeat;
        Booking accountbookings = new Booking(accountFlight, seats);
        if(account.AccountBookings.Any(b => b == accountbookings)){
            Booking existingBooking = account.AccountBookings.Where(a => a == accountbookings).ToList()[0];
            if(!AccountReservation.editing){
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