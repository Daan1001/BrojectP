using Newtonsoft.Json;
public static class AccountBookings{
    public static bool editing = false;

    public static void CancelBooking(Account account){
        if(account.AccountBookings.Count() > 0){
            Console.WriteLine("Choose a booking to cancel (press any key to continue)");
            Console.ReadKey();

            List<Flight> flights1 = account.AccountBookings.Select(b => b.BookedFlight).ToList();

            List<String> options = account.AccountBookings.Select(b => "("+b.BookedFlight.ToString(flights1)+")").ToList();

            OptionSelection<string>.Start(options, OptionSelection<String>.GoBack);
        } else {
            Console.WriteLine("This account doesn't have any bookings yet (Press any key to continue)");
            Console.ReadKey();
        }
    }

    public static void DeleteBooking(string booking, Account account){
        account = AccountBookings.UpdateAccount(account);
        List<Flight> flights2 = account.AccountBookings.Select(b => b.BookedFlight).ToList();

        foreach (Booking bookings in account.AccountBookings.Where( b => ("("+b.BookedFlight.ToString(flights2)+")").Substring(1, 6) == booking.Substring(1, 6))){
            Flight newflight = bookings.BookedFlight;
            // change seats available in flights.json to add the amount of seats previously booked(Works)
            List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
            newflight = flights.FirstOrDefault(flight => flight.FlightId == booking.Substring(1, 6))!;
            int bookedseats = bookings.BookedSeats.Count();
            int SeatsAvailable = Convert.ToInt32(newflight.SeatsAvailable);
            SeatsAvailable = SeatsAvailable + bookedseats;
            string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
            newflight.SeatsAvailable = SeatsAvailablestring;
            AddingFlights.SaveChanges(newflight);

            //removing seats from the {flightID}.json file so they become available again(Works)
            string filepath = $"DataSources/{bookings.BookedFlight.FlightId}.json";
            string json = File.ReadAllText(filepath);
            List<Seat> seatList = JsonConvert.DeserializeObject<List<Seat>>(json)!;
            List<Seat> seatlistcopy = new List<Seat>(seatList);

            bookings.BookedSeats.ForEach(seat1 => seatList.Where(seat2 => seat1.Letter == seat2.Letter && seat1.Row == seat2.Row).ToList().ForEach(seat2 => {seat1.ResetBooking(); seatlistcopy.Remove(seat2);}));

            string json2 = JsonConvert.SerializeObject(seatlistcopy, Formatting.Indented);
            File.WriteAllText(filepath, json2);

            // delete it from account.json(Works)
            account.DeleteFromJson();
            account.AccountBookings.Remove(bookings);
            JsonFile<Account>.Read("DataSources/Accounts.json");
            JsonFile<Account>.Write("DataSources/Accounts.json", account);

            Console.WriteLine("Booking canceled (press any key to continue)");
            Console.ReadKey();
            MainMenu.Start();
        }
    }

    public static void ShowBooking(){
        UpdateUser();
        ShowBooking(MainMenu.currentUser!);
    }

    public static void ShowBooking(Account account){
        Account currentAccount = account;
        if (currentAccount.AccountBookings.Count() > 0){
            foreach(Booking flight in currentAccount.AccountBookings){
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine(flight);
            }
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        else{
            Console.WriteLine("This account doesnt have any bookings yet (Press any key to continue)");
            Console.ReadKey();
        }
    }

    public static void EditBooking(){
        editing = true;
        if(OptionSelection<Account>.selectedAccount is null){
            UpdateUser();
            OptionSelection<Booking>.Start(MainMenu.currentUser!.AccountBookings, OptionSelection<Booking>.GoBack);
        } else {
            if(OptionSelection<Account>.selectedAccount.AccountBookings.Count() > 0){
                OptionSelection<Account>.selectedAccount = AccountBookings.UpdateAccount(OptionSelection<Account>.selectedAccount);
                OptionSelection<Booking>.Start(OptionSelection<Account>.selectedAccount.AccountBookings, OptionSelection<Booking>.GoBack);
            } else {
                Console.WriteLine("This account doesnt have any bookings yet (Press any key to continue)");
                Console.ReadKey();
            }
        }
    }

    public static void UpdateUser(){
        MainMenu.currentUser = UpdateAccount(MainMenu.currentUser!);
    }
    public static Account UpdateAccount(Account accountToUpdate){
        string filePath = "DataSources/Accounts.json";
        string jsonContent = File.ReadAllText(filePath);
        List<Account> newBookings = JsonConvert.DeserializeObject<List<Account>>(jsonContent)!;
        accountToUpdate = newBookings.FirstOrDefault(account => account.username == accountToUpdate.username)!;
        return accountToUpdate;
    }
}