using Newtonsoft.Json;

public class Boeing737 : Airplane
{
    protected static int FirstClassPrice = 0;
    protected static int BusinessClassPrice = 0;
    protected static int EconomyClassPrice = 500;
    public Boeing737(char letter, int numbers) : base (letter, numbers) {
        
    }

    public override void InitializeSeats(int firstClassPrice , int businessClassPrice = 0, int economyClassPrice = 500)
    {
        for (char letter = 'A'; letter <= LetterSeat; letter++){
            for (int row = 1; row <= NumberOfRows; row++){
                Seat? existingSeat = bookedSeats.Find(s => s.Row == row && s.Letter == letter);
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

    public override void SetPrices(int firstClassPrice, int businessClassPrice, int economyClassPrice){
        FirstClassPrice = firstClassPrice;
        BusinessClassPrice = businessClassPrice;
        EconomyClassPrice = economyClassPrice;
        InitializeSeats(firstClassPrice, businessClassPrice, economyClassPrice);
    }

    public override void SetClassPrices(){
        int economyclassPrice;
        do{
            Console.WriteLine($"This is the current Economy Class seat price: {EconomyClassPrice}");
            Console.WriteLine("What is the new Economy Class seat price (positive number only): ");
            Console.Write(">>> ");
        } while (!int.TryParse(Console.ReadLine(), out economyclassPrice) || economyclassPrice <= 0);
        Console.Clear();
        Console.WriteLine("The new prices has been set.");
        Console.WriteLine($"Economy Class seat price: {economyclassPrice}.");
        Console.WriteLine();
        Console.WriteLine("Press any button to continue.");
        Console.ReadKey();
        SetPrices(0, 0, economyclassPrice);
    }
    public override void DisplaySeats()
    {
        // Calculate the total width of the seating arrangement
        Console.WriteLine("             [Economy Class Seat]");
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 5;
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
                    if(row == 1 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Console.Write(" Use arrow keys to navigate and press Enter to select a seat.");
                    }
                    if(row ==2 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Color.Red(" Red:", false);
                        Console.Write(" Booked Seat.");
                    }
                    if(row == 3 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Color.Yellow(" Yelow:", false);
                        Console.Write(": Available Economy Class Seat with extra legroom.");
                    }
                    if(row == 4 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Console.Write($" White: Available Economy Class Seat. Starting at: {EconomyClassPrice}");
                    }
                    if(row == 5 && letter == 'F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Color.Magenta(" Magenta:", false);
                        Console.Write($" Your current selected seats.");

                    }
                    if(row == 6 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("||");
                        Console.Write(" Press ESC to finish the booking.");
                    }
                    if(row == 8 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" *");
                        Console.Write(" Price will vary depending on the selected seat. *");
                    }
                    if(row == 9 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("|| - Window Seats have a price increase of 20% on top of the starting price.");
                    }
                    if(row == 10 && letter =='F'){
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("|| - Extra legroom seats have a price increase of 30 euro's on top of the starting price.");
                    }
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
    }
    public override void Start(Flight CurrentFlight)
    {
        string new_filepath = $"DataSources/{CurrentFlight.FlightId}.json";
        cursorRow = 1;  
        cursorSeat = 0; 
        bookedSeats.Clear();
        Seat.Seats.Clear();
        //TemporarlySeat.Clear();
        LoadBookedSeatsFromJson(new_filepath); 
        // SetClassPrices();
        InitializeSeats(FirstClassPrice, BusinessClassPrice, EconomyClassPrice);
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
        Console.Clear();
        bool confirmBooking = Prices.TicketPrices(CurrentFlight); // Ask for confirmation after finishing the booking
        if (confirmBooking)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
            int SeatsAvailable = Convert.ToInt32(CurrentFlight.SeatsAvailable);
            SeatsAvailable = SeatsAvailable - TemporarlySeat.Count();
            string SeatsAvailablestring = Convert.ToString(SeatsAvailable);
            CurrentFlight.SeatsAvailable = SeatsAvailablestring;
            string json = File.ReadAllText("DataSources/Flights.json");
            List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(json)!;
            foreach (Flight flight in flights!){   
                if (flight.FlightId == CurrentFlight.FlightId){
                    flight.SeatsAvailable = CurrentFlight.SeatsAvailable;
                }
            }
            string filepath = $"DataSources/{CurrentFlight.FlightId}.json";
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
            // InitializeSeats();
            // LoadBookedSeatsFromJson(new_filepath); 
            Start(CurrentFlight);
        }
    }
}