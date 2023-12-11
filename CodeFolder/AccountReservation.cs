using Newtonsoft.Json;
public static class AccountReservation{
    public static bool editing = false;
    public static void CancelReservation(){
        Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
        Console.ReadKey();

        List<string> options = new List<string>();
        List<Flight> flights1 = new List<Flight>();
        foreach(Booking booking1 in MainMenu.currentUser!.AccountBookings){ //checks if flight is still relevant
            DateTime myDate = DateTime.ParseExact(booking1.BookedFlight.FlightDate!, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime today = DateTime.Today;
            if(DateTime.Today < myDate){
                flights1.Add(booking1.BookedFlight);
            }
        }
        foreach(Booking booking in MainMenu.currentUser.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights1)})";
            options.Add(data);
        }
        OptionSelection<string>.Start(options, OptionSelection<String>.GoBack);
    }

    public static void DeleteReservation(string reservation){
        UpdateUser();
        List<Flight> flights2 = new List<Flight>();
        foreach(Booking booking1 in MainMenu.currentUser!.AccountBookings){ //checks if flight is still relevant
            DateTime myDate = DateTime.ParseExact(booking1.BookedFlight.FlightDate!, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime today = DateTime.Today;
            if(DateTime.Today < myDate){
                flights2.Add(booking1.BookedFlight);
            }
        }
        foreach (Booking booking in MainMenu.currentUser.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights2)})";
            Flight newflight = booking.BookedFlight;
            if (data.Substring(1, 6) == reservation.Substring(1, 6)){
                // change seats available in flights.json to add the amount of seats previously booked
                List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
                newflight = flights.FirstOrDefault(flight => flight.FlightId == reservation.Substring(1, 6))!;
                int bookedseats = booking.BookedSeats.Count();
                int SeatsAvailable = Convert.ToInt32(newflight.SeatsAvailable);
                SeatsAvailable = SeatsAvailable + bookedseats;
                string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
                newflight.SeatsAvailable = SeatsAvailablestring;
                AddingFlights.SaveChanges(newflight);

                //removing seats from the {flightID}.json file so they become available again
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

                // delete it from account.json
                MainMenu.currentUser.DeleteFromJson();
                MainMenu.currentUser.AccountBookings.Remove(booking);
                JsonFile<Account>.Read("DataSources/Accounts.json");
                JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);

                Console.WriteLine("Reservation canceled(press any key to continue)");
                Console.ReadKey();
                MainMenu.Start();
            }
        }
    }
    public static void ShowReservation(){
        UpdateUser();
        List<Booking> flights2 = new List<Booking>();
        foreach(Booking booking1 in MainMenu.currentUser!.AccountBookings){ //checks if flight is still relevant
            DateTime myDate = DateTime.ParseExact(booking1.BookedFlight.FlightDate!, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime today = DateTime.Today;
            if(today < myDate){
                flights2.Add(booking1);
            }
        }
        //Account currentAccount = MainMenu.currentUser!;
        foreach(Booking flight in flights2){
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(flight);
        }
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void ShowReservation(Account account){
        Account currentAccount = account;
        foreach(Booking flight in currentAccount.AccountBookings){
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(flight);
        }
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void EditReservation(){
        UpdateUser();
        editing = true;
        OptionSelection<Booking>.Start(MainMenu.currentUser!.AccountBookings, OptionSelection<Booking>.GoBack);

        //daan mag hier werken
        Console.WriteLine("test");
        Console.ReadKey();
    }

    public static void UpdateUser(){
        string filePath = "DataSources/Accounts.json";
        string jsonContent = File.ReadAllText(filePath);
        List<Account> newBookings = JsonConvert.DeserializeObject<List<Account>>(jsonContent)!;
        MainMenu.currentUser = newBookings.FirstOrDefault(account => account.username == MainMenu.currentUser?.username);
    }
}