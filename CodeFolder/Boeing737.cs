using Newtonsoft.Json;

public class Boeing737 : DisplaySeating
{
    public Boeing737(char letter, int numbers) : base (letter, numbers) {}

    public override void InitializeSeats(int firstClassPrice = 0, int businessClassPrice = 0, int economyClassPrice = 500)
    {
        for (char letter = 'A'; letter <= LetterSeat; letter++){
            for (int row = 1; row <= NumberOfRows; row++){
                Seat? existingSeat = DisplaySeating.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                if (existingSeat != null){
                    // The seat is already booked (based on the JSON data)
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    // The seat is not in the list of booked seats (initialize as unbooked)
                    bool isFirstLetter = letter == 'A';
                    bool isLastLetter = letter == LetterSeat;
                    int seatPrice;
                    if (isFirstLetter || isLastLetter){
                        // Increase the price by 20%
                        seatPrice = economyClassPrice + (int)(economyClassPrice * 0.2);
                    }
                    else{
                        // Regular price
                        seatPrice = economyClassPrice;
                    }
                    bool isExtraLegroom = row == 16 || row == 17;
                    seatPrice = isExtraLegroom ? seatPrice + 30 : seatPrice; // Extra 30 euros for extra legroom
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
    }
    public override void DisplaySeats()
    {
        // Calculate the total width of the seating arrangement
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 20;
        Console.Write("     ");
        for (char letter = 'A'; letter <= LetterSeat; letter++){
            // Add an extra space after column C
            if (letter == 'D'){
                Console.Write("   ");
            }
            Console.Write($"{letter,-5} ");
        }
        Console.WriteLine();
        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
        // Dictionary to store the maximum length of seat identifier for each column
        Dictionary<char, int> maxColumnLengths = new Dictionary<char, int>();
        for (int row = 1; row <= NumberOfRows; row++){
            Console.Write($" {row,2}|");
            for (char letter = 'A'; letter <= LetterSeat; letter++){
                Seat? seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);
                if (seat != null){
                    if (cursorRow == row && cursorSeat == letter - 'A') {
                        Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color for the selected seat
                    }
                    if (letter == 'D') {
                        // Add an extra space after column C
                        Console.Write("   ");
                    }
                    if (row == 16 || row == 17) {
                        // Check if the extra legroom seat is booked
                        if (seat.Booked){
                            Console.ForegroundColor = ConsoleColor.Red; // Extra legroom seat is booked, set to red
                        }
                        else{
                            Console.ForegroundColor = ConsoleColor.Yellow; // Extra legroom seat is not booked, set to yellow
                        }
                    }
                    else{
                        // Set the text color to red if the seat is booked
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.White;
                    }
                    // Display the seat letter and number with dynamic spacing for better alignment
                    Console.Write(seat.Booked ? $"|{letter}{row,-2}| " : $"|{letter}{row,-2}| ");
                    // Update the maximum length for the current column
                    maxColumnLengths[letter] = Math.Max(maxColumnLengths.GetValueOrDefault(letter), $"{letter}{row}".Length);
                    // Reset text and background color after printing the current seat
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            // Add extra spacing for the walking path (after every section)
            Console.Write("   ");
            Console.WriteLine();
        }
        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
        Console.WriteLine("Use arrow keys to navigate and press Enter to select a seat.");
        Color.Red(" Red:", false);
        Console.WriteLine(" Booked Seat.");
        Color.Yellow(" Yelow:", false);
        Console.WriteLine(" Extra legroom seat.");
        Console.WriteLine(" White: Available Seat.");
        Console.WriteLine(" BACKSPACE: To unselect a seat.");
        Console.WriteLine(" Press ESC to finish the booking.");
        Console.WriteLine();
    }
    public override void Start(Flight CurrentFlight)
    {
        string new_filepath = $"DataSources/{CurrentFlight.FlightId}.json";
        cursorRow = 1;  
        cursorSeat = 0; 
        // bookedSeats.Clear();
        // TemporarlySeat.Clear();
        LoadBookedSeatsFromJson(new_filepath); 
        InitializeSeats();
        DisplaySeats();
        bool isBookingComplete = false;
        while (!isBookingComplete)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;

                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;

                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;

                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;

                case ConsoleKey.Enter:
                    // SelectAndBookSeat();
                    DisplaySeats();
                    SelectAndBookSeat();
                    break;

                case ConsoleKey.Backspace:
                    DisplaySeats();
                    UnselectSeat();
                    break;

                case ConsoleKey.Escape:
                    isBookingComplete = true;
                    break;

                default:
                    Console.WriteLine("Invalid input. Please use arrow keys to navigate.");
                    break;
            }
        }
        bool confirmBooking = Prices.TicketPrices(CurrentFlight); // Ask for confirmation after finishing the booking
        if (confirmBooking)
        {
            if (MainMenu.currentUser! != null!){
                MainMenu.currentUser.DeleteFromJson();
                MainMenu.currentUser.AccountFlights.Add(CurrentFlight);
                JsonFile<Account>.Read("DataSources/Accounts.json");
                JsonFile<Account>.Write("DataSources/Accounts.json", MainMenu.currentUser);
            }
            int SeatsAvailable = Convert.ToInt32(CurrentFlight.TotalSeats);
            SeatsAvailable = SeatsAvailable - bookedSeats.Count();
            string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
            CurrentFlight.SeatsAvailable = SeatsAvailablestring;
            string json = File.ReadAllText("DataSources/Flights.json");
            List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
            foreach (Flight flight in flights!){   
                if (flight.FlightId == CurrentFlight.FlightId){
                    flight.SeatsAvailable = CurrentFlight.SeatsAvailable;
                }
            }
            string updatedJson = JsonConvert.SerializeObject(flights, Formatting.Indented);
            File.WriteAllText("DataSources/Flights.json", updatedJson);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
            
            SaveBookedSeatsToJson(new_filepath); // Specify the desired file path
            TemporarlySeat.Clear();
            bookedSeats.Clear();
            Seat.Seats.Clear();
            Console.ReadKey();
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
            // InitializeSeats();
            // LoadBookedSeatsFromJson(new_filepath); 
            Start(CurrentFlight);
        }
    }
}