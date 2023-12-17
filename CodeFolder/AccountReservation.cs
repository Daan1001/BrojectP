using Newtonsoft.Json;
public static class AccountReservation{
    public static bool editing = false;

    public static void CancelReservation(Account account){
        if(account.AccountBookings.Count() > 0){
            Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
            Console.ReadKey();

            List<string> options = new List<string>();
            List<Flight> flights1 = new List<Flight>();
            foreach(Booking booking1 in account.AccountBookings){
                flights1.Add(booking1.BookedFlight);
            }
            foreach(Booking booking in account.AccountBookings){
                string data = $"({booking.BookedFlight.ToString(flights1)})";
                options.Add(data);
            }
            OptionSelection<string>.Start(options, OptionSelection<String>.GoBack);
        } else {
            Console.WriteLine("This account doesnt have any reservations yet (Press any key to continue)");
            Console.ReadKey();
        }
    }

    public static void DeleteReservation(string reservation, Account account){
        account = AccountReservation.UpdateAccount(account);
        List<Flight> flights2 = new List<Flight>();
        foreach(Booking booking1 in account.AccountBookings){
            flights2.Add(booking1.BookedFlight);
        }
        foreach (Booking booking in account.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights2)})";
            Flight newflight = booking.BookedFlight;
            if (data.Substring(1, 6) == reservation.Substring(1, 6)){
                // change seats available in flights.json to add the amount of seats previously booked(Works)
                List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
                newflight = flights.FirstOrDefault(flight => flight.FlightId == reservation.Substring(1, 6))!;
                int bookedseats = booking.BookedSeats.Count();
                int SeatsAvailable = Convert.ToInt32(newflight.SeatsAvailable);
                SeatsAvailable = SeatsAvailable + bookedseats;
                string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
                newflight.SeatsAvailable = SeatsAvailablestring;
                AddingFlights.SaveChanges(newflight);

                //removing seats from the {flightID}.json file so they become available again(Works)
                string filepath = $"DataSources/{booking.BookedFlight.FlightId}.json";
                string json = File.ReadAllText(filepath);
                List<Seat> seatList = JsonConvert.DeserializeObject<List<Seat>>(json)!;
                List<Seat> seatlistcopy = new List<Seat>(seatList);
                foreach(Seat seat in booking.BookedSeats){
                    foreach (Seat seat1 in seatList){
                        if (seat.Letter == seat1.Letter && seat.Row == seat1.Row){
                            seat.ResetBooking();
                            seatlistcopy.Remove(seat1);
                        }
                    }
                }
                string json2 = JsonConvert.SerializeObject(seatlistcopy, Formatting.Indented);
                File.WriteAllText(filepath, json2);

                // delete it from account.json(Works)
                account.DeleteFromJson();
                account.AccountBookings.Remove(booking);
                JsonFile<Account>.Read("DataSources/Accounts.json");
                JsonFile<Account>.Write("DataSources/Accounts.json", account);

                Console.WriteLine("Reservation canceled (press any key to continue)");
                Console.ReadKey();
                MainMenu.Start();
            }
        }
    }

    public static void ShowReservation(){
        UpdateUser();
        ShowReservation(MainMenu.currentUser!);
    }

    public static void ShowReservation(Account account){
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
            Console.WriteLine("This account doesnt have any reservations yet (Press any key to continue)");
            Console.ReadKey();
        }
    }

    public static void EditReservation(){
        editing = true;
        if(OptionSelection<Account>.selectedAccount is null){
            UpdateUser();
            OptionSelection<Booking>.Start(MainMenu.currentUser!.AccountBookings, OptionSelection<Booking>.GoBack);
        } else {
            if(OptionSelection<Account>.selectedAccount.AccountBookings.Count() > 0){
                OptionSelection<Account>.selectedAccount = AccountReservation.UpdateAccount(OptionSelection<Account>.selectedAccount);
                OptionSelection<Booking>.Start(OptionSelection<Account>.selectedAccount.AccountBookings, OptionSelection<Booking>.GoBack);
            } else {
                Console.WriteLine("This account doesnt have any reservations yet (Press any key to continue)");
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