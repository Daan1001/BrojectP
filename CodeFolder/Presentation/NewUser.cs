namespace CodeFolder;
using System.Text.RegularExpressions;

public class Account
{
    private string Name { get; set; }
    private string Surname { get; set; }
    private string DateOfBirth { get; set; }
    private string Password { get; set; }
    
    public Account (int id, string name, string surname, string dateOfBirth, string password)
    {
        // this.ID = id++;
        this.Name = name;
        this.Surname = surname;
        this.DateOfBirth = dateOfBirth;
        this.Password = password;
        //list of previous commutes by plane, can only be implemented after the tickets have been made :)
        // List<Ticket> tripHistory = new List<Ticket>();
    }
    
    
    // private List<Ticket> tripHistory { get; set; }

    public void NewAccount()
    {
        do
        {
            // asks for first name and checks if given answer is not null
          Console.Write("First Name:\n-> ");
          string Name = Console.ReadLine();  
        } while (Name != "");

        do
        {
            // asks for last name and checks if given answer is not null
            Console.Write("Last Name:\n-> ");
            string Surname = Console.ReadLine();
        } while (Surname != "");

        do
        {
            // asks for Date of Birth and checks if given answer is valid and follows DD-MM-YYYY format

            Console.Write("Date Of Birth (DD-MM-YYYY):\n-> ");
            string DateOfBirth = Console.ReadLine();
        } while (DateOfBirth != "" && !ConfirmDoB(DateOfBirth));

        do
        {
            // asks for password and checks if answer meets security standards

            Console.Write("Password (must be 8 characters long and contain both at least one number and symbol):\n-> ");
            string Password = Console.ReadLine();
        } while (!CheckPasswordSecurity(Password));
        
        
        

        if (accountList == null)
        {
            List<Account> accountList = new List<Account>();
        }
        Account NewUser = new Account(accountList.Count(), Name, Surname, DateOfBirth, Password);
        accountList.Add(NewUser);
    }
    private static List<Account> accountList;

    public bool ConfirmDoB(string dateOfBirth)
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
    
    public bool CheckPasswordSecurity(string password)
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

    public bool PasswordSymbolChecker(string password)
    {
        string pattern = @"[\p{P}\p{S}]";
        bool hasSymbols = Regex.IsMatch(password, pattern);
        return hasSymbols;
    }
}