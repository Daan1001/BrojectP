using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class Account: IEquatable<Account>{
    [JsonProperty(Order = 3)]
    public Boolean isAdmin{get; set;}
    [JsonProperty(Order = 4)]
    public Boolean isSuperAdmin{get; set;}
    [JsonProperty(Order = 1)]
    public string username;
    [JsonProperty(Order = 2)]
    public string? passwordHash;
    [JsonProperty(Order = 5)]
    public List<Booking> AccountBookings = new List<Booking>();
    // public DateTime dateOfBirth;
    public string email;
    // public int phoneNumber;
    public Account(string username, string password, string email, bool isAdmin, bool isSuperAdmin){
        this.username = username;
        if(password != null){
            this.passwordHash = Password.Encrypt(password);
        }
        this.email = email;
        this.isAdmin = isSuperAdmin ? true : isAdmin;
        this.isSuperAdmin = isSuperAdmin;
    }

    public void AccountInformation(){
        Console.Clear();
        MainMenu.AirportName();
        if(MainMenu.currentUser is not null && MainMenu.currentUser.isAdmin && OptionSelection<Account>.selectedAccount is not null){
            OptionSelection<String>.selectedAccount = OptionSelection<Account>.selectedAccount;
            List<String> options = new List<string>(){"Username: "+this.username};
            if(MainMenu.currentUser.isSuperAdmin){
                options.Add("Is Admin: "+Convert.ToString(this.isAdmin));
                options.Add("Is Super Admin: "+Convert.ToString(this.isSuperAdmin));
                options.Add("Bookings");
            }
            options.Add("Reset password");
            options.Add("Delete account(!)");
            options.Add("<-- Go back");
            OptionSelection<String>.Start(options);
        } else {
            Console.WriteLine(this);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            OptionSelection<String>.Start(new List<string>(){"Change username","Reset password","Delete account(!)","<-- Go back"});
        }
    }
    public static void ViewAllAccount(){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        OptionSelection<Account>.Start(JsonFile<Account>.listOfObjects!, OptionSelection<Account>.GoBack);
    }

    public void DeleteFromJson(){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        List<Account> allAccountList = JsonFile<Account>.listOfObjects!;

        allAccountList.Where(a => a == this).ToList()
            .ForEach(a => {allAccountList.Remove(a);
        string updatedJson = JsonConvert.SerializeObject(allAccountList, Formatting.Indented);
        File.WriteAllText("DataSources/Accounts.json", updatedJson);});
    }
    public void changeUsername(String NewUsername){
        this.DeleteFromJson();
        this.username = NewUsername;
        JsonFile<Account>.Write("DataSources/Accounts.json",this);
        Console.WriteLine("Username changed to \""+NewUsername+"\"");
    }
    public void changeUsernameInput(){
        Console.Clear();
        MainMenu.AirportName();
        JsonFile<Account>.Read("DataSources/Accounts.json");
        List<Account> allAccountList = JsonFile<Account>.listOfObjects!;
        Boolean correctUsername = false;
        Console.CursorVisible = true;
        while(!correctUsername){
            Console.WriteLine("What will be the new username?");
            String NewUsername = Console.ReadLine()!;
            if(!allAccountList.Any(Account => Account.username == NewUsername)){
                Console.CursorVisible = false;
                changeUsername(NewUsername);
                correctUsername = true;
            } else if(NewUsername == this.username){
                Console.WriteLine("Username didn't change");
                correctUsername = true;
            } else {
                Console.WriteLine("This username already exists");
            }
        }
        Console.CursorVisible = false;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ChangePassword(String NewPassword){
        this.DeleteFromJson();
        this.passwordHash = Password.Encrypt(NewPassword);
        JsonFile<Account>.Write("DataSources/Accounts.json",this);
        Console.WriteLine("Password changed");
    }

    public void ChangePasswordInput(){
        Console.Clear();
        MainMenu.AirportName();
        JsonFile<Account>.Read("DataSources/Accounts.json");
        List<Account> allAccountList = JsonFile<Account>.listOfObjects!;
        Console.CursorVisible = true;
        String NewPassword = "";
        do{
            Console.WriteLine("What will be the new password?");
            NewPassword = Console.ReadLine()!;
        }while(!Password.CheckPasswordSecurity(NewPassword));
        Console.CursorVisible = false;
        ChangePassword(NewPassword);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void switchAdminBoolean(){
        this.DeleteFromJson();
        if(this.isAdmin && !this.isSuperAdmin){
            this.isAdmin = false;
        } else{
            this.isAdmin = true;
        }
        JsonFile<Account>.Write("DataSources/Accounts.json",this);
    }
    public void switchSuperAdminBoolean(){
        this.DeleteFromJson();
        if(this.isSuperAdmin){
            this.isSuperAdmin = false;
        } else{
            this.isAdmin = true;
            this.isSuperAdmin = true;
        }
        JsonFile<Account>.Write("DataSources/Accounts.json",this);
    }

    public static void UpdateUser(){
        MainMenu.currentUser = UpdateAccount(MainMenu.currentUser!);
    }
    public static Account UpdateAccount(Account accountToUpdate){
        string filePath = "DataSources/Accounts.json";
        string jsonContent = File.ReadAllText(filePath);
        List<Account> newBookings = JsonConvert.DeserializeObject<List<Account>>(jsonContent)!;
        accountToUpdate = newBookings.FirstOrDefault(account => account.username == accountToUpdate.username)!;
        return accountToUpdate;
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
        } else if(this.username == other?.username){ // && this.passwordHash == other?.passwordHash && this.isAdmin == other?.isAdmin && this.isSuperAdmin == other.isSuperAdmin
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

    // public static bool ConfirmDoB(string dateOfBirth)
    // {
    //     bool test =  DateTime.TryParseExact(dateOfBirth, "dd-MM-YYYY", null, System.Globalization.DateTimeStyles.None, out DateTime result);
    //     if(test){
    //         DateTime test2 = result;
    //     }
    //     return test;
    // }
}
