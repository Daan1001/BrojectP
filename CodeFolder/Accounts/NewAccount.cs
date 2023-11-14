public static class NewAccount{
    private static Boolean newStandardAdminAccount = false;
    private static Boolean newSuperAdminAccount = false;

    // private static List<List<Account>>? AllAccounts;
    // private static 
    // private static  AllStandardAdminAccountsInJson;
    // private static  AllSuperAdminAccountsInJson;

    public static void Make(String username, String password){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        if(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){
            Console.WriteLine("This Username is already in use.");
            return;
        }
        
        JsonFile<Account>.Write("DataSources/Accounts.json", new Account(username, password, newStandardAdminAccount, newSuperAdminAccount));
        Console.Write("New ");
        if(newStandardAdminAccount){
            Console.Write("admin ");
        } else if(newSuperAdminAccount){
            Console.Write("super admin ");
        }
        Console.WriteLine("account created!");
    }
    public static void MakeInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        if(MainMenu.currentUser != null){
            if(MainMenu.currentUser!.isSuperAdmin){ 
                Console.WriteLine("Will it be an admin account? (Y/N)");
                ConsoleKeyInfo KeyPressed;
                do{
                    KeyPressed = Console.ReadKey();
                    Console.WriteLine();
                } while(!(KeyPressed.Key != ConsoleKey.Y || KeyPressed.Key != ConsoleKey.N));
                newStandardAdminAccount = KeyPressed.Key == ConsoleKey.Y;
                if(newStandardAdminAccount){
                    Console.WriteLine("Will it be a super admin account? (Y/N)");
                    do{
                    KeyPressed = Console.ReadKey();
                    Console.WriteLine();
                    } while(!(KeyPressed.Key != ConsoleKey.Y || KeyPressed.Key != ConsoleKey.N));
                    newSuperAdminAccount = KeyPressed.Key == ConsoleKey.Y;
                }
            }
        }
        
        Console.WriteLine("Fill in the username:");
        String username = Console.ReadLine()!;
        Console.WriteLine("Fill in the password:");
        Make(username, Console.ReadLine()!);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}