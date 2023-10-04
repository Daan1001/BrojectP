using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json; 

public class Menu{
    private static int selectedOption = 0;
    private static int hoveringOption = 0;
    private User user;
    private static string[] options = { "Log in", "Sign in", "Account information","Book a flight", "Leave a review", "Exit"};
    public static ConsoleKeyInfo keyInfo;
    // Seat[,] seats = new Seat[6, 4]; // Initialize an imaginary 6x4 plane seat layout

    // private void AddOptions(){

    // }
    public static void Start(){
        Boolean stop = false;
        while(!stop){
            Console.Clear();
            for (int i = 0; i < options.Length; i++){
                if (i == hoveringOption){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }
            // Read key press
            keyInfo = Console.ReadKey();
            // Process arrow keys
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    hoveringOption = Math.Max(0, hoveringOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    hoveringOption = Math.Min(options.Length - 1, hoveringOption + 1);
                    break;
                case ConsoleKey.Enter:
                    selectedOption = hoveringOption;
                    Action(selectedOption);
                    Console.WriteLine();
                    Console.ReadKey();
                    break;
            }
            stop = options[selectedOption] == options[options.Count()-1];
        }
    }
    public static void Action(int selectedOption){
        switch (selectedOption){
            case 0: // log in
                Console.WriteLine("Still a W.I.P.");
                break;
            case 1: // sign in
                User.NewUser();
                break;
            case 2: // account information
                Console.WriteLine("Still a W.I.P.");
                break;
            case 3: // booking flight
                Console.WriteLine("Still a W.I.P.");
                break;
            case 4: // leave a review
                Console.WriteLine("Still a W.I.P.");
                break;
        }
    }

}

public class Program{
    public static void Main(){
        Menu.Start();
        // User.NewUser();
    }
}
// public class Seat{
//     public Boolean isBooked = false;
// }
// class Program
// {
//     static void Main()
//     {
//         int selectedOption = 0;
//         string[] options = { "option1", "option2", "Option 3" };

//         char[,] seats = new char[6, 4]; // Initialize an imaginary 6x4 plane seat layout

//         while (true)
//         {
//             Console.Clear();

//             // Display options
//             for (int i = 0; i < options.Length; i++)
//             {
//                 if (i == selectedOption)
//                 {
//                     Console.BackgroundColor = ConsoleColor.Gray;
//                     Console.ForegroundColor = ConsoleColor.Black;
//                 }

//                 Console.WriteLine(options[i]);

//                 // Reset colors
//                 Console.ResetColor();
//             }

//             // Read key press
//             ConsoleKeyInfo keyInfo = Console.ReadKey();
//             // Process arrow keys
//             switch (keyInfo.Key)
//             {
//                 case ConsoleKey.UpArrow:
//                     selectedOption = Math.Max(0, selectedOption - 1);
//                     break;
//                 case ConsoleKey.DownArrow:
//                     selectedOption = Math.Min(options.Length - 1, selectedOption + 1);
//                     break;
//                 case ConsoleKey.Enter:
//                     switch (selectedOption)
//                     {
//                         case 0:
//                             CreateAccount();
//                             break;
//                         case 1:
//                             char[,]  seat = new char[6, 4];
//                             DisplaySeat(seat);
//                             break;
//                         case 2:
//                             // Perform action for Option 3
//                             Console.WriteLine("\nSelected: " + options[selectedOption]);
//                             break;
//                     }
//                     break;
//             }
//         }
//     }

//     static void CreateAccount()
//     {
//         Console.Clear();
//         Console.WriteLine("Create Account\n");

//         Console.Write("Enter First Name: ");
//         string firstName = Console.ReadLine();

//         Console.Write("Enter Last Name: ");
//         string lastName = Console.ReadLine();

//         Console.Write("Enter Phone Number: ");
//         string phoneNumber = Console.ReadLine();

//         // You can perform additional actions with the entered data, such as storing it in a database.

//         Console.WriteLine("\nAccount Created:");
//         Console.WriteLine("First Name: " + firstName);
//         Console.WriteLine("Last Name: " + lastName);
//         Console.WriteLine("Phone Number: " + phoneNumber);

//         Console.WriteLine("\nPress Enter to continue...");
//         Console.ReadLine();
//     }
    
//     static void DisplaySeat(char[,] seat)
//     {
//         Console.Clear();
//         Console.WriteLine("Imaginary Plane Seat");

//         // Display row numbers
//         Console.Write("   "); // Adjust spacing for alignment with letters
//         for (int col = 0; col < seat.GetLength(1); col++)
//         {
//             Console.Write($"{(char)('A' + col),3} ");
//         }
//         Console.WriteLine();

//         // Display seat layout
//         for (int i = 0; i < seat.GetLength(0); i++)
//         {
//             Console.Write($"{i + 1,2} "); // Use two spaces for row numbers

//             for (int j = 0; j < seat.GetLength(1); j++)
//             {
//                 if (seat[i, j] == '\0')
//                 {
//                     Console.Write($" [ ] ");
//                 }
//                 else
//                 {
//                     Console.Write($" [{seat[i, j]}] ");
//                 }
//             }
//             Console.WriteLine(); // Move to the next row
//         }

//         Console.WriteLine("\nInstructions:");
//         Console.WriteLine("1. Select seats by entering the seat coordinates (e.g., A1, B2).");
//         Console.WriteLine("2. Press Enter after each seat selection.");
//         Console.WriteLine("3. Type 'CONFIRM' and press Enter to save and exit.");

//         List<string> selectedSeats = new List<string>();

//         while (true)
//         {
//             Console.Write("Enter seat (e.g., A1): ");
//             string input = Console.ReadLine().Trim().ToUpper();

//             if (string.IsNullOrEmpty(input))
//             {
//                 // Handle case where Enter is pressed without providing a seat selection
//                 Console.WriteLine("Invalid input. Please enter a valid seat or type 'Confirm' to save and exit.");
//                 continue;
//             }

//             if (input.Equals("CONFIRM", StringComparison.OrdinalIgnoreCase))
//             {
//                 // SaveSelectedSeats(selectedSeats);
//                 Console.WriteLine("Selected seats saved to seats.json. Press Enter to exit.");
//                 break;
//             }

//             if (ValidateSeatSelection(input, seat, out int row, out int col))
//             {
//                 seat[row, col] = 'X';
//                 selectedSeats.Add(input);
//                 DisplaySeat(seat);
//             }
//             else
//             {
//                 Console.WriteLine("Invalid seat selection. Please try again.");
//             }
//         }
//     }

//     static bool ValidateSeatSelection(string input, char[,] seat, out int row, out int col)
//     {
//         row = -1;
//         col = -1;

//         if (input.Length >= 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
//         {
//             col = char.ToUpper(input[0]) - 'A';
//             row = int.Parse(input.Substring(1)) - 1;

//             if (row >= 0 && row < seat.GetLength(0) && col >= 0 && col < seat.GetLength(1) && seat[row, col] == '\0')
//             {
//                 return true;
//             }
//         }

//         return false;
//     }

//     // static void SaveSelectedSeats(List<string> selectedSeats)
//     // {
//     //     // Convert the list of selected seats to a JSON string
//     //     string json = JsonConvert.SerializeObject(selectedSeats);

//     //     // Write the JSON string to a file
//     //     File.WriteAllText("seats.json", json);
//     // }
// }