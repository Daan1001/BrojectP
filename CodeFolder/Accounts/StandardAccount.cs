public class StandardAccount: Account{
    public override string AccountType { get => "StandardAccount"; }
    public StandardAccount(string username, string password): base(username, password){}
}