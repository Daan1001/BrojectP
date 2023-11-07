using System.Security.Cryptography;
using System.Text;

public static class Password{
    public static string Encrypt(string value){
        Console.WriteLine(value); // testing
        var hash = SHA256.Create();
        var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(byteArray).ToLower();
    }
    public static bool CompareGivenPasswordWithPasswordHashFromUsername(String password, String username){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){
            if(JsonFile<Account>.listOfObjects![i].username == username){
                if(JsonFile<Account>.listOfObjects[i].passwordHash == Password.Encrypt(password)){
                    return true;
                } else {
                    return false;
                }
            }
            Console.WriteLine("Username doesn't exist yet");
        }
        return false;
    }
}