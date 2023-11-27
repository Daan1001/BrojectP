public class Airbus330 : DisplaySeating
{
    public Airbus330(char letter, int numbers) : base(letter, numbers){}

    public override void InitializeSeats(int firstClassPrice, int businessClassPrice, int economyClassPrice)
    {
        // Initialize first-class seats
        for(char letter = 'A'; letter <= 'F'; letter++){
            for(int row = 1; row <= 2; row++){
                Seat? existingSeat = DisplaySeating.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'F') ? (int)(firstClassPrice * 1.2) : firstClassPrice;
                if (existingSeat != null){
                    // The seat is already booked (based on the JSON data)
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new FirstClass("First Class", letter, row, false, seatPrice);
                }
            }
        }
        for(char letter = 'A'; letter <= 'I'; letter++){
            for(int row =3; row <= 38; row++){
                Seat? existingSeat = DisplaySeating.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2) : economyClassPrice;
                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    if(row == 3 || row == 10 || row == 29){
                        int legroom = 30;
                        seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2 +legroom) : economyClassPrice + legroom;
                    }
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
        for(char letter ='A'; letter <= 'G'; letter++){
            for(int row=39; row <= 44; row++){
                Seat? existingSeat = DisplaySeating.bookedSeats.Find(s => s.Row == row && s.Letter == letter);
                int seatPrice = (letter == 'A' || letter == 'I') ? (int)(economyClassPrice * 1.2) : economyClassPrice;
                if (existingSeat != null){
                    new Seat(existingSeat.TypeClass, letter, row, true, existingSeat.Price);
                }
                else{
                    new EconomyClass("Economy Class", letter, row, false, seatPrice);
                }
            }
        }
    }

    public override void DisplaySeats()
    {

    }

    public override void Start(Flight CurrentFlight)
    {

    }
}

