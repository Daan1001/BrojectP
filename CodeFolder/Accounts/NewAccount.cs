public static class NewAccount{
    private static Boolean newStandardAdminAccount = false;
    private static Boolean newSuperAdminAccount = false;
    public static void Make(String username, String password){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        if(JsonFile<Account>.listOfObjects!.Any(Account => Account.username == username)){
            Console.WriteLine("This Username is already in use.");
        } else {
            if(newSuperAdminAccount){
                JsonFile<Account>.Write("DataSources/Accounts.json", new SuperAdminAccount(username, password));
            } else if(newStandardAdminAccount){
                JsonFile<Account>.Write("DataSources/Accounts.json", new StandardAdminAccount(username, password));    
            } else {
                JsonFile<Account>.Write("DataSources/Accounts.json", new StandardAccount(username, password));
            }
            Console.WriteLine("New User created!");
        }
    }
    public static void MakeInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        if(MainMenu.currentUser != null){
            if(MainMenu.currentUser!.canMakeAdminAccounts){ // or MainMenu.currentUser is SuperAdminAccount
                Console.WriteLine("Will it be an admin account? (Y/N)");
                while(Console.ReadKey().Key != ConsoleKey.Y || Console.ReadKey().Key != ConsoleKey.N){
                    newStandardAdminAccount = Console.ReadKey().Key == ConsoleKey.Y;
                }
                if(newStandardAdminAccount){
                    Console.WriteLine("Will it be a super admin account? (Y/N)");
                    while(Console.ReadKey().Key != ConsoleKey.Y || Console.ReadKey().Key != ConsoleKey.N){
                        newSuperAdminAccount = Console.ReadKey().Key == ConsoleKey.Y;
                    }
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