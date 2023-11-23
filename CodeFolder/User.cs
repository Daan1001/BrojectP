using System.Security.Cryptography;
using System.Text;
// 4 derived classes:
// -AccountlessUser
// -UserWithAccount
// -AdminUser
// -SuperAdminUser
public class User{
    public String username{get; set;}
    public String encryptedPassword{get; set;}
    public User(String username, String password){
        this.username = username;
        this.encryptedPassword = password;
    }
    public static void NewUser(String username, String encrypted){
        JsonFile<User>.Read("DataSources/Accounts.json");
        if(JsonFile<User>.listOfObjects!.Any(user => user.username == username)){
            Console.WriteLine("This username is already in use.");
        } else {
            JsonFile<User>.Write("DataSources/Accounts.json", new User(username, encrypted));
            Console.WriteLine("New user created!");
        }
    }
    public static void NewUserInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        Console.WriteLine("Fill in your username:");
        String Uname = Console.ReadLine()!;
        Console.WriteLine("Fill in your password:");
        String EncryptedPassword = Password.Encrypt(Console.ReadLine()!);
        NewUser(Uname, EncryptedPassword);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    public static void LogIn(String username, string password){
        JsonFile<User>.Read("DataSources/Accounts.json");
        for(int i = 0; i < JsonFile<User>.listOfObjects!.Count(); i++){
            if(JsonFile<User>.listOfObjects![i].username == username){
                if(Password.CompareGivenPasswordWithPasswordHashFromUsername(password, username)){
                    MainMenu.user = JsonFile<User>.listOfObjects[i];
                    Console.WriteLine("Succesfully logged in as \""+username+"\"!");
                    return;
                } else {
                    Console.WriteLine("Log in failed. Password is incorrect.");
                    return;
                }
            }
        }
        Console.WriteLine("Log in failed. User doesn't exist.");
    }
    public static void LogInInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        Console.WriteLine("What is your username?");
        String? a = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? b = Console.ReadLine();
        LogIn(a!, b!);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}