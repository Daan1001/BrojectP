using System;
using CodeFolder;

public class Prices{
    public static int Korting;
    public static double TotalpriceDouble;

    public static void TicketPrices(Flight currentflight)
    {
        if (MainMenu.currentUser! != null!){
            Korting = CalculateDiscount(MainMenu.currentUser);
        }
        Console.WriteLine("Confirmation Screen:");
        Console.WriteLine($"Selected flight: {currentflight.AirplaneType} to {currentflight.Destination}, {currentflight.Country}");
        Console.WriteLine($"Departure time: {currentflight.FlightDate} at {currentflight.DepartureTime}");
        Console.WriteLine($"Price P.P: {currentflight.BasePrice}");
        Console.WriteLine("Selected Seats:");
        int totalprice = 0;
        string Basepricestring = currentflight.BasePrice!.Substring(1);
        int BasePriceInt = Convert.ToInt32(Basepricestring);
        int count = 1;
        Booking.seatsstring = $@"Price P.P: {currentflight.BasePrice}.
Selected seats:";
        foreach (var seat in Airplane.TemporarlySeat){
            if(seat.Booked == true){
                string seatsstringlist = $"{count}. Class: {seat.TypeClass} Seat: {seat.Letter}{seat.Row} Price: €{seat.Price}";
                Console.WriteLine(seatsstringlist);
                count++;
                totalprice = totalprice + seat.Price + BasePriceInt;
                Booking.seatsstring = Booking.seatsstring + $@"
{seatsstringlist}"; //adds seat to own line

            }  
        }
        double percentage = (double)Korting/100;
        double percentagekorting = 1.0 - percentage;
        double totalpricedouble = CalculatePrice(Convert.ToDouble(totalprice), percentagekorting);
        TotalpriceDouble = totalpricedouble;
        TotalpriceDouble = Math.Round(TotalpriceDouble, 2);
        Booking.seatsstring = Booking.seatsstring + $@"
Price before discount: €{totalprice}
Current discount: {Korting}%
Total price: €{TotalpriceDouble}
Have a great flight!";
        Console.WriteLine($"Price before discount: €{totalprice}");
        Console.WriteLine($"Current discount: {Korting}%");
        Console.WriteLine($"Total price: €{TotalpriceDouble}");
    }

    public static int CalculateDiscount(Account account){ // has to be public for unit test
        int korting = 0;
        int count = account.AccountBookings.Count();
        if(AccountBookings.editing){
            count--;
        }
        //decides the discount based on how many flights user has booked
        if(count == 0){
            korting = 0;
        }
        else{
            int modulo = count % 3;
            if (modulo == 1){
                korting = 5;
            }
            if (modulo == 2){
                korting = 10;
            }
            if (modulo == 0){
                korting = 15;
            }
        }
        return korting;
    }
    public static double CalculatePrice(double totalprice, double percentagekorting){ // has to be public for unit test
        return totalprice * percentagekorting;
    }

}