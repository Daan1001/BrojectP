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

    // public GenerateSeat(char letter, int row, bool booked ) 
    // {}
    public void Start()
    {
        cursorRow = 1;  
        cursorSeat = 0; 
        InitializeSeats();
        DisplaySeats();
        

        Console.WriteLine("Use arrow keys to navigate and press Enter to select a seat.");
        Console.WriteLine("X: Booked Seat");
        Console.WriteLine("O: Available Seat");

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
        Console.Write("  ");
        for (char letter = 'A'; letter <= LetterSeat; letter++)
        {
            Console.Write($"{letter} ");
        }
        Console.WriteLine();

        Console.WriteLine("  +---------");

        for (int row = 1; row <= NumberOfRows; row++)
        {
            Console.Write($" {row}|");

            for (char letter = 'A'; letter <= LetterSeat; letter++)
            {
                Seat seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);

                if (seat != null)
                {
                    if (cursorRow == row && cursorSeat == letter - 'A')
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }

                    Console.Write(seat.Booked ? "X " : "O ");

                    // Reset background color after printing the current seat
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("  +---------");
    }

    static void SelectAndBookSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter - 'A' == cursorSeat);

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
        if (cursorSeat > 'A')
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
        if (cursorSeat < LetterSeat)
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
        Console.Clear();
        DisplaySeats();
    }
    
}