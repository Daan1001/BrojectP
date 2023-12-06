using Newtonsoft.Json;
public class Airbus330 : Airplane
{
    public Airbus330(char letter, int numbers) : base(letter, numbers){}

    public override void InitializeSeats(int firstClassPrice = 1000, int businessClassPrice = 750, int economyClassPrice = 500)
    {
        // Initialize first-class seats
        for(char letter = 'A'; letter <= 'F'; letter++){
            for(int row = 1; row <= 2; row++){
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'F') ? (int)(firstClassPrice * 1.2) : firstClassPrice;
                if (existingSeat != null){
                    // The seat is already booked (based on the JSON data)
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new FirstClass("First Class", letter, row, false, seatPrice);
                }
            }
        }
        for(char letter = 'A'; letter <= 'I'; letter++){
            for(int row =3; row <= 38; row++){
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2) : economyClassPrice;
                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    if(row == 3 || row == 10 || row == 30){
                        int legroom = 30;
                        seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2 +legroom) : economyClassPrice + legroom;
                    }
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
        for(char letter ='A'; letter <= 'G'; letter++){
            for(int row=39; row <= 44; row++){
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'G') ? (int)(economyClassPrice * 1.2) : economyClassPrice;
                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
    }

// Airbus330 airbus330 = new('I',44);
    public override void DisplaySeats()
    {   
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 3;
        Console.Write("    ");
        for (char letter = 'A'; letter <= 'F'; letter++){
            if(letter == 'C' || letter == 'E'){
                Console.Write("   ");
            }
            Console.Write($"{letter,-4} ");
        }
        Console.WriteLine();
        Console.WriteLine($" +{new string('-', totalWidth - 3)}+"); 
        
        Dictionary<char, int> maxColumnLengths = new Dictionary<char, int>();
        for (int row = 1; row <= NumberOfRows; row++)
        {
            Console.Write($" {row,2}|");

            for (char letter = 'A'; letter <= LetterSeat; letter++)
            {
                Seat? seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);

                if (seat != null)
                {
                    if (cursorRow == row && cursorSeat == letter - 'A'){
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    if (letter >= 'A' && letter <= 'F' && row <= 2){
                        // First-class seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.Green;
                    }
                    else if(letter >= 'A' && letter <= 'I' && row == 3){
                        // extra legroom seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.Yellow;
                    }
                    else if (letter >= 'A'&& letter <='I' && row == 10 || row == 30){
                        // extra legroom seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.Yellow;
                    }
                    else if (letter <= LetterSeat && row >= 4 && row <= 44){
                        // Economy class seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.White;
                    }
                    if(letter == 'C'  && row <= 2|| letter == 'E'  && row <= 2){
                        Console.Write("   ");
                    }
                    if(letter == 'D'  && row >= 3 && row <= 38|| letter == 'G' && row >= 3 && row <= 38){
                        Console.Write("     ");
                        // Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    }
                    if(letter =='C' && row >= 39 && row <= 44 || letter == 'F' && row >= 39 && row <= 44){
                        Console.Write("     ");   
                    }
                    Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    maxColumnLengths[letter] = Math.Max(maxColumnLengths.GetValueOrDefault(letter), $"{letter}{row}".Length);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }

            Console.WriteLine();

            
            if (row == 2 || row == 9)
            {
                Console.WriteLine();

                // Display headers A to I
                Console.Write("    ");
                for (char letter = 'A'; letter <= LetterSeat; letter++)
                {   
                    if(letter == 'D' || letter =='G'){
                        Console.Write("     ");
                    }
                    Console.Write($"{letter,-4} ");
                }
                Console.WriteLine();
                Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
            }
            if (row == 38){
                Console.WriteLine();
                // Display headers A to G
                Console.Write("    ");
                for (char letter = 'A'; letter <= 'G'; letter++)
                {   
                    if(letter == 'C' || letter =='F'){
                        Console.Write("     ");
                    }
                    Console.Write($"{letter,-4} ");
                }
                Console.WriteLine();
                Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
            }
        }
        Console.WriteLine();
        Console.WriteLine(" Use arrow keys to navigate and press Enter to select a seat.");
        Color.Red(" Red:", false);
        Console.WriteLine(" Booked Seat.");
        Color.Green(" Green:", false);
        Console.WriteLine(" First-Class Seat.");
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
        Console.Clear();
        bool confirmBooking = Prices.TicketPrices(CurrentFlight); // Ask for confirmation after finishing the booking
        if (confirmBooking)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
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
            Start(CurrentFlight);
        }
    }
}

