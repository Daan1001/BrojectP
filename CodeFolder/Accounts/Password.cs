using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public static class Password{
    public static string Encrypt(string value){
        var hash = SHA256.Create();
        var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(byteArray).ToLower();
    }
    public static bool CheckPasswordHash(String password, String username){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){
            if(JsonFile<Account>.listOfObjects![i].username == username){
                if(JsonFile<Account>.listOfObjects[i].passwordHash == Password.Encrypt(password)){
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }
    public static bool CheckPasswordSecurity(string password)
    {

        if (!(password.Length >= 8))
        {
            Console.WriteLine("Password must be at least 8 characters long.");
            return false;;
        }

        if (!password.Any(char.IsDigit))
        {
            Console.WriteLine("Password must contain one digit.");
            return false;
        }


        if (!PasswordSymbolChecker(password))
        {
            Console.WriteLine("Password must contain at least one symbol.");
            return false;
        }

        return true;
    }
    public static bool PasswordSymbolChecker(string password)
    {
        string pattern = @"[\p{P}\p{S}]";
        bool hasSymbols = Regex.IsMatch(password, pattern);
        return hasSymbols;
    }
}