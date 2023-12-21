using CodeFolder;
using Newtonsoft.Json;
public abstract class Airplane 
{
    public static int cursorRow = 0;
    public static int cursorSeat = 0;
    public static List<Seat> bookedSeats = new List<Seat>();
    public static List<Seat> TemporarlySeat = new List<Seat>();
    public char LetterSeat { get; private set; }
    public int NumberOfRows { get; private set; }
    public static int FirstClassPrice{get; protected set;}
    public static int BusinessClassPrice {get; protected set;}
    public static int EconomyClassPrice {get; protected set;}
    public Airplane(char letterseat, int numberofrows){
        LetterSeat = letterseat;
        NumberOfRows = numberofrows;
    }
    public abstract void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice);
    public abstract void DisplaySeats();

    public abstract void SetPrices(int firstClassPrice, int businessClassPrice, int economyClassPrice);
    public abstract void SetClassPrices();

    public virtual void UpdateSeat(Flight currentFlight){
        string newFilePath = $"DataSources/{currentFlight.FlightId}.json";
        // cursorRow = 1;
        // cursorSeat = 0;
        bookedSeats.Clear();
        Seat.Seats.Clear();
        LoadBookedSeatsFromJson(newFilePath);
        InitializeSeats(FirstClassPrice, BusinessClassPrice, EconomyClassPrice);
        DisplaySeats();
    }

    public virtual void Start(Flight currentFlight)
    {
        UpdateSeat(currentFlight);
        cursorRow = 1;
        cursorSeat = 0;
        bool isBookingComplete = false;

        while (!isBookingComplete)
        {
            isBookingComplete = Movement.MovementInPut(this);
        }
        Console.Clear();
        Prices.TicketPrices(currentFlight);
        bool confirmBooking = ConfirmBooking(currentFlight); // Ask for confirmation after finishing the booking
        //###############################

        //###############################
        if (confirmBooking)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
            int SeatsAvailable = Convert.ToInt32(currentFlight.TotalSeats);
            SeatsAvailable = SeatsAvailable - bookedSeats.Count();
            string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
            currentFlight.SeatsAvailable = SeatsAvailablestring;
            string json = File.ReadAllText("DataSources/Flights.json");
            List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
            foreach (Flight flight in flights!){   
                if (flight.FlightId == currentFlight.FlightId){
                    flight.SeatsAvailable = currentFlight.SeatsAvailable;
                }
            }
            string filepath = $"DataSources/{currentFlight.FlightId}.json";
            // foreach(var seat in bookedSeats){
            //     Console.WriteLine(seat.ShowSeat()+ "This is in BookseatList.");
            // }

            // foreach( var seat in TemporarlySeat){
            //     Console.WriteLine(seat.ShowSeat() + "this is in TempSeatList");
            // }
            SaveBookedSeatsToJson(filepath); // Specify the desired file path
            TemporarlySeat.Clear();
            bookedSeats.Clear();
            Seat.Seats.Clear();
            Console.ReadKey();
            string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
            File.WriteAllText("DataSources/Flights.json", updatedJson);
            Program.Main();
        }
        else{
            // Roll back the booked seats to available
            foreach (var seat in TemporarlySeat){
                
                seat.ResetSeat();
            }
            Console.Clear();
            Console.WriteLine("Booking canceled. Selected seats are now available.");
            Console.WriteLine();
            TemporarlySeat.Clear();
            // bookedSeats.Clear();
            Start(currentFlight);
        }
    }
    public void SelectAndBookSeat(){
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        //Seat? selectedSeat = bookedSeats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        if (selectedSeat != null && selectedSeat.Booked == false){
            selectedSeat.Book();
            TemporarlySeat.Add(selectedSeat);
            //selectedSeat.ShowSeat();
            //bookedSeats.Add(selectedSeat);
        }
    } 
    public void UnselectSeat(){
        //Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        Seat? selectedSeat = TemporarlySeat.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        Seat? selectedBookedSeat = bookedSeats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        Seat? AlphaSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat is not null){
            // Unselect the seat
            Console.WriteLine($"Seat: {selectedSeat.Letter}{selectedSeat.Row} unselected.");
            selectedSeat.ResetBooking(); // you have a method to unbook the seat in your Seat class
            selectedBookedSeat!.ResetBooking();
            AlphaSeat.Booked = false;
            TemporarlySeat.Remove(selectedSeat); // Remove the seat from the TemporarlySeat list
            bookedSeats.Remove(selectedSeat);
        }
        // Console.WriteLine(selectedSeat.ShowSeat() + "this is TempList");
        // Console.WriteLine();
        // Console.WriteLine(selectedBookedSeat.ShowSeat()+ " This is BookedSeatsList");
    }
    public void LoadBookedSeatsFromJson(string filePath){
        if (File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            // updates current list of seat or starts a new empty list
            bookedSeats = JsonConvert.DeserializeObject<List<Seat>>(json) ?? new List<Seat>();
        }
    }
    public void SaveBookedSeatsToJson(string filePath){
        // Save the bookedSeats list to a JSON file
        string json = JsonConvert.SerializeObject(bookedSeats, Formatting.Indented);
        File.WriteAllText(filePath, json);
        //Console.WriteLine($"Booked seats saved to {filePath}");
    }
    
    public Boolean ConfirmBooking(Flight currentflight){
        // string seatsstring = $"Price P.P: {currentflight.BasePrice}.\nSelected seats:";
        // seatsstring = seatsstring + $"\nPrice before discount: €{totalprice}\nCurrent discount: {Korting}%\nTotal price: €{TotalpriceDouble}\nHave a great flight!";
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key;
        do{
            key = Console.ReadKey();
            Console.WriteLine();
        } while(!(key.Key == ConsoleKey.Y || key.Key == ConsoleKey.N));

        if (key.Key == ConsoleKey.Y){
            if (Airplane.TemporarlySeat.Count() > 0){
                if (MainMenu.currentUser is not null){
                    if (AccountBookings.editing){
                        ConfirmationEmail.SendEditNotification(MainMenu.currentUser.username, MainMenu.currentUser.email, currentflight.FlightId, Booking.seatsstring);
                    }
                    else{
                        ConfirmationEmail.SendConfirmation($"{MainMenu.currentUser.username}", $"{MainMenu.currentUser.email}", $"{currentflight.FlightId}", $"Rotterdam", $"{currentflight.Destination}", $"{currentflight.DepartureTime}", $"{currentflight.ArrivalTime}", Booking.seatsstring);
                    }
                    if(OptionSelection<Account>.selectedAccount is not null){
                        OptionSelection<Account>.selectedAccount.DeleteFromJson();
                        AddBooking(currentflight, OptionSelection<Account>.selectedAccount);
                    } else {
                        MainMenu.currentUser.DeleteFromJson();
                        AddBooking(currentflight, MainMenu.currentUser);
                    }
                    JsonFile<Account>.Read("DataSources/Accounts.json");
                    if(OptionSelection<Account>.selectedAccount is null){
                        JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
                    } else {
                        JsonFile<Account>.Write("DataSources/Accounts.json", OptionSelection<Account>.selectedAccount);
                    }
                }
            } else {
                if(AccountBookings.editing){
                    Account account;
                    if(OptionSelection<Account>.selectedAccount is null){
                        account = MainMenu.currentUser!;
                    } else {
                        account = OptionSelection<Account>.selectedAccount;
                    }
                    account!.DeleteFromJson();
                    account!.AccountBookings.Remove(account.AccountBookings.Where(b => b.BookedFlight == currentflight).First());
                    JsonFile<Account>.Write("DataSources/Accounts.json", account);
                }
            }
            AccountBookings.editing = false;
        }
        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
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

    public void RedrawSeats(){
        //Console.SetCursorPosition(0, 0);
        DisplaySeats();
    }
}