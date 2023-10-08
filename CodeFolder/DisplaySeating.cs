using Newtonsoft.Json;

public class DisplaySeating 
{
    static int cursorRow = 0;
    static int cursorSeat = 0;
    private List<Seat> bookedSeats = new List<Seat>();

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
        LoadBookedSeatsFromJson("booked_seats.json"); 
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

                case ConsoleKey.Backspace:
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
        bool confirmBooking = ConfirmBooking(); // Ask for confirmation after finishing the booking

        if (confirmBooking)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
            SaveBookedSeatsToJson("booked_seats.json"); // Specify the desired file path
            
        }
        else
        {
            // Roll back the booked seats to available
            foreach (var seat in bookedSeats)
            {
                seat.ResetBooking();
            }
            Console.Clear();
            Console.WriteLine("Booking canceled. Selected seats are now available.");
            Console.WriteLine();
            Start();
        }
    }

    public void InitializeSeats()
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
        Console.WriteLine("'Red': Booked Seat.");
        Console.WriteLine("'White'': Available Seat.");
        Console.WriteLine("Press ESC to finish the booking.");
    }



    public void SelectAndBookSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat != null)
        {
            selectedSeat.Book();
            bookedSeats.Add(selectedSeat);
        }
    }
    // public void SelectAndBookSeat()
    // {
    //     Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

    //     if (selectedSeat != null)
    //     {
    //         // Display confirmation screen after finishing the booking
    //         bool confirmBooking = ConfirmBooking();

    //         if (confirmBooking)
    //         {
    //             selectedSeat.Book();
    //             bookedSeats.Add(selectedSeat); // Add the selected and booked seat to the list
    //         }
    //         else
    //         {
    //             Console.WriteLine("Booking canceled. Selected seat is now available.");
    //         }

    //         DisplaySeats(); // Display the seats again after the booking decision
    //     }
    // }    

    public void UnselectSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat != null)
        {
            // Unselect the seat
            Console.WriteLine($"Seat {selectedSeat} unselected.");
            selectedSeat.ResetSeat(); // Assuming you have a method to unbook the seat in your Seat class
            bookedSeats.Remove(selectedSeat); // Remove the seat from the bookedSeats list
        }

        DisplaySeats(); // Display the seats again without the selection
    }


    public void LoadBookedSeatsFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            // updates current list of seat or starts a new empty list
            bookedSeats = JsonConvert.DeserializeObject<List<Seat>>(json) ?? new List<Seat>();
        }
    }

    public void SaveBookedSeatsToJson(string filePath)
    {
        // Save the bookedSeats list to a JSON file
        string json = JsonConvert.SerializeObject(bookedSeats, Formatting.Indented);
        File.WriteAllText(filePath, json);

        //Console.WriteLine($"Booked seats saved to {filePath}");
    }
    public bool ConfirmBooking()
    {
        Console.WriteLine("Confirmation Screen:");
        Console.WriteLine("Selected Seats:");

        foreach (var seat in bookedSeats)
        {
            Console.WriteLine(seat);
            
            // gotta include the price but, have to change the Seat class constructor also the inittializedseat methode 
        }

        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();

        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
    }

    public void MoveUp()
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

    public void MoveDown()
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

    public void MoveLeft()
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

    public void MoveRight()
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
    public void RedrawSeats()
    {
        Console.SetCursorPosition(0, 0);
        DisplaySeats();
    }
    
}
