using Newtonsoft.Json;

public class DisplaySeating 
{
    static int cursorRow = 0;
    static int cursorSeat = 0;
    public static List<Seat> bookedSeats = new List<Seat>();
    public static List<Seat> TemporarlySeat = new List<Seat>();

    public char LetterSeat { get; private set; }
    public int NumberOfRows { get; private set; }       

    public DisplaySeating(char letterseat, int numberofrows)
    {
        LetterSeat = letterseat;
        NumberOfRows = numberofrows;
    }
    public void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice)
{
    for (char letter = 'A'; letter <= LetterSeat; letter++)
    {
        for (int row = 1; row <= NumberOfRows; row++)
        {
            Seat? existingSeat = bookedSeats.Find(s => s.Row == row && s.Letter == letter);

            if (existingSeat != null)
            {
                // The seat is already booked (based on the JSON data)
                new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
            }
            else
            {
                // The seat is not in the list of booked seats (initialize as unbooked)
                if (row >= 1 && row <= 6)
                {
                    new FirstClass("First Class", letter, row, false, firstClassPrice);
                }
                else if (row >= 7 && row <= 14)
                {
                    new BusinessClass("Business Class", letter, row, false, businessClassPrice);
                }
                else
                {
                    new EconomyClass("Economy Class", letter, row, false, economyClassPrice);
                }
            }
        }
    }
}

    public void Start()
    {
        cursorRow = 1;  
        cursorSeat = 0; 
        LoadBookedSeatsFromJson("DataSources/booked_seats.json"); 
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
        bool confirmBooking = ConfirmBooking(); // Ask for confirmation after finishing the booking

        if (confirmBooking)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Booking completed. Thank you!");
            Console.WriteLine();
            SaveBookedSeatsToJson("DataSources/booked_seats.json"); // Specify the desired file path
            TemporarlySeat.Clear();
            
        }
        else
        {
            // Roll back the booked seats to available
            foreach (var seat in bookedSeats)
            {
                seat.ResetSeat();
            }
            Console.Clear();
            Console.WriteLine("Booking canceled. Selected seats are now available.");
            Console.WriteLine();
            TemporarlySeat.Clear();
            Start();
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
        Console.WriteLine("'BACKSPACE': To unselect a seat.");
        Console.WriteLine("Press ESC to finish the booking.");
        Console.WriteLine();
        
    }
   

    // public void DisplaySeats()
    // {
    //     // Calculate the total width of the seating arrangement
    //     int totalWidth = (LetterSeat - 'A' + 1) * 6 + 20;

    //     Console.Write("    ");
    //     for (char letter = 'A'; letter <= LetterSeat; letter++)
    //     {
    //         Console.Write($"{letter,-7} ");
    //     }
    //     Console.WriteLine();

    //     Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");

    //     for (int row = 1; row <= NumberOfRows; row++)
    //     {
    //         Console.Write($" {row,2}|");

    //         for (char letter = 'A'; letter <= LetterSeat; letter++)
    //         {
    //             Seat? seat = Seat.Seats.Find(s => s.Row == row && s.Letter == letter);

    //             if (seat != null)
    //             {
    //                 if (cursorRow == row && cursorSeat == letter - 'A')
    //                 {
    //                     Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color for the selected seat
    //                 }

    //                 // Check if the seat is in the list of booked seats
    //                 bool isBooked = bookedSeats.Contains(seat);

    //                 // Set the text color to red if the seat is booked
    //                 Console.ForegroundColor = isBooked ? ConsoleColor.Red : ConsoleColor.White;

    //                 // Display the seat letter and number with dynamic spacing for better alignment
    //                 Console.Write(isBooked ? $"{letter}{row,-6} " : $"{letter}{row,-6} ");

    //                 // Reset text and background color after printing the current seat
    //                 Console.ForegroundColor = ConsoleColor.White;
    //                 Console.BackgroundColor = ConsoleColor.Black;
    //             }
    //         }

    //         Console.WriteLine();
    //     }

    //     Console.WriteLine($"  +{new string('-', totalWidth - 3)}+");
    //     Console.WriteLine("Use arrow keys to navigate and press Enter to select a seat.");
    //     Console.WriteLine("'Red': Booked Seat.");
    //     Console.WriteLine("'White'': Available Seat.");
    //     Console.WriteLine("'BACKSPACE': To unselect a seat.");
    //     Console.WriteLine("Press ESC to finish the booking.");
    //     Console.WriteLine();
    // }


    public void SelectAndBookSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat != null && selectedSeat.Booked == false)
        {
            selectedSeat.Book();
            TemporarlySeat.Add(selectedSeat);
            //selectedSeat.ShowSeat();
            //bookedSeats.Add(selectedSeat);
        }
    } 

    public void UnselectSeat()
    {
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));

        if (selectedSeat != null)
        {
            // Unselect the seat
            Console.WriteLine($"Seat: {selectedSeat.Letter}{selectedSeat.Row} unselected.");
            selectedSeat.ResetSeat(); // you have a method to unbook the seat in your Seat class
            bookedSeats.Remove(selectedSeat); // Remove the seat from the bookedSeats list
        }

        // DisplaySeats(); // Display the seats again without the selection
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

        foreach (var seat in TemporarlySeat)
        {
            Console.WriteLine(seat.ShowSeat());
            
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
        // Console.SetCursorPosition(0, 0);
        DisplaySeats();
    }
    
}