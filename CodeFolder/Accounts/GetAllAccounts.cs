public static class AllAccounts{
    public static List<List<Account>> Get(){
        JsonFile<StandardAccount>.Read("DataSources/Accounts.json");
        List<StandardAccount>? AllStandardAccountsInJson = JsonFile<StandardAccount>.listOfObjects;
        JsonFile<StandardAdminAccount>.Read("DataSources/Accounts.json");
        List<StandardAdminAccount>? AllStandardAdminAccountsInJson = JsonFile<StandardAdminAccount>.listOfObjects;
        JsonFile<SuperAdminAccount>.Read("DataSources/Accounts.json");
        List<SuperAdminAccount>? AllSuperAdminAccountsInJson = JsonFile<SuperAdminAccount>.listOfObjects;
        return new List<List<Account>>(){AllStandardAccountsInJson!.Cast<Account>().ToList(), AllStandardAdminAccountsInJson!.Cast<Account>().ToList(), AllSuperAdminAccountsInJson!.Cast<Account>().ToList(), };
    }
}