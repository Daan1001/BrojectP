using System.Security.Cryptography.X509Certificates;

public class Account{
    public Boolean isSuperAdmin{get; set;}
    public Boolean isAdmin{get; set;}
    public string username;
    public string? passwordHash;
    public Account(string username, string password, Boolean isAdmin, Boolean isSuperAdmin){
        this.username = username;
        if(password != null){
            this.passwordHash = Password.Encrypt(password);
        }
        this.isAdmin = isAdmin;
        this.isSuperAdmin = isSuperAdmin;
    }
}
