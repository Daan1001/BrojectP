using Newtonsoft.Json;
public class Airbus330 : Airplane
{   
    public override int FirstClassPrice {get;set;} = 250;
    public override int BusinessClassPrice{get;set;} = 0;
    public override int EconomyClassPrice{get;set;} = 50;
    public Airbus330(char letter, int numbers) : base(letter, numbers){}
    public override void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice)
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
    public override void SetClassPrices(){
        int firstclassPrice, businessclassPrice, economyclassPrice;
        Console.WriteLine("Type \"Cancel\" or \"Quit\" anytime when asked for input anytime to cancel.");
        Console.WriteLine($"This is the current First Class seat price: {FirstClassPrice}");
        Console.WriteLine("What is the new First Class seat price (positive number only): ");
        String input;
        do{
            Console.Write(">>> ");
            input = Console.ReadLine()!;
            MainMenu.Return(input);
        } while (!int.TryParse(input, out firstclassPrice) || firstclassPrice <= 0);
        Console.WriteLine($"This is the current Economy Class seat price: {EconomyClassPrice}");
        Console.WriteLine("What is the new Economy Class seat price (positive number only): ");
        do{
            Console.Write(">>> ");
            input = Console.ReadLine()!;
            MainMenu.Return(input);
        } while (!int.TryParse(input, out economyclassPrice) || economyclassPrice <= 0);
        Console.Clear();
        SetPrices(firstclassPrice, 0, economyclassPrice);
        if(FirstClassPrice == firstclassPrice && BusinessClassPrice ==0 &&  EconomyClassPrice == economyclassPrice ){
            Console.WriteLine("The new prices has been set.");
            Console.WriteLine($"First Class seat price: {firstclassPrice}.");
            Console.WriteLine($"Economy Class seat price: {economyclassPrice}.");
            Console.WriteLine();
            Console.WriteLine("Press any button to continue.");
            Console.ReadKey();
        }
        else{
            Console.WriteLine("NOPEEEEE it does not work");
            Console.ReadKey();
        }
        

    }
    public override void DisplaySeats()
    {   
        Color.Green("                  [First class Seat]", false);
        // Console.ResetColor();
        Console.WriteLine();
        
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 3;
        Console.Write("    ");
        for (char letter = 'A'; letter <= 'F'; letter++){
            if(letter == 'C' || letter == 'E'){
                Console.Write("          ");
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

                if (seat is not null)
                {

                    SeatColoring.SetColor(cursorRow, row, cursorSeat, letter, TemporarlySeat, seat, LetterSeat, this);

                    if(letter == 'C'  && row <= 2|| letter == 'E'  && row <= 2){
                        Console.Write("          ");
                    }
                    if(letter == 'D'  && row >= 3 && row <= 38|| letter == 'G' && row >= 3 && row <= 38){
                        Console.Write("     ");
                        // Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    }
                    if(letter =='C' && row >= 39 && row <= 44 || letter == 'F' && row >= 39 && row <= 44){
                        Console.Write("          ");   
                    }
                    Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");

                    Legend.print(row, letter, this);
                    
                    maxColumnLengths[letter] = Math.Max(maxColumnLengths.GetValueOrDefault(letter), $"{letter}{row}".Length);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.WriteLine();
            if (row == 2 || row == 9){
                Console.WriteLine();
                Console.WriteLine("                  [Economy seat class]");
                // Display headers A to I
                Console.Write("    ");
                for (char letter = 'A'; letter <= LetterSeat; letter++){   
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
                Console.WriteLine("                  [Economy seat class]");
                // Display headers A to I
                // Display headers A to G
                Console.Write("    ");
                for (char letter = 'A'; letter <= 'G'; letter++){   
                    if(letter == 'C' || letter =='F'){
                        Console.Write("          ");
                    }
                    Console.Write($"{letter,-4} ");
                }
                Console.WriteLine();
                Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
            }
        }
        Console.WriteLine();
    }
}