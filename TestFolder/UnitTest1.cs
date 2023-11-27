namespace TestFolder;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestNewAccountSavedInJson(){
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

        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects.Count(); // get object count in json

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
}