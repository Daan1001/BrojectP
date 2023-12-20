
public static class SeatColoring{
    public static void SetColor(int cursorRow, int row, int cursorSeat, char letter, List<Seat> TemporarlySeat, Seat seat, char LetterSeat, Airplane airplane){
        if (cursorRow == row && cursorSeat == letter - 'A'){
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
        if(airplane is Airbus330){
            if (letter >= 'A' && letter <= 'F' && row <= 2){
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta : ConsoleColor.Green;
            }
            else if(letter >= 'A' && letter <= 'I' && row == 3){
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta : ConsoleColor.Yellow;
            }
            else if (letter >= 'A'&& letter <='I' && row == 10 || row == 30){
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta : ConsoleColor.Yellow;
            }
            else if (letter <= LetterSeat && row >= 4 && row <= 44){
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta : ConsoleColor.White;
            }
        } else if(airplane is Boeing737){
            if (row == 16 || row == 17) {
                Console.ForegroundColor = seat.Booked &&!TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta :ConsoleColor.Yellow;
            }
            else{
                Console.ForegroundColor = seat.Booked &&!TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta :ConsoleColor.White;
            }
        } else if(airplane is Boeing787){
            if (letter >= 'A' && letter <= 'F' && row <= 6)
            {
                Console.ForegroundColor = seat.Booked &&!TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta :ConsoleColor.Green;
            }
            else if (letter <= LetterSeat && row >= 7 && row <= 16)
            {
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s == seat)? ConsoleColor.Red : Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta: ConsoleColor.Blue;
            }
            else if (letter <= LetterSeat && row >= 17 && row <= 28)
            {
                Console.ForegroundColor = seat.Booked && !TemporarlySeat.Any(s => s ==seat) ? ConsoleColor.Red :Console.ForegroundColor = seat.Booked && TemporarlySeat.Any(s => s == seat)? ConsoleColor.Magenta: ConsoleColor.White;
            }
        }
    }
}
