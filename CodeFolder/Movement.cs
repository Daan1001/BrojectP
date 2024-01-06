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
        if(airplane is Boeing737){
           if (Airplane.cursorRow > 1){  // Access static field using class name
                Airplane.cursorRow--;
                airplane.RedrawSeats();
            }
            else {
                airplane.RedrawSeats();
            }
        }
        else if (airplane is Boeing787){
            if (Airplane.cursorRow > 1){  // Access static field using class name
                if(Airplane.cursorRow == 7 && Airplane.cursorSeat == 2 ||
                   Airplane.cursorRow == 7 && Airplane.cursorSeat == 4 ||
                   Airplane.cursorRow == 7 && Airplane.cursorSeat == 4 ){
                
                    Airplane.cursorRow--;
                    Airplane.cursorSeat--;
                    airplane.RedrawSeats();
                   }
                // Seat F7 misschien naar D6 inplaats van E6;
                else if(Airplane.cursorRow == 7 && Airplane.cursorSeat == 6 ||
                        Airplane.cursorRow == 7 && Airplane.cursorSeat == 7 ||
                        Airplane.cursorRow == 7 && Airplane.cursorSeat == 5 ){

                    Airplane.cursorRow--;
                    Airplane.cursorSeat -= 2;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 7 && Airplane.cursorSeat == 8){
                    Airplane.cursorRow--;
                    Airplane.cursorSeat -= 3;
                    airplane.RedrawSeats();
                }
                else{
                    Airplane.cursorRow--;
                    airplane.RedrawSeats();
                }
            }
            else {
                airplane.RedrawSeats();
            }
        
        }
        else if (airplane is Airbus330){
            if (Airplane.cursorRow > 1){  // Access static field using class name
                if(Airplane.cursorRow == 3 && Airplane.cursorSeat == 2 || Airplane.cursorRow == 3 && Airplane.cursorSeat == 3 ||
                   Airplane.cursorRow == 3 && Airplane.cursorSeat == 4 ){

                    Airplane.cursorRow--;
                    Airplane.cursorSeat--;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 3 && Airplane.cursorSeat == 5 || Airplane.cursorRow == 3 && Airplane.cursorSeat == 6 ||
                        Airplane.cursorRow == 3 && Airplane.cursorSeat == 7 ){
                        
                        Airplane.cursorRow--;
                        Airplane.cursorSeat-= 2;
                        airplane.RedrawSeats();
                }
                else if (Airplane.cursorRow == 3 && Airplane.cursorSeat == 8){

                    Airplane.cursorRow--;
                    Airplane.cursorSeat-= 3;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 39 && Airplane.cursorSeat == 2 || Airplane.cursorRow == 39 && Airplane.cursorSeat == 3 ||
                        Airplane.cursorRow == 39 && Airplane.cursorSeat == 4 ){
                        
                        Airplane.cursorRow--;
                        Airplane.cursorSeat++;
                        airplane.RedrawSeats();
                }   
                else if (Airplane.cursorRow == 39 && Airplane.cursorSeat == 5 || Airplane.cursorRow == 39 && Airplane.cursorSeat == 6){
                        Airplane.cursorRow--;
                        Airplane.cursorSeat+= 2;
                        airplane.RedrawSeats();
                }
                else{
                    Airplane.cursorRow--;
                    airplane.RedrawSeats();
                } 
            }
            else {
                Airplane.cursorRow--;
                airplane.RedrawSeats();
                }
        }
        else {
                airplane.RedrawSeats();
            }
        }
        
    public static void MoveDown(Airplane airplane){
        if(airplane is Boeing737){
           if (Airplane.cursorRow < airplane.NumberOfRows){
                Airplane.cursorRow++;
                airplane.RedrawSeats();
            }
            else {
                airplane.RedrawSeats();
            }
        }
        //done
        else if (airplane is Boeing787){
            if (Airplane.cursorRow < airplane.NumberOfRows){
                if(Airplane.cursorRow == 6 && Airplane.cursorSeat == 2 || Airplane.cursorRow == 6 && Airplane.cursorSeat == 3 ){
                    Airplane.cursorRow++;
                    Airplane.cursorSeat++;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 6 && Airplane.cursorSeat == 4 || Airplane.cursorRow == 6 && Airplane.cursorSeat == 5){
                    Airplane.cursorRow++;
                    Airplane.cursorSeat += 2;
                    airplane.RedrawSeats();
                }
                else{
                    Airplane.cursorRow++;
                    airplane.RedrawSeats();
                }
            }
            else {
                airplane.RedrawSeats();
            }
        
        }
        else if (airplane is Airbus330){
            if (Airplane.cursorRow < airplane.NumberOfRows){
                if(Airplane.cursorRow == 2 && Airplane.cursorSeat == 2  || Airplane.cursorRow == 2 && Airplane.cursorSeat == 3   ){
                    Airplane.cursorRow++; 
                    Airplane.cursorSeat++;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 2 && Airplane.cursorSeat == 4  || Airplane.cursorRow == 2 && Airplane.cursorSeat == 5 ){
                    Airplane.cursorRow++;
                    Airplane.cursorSeat += 2;
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 38 && Airplane.cursorSeat == 3 || Airplane.cursorRow == 38 && Airplane.cursorSeat == 4|| 
                        Airplane.cursorRow == 38 && Airplane.cursorSeat == 5 || Airplane.cursorRow == 38 && Airplane.cursorSeat == 6||
                        Airplane.cursorRow == 38 && Airplane.cursorSeat == 2){
                        
                    Airplane.cursorRow++;
                    Airplane.cursorSeat--;   
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorRow == 38 && Airplane.cursorSeat == 7 || Airplane.cursorRow == 38 && Airplane.cursorSeat == 8){
                    Airplane.cursorRow++;
                    Airplane.cursorSeat-= 2;
                    airplane.RedrawSeats();
                }
                else{
                    Airplane.cursorRow++;
                    airplane.RedrawSeats();
                }
            }
            else {
                airplane.RedrawSeats();
            }
            // if (Airplane.cursorRow < airplane.NumberOfRows){
            //     Airplane.cursorRow++;
            //     airplane.RedrawSeats();
            // }
            // else {
            //     airplane.RedrawSeats();
            // }
        }
    }
        // if (Airplane.cursorRow < airplane.NumberOfRows){
        //     Airplane.cursorRow++;
        //     airplane.RedrawSeats();
        // }
        // else {
        //     airplane.RedrawSeats();
        // }


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
            if(airplane is Boeing737){
                if(Airplane.cursorSeat < airplane.LetterSeat - 'A'){
                    Airplane.cursorSeat++;
                    airplane.RedrawSeats();
                }
                else{
                    airplane.RedrawSeats();
                }
            }
            else if( airplane is Boeing787){
                if(Airplane.cursorSeat ==5 && Airplane.cursorRow <= 6){
                    airplane.RedrawSeats();
                }
                else if (Airplane.cursorSeat < airplane.LetterSeat - 'A'){
                    Airplane.cursorSeat++;
                    airplane.RedrawSeats();
                }
                else{
                    airplane.RedrawSeats();
                }
            }
            else if( airplane is Airbus330){
                if(Airplane.cursorRow  <= 2 && Airplane.cursorSeat == 5 || Airplane.cursorRow  >= 39 && Airplane.cursorSeat == 6 && Airplane.cursorRow <=44){
                    airplane.RedrawSeats();
                }
                else if(Airplane.cursorSeat < airplane.LetterSeat - 'A'){
                    Airplane.cursorSeat++;
                    airplane.RedrawSeats();
                }
                else{
                    airplane.RedrawSeats();
                }
            }
        }
}