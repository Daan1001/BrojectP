using System.Text.Json;

namespace CodeFolder.DataAccess;

using static JsonSerializer;

public class AccountsAccess
{
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
}