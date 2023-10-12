// // Purpose: Display a menu and allow the user to select an option.

// using System;

// namespace CodeFolder
// {
//     public class Menu
//     {
//         private int selectedOption = 0;
//         private string[] options = { "Appetizers", "Main Courses", "Exit" };

//         public void DisplayMenu()
//         {
//             while (true)
//             {
//                 Console.Clear();
//                 Console.WriteLine("Welcome to the Airline Menu!");
//                 Console.WriteLine("Please choose a category:");
//                 for (int i = 0; i < options.Length; i++)
//                 {
//                     if (i == selectedOption)
//                     {
//                         Console.BackgroundColor = ConsoleColor.Gray;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     Console.WriteLine((i + 1) + ". " + options[i]);
//                     Console.ResetColor();
//                 }

//                 ConsoleKeyInfo keyInfo = Console.ReadKey();

//                 switch (keyInfo.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         selectedOption = Math.Max(0, selectedOption - 1);
//                         break;

//                     case ConsoleKey.DownArrow:
//                         selectedOption = Math.Min(options.Length - 1, selectedOption + 1);
//                         break;

//                     case ConsoleKey.Enter:
//                         HandleMenuSelection(selectedOption + 1);
//                         break;
//                 }

//                 if (selectedOption == options.Length - 1)
//                 {
//                     Console.WriteLine("Thank you for visiting. Goodbye!");
//                     return;
//                 }
//             }
//         }

//         private void HandleMenuSelection(int choice)
//         {
//             switch (choice)
//             {
//                 case 1:
//                     DisplayAppetizers();
//                     break;
//                 case 2:
//                     DisplayMainCourses();
//                     break;
//                 case 3:
//                     Console.WriteLine("Thank you for visiting. Goodbye!");
//                     Environment.Exit(0);
//                     break;
//                 default:
//                     Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
//                     break;
//             }

//             Console.WriteLine("Press any key to continue...");
//             Console.ReadKey();
//         }

//         private void DisplayAppetizers()
//         {
//             Console.Clear();
//             Console.WriteLine("**Appetizers**");
//             Console.WriteLine("1. Spinach and Artichoke Dip");
//             Console.WriteLine("2. Garlic Parmesan Wings");
//             Console.WriteLine("3. Caprese Salad");
//             Console.WriteLine();
//         }

//         private void DisplayMainCourses()
//         {
//             Console.Clear();
//             Console.WriteLine("**Main Courses**");
//             Console.WriteLine("4. Grilled Salmon");
//             Console.WriteLine("5. Chicken Alfredo");
//             Console.WriteLine("6. Vegetarian Stir-Fry");
//             Console.WriteLine();
//         }
//     }
// }
