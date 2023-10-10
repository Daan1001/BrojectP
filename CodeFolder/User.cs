public class User{
    public static User BasicTestUser = new User("user", "user"); 
    private String username;
    private String password;
    public User(String username, String password){
        this.username = username;
        this.password = password;
    }
    public static void NewUser(String username, String password){
        User user = new User(username, password);
        Console.WriteLine("New user created! (press any key to continue)");
        Console.ReadKey();
        // StreamWriter writer = new("Accounts.json");
        // string json = JsonConvert.SerializeObject(selectedSeats);
    }
    public static void NewUserInput(){
        Console.CursorVisible = true;
        Console.WriteLine("Fill in your username:");
        String Uname = Console.ReadLine()!;
        Console.WriteLine("Fill in your password:");
        String Upassword = Console.ReadLine()!;
        NewUser(Uname, Upassword);
        
        
    }
    public static void LogIn(String username, string password){
        if(username == BasicTestUser.username && password == BasicTestUser.password){
            MainMenu.user = BasicTestUser;
            Console.WriteLine("Succesfully logged in as \""+username+"\"! (press any key to continue)");
            Console.ReadKey();
        } else {
            Console.WriteLine("Log in failed. (press any key to continue)");
            Console.ReadKey();
        }
    }
    public static void LogInInput(){
        Console.CursorVisible = true;
        Console.WriteLine("What is your username?");
        String? a = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? b = Console.ReadLine();
        LogIn(a!, b!);
    }
}