using System.Text.Json;

namespace CodeFolder.DataAccess;

using static JsonSerializer;

public class AccountsAccess
{
    private static AccountModel _account;

    public static void AddtoList(AccountModel NewAccount)
    {
        List<AccountModel> _accounts = new List<AccountModel>();
        
        _accounts.Add(NewAccount);
    }

    public static void WriteToJson(AccountModel account)
    {
        JsonFile<AccountModel>.Read("DataSources/Accounts.json");
        JsonFile<AccountModel>.Write("DataSources/Accounts.json", account);
        
    }

    
    
    //searches for accounts by username  
    public static AccountModel GetAccount(string Username)
    {
        List<AccountModel> accountlist;
        accountlist = JsonFile<AccountModel>.Read("DataSources/Accounts.json");

        foreach (var account in accountlist)
        {
            if (account.Name == Username)
            {
                AccountModel _account = account;
            }
        }
        return _account;
    }
}