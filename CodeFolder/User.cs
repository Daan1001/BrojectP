// using System.Security.Cryptography;
// using System.Text;
// // 4 derived classes:
// // -UserlessUser
// // -StandardUserUser
// // -AdminUserUser
// // -SuperAdminUserUser


// public class User{
//     public String username{get; set;}
//     public String encryptedPassword{get; set;}
//     public User(String Username, String password){
//         this.username = Username;
//         this.encryptedPassword = password;
//     }
//     public static void NewUser(String Username, String encrypted){
//         JsonFile<User>.Read("DataSources/Users.json");
//         if(JsonFile<User>.listOfObjects!.Any(User => User.username == Username)){
//             Console.WriteLine("This Username is already in use.");
//         } else {
//             JsonFile<User>.Write("DataSources/Users.json", new User(Username, encrypted));
//             Console.WriteLine("New User created!");
//         }
//     }
//     public static void NewUserInput(){
//         Console.Clear();
//         MainMenu.AirportName();
//         Console.CursorVisible = true;
//         Console.WriteLine("Fill in your Username:");
//         String Uname = Console.ReadLine()!;
//         Console.WriteLine("Fill in your password:");
//         String EncryptedPassword = Password.Encrypt(Console.ReadLine()!);
//         NewUser(Uname, EncryptedPassword);
//         Console.WriteLine("Press any key to continue");
//         Console.ReadKey();
//     }
//     public static void LogIn(String Username, string password){
//         JsonFile<User>.Read("DataSources/Users.json");
//         for(int i = 0; i < JsonFile<User>.listOfObjects!.Count(); i++){
//             if(JsonFile<User>.listOfObjects![i].username == Username){
//                 if(Password.CompareGivenPasswordWithPasswordHashFromUsername(password, Username)){
//                     MainMenu.currentUser = JsonFile<User>.listOfObjects[i];
//                     Console.WriteLine("Succesfully logged in as \""+Username+"\"!");
//                     return;
//                 } else {
//                     Console.WriteLine("Log in failed. Password is incorrect.");
//                     return;
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