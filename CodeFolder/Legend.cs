public static class Legend{
    public static void print(int row, char letter, Airplane airplane){
        if(airplane is Airbus330){
            if(row == 3 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| Use arrow keys to navigate and press Enter to select a seat.");
            }
            if(row ==4 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Red(" Red:", false);
                Console.Write(" Booked Seat.");
            }
            if(row == 5 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Green(" Green:", false);
                Console.Write($" Available  First Class Seat. Starting at: {Airbus330.FirstClassPrice}");
            }
            if(row == 6 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Yellow(" Yellow:", false);
                Console.Write($" Available Economy Class Seat with extra legroom.");
            }
            if(row == 7 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");
            }
            if(row == 8 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"|| White: Available Economy Class Seat. Starting at: {Airbus330.EconomyClassPrice}");
            }
            if(row == 9 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| BACKSPACE: To unselect a seat.");
            }
            if(row == 10 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("*");
                Console.Write(" Price will vary depending on the selected seat.*");
            }
            if(row == 11 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Window Seats have a price increase of 20% on top of the starting price.");
            }
            if(row == 12 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Extra legroom seats have a price increase of 30 euro's on top of the starting price.");
            }
        } else if(airplane is Boeing737){
            if(row == 1 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Console.Write(" Use arrow keys to navigate and press Enter to select a seat.");
            }
            if(row ==2 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Red(" Red:", false);
                Console.Write(" Booked Seat.");
            }
            if(row == 3 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Yellow(" Yelow:", false);
                Console.Write(" Available Economy Class Seat with extra legroom.");
            }
            if(row == 4 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Console.Write($" White: Available Economy Class Seat. Starting at: {Boeing737.EconomyClassPrice}");
            }
            if(row == 5 && letter == 'F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");

            }
            if(row == 6 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Console.Write(" Press ESC to finish the booking.");
            }
            if(row == 8 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" *");
                Console.Write(" Price will vary depending on the selected seat. *");
            }
            if(row == 9 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Window Seats have a price increase of 20% on top of the starting price.");
            }
            if(row == 10 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Extra legroom seats have a price increase of 30 euro's on top of the starting price.");
            }
        } else if(airplane is Boeing787){
            if(row ==1 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Red(" Red:", false);
                Console.Write(" Booked Seat.");
            }
            if(row == 2 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Green(" Green:", false);
                Console.Write($"  Available  First Class Seat. Starting at: {Boeing787.FirstClassPrice}");
            }
            if(row == 3 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Blue(" Blue:", false); 
                Console.Write($" Available Business Class Seat. Starting at: {Boeing787.BusinessClassPrice}");
            }
            if(row == 4 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");
            }
            if(row == 5 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||"); 
                Console.Write($" White: Available Economy Class Seat. Starting at: {Boeing787.EconomyClassPrice}");
            }
            if(row == 6 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Console.Write(" BACKSPACE: To unselect a seat.");
            }
        }
    }
}
