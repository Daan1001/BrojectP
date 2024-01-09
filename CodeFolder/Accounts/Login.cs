public static class Login{
    public static void LogIn(String Username, string password){
        JsonFile<Account>.Read("DataSources/Accounts.json");
        for(int i = 0; i < JsonFile<Account>.listOfObjects!.Count(); i++){
            if(JsonFile<Account>.listOfObjects![i].username == Username){
                if(Password.CheckPasswordHash(password, Username)){
                    MainMenu.currentUser = JsonFile<Account>.listOfObjects[i];
                    Console.WriteLine("Succesfully logged in as \""+Username+"\"!");
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
        Console.WriteLine("Type \"Cancel\" or \"Quit\" anytime when asked for input to cancel.");
        Console.WriteLine("What is your username?");
        String? a = Console.ReadLine();
        MainMenu.Return(a!);
        Console.WriteLine("What is your password?");
        String? b = Console.ReadLine();
        MainMenu.Return(b!);
        LogIn(a!, b!);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}