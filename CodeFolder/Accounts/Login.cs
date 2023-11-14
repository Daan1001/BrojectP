// public static class Login{
//     public static void LogIn(String Username, string password){
//         List<List<Account>> ShouldBeAllAccounts = AllAccounts.Get();
//         for(int listNumber = 0; listNumber < ShouldBeAllAccounts.Count(); listNumber++){
//             for(int objectNumber = 0; objectNumber < ShouldBeAllAccounts[listNumber].Count(); objectNumber++){
//                 if(ShouldBeAllAccounts[listNumber][objectNumber].username == Username){
//                     // if(Password.CompareGivenPasswordWithPasswordHashFromUsername(password, Username)){
//                     if(ShouldBeAllAccounts[listNumber][objectNumber].passwordHash == Password.Encrypt(password)){
//                         MainMenu.currentUser = ShouldBeAllAccounts[listNumber][objectNumber];
//                         Console.WriteLine("Succesfully logged in as \""+Username+"\"!");
//                         return;
//                     } else {
//                         Console.WriteLine("Log in failed. Password is incorrect.");
//                         return;
//                     }
//                 }
//             }
//         }
//         Console.WriteLine("Log in failed. User doesn't exist.");
//     }
//     public static void LogInInput(){
//         Console.Clear();
//         MainMenu.AirportName();
//         Console.CursorVisible = true;
//         Console.WriteLine("What is your Username?");
//         String? a = Console.ReadLine();
//         Console.WriteLine("What is your password?");
//         String? b = Console.ReadLine();
//         LogIn(a!, b!);
//         Console.WriteLine("Press any key to continue");
//         Console.ReadKey();
//     }
// }

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
        Console.WriteLine("What is your Username?");
        String? a = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? b = Console.ReadLine();
        LogIn(a!, b!);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}