using Newtonsoft.Json;
public static class AccountReservation{
    public static Account currentAccount = MainMenu.currentUser!;
    public static void CancelReservation(){
        Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
        Console.ReadKey();

        List<string> options = new List<string>();
        foreach(Flight flight in currentAccount.AccountFlights){
            string data = $"({flight.ToString()})";
            options.Add(data);
        }
        OptionSelection<string>.Start(options);
    }

    public static void DeleteReservation(string reservation){
        foreach (Flight flight in currentAccount.AccountFlights){
            string data = $"({flight.ToString()})";
            if (data == reservation){
                currentAccount.DeleteFromJson();
                currentAccount.AccountFlights.Remove(flight);
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
        foreach(Flight flight in currentAccount.AccountFlights){
            Console.WriteLine($"Flight {flight.FlightId} to {flight.Destination}, {flight.Country}, with {flight.AirplaneType}. departure time: {flight.FlightDate} at {flight.DepartureTime}.");
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