namespace CodeFolder.Logic;
using System.Text.RegularExpressions;

public class AccountsLogic
{
    public static bool ConfirmDoB(string dateOfBirth)
    {
        if (DateTime.TryParseExact(dateOfBirth, "dd-MM-YYYY", null, System.Globalization.DateTimeStyles.None,
                out DateTime result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static bool CheckPasswordSecurity(string password)
    {
        bool passwordLen = false;
        bool passwordNum = false;
        bool passwordSym = false;

        if (password.Length >= 8)
        {
            passwordLen = true;
        }
        else
        {
            Console.WriteLine("Password must be at least 8 characters long.");
        }

        if (password.Any(char.IsDigit))
        {
            passwordNum = true;
        }
        else
        {
            Console.WriteLine("Password must contain one digit.");
        }

        if (PasswordSymbolChecker(password))
        {
            passwordSym = true;
        }
        else
        {
            Console.WriteLine("Password must contain at least one symbol.");
        }

        bool totalCheck = passwordLen && passwordNum && passwordSym;
        return totalCheck;
    }

    public static bool PasswordSymbolChecker(string password)
    {
        string pattern = @"[\p{P}\p{S}]";
        bool hasSymbols = Regex.IsMatch(password, pattern);
        return hasSymbols;
    }
}