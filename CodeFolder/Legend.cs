public static class Legend{
    public static void print(int row, char letter, Airplane airplane){
        if(airplane is Airbus330){
            if(row == 3 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| Use arrow keys to navigate and press Enter to select a seat.");
            }
            else if(row ==4 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Red(" Red:", false);
                Console.Write(" Booked Seat.");
            }
            else if(row == 5 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Green(" Green:", false);
                Console.Write($" Available  First Class Seat. Starting at: {Airplane.airbus330.FirstClassPrice}");
            }
            else if(row == 6 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Yellow(" Yellow:", false);
                Console.Write($" Available Economy Class Seat with extra legroom.");
            }
            else if(row == 7 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");
            }
            else if(row == 8 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"|| White: Available Economy Class Seat. Starting at: {Airplane.airbus330.EconomyClassPrice}");
            }
            else if(row == 9 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| BACKSPACE: To unselect a seat.");
            }
            else if(row == 10 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("*");
                Console.Write(" Price will vary depending on the selected seat.*");
            }
            else if(row == 11 && letter =='I'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Window Seats have a price increase of 20% on top of the starting price.");
            }
            else if(row == 12 && letter =='I'){
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
            else if(row ==2 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Red(" Red:", false);
                Console.Write(" Booked Seat.");
            }
            else if(row == 3 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Yellow(" Yelow:", false);
                Console.Write(" Available Economy Class Seat with extra legroom.");
            }
            else if(row == 4 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Console.Write($" White: Available Economy Class Seat. Starting at: {Airplane.boeing737.EconomyClassPrice}");
            }
            else if(row == 5 && letter == 'F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");

            }
            else if(row == 6 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("||");
                Console.Write(" Press ESC to finish the booking.");
            }
            else if(row == 8 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" *");
                Console.Write(" Price will vary depending on the selected seat. *");
            }
            else if(row == 9 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("|| - Window Seats have a price increase of 20% on top of the starting price.");
            }
            else if(row == 10 && letter =='F'){
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
            else if(row == 2 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Green(" Green:", false);
                Console.Write($"  Available  First Class Seat. Starting at: {Airplane.boeing787.FirstClassPrice}");
            }
            else if(row == 3 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Blue(" Blue:", false); 
                Console.Write($" Available Business Class Seat. Starting at: {Airplane.boeing787.BusinessClassPrice}");
            }
            else if(row == 4 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Color.Magenta(" Magenta:", false);
                Console.Write($" Your current selected seats.");
            }
            else if(row == 5 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||"); 
                Console.Write($" White: Available Economy Class Seat. Starting at: {Airplane.boeing787.EconomyClassPrice}");
            }
            else if(row == 6 && letter =='F'){
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\t  ||");
                Console.Write(" BACKSPACE: To unselect a seat.");
            }
        }
    }
}
