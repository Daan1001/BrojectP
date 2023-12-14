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
    
}