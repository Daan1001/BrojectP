using Newtonsoft.Json;
public static class AccountReservation{
    public static Account currentAccount = MainMenu.currentUser!;
    public static void CancelReservation(){
        Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
        Console.ReadKey();

        List<string> options = new List<string>();
        List<Flight> flights1 = new List<Flight>();
        foreach(Booking booking1 in currentAccount.AccountBookings){
            flights1.Add(booking1.BookedFlight);
        }
        foreach(Booking booking in currentAccount.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights1)})";
            options.Add(data);
        }
        OptionSelection<string>.Start(options);
    }

    public static void DeleteReservation(string reservation){
        List<Flight> flights2 = new List<Flight>();
        foreach(Booking booking1 in currentAccount.AccountBookings){
            flights2.Add(booking1.BookedFlight);
        }
        foreach (Booking booking in currentAccount.AccountBookings){
            string data = $"({booking.BookedFlight.ToString(flights2)})";
            if (data == reservation){
                currentAccount.DeleteFromJson();
                currentAccount.AccountBookings.Remove(booking);
                JsonFile<Account>.Read("DataSources/Accounts.json");
                JsonFile<Account>.Write("DataSources/Accounts.json", currentAccount);
                Console.WriteLine("Reservation canceled(press any key to continue)");
                Console.ReadKey();
                MainMenu.Start();
            }
        }
    }
    public static void ShowReservation(){
        Account currentAccount = MainMenu.currentUser!;
        foreach(Booking flight in currentAccount.AccountBookings){
            Console.WriteLine($"Flight {flight.BookedFlight.FlightId} to {flight.BookedFlight.Destination}, {flight.BookedFlight.Country}, with {flight.BookedFlight.AirplaneType}. departure time: {flight.BookedFlight.FlightDate} at {flight.BookedFlight.DepartureTime}.");
        }
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void EditReservation(){
        //daan mag hier werken
        Console.WriteLine("test");
        Console.ReadKey();
    }
}