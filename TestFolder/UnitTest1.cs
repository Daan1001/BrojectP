namespace TestFolder;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestNewAccountSavedInJson(){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountBefore = JsonFile<Account>.listOfObjects.Count();
        NewAccount.Make("UserName", "Password");
        JsonFile<Account>.Read("DataSources/Accounts.json");
        int amountAfter = JsonFile<Account>.listOfObjects.Count();

        Assert.IsTrue(amountBefore+1 == amountAfter);

    }
    // public void TestNewAccountPasswordEncryption(){
    //     NewAccount.Make("UserName", "Password");
    //     String manuallyEncrypted = Password.Encrypt("Password");
    //     String AutoEncrypted = 

    //     Assert.AreEqual(manuallyEncrypted == AutoEncrypted);

    // }
}