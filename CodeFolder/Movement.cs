public class Movement{

    public static bool MovementInPut(Airplane airplane)
    {
        ConsoleKeyInfo key = Console.ReadKey();
        Console.Clear();

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                MoveUp(airplane);
                break;

            case ConsoleKey.DownArrow:
                MoveDown(airplane);
                break;

            case ConsoleKey.LeftArrow:
                MoveLeft(airplane);
                break;

            case ConsoleKey.RightArrow:
                MoveRight(airplane);
                break;

            case ConsoleKey.Enter:
                airplane.DisplaySeats();
                airplane.SelectAndBookSeat();
                break;

            case ConsoleKey.Backspace:
                airplane.DisplaySeats();
                airplane.UnselectSeat();
                break;

            case ConsoleKey.Escape:
                return true; // Indicate that the booking is complete

            default:
                Console.WriteLine("Invalid input. Please use arrow keys to navigate.");
                break;
        }

        return false; // Indicate that the booking is not complete
    }


    public static void MoveUp(Airplane airplane){
        if (Airplane.cursorRow > 1){  // Access static field using class name
            Airplane.cursorRow--;
            airplane.RedrawSeats();
        }
        else {
            airplane.RedrawSeats();
        }
    }

    public static void MoveDown(Airplane airplane){
        if (Airplane.cursorRow < airplane.NumberOfRows){
            Airplane.cursorRow++;
            airplane.RedrawSeats();
        }
        else {
            airplane.RedrawSeats();
        }
    }

    public static void MoveLeft(Airplane airplane){
        if(Airplane.cursorSeat > 0){
            Airplane.cursorSeat--;
            airplane.RedrawSeats();
        }
        else{
            airplane.RedrawSeats();
        }
    }

    public static void MoveRight(Airplane airplane){
        if(Airplane.cursorSeat < airplane.LetterSeat - 'A'){
            Airplane.cursorSeat++;
            airplane.RedrawSeats();
        }
        else{
            airplane.RedrawSeats();
        }
    }

    // public virtual void MoveUp(){
    //     if (cursorRow > 1){
    //         cursorRow--;
    //         RedrawSeats();
    //     }
    //     else {
    //         RedrawSeats();
    //     }
    // }

    // public virtual void MoveDown(){
    //     if (cursorRow < NumberOfRows){
    //         cursorRow++;
    //         RedrawSeats();
    //     }
    //     else {
    //         RedrawSeats();
    //     }
    // }
    // public virtual void MoveLeft(){
    //     if (cursorSeat > 0){
    //         cursorSeat--;
    //         RedrawSeats();
    //     }
    //     else {
    //         RedrawSeats();
    //     }
    // }
    // public virtual void MoveRight(){
    //     if (cursorSeat < LetterSeat - 'A'){
    //         cursorSeat++;
    //         RedrawSeats();
    //     }
    //     else {
    //         RedrawSeats();
    //     }
    // }


}