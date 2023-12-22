using Newtonsoft.Json;

public class Boeing737 : Airplane
{
    public override int FirstClassPrice {get;set;} = 0;
    public override int BusinessClassPrice{get;set;} = 0;
    public override int EconomyClassPrice{get;set;} = 50;

    public Boeing737(char letter, int numbers) : base (letter, numbers) {}
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
    public override void DisplaySeats(){
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

                    SeatColoring.SetColor(cursorRow, row, cursorSeat, letter, TemporarlySeat, seat, LetterSeat, this);

                    if (letter == 'D') {
                        // Add an extra space after column C
                        Console.Write("   ");
                    }

                    // Display the seat letter and number with dynamic spacing for better alignment
                    Console.Write(seat.Booked ? $"|{letter}{row,-2}| " : $"|{letter}{row,-2}| ");

                    Legend.print(row, letter, this);
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
    public override void UpdateSeat(Flight currentFlight){
        string new_filePath = $"DataSources/{currentFlight.FlightId}.json";
        bookedSeats.Clear();
        Seat.Seats.Clear();
        LoadBookedSeatsFromJson(new_filePath); 
        InitializeSeats(FirstClassPrice, BusinessClassPrice, EconomyClassPrice);
        DisplaySeats();
    }
    public override void Start(Flight currentFlight){
        Console.Clear();
        UpdateSeat(currentFlight);
        base.Start(currentFlight);
    }
}