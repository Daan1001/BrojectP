using Newtonsoft.Json;
public abstract class Airplane 
{
    protected static int cursorRow = 0;
    public static int cursorSeat = 0;
    public static List<Seat> bookedSeats = new List<Seat>();
    public static List<Seat> TemporarlySeat = new List<Seat>();
    public char LetterSeat { get; private set; }
    public int NumberOfRows { get; private set; }       
    public Airplane(char letterseat, int numberofrows){
        LetterSeat = letterseat;
        NumberOfRows = numberofrows;
    }
    public abstract void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice);
    public abstract void Start(Flight CurrentFlight);
    public abstract void DisplaySeats();
    public void SelectAndBookSeat(){
        Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        //Seat? selectedSeat = bookedSeats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        if (selectedSeat != null && selectedSeat.Booked == false){
            selectedSeat.Book();
            TemporarlySeat.Add(selectedSeat);
            //selectedSeat.ShowSeat();
            //bookedSeats.Add(selectedSeat);
        }
    } 
    public void UnselectSeat(){
        //Seat? selectedSeat = Seat.Seats.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        Seat? selectedSeat = TemporarlySeat.Find(s => s.Row == cursorRow && s.Letter == (char)(cursorSeat + 'A'));
        if (selectedSeat != null){
            // Unselect the seat
            Console.WriteLine($"Seat: {selectedSeat.Letter}{selectedSeat.Row} unselected.");
            selectedSeat.ResetSeat(); // you have a method to unbook the seat in your Seat class
            TemporarlySeat.Remove(selectedSeat); // Remove the seat from the bookedSeats list
        }
        // DisplaySeats(); // Display the seats again without the selection
    }
    public void LoadBookedSeatsFromJson(string filePath){
        if (File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            // updates current list of seat or starts a new empty list
            bookedSeats = JsonConvert.DeserializeObject<List<Seat>>(json) ?? new List<Seat>();
        }
    }
    public void SaveBookedSeatsToJson(string filePath){
        // Save the bookedSeats list to a JSON file
        string json = JsonConvert.SerializeObject(bookedSeats, Formatting.Indented);
        File.WriteAllText(filePath, json);
        //Console.WriteLine($"Booked seats saved to {filePath}");
    }
    public bool ConfirmBooking(){
        Console.WriteLine("Confirmation Screen:");
        Console.WriteLine("Selected Seats:");
        foreach (var seat in TemporarlySeat){
            if(seat.Booked == true){
                Console.WriteLine(seat.ShowSeat());
            }   
            // gotta include the price but, have to change the Seat class constructor also the inittializedseat methode 
        }
        Console.Write("Confirm booking? (Y/N): ");
        ConsoleKeyInfo key = Console.ReadKey();
        // Return true if the user pressed 'Y' (yes), otherwise return false
        return key.Key == ConsoleKey.Y;
    }

    public virtual void MoveUp(){
        if (cursorRow > 1){
            cursorRow--;
            RedrawSeats();
        }
        else {
            RedrawSeats();
        }
    }

    public virtual void MoveDown(){
        if (cursorRow < this.NumberOfRows){
            cursorRow++;
            RedrawSeats();
        }
        else {
            RedrawSeats();
        }
    }
    public void MoveLeft(){
        if (cursorSeat > 0){
            cursorSeat--;
            RedrawSeats();
        }
        else {
            RedrawSeats();
        }
    }
    public void MoveRight(){
        if (cursorSeat < LetterSeat - 'A'){
            cursorSeat++;
            RedrawSeats();
        }
        else {
            RedrawSeats();
        }
    }
    public void RedrawSeats(){
        // Console.SetCursorPosition(0, 0);
        DisplaySeats();
    }
}