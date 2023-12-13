using System.Reflection;
using Newtonsoft.Json;

public class Boeing787 : Airplane
{
    public static int FirstClassPrice = 1000;
    public static int BusinessClassPrice = 750;
    public static int EconomyClassPrice = 500;
    public Boeing787(char letter, int numbers) : base (letter, numbers) {}
    public override void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice)
    {
        // Initialize first-class seats
        for (char letter = 'A'; letter <= 'F'; letter++)
        {
            for (int row = 1; row <= 6; row++)
            {
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'F') ? (int)(firstClassPrice * 1.2) : firstClassPrice;

                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new FirstClass("First Class", letter, row, false, seatPrice);
                }
            }
        }

        // Initialize business-class seats
        for (char letter = 'A'; letter <= 'I'; letter++)
        {
            for (int row = 7; row <= 16; row++)
            {
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(businessClassPrice * 1.2) : businessClassPrice;

                if (existingSeat != null)
                {
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else
                {
                    new BusinessClass("Business Class", letter, row, false, seatPrice);
                }
            }
        }
        // Initialize economy-class seats
        for (char letter = 'A'; letter <= 'I'; letter++)
        {
            for (int row = 17; row <= 28; row++)
            {
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2) : economyClassPrice;

                if (existingSeat != null)
                {
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else
                {
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
    }
    public override void SetPrices(int firstClassPrice, int businessClassPrice, int economyClassPrice)
    {
        FirstClassPrice = firstClassPrice;
        BusinessClassPrice = businessClassPrice;
        EconomyClassPrice = economyClassPrice;
        InitializeSeats(firstClassPrice, businessClassPrice, economyClassPrice);
    }
    public override void SetClassPrices(){
        int firstclassPrice, businessclassPrice, economyclassPrice;
        do{
            Console.WriteLine($"This is the current First Class seat price: {FirstClassPrice}");
            Console.WriteLine("What is the new First Class seat price (positive number only): ");
            Console.Write(">>> ");
        } while (!int.TryParse(Console.ReadLine(), out firstclassPrice) || firstclassPrice <= 0);
        do{
            Console.WriteLine($"This is the current Business Class seat price: {BusinessClassPrice}");
            Console.WriteLine("What is the new Business Class seat price (positive number only): ");
            Console.Write(">>> ");
        } while (!int.TryParse(Console.ReadLine(), out businessclassPrice) || businessclassPrice <= 0);
        do{
            Console.WriteLine($"This is the current Economy Class seat price: {EconomyClassPrice}");
            Console.WriteLine("What is the new Economy Class seat price (positive number only): ");
            Console.Write(">>> ");
        } while (!int.TryParse(Console.ReadLine(), out economyclassPrice) || economyclassPrice <= 0);
        Console.Clear();
        Console.WriteLine("The new prices has been set.");
        Console.WriteLine($"First Class seat price: {firstclassPrice}.");
        Console.WriteLine($"Business Class seat price: {businessclassPrice}.");
        Console.WriteLine($"Economy Class seat price: {economyclassPrice}.");
        Console.WriteLine();
        Console.WriteLine("Press any button to continue.");
        Console.ReadKey();
        SetPrices(firstclassPrice, businessclassPrice, economyclassPrice);
    }

    public override void DisplaySeats()
    {
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 3;
        Console.Write("    ");
        for (char letter = 'A'; letter <= 'F'; letter++)
        {
            if(letter == 'C' || letter == 'E'){
                Console.Write("          ");
            }
            Console.Write($"{letter,-4} ");
        }
        Console.WriteLine();

        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+"); 

        Dictionary<char, int> maxColumnLengths = new Dictionary<char, int>();

        for (int row = 1; row <= NumberOfRows; row++)
        {
            Console.Write($" {row,2}|");

            for (char letter = 'A'; letter <= LetterSeat; letter++)
            {
                Seat? seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);

                if (seat != null)
                {
                    if (cursorRow == row && cursorSeat == letter - 'A')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    if (letter >= 'A' && letter <= 'F' && row <= 6)
                    {
                        // First-class seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.Green;
                    }
                    else if (letter <= LetterSeat && row >= 7 && row <= 16)
                    {
                        // Business class seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.Blue;
                    }
                    else if (letter <= LetterSeat && row >= 17 && row <= 28)
                    {
                        // Economy class seats
                        Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.White;
                    }
                    if(letter == 'C'  && row <= 6|| letter == 'E'  && row <= 6){
                        Console.Write("          ");
                    }
                    if(letter == 'D'  && row > 6 && row <= 28|| letter == 'G'  && row > 6 && row <= 28){
                        Console.Write("     ");
                        // Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    }
                    Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    if(row == 1 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Console.Write(" Use arrow keys to navigate and press Enter to select a seat.");
                    }
                    if(row ==2 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Color.Red(" Red:", false);
                        Console.Write(" Booked Seat.");
                    }
                    if(row == 3 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Color.Green(" Green:", false);
                        Console.Write(" Available  First-Class Seat.");
                    }
                    if(row == 4 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Color.Blue(" Blue:", false);
                        Console.Write(" Available Business Class Seat.");
                    }
                    if(row == 5 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Console.Write(" White: Available Economy Seat.");
                    }
                    if(row == 6 && letter =='F'){
                        Console.ResetColor();
                        Console.Write("\t  ||");
                        Console.Write(" BACKSPACE: To unselect a seat.");
                    }

                    maxColumnLengths[letter] = Math.Max(maxColumnLengths.GetValueOrDefault(letter), $"{letter}{row}".Length);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }

            Console.WriteLine();

            // Skip 2 lines after displaying seats 6 and 16
            if (row == 6){
                int numberOfTabs = 40;
                string message = "\t\t  || Press ESC to finish the booking.";
                string indentedMessage = message.PadLeft(message.Length + numberOfTabs);
                Console.WriteLine(indentedMessage);
            }
            if (row == 16){
                Console.WriteLine();
            }
            if (row == 6)
            {   
                // Display headers A to I after row 6
                Console.Write("    ");
                for (char letter = 'A'; letter <= LetterSeat; letter++)
                {   
                    if(letter == 'D' || letter =='G'){
                        Console.Write("     ");
                    }
                    Console.Write($"{letter,-4} ");
                }
                Console.Write("* Price will vary depending on the selected seat. *");
                Console.WriteLine();
                Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
            }
            if (row == 16)
            {   
                // Display headers A to I after row 6
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
        }

        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
    }
    public override void Start(Flight CurrentFlight)
    {
        string new_filepath = $"DataSources/{CurrentFlight.FlightId}.json";
        cursorRow = 1;  
        cursorSeat = 0; 
        bookedSeats.Clear();
        // TemporarlySeat.Clear();
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