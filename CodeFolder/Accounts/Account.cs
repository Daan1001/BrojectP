using System.Security.Cryptography.X509Certificates;

public abstract class Account{
    public virtual Boolean canMakeAdminAccounts{get;}
    public virtual Boolean isAdmin{get;}
    public string username;
    public string passwordHash;
    public Account(string username, string password){
        this.username = username;
        Console.WriteLine(password+"tests"); // testing
        // Console.WriteLine(password.Length); // testing
        // Console.WriteLine(Password.Encrypt(password)); // testing
        // this.passwordHash = Password.Encrypt(password);
        this.passwordHash = password; // dit is zonder hash
        canMakeAdminAccounts = false;
        isAdmin = false;
    }
}
