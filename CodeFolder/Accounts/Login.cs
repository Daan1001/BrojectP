public static class Login{
    public static void LogIn(String Username, string Password){
        // JsonFile<Account>.Read("DataSources/Accounts.json");
        for(int listNumber = 0; listNumber < AllAccounts.Get().Count(); listNumber++){
            for(int objectNumber = 0; objectNumber < AllAccounts.Get()[listNumber].Count(); objectNumber++){
                if(AllAccounts.Get()[listNumber][objectNumber].username == Username){
                    // if(Password.CompareGivenPasswordWithPasswordHashFromUsername(password, Username)){
                    if(AllAccounts.Get()[listNumber][objectNumber].passwordHash == Password){ // dit is zonder hash
                        MainMenu.currentUser = AllAccounts.Get()[listNumber][objectNumber];
                        Console.WriteLine("Succesfully logged in as \""+Username+"\"!");
                        return;
                    } else {
                        Console.WriteLine("Log in failed. Password is incorrect.");
                        return;
                    }
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