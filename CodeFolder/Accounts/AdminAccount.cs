public abstract class AdminAccount: Account{
    public override Boolean isAdmin {get;}
    public AdminAccount(string username, string password): base(username, password){
        isAdmin = true;
    }
}