using CodeFolder.DataModels;
using CodeFolder.Logic;

namespace CodeFolder;
using System.Text.RegularExpressions;

public class Account
{
    private int _id = 0;
    private string _name = "";
    private string _surname = "";
    private string _dateOfBirth = "";
    private string _password = "";
    public void NewAccount()
    {
        do
        {
            // asks for first name and checks if given answer is not null
          Console.Write("First Name:\n-> ");
          string name = Console.ReadLine();  
        } while (_name != "");

        do
        {
            // asks for last name and checks if given answer is not null
            Console.Write("Last Name:\n-> ");
            string surname = Console.ReadLine();
        } while (_surname != "");

        do
        {
            // asks for Date of Birth and checks if given answer is valid and follows DD-MM-YYYY format

            Console.Write("Date Of Birth (DD-MM-YYYY):\n-> ");
            string dateOfBirth = Console.ReadLine();
        } while (_dateOfBirth != "" && !AccountsLogic.ConfirmDoB(_dateOfBirth));

        do
        {
            // asks for password and checks if answer meets security standards

            Console.Write("Password (must be 8 characters long and contain both at least one number and symbol):\n-> ");
            string password = Console.ReadLine();
        } while (!AccountsLogic.CheckPasswordSecurity(_password));
        
        
        

        if (_accountList == null)
        {
            List<Account> accountList = new List<Account>();
        }
        AccountModel newUser = new AccountModel(_accountList.Count() + 1, _name, _surname, _dateOfBirth, _password);
        
        _accountList.Add(newUser);
    }
    private static List<Account> _accountList;

    
}