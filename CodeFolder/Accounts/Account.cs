using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Account: IEquatable<Account>{
    public Boolean isSuperAdmin{get; set;}
    public Boolean isAdmin{get; set;}
    public string username;
    public string? passwordHash;
    // public DateTime dateOfBirth;
    // public String email;
    // public int phoneNumber;
    public Account(string username, string password, Boolean isAdmin, Boolean isSuperAdmin){
        this.username = username;
        if(password != null){
            this.passwordHash = Password.Encrypt(password);
        }
        this.isAdmin = isAdmin;
        this.isSuperAdmin = isSuperAdmin;
    }

    public void AccountInformation(){
        Console.Clear();
        MainMenu.AirportName();
        Console.WriteLine(this);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override string ToString(){
        String accountType = "";
        if(isSuperAdmin){
            accountType = "a super admin";
        } else if(isAdmin){
            accountType = "an admin";
        } else {
            accountType = "a standard";
        }
        return "Username: "+username+"\nThis is "+accountType+" account.";
    }
    

    public bool Equals(Account? other)
    {
        if(other is null){
            return false;
        } else if(this.username == other?.username && this.passwordHash == other?.passwordHash && this.isAdmin == other?.isAdmin && this.isSuperAdmin == other.isSuperAdmin){
            return true;
        } else {
            return false;
        }
    }
    public static bool operator ==(Account one, Account two){
        if(one is null || two is null){
            if (one is null){
                return two is null;
            } else{
                return false;
            }
        } else {
            return one.Equals(two);
        }
    }
    public static bool operator !=(Account one, Account two){
       return !(one == two);
    }

    // public static Account GetAccount(string Username)
    // {
    //     List<Account> accountlist;
    //     JsonFile<Account>.Read("DataSources/Accounts.json");
    //     accountlist = JsonFile<Account>.listOfObjects!;
    //     Account _account;

    //     foreach (var account in accountlist)
    //     {
    //         if (account.username == Username)
    //         {
    //             _account = account;
    //         }
    //     }
    //     return _account;
    // }

    // public static bool ConfirmDoB(string dateOfBirth)
    // {
    //     bool test =  DateTime.TryParseExact(dateOfBirth, "dd-MM-YYYY", null, System.Globalization.DateTimeStyles.None, out DateTime result);
    //     if(test){
    //         DateTime test2 = result;
    //     }
    //     return test;
    // }
}
