using Newtonsoft.Json;
public static class AccountReservation{
    public static void CancelReservation(){
        Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
        Console.ReadKey();

        List<string> options = new List<string>();
        List<Flight> flights1 = new List<Flight>();
        foreach(Booking booking1 in MainMenu.currentUser!.AccountBookings){
            flights1.Add(booking1.BookedFlight);
        }
        foreach(Booking booking in MainMenu.currentUser.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights1)})";
            options.Add(data);
        }
        OptionSelection<string>.Start(options);
    }

    public static void DeleteReservation(string reservation){
        UpdateUser();
        List<Flight> flights2 = new List<Flight>();
        foreach(Booking booking1 in MainMenu.currentUser!.AccountBookings){
            flights2.Add(booking1.BookedFlight);
        }
        foreach (Booking booking in MainMenu.currentUser.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights2)})";
            Flight newflight = booking.BookedFlight;
            if (data.Substring(1, 6) == reservation.Substring(1, 6)){
                // change seats available in flights.json to add the amount of seats previously booked(Works)
                List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
                foreach(Flight flight in flights){
                    if (flight.FlightId == reservation.Substring(1, 6)){
                        newflight = flight;
                    }
                }
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
        Account currentAccount = MainMenu.currentUser!;
        foreach(Booking flight in currentAccount.AccountBookings){
            Console.WriteLine("---------------------------------------------------------------------");
            // Console.WriteLine($"Flight {flight.BookedFlight.FlightId} to {flight.BookedFlight.Destination}, {flight.BookedFlight.Country}, with {flight.BookedFlight.AirplaneType}. departure time: {flight.BookedFlight.FlightDate} at {flight.BookedFlight.DepartureTime}.");
            // Console.Write("Seats: ");
            // foreach(Seat seat in flight.BookedSeats){
            //     Console.Write($"{seat.ToString()}, ");
            // }
            Console.WriteLine(flight);
        }
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void EditReservation(){
        //daan mag hier werken
        Console.WriteLine("test");
        Console.ReadKey();
    }

    public static void UpdateUser(){
        string filePath = "DataSources/Accounts.json";
        string jsonContent = File.ReadAllText(filePath);
        List<Account> newBookings = JsonConvert.DeserializeObject<List<Account>>(jsonContent)!;
        foreach (Account account in newBookings){
            if (account.username == MainMenu.currentUser!.username){
                MainMenu.currentUser = account;
            }
        }
    }
}