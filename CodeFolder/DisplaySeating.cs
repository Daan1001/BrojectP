public class DisplaySeating 
{
    static int cursorRow = 0;
    static int cursorSeat = 0;

    public char LetterSeat { get; private set; }
    public int NumberOfRows { get; private set; }       

    public DisplaySeating(char letterseat, int numberofrows)
    {
        this.LetterSeat = letterseat;
        this.NumberOfRows = numberofrows;
    }

    public void Start()
    {
        cursorRow = 1;  
        cursorSeat = 0; 
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
                    SelectAndBookSeat();
                    DisplaySeats();
                    break;

                case ConsoleKey.Escape:
                    isBookingComplete = true;
                    Console.WriteLine("Booking completed. Thank you!");
                    break;

                default:
                    Console.WriteLine("Invalid input. Please use arrow keys to navigate.");
                    break;
            }
        }
    }

    void InitializeSeats()
    {
        for (char letter = 'A'; letter <= this.LetterSeat; letter++)
        {
            for (int row = 1; row <= this.NumberOfRows; row++)
            {
                new Seat(letter, row, false);
            }
        }
    }
   public void DisplaySeats()
    {
        // Calculate the total width of the seating arrangement
        int totalWidth = (LetterSeat - 'A' + 1) * 6 + 20;

        Console.Write("    ");
        for (char letter = 'A'; letter <= LetterSeat; letter++)
        {
            Console.Write($"{letter,-7} ");
        }
        Console.WriteLine();

        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");

        // Dictionary to store the maximum length of seat identifier for each column
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
                        Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color for the selected seat
                    }

                    // Set the text color to red if the seat is booked
                    Console.ForegroundColor = seat.Booked ? ConsoleColor.Red : ConsoleColor.White;

                    // Display the seat letter and number with dynamic spacing for better alignment
                    Console.Write(seat.Booked ? $"{letter}{row,-6} " : $"{letter}{row,-6} ");

                    // Update the maximum length for the current column
                    maxColumnLengths[letter] = Math.Max(maxColumnLengths.GetValueOrDefault(letter), $"{letter}{row}".Length);

                    // Reset text and background color after printing the current seat
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
        Console.WriteLine("Use arrow keys to navigate and press Enter to select a seat.");
        Console.WriteLine("'Red': Booked Seat");
        Console.WriteLine("'White'': Available Seat");
    }



    void SelectAndBookSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat != null)
        {
            selectedSeat.Book();
        }
    }

    void MoveUp()
    {
        if (cursorRow > 1)
        {
            cursorRow--;
            RedrawSeats();
        }
        else 
        {
            RedrawSeats();
        }
    }

    void MoveDown()
    {
        if (cursorRow < this.NumberOfRows)
        {
            cursorRow++;
            RedrawSeats();
        }
        else 
        {
            RedrawSeats();
        }
    }

     void MoveLeft()
    {
        if (cursorSeat > 0)
        {
            cursorSeat--;
            RedrawSeats();
        }
        else 
        {
            RedrawSeats();
        }
    }

    void MoveRight()
    {
        if (cursorSeat < LetterSeat - 'A')
        {
            cursorSeat++;
            RedrawSeats();
        }
        else 
        {
            RedrawSeats();
        }
    }
    void RedrawSeats()
    {
        Console.SetCursorPosition(0, 0);
        DisplaySeats();
    }
    
}
