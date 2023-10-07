public class User{
    private String username;
    private String password;
    public User(String username, String password){
        this.username = username;
        this.password = password;
    }
    public static void NewUser(){
        Console.WriteLine("Fill in your username:");
        String Uname = Console.ReadLine()!;
        Console.WriteLine("Fill in your password:");
        String Upassword = Console.ReadLine()!;
        User user = new User(Uname, Upassword);
        Console.WriteLine("New user created!");
        // StreamWriter writer = new("Accounts.json");
        // string json = JsonConvert.SerializeObject(selectedSeats);
        
    }
}