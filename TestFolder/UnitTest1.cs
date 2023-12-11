namespace TestFolder;
using System;

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
        string email = "email";
        while(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){ // checks if the username is the same as one made by a previous test
            i++;
            username = "UserName"+i;
        }
        NewAccount.Make(username, password, email); // makes account

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
        string email = "emailaccount";
        while(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){ // checks if the username is the same as one made by a previous test
            i++;
            username = "UserName"+i;
        }
        NewAccount.Make(username, password, email); // makes account
        NewAccount.Make(username, password, email); // makes account

        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects.Count(); // get object count in json

        Assert.IsTrue(amountBefore+1 == amountAfter); // checks if it only adds 1 instead of 2 to json
    }

    [TestMethod]
    public void TestNewAccountPasswordEncryption(){
        Account account = new Account("UserName", "Password1!", "email", false, false);
        string manuallyEncrypted = Password.Encrypt("Password1!");
        string AutoEncrypted = account.passwordHash;

        Assert.AreEqual(manuallyEncrypted, AutoEncrypted);
    }
    [TestMethod]
    public void TestDiscountPrices(){
        Account account = new Account("Sander5", "Sander123!","emailgoeshere", false, false);
        MainMenu.currentUser = account;
        List<Flight> flights = ShowFlights.LoadFlightsFromJson("DataSources/flights.json");
        account.AccountFlights.Add(flights[1]);
        Seat seat= new Seat("First Class", 'B', 1, true, 500);
        Airplane.TemporarlySeat.Add(seat);
        Prices.TicketPrices(flights[2]);
 
        Assert.AreEqual(Prices.Korting, 5);
    }
}