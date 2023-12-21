using Newtonsoft.Json;
public abstract class Airplane 
{
    public static int cursorRow = 0;
    public static int cursorSeat = 0;
    public static List<Seat> bookedSeats = new List<Seat>();
    public static List<Seat> TemporarlySeat = new List<Seat>();
    public char LetterSeat { get; private set; }
    public int NumberOfRows { get; private set; }   
    public abstract int FirstClassPrice{get; set;}
    public abstract int BusinessClassPrice {get; set;}
    public abstract int EconomyClassPrice {get; set;}

    public static Boeing737 boeing737 = new('F', 33);
    public static Boeing787 boeing787 = new('I', 28);
    public static Airbus330 airbus330 = new('I',44);

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
        bool confirmBooking = Prices.TicketPrices(currentFlight); // Ask for confirmation after finishing the booking
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
    public void RedrawSeats(){
        DisplaySeats();
    }
}