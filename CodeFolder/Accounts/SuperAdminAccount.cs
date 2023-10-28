public class SuperAdminAccount: AdminAccount{
    public override Boolean canMakeAdminAccounts{get;}
    public SuperAdminAccount(string username, string password): base(username, password){
        canMakeAdminAccounts = true;
    }
}