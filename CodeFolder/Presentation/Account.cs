using System.Runtime.ExceptionServices;
using CodeFolder.DataAccess;
using CodeFolder.Logic;

namespace CodeFolder;

public class Account
{
    private int? id;
    private string? name;
    private string? surname;
    private string? dateOfBirth;
    private string? password;
    
    public void NewAccount()
    {
        do
        {
            // asks for first name and checks if given answer is not null
          Console.Write("First Name:\n-> ");
          string? name = Console.ReadLine();  
        } while (name != "");

        do
        {
            // asks for last name and checks if given answer is not null
            Console.Write("Last Name:\n-> ");
            string? surname = Console.ReadLine();
        } while (surname != "");

        do
        {
            // asks for Date of Birth and checks if given answer is valid and follows DD-MM-YYYY format

            Console.Write("Date Of Birth (DD-MM-YYYY):\n-> ");
            string? dateOfBirth = Console.ReadLine();
        } while (dateOfBirth != "" && !AccountsLogic.ConfirmDoB(dateOfBirth));

        do
        {
            // asks for password and checks if answer meets security standards

            Console.Write("Password (must be 8 characters long and contain both at least one number and symbol):\n-> ");
            string? password = Console.ReadLine();
        } while (!AccountsLogic.CheckPasswordSecurity(password));
        
        // writing generated account to list, to be added to the JSON file
        List<AccountModel> accountList = new List<AccountModel>();
        
        AccountModel newUser = new AccountModel(_accountList.Count() + 1, name, surname, dateOfBirth, password);
        
        AccountsAccess.AddtoList(newUser);
    }
    private static List<Account>? _accountList;

    
}