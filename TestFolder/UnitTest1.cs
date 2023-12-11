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
        NewAccount.Make(username, password); // makes account

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
        NewAccount.Make(username, password); // makes account
        NewAccount.Make(username, password); // makes account

        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects.Count(); // get object count in json

        Assert.IsTrue(amountBefore+1 == amountAfter); // checks if it only adds 1 instead of 2 to json
    }

    [TestMethod]
    public void TestNewAccountPasswordEncryption(){
        Account account = new Account("UserName", "Password1!", false, false);
        String manuallyEncrypted = Password.Encrypt("Password1!");
        String AutoEncrypted = account.passwordHash;

        Assert.AreEqual(manuallyEncrypted, AutoEncrypted);
    }
    // [TestMethod]
    // public void TestDiscountPrices(){
    //     Account account = new Account("Sander5", "Sander123!", false, false);
    //     MainMenu.currentUser = account;
    //     List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
    //     account.AccountBookings.Add(flights[1]);
    //     Seat seat= new Seat("First Class", 'B', 1, true, 500);
    //     Airplane.TemporarlySeat.Add(seat);
    //     Prices.TicketPrices(flights[2]);
 
    //     Assert.AreEqual(Prices.Korting, 5);
    // }
    [TestMethod]
    public void TestingPlusOperatorAndEqualsOperatorBooking(){
        JsonFile<Flight>.Read("DataSources/Flights.json");
        Flight flight = JsonFile<Flight>.listOfObjects![0];
        Seat seat1 = new Seat("Economy", 'A', 1, true, 22);
        Seat seat2 = new Seat("Economy", 'B', 1, true, 22);
        Seat seat3 = new Seat("Business", 'A', 2, true, 22);
        Booking Booking1 = new Booking(flight, new List<Seat>{seat1, seat2});
        Booking Booking2 = new Booking(flight, new List<Seat>{seat3});
        Booking ExpectedResult = new Booking(flight, new List<Seat>{seat1, seat2, seat3});

        Booking? ActualResult = Booking1 + Booking2;

        Assert.IsTrue(ExpectedResult == ActualResult!);
    }

    [TestMethod]
    [DataRow("Economy", 'A', 1, true, 22)]
    public void TestingEqualsSeats(String clas, Char letter, int row, bool booked, int price){
        Seat seat1 = new Seat(clas, letter, row, booked, price);
        Seat seat2 = new Seat(clas, letter, row, booked, price);

        // Assert.AreEqual(seat1, seat2);
        Assert.IsTrue(seat1 == seat2);
    }
    
}