using System.Text.RegularExpressions;


public static class NewAccount{
    private static Boolean newStandardAdminAccount = false;
    private static Boolean newSuperAdminAccount = false;

    public static void Make(string username, string password, string email){
        if(Password.CheckPasswordSecurity(password)){
            JsonFile<Account>.Read("DataSources/Accounts.json");
            if(CheckUsernamesExistence(username)){
                return;
            }
            JsonFile<Account>.Write("DataSources/Accounts.json", new Account(username, password, email, newStandardAdminAccount, newSuperAdminAccount));
            Console.Write("New ");
            if(newStandardAdminAccount){
                Console.Write("admin ");
            } else if(newSuperAdminAccount){
                Console.Write("super admin ");
            }
            Console.WriteLine("account created!");
        }
    }
    public static void MakeInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        if(MainMenu.currentUser! != null!){ 
            if(MainMenu.currentUser!.isSuperAdmin){ 
                Console.WriteLine("Will it be an admin account? (Y/N)");
                ConsoleKeyInfo KeyPressed;
                do{
                    KeyPressed = Console.ReadKey();
                    Console.WriteLine();
                } while(!(KeyPressed.Key == ConsoleKey.Y || KeyPressed.Key == ConsoleKey.N));
                newStandardAdminAccount = KeyPressed.Key == ConsoleKey.Y;
                if(newStandardAdminAccount){
                    Console.WriteLine("Will it be a super admin account? (Y/N)");
                    do{
                        KeyPressed = Console.ReadKey();
                        Console.WriteLine();
                    } while(!(KeyPressed.Key == ConsoleKey.Y || KeyPressed.Key == ConsoleKey.N));
                    newSuperAdminAccount = KeyPressed.Key == ConsoleKey.Y;
                }
            }
        }
        string username;
        string password;
        string email = "";
        Console.WriteLine("Fill in the username:");
        do{
            username = Console.ReadLine()!;
        } while(!CheckUsernameSecurity(username));
        Console.WriteLine("Fill in the password: (Password must be 8 characters long and contain both at least one number and symbol)");
        do{
            password = Console.ReadLine()!;
        } while (!Password.CheckPasswordSecurity(password));

        do
        {
            Console.WriteLine("Enter your email address: ");
            email = Console.ReadLine()!;
        } while (!IsValidEmail(email));
        Make(username, password, email);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static bool CheckUsernameSecurity(String username){
        if(CheckUsernamesExistence(username)){
            return false;
        } else if(username == ""){
            Console.WriteLine("The username may not be empty.");
            return false;
        } else {
            return true;
        }
    }
    
    static bool IsValidEmail(string email)
    {
        // Regular expression for a simple email validation
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        Regex regex = new Regex(pattern);

        // Check if the email matches the pattern
        return regex.IsMatch(email);
    }

    public static bool CheckUsernamesExistence(String username){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        if(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){
            Console.WriteLine("This username is already in use.");
            return true;
        } else {
            return false;
        }
    }
}