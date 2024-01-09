using System.Reflection;
using Newtonsoft.Json;
using Spectre.Console;

public class Boeing787 : Airplane
{
    public override int FirstClassPrice {get;set;} = 250;
    public override int BusinessClassPrice{get;set;} = 90;
    public override int EconomyClassPrice{get;set;} = 50;

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
        for (char letter = 'A'; letter <= 'I'; letter++){
            for (int row = 7; row <= 16; row++){
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(businessClassPrice * 1.2) : businessClassPrice;
                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new BusinessClass("Business Class", letter, row, false, seatPrice);
                }
            }
        }
        // Initialize economy-class seats
        for (char letter = 'A'; letter <= 'I'; letter++){
            for (int row = 17; row <= 28; row++){
                Seat? existingSeat = Airplane.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2) : economyClassPrice;
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
        Console.WriteLine($"This is the current Business Class seat price: {BusinessClassPrice}");
        Console.WriteLine("What is the new Business Class seat price (positive number only): ");
        do{
            Console.Write(">>> ");
            input = Console.ReadLine()!;
            MainMenu.Return(input);
        } while (!int.TryParse(input, out businessclassPrice) || businessclassPrice <= 0);
        Console.WriteLine($"This is the current Economy Class seat price: {EconomyClassPrice}");
        Console.WriteLine("What is the new Economy Class seat price (positive number only): ");
        do{
            Console.Write(">>> ");
            input = Console.ReadLine()!;
            MainMenu.Return(input);
        } while (!int.TryParse(input, out economyclassPrice) || economyclassPrice <= 0);
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
        Color.Green("                   [First class Seat]", false);
        // Console.ResetColor();
        Console.WriteLine();
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

        Console.Write($"  +{new string('-', totalWidth - 3)}+"); 
        Console.WriteLine("|| Use arrow keys to navigate and press Enter to select a seat.");

        Dictionary<char, int> maxColumnLengths = new Dictionary<char, int>();

        for (int row = 1; row <= NumberOfRows; row++)
        {
            Console.Write($" {row,2}|");

            for (char letter = 'A'; letter <= LetterSeat; letter++)
            {
                Seat? seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);

                if (seat != null)
                {

                    SeatColoring.SetColor(cursorRow, row, cursorSeat, letter, TemporarlySeat, seat, LetterSeat, this);


                    if(letter == 'C'  && row <= 6|| letter == 'E'  && row <= 6){
                        Console.Write("          ");
                    }
                    if(letter == 'D'  && row > 6 && row <= 28|| letter == 'G'  && row > 6 && row <= 28){
                        Console.Write("     ");
                        // Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");
                    }
                    Console.Write(seat.Booked ? $"{letter}{row,-3} " : $"{letter}{row,-3} ");

                    Legend.print(row, letter, this);

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
                Console.WriteLine();
                Color.Blue("                   [Business Class Seat]", false);
                Console.WriteLine();
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
                Console.Write($"  +{new string('-', totalWidth - 3)}+");
                Console.WriteLine("|| - Window Seats have a price increase of 20% on top of the starting price.");
            }
            if (row == 16)
            {   
                Console.WriteLine();
                Console.WriteLine("                   [Economy Class Seat]");
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
}