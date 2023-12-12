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
        foreach (var seat in Airplane.TemporarlySeat)
        {
            if(seat.Booked == true){
                Console.WriteLine($"{count}. Class: {seat.TypeClass} Seat: {seat.Letter}{seat.Row} Price: €{seat.Price}");
                count++;
                totalprice = totalprice + seat.Price + BasePriceInt;
            }   
        }
        double percentage = (double)Korting/100;
        double percentagekorting = 1.0 - percentage;
        double totalpricedouble = CalculatePrice(Convert.ToDouble(totalprice), percentagekorting);
        TotalpriceDouble = totalpricedouble;
        TotalpriceDouble = Math.Round(TotalpriceDouble, 2);
        Console.WriteLine($"Price before discount: {totalprice}");
        Console.WriteLine($"Current discount: {Korting}%");
        Console.WriteLine($"Total price: €{TotalpriceDouble}");
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Y){
            if (Airplane.TemporarlySeat.Count() > 0){
                if (MainMenu.currentUser is not null){
                    AccountReservation.UpdateUser();
                    MainMenu.currentUser.DeleteFromJson();
                    Flight accountFlight = currentflight;
                    List<Seat> seats = Airplane.TemporarlySeat;
                    Booking accountbookings = new Booking(accountFlight, seats);

                    if(MainMenu.currentUser.AccountBookings.Any(b => b == accountbookings)){
                        Booking existingBooking = MainMenu.currentUser.AccountBookings.Where(a => a == accountbookings).ToList()[0];
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
                        MainMenu.currentUser.AccountBookings.Remove(existingBooking);
                    }
                    AccountReservation.editing = false;
                    MainMenu.currentUser.AccountBookings.Add(accountbookings);
                    JsonFile<Account>.Read("DataSources/Accounts.json");
                    JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
                }
            }
        }
        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
    }

    public static int CalculateDiscount(Account account){
        int korting = 0;
         //decides the discount based on how many flights user has booked
        if (account.AccountBookings.Count() == 1){
            korting = 5;
        }
        if (account.AccountBookings.Count() == 2){
            korting = 10;
        }
        if (account.AccountBookings.Count() >= 3){
            korting = 15;
        }
        return korting;
    }
    public static double CalculatePrice(double totalprice, double percentagekorting){
        return totalprice * percentagekorting;
    }
}