namespace TestFolder;
 
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestNewAccountSavedInJson(){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountBefore = JsonFile<Account>.listOfObjects!.Count(); // get object count in json
 
        int i = 1;
        string username = "UserName"+i;
        string password = "Password1!";
        while(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){ // checks if the username is the same as one made by a previous test
            i++;
            username = "UserName"+i;
        }
        NewAccount.Make(username, password, "1064928@hr.nl"); // makes account
 
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects!.Count(); // get object count in json
 
        Assert.IsTrue(amountBefore+1 == amountAfter);
    }
 
    [TestMethod]
    public void TestNewAccountIdenticalUsername(){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountBefore = JsonFile<Account>.listOfObjects.Count(); // get object count in json
 
        int i = 1;
        string username = "UserName"+i;
        string password = "Password1!";
        while(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){ // checks if the username is the same as one made by a previous test
            i++;
            username = "UserName"+i;
        }
        NewAccount.Make(username, password, "1064928@hr.nl"); // makes account
        NewAccount.Make(username, password, "1064928@hr.nl"); // makes account
 
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects.Count(); // get object count in json
 
        Assert.IsTrue(amountBefore+1 == amountAfter); // checks if it only adds 1 instead of 2 to json
    }
 
    [TestMethod]
    public void TestNewAccountPasswordEncryption(){
        Account account = new Account("UserName", "Password1!", "1064928@hr.nl", false, false);
        String manuallyEncrypted = Password.Encrypt("Password1!");
        String AutoEncrypted = account.passwordHash;
 
        Assert.AreEqual(manuallyEncrypted, AutoEncrypted);
    }
 
    [TestMethod]
    [DataRow("Economy", "Business", 'A', 'D', 1, 2, true, 22)]
    [DataRow("Business", "First-Class", 'E', 'F', 3, 1, true, 50)]
    public void TestingPlusOperatorAndEqualsOperatorBooking(String clas, String clas2, Char letter, Char letter2, int row, int row2, bool booked, int price){
        JsonFile<Flight>.Read("DataSources/Flights.json");
        Flight flight = JsonFile<Flight>.listOfObjects![0];
        Seat seat1 = new Seat(clas, letter, row, booked, price);
        Seat seat2 = new Seat(clas, letter2, row, booked, price);
        Seat seat3 = new Seat(clas2, letter, row2, booked, price);
        Booking Booking1 = new Booking(flight, new List<Seat>{seat1, seat2});
        Booking Booking2 = new Booking(flight, new List<Seat>{seat3});
        Booking ExpectedResult = new Booking(flight, new List<Seat>{seat1, seat2, seat3});
 
        Booking? ActualResult = Booking1 + Booking2;
 
        Assert.IsTrue(ExpectedResult == ActualResult!);
    }
 
    [TestMethod]
    [DataRow("Economy", 'A', 1, true, 22, true)]
    [DataRow("Economy", 'B', 1, true, 22, false)]
    [DataRow("Business", 'D', 14, true, 50, false)]
    [DataRow("First-Class", 'B', 6, true, 40, false)]
    public void TestingEqualsSeats(String clas, Char letter, int row, bool booked, int price, bool expected){
        Seat seat1 = new Seat(clas, letter, row, booked, price);
        Seat seat2 = new Seat("Economy", 'A', 1, true, 22);

        // Assert.AreEqual(seat1, seat2);
        Assert.IsTrue((seat1 == seat2) == expected);
        //Assert.IsFalse(seat1 == seat3);

    }
 
    // test if create flight porperly creates flights
    [TestMethod]
    [DataRow("105491", "Boeing 737", "05", "Frankfurt", "Germany", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60")]
    public void TestingCreateFlight(string id, string airplaneType, string Gate, string city, string country, string FlightDate, string DepartureTime, string ArrivalTime, string seatsAvailable, string totalSeats, string baseprice){
        Flight flight1 = new Flight
        {
            FlightId = id,
            AirplaneType = airplaneType,
            Terminal = Gate,
            Country = city,
            Destination = country,
            FlightDate = FlightDate,
            DepartureTime = DepartureTime,
            ArrivalTime = ArrivalTime,
            SeatsAvailable = seatsAvailable,
            TotalSeats = totalSeats,
            BasePrice = baseprice
        };
        Flight flight2 = AddingFlights.CreateFlight(id, airplaneType, Gate, city, country, FlightDate, DepartureTime, ArrivalTime, seatsAvailable, totalSeats, baseprice);
        Assert.IsTrue(flight1 == flight2);
    }
 
    //tests if reservations are deleted
    [TestMethod]
    [DataRow("Sander", "Password", false, false)]
    public void TestDeleteReservation(string name, string password, bool bool1, bool bool2 ){
        Flight flight2 = AddingFlights.CreateFlight("105491", "Boeing 737", "Gate 05", "Frankfurt", "Germany", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60");
        List<Flight> flights = new List<Flight>();
        flights.Add(flight2);
        Seat seat2 = new Seat("Economy", 'A', 1, true, 22);
        List<Seat> seats = new List<Seat>();
        seats.Add(seat2);
        Booking booking = new Booking(flight2, seats);
        Account account = new Account(name, password, "1064928@hr.nl", bool1, bool2);
        account.AccountBookings.Add(booking);
        MainMenu.currentUser = account;
        AccountBookings.DeleteBooking(flight2.ToString(flights), account);
        AccountBookings.UpdateUser();
        Assert.IsTrue(MainMenu.currentUser.AccountBookings.Count == 0);
    }
 
    //tests if GetTotalSeats returns right amount of seats
    [TestMethod]
    [DataRow("Boeing 787", 234)]
    [DataRow("Boeing 737", 198)]
    [DataRow("Airbus 330", 378)]
    public void TestGetTotalSeats(string airplaneType, int testSeats){
        int totalSeats = AddingFlights.GetTotalSeats(airplaneType);
        Assert.IsTrue(totalSeats == testSeats);
    }
 
    // find flight based on id
    [TestMethod]
    [DataRow("345678")]
    public void TestReturnedFlight(string flightid){
        Flight flight = AddingFlights.FindFlight(flightid);
        Assert.IsTrue(flight != null);
    }
 
    // testing discount for flights
    [TestMethod]
    [DataRow(1, 5)]
    [DataRow(2, 10)]
    [DataRow(3, 15)]
    [DataRow(0, 0)]
    [DataRow(4, 5)]
    public void TestDiscountPrices(int amount, int totalkorting){
        Flight flight2 = AddingFlights.CreateFlight("105491", "Boeing 737", "Gate 05", "Frankfurt", "Germany", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60");
        List<Flight> flights = new List<Flight>();
        flights.Add(flight2);
        Seat seat2 = new Seat("Economy", 'A', 1, true, 22);
        List<Seat> seats = new List<Seat>();
        seats.Add(seat2);
        Booking booking = new Booking(flight2, seats);
        Account account = new Account("Sander", "Password", "1054914@hr.nl", false, false);
        for (int i = 0; i < amount; i++){
            account.AccountBookings.Add(booking);
        }
        int discount = Prices.CalculateDiscount(account);
        Assert.IsTrue(discount == totalkorting);
    }
 
    [TestMethod] // tests if remove whitespace removes the whitespace properly
    [DataRow("105491", "Boeing 737", "Gate 05", "Germany", "Frankfurt", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60", "105491|Gate05|Germany|Frankfurt|10-10-2024|10:00:00|12:00:00|Boeing737|198/198|€60")]
    public void TestRemoveWhiteSpace(string id, string airplaneType, string Gate, string city, string country, string FlightDate, string DepartureTime, string ArrivalTime, string seatsAvailable, string totalSeats, string baseprice, string flightstring2){
        Flight flight1 = AddingFlights.CreateFlight(id, airplaneType, Gate, country, city, FlightDate, DepartureTime, ArrivalTime, seatsAvailable, totalSeats, baseprice);
        Flight flight2 = AddingFlights.CreateFlight("105491", "Boeing 737", "Gate 05", "Manchester", "United Kingdom", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60");
        List<Flight> flights = new List<Flight>();
        flights.Add(flight2);
        flights.Add(flight1);
        string flightstring = FlightSelection.RemoveWhitespace(flight1.ToString(flights));
        Assert.IsTrue(flightstring == flightstring2);
    }
 
    [TestMethod] // tests if flight == works properly
    [DataRow("105491", "Boeing 737", "Gate 05", "Germany", "Frankfurt", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60")]
    public void TestingEqualFlights(string id, string airplaneType, string Gate, string city, string country, string FlightDate, string DepartureTime, string ArrivalTime, string seatsAvailable, string totalSeats, string baseprice){
        Flight flight1 = AddingFlights.CreateFlight(id, airplaneType, Gate, country, city, FlightDate, DepartureTime, ArrivalTime, seatsAvailable, totalSeats, baseprice);
        Flight flight2 = AddingFlights.CreateFlight(id, airplaneType, Gate, country, city, FlightDate, DepartureTime, ArrivalTime, seatsAvailable, totalSeats, baseprice);
        Assert.IsTrue(flight1 == flight2);
    }
 
    [TestMethod]
    [DataRow(100, 0.5, 50)]
    public void TestCalculatePrice(double totalprice, double percentagekorting, double totaltotalprice){
        double answer = Prices.CalculatePrice(totalprice, percentagekorting);
        Assert.IsTrue(answer == totaltotalprice);
    }
 
    [TestMethod]
    [DataRow("345678", "Airbus 330", "Gate 05", "Germany", "Frankfurt", "10-10-2024", "10:00:00", "12:00:00", "198", "198", "€60")]
    public void TestFlightSelection(string id, string airplaneType, string Gate, string city, string country, string FlightDate, string DepartureTime, string ArrivalTime, string seatsAvailable, string totalSeats, string baseprice){
        Flight flight1 = AddingFlights.CreateFlight(id, airplaneType, Gate, country, city, FlightDate, DepartureTime, ArrivalTime, seatsAvailable, totalSeats, baseprice);
        string plane = FlightSelection.Selection(flight1.ToString());
        Assert.IsTrue(plane == flight1.AirplaneType);
    }
 
    [TestMethod]
    [DataRow(1000, 750, 500)]
    [DataRow(1200, 900, 600)]
    public void SetPrices_UpdatesPricesCorrectly(int firstClassPrice, int businessClassPrice, int economyClassPrice)
    {
        Boeing787 boeing787 = new Boeing787('A', 1);
        boeing787.SetPrices(firstClassPrice, businessClassPrice, economyClassPrice);
 
        // Assert
        Assert.AreEqual(firstClassPrice, Boeing787.FirstClassPrice);
        Assert.AreEqual(businessClassPrice, Boeing787.BusinessClassPrice);
        Assert.AreEqual(economyClassPrice, Boeing787.EconomyClassPrice);
    }
}