public class SeatLayoutConfiguration
{
    public int FirstClassStartRow { get; set; }
    public int FirstClassEndRow { get; set; }
    public int BusinessClassStartRow { get; set; }
    public int BusinessClassEndRow { get; set; }
    public int EconomyClassStartRow { get; set; }
    public int EconomyClassEndRow { get; set; }
    public char LetterSeat { get; set; } // Add this property for specifying the letter of the last seat
    public int FirstClassPrice { get; set; } // Add this property for specifying the price of first class seats
    public int BusinessClassPrice { get; set; } // Add this property for specifying the price of business class seats
    public int EconomyClassPrice { get; set; } // Add this property for specifying the price of economy class seats

    
}