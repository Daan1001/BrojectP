public static class Login<T>{
    public static void LogIn(String Username, string password){
        JsonFile<T>.Read("DataSources/Accounts.json");
        for(int i = 0; i < JsonFile<T>.listOfObjects!.Count(); i++){
            Account account = JsonFile<Account>.listOfObjects[i];
            //if (type == "StandardAccount")
            //if(JsonFile<T>.listOfObjects![i].username == Username){
            if(account.username == Username){
                if(Password.CompareGivenPasswordWithPasswordHashFromUsername(password, Username)){
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
        Console.WriteLine("What is your Username?");
        String? a = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? b = Console.ReadLine();
        LogIn(a!, b!);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}