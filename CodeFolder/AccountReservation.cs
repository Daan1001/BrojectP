public static class AccountReservation{
    public static void CancelReservation(){
        Console.WriteLine("Choose a reservation to cancel (press any key to continue)");
        Console.ReadKey();

        Account currentAccount = MainMenu.currentUser!;
        List<string> options = new List<string>();
        foreach(Flight flight in currentAccount.AccountFlights){
            options.Add(flight.ToString());
        }
        OptionSelection<string>.Start(options);
        Console.WriteLine("test");
        Console.ReadKey();
    }
    public static void ShowReservation(){
        Account currentAccount = MainMenu.currentUser!;
        foreach(Flight flight in currentAccount.AccountFlights){
            Console.WriteLine(flight.ToString());
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