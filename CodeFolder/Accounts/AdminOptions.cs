public static class AdminOptions{
    public static void ViewAllAccount(){
        // Console.Clear();
        // MainMenu.AirportName();
        JsonFile<Account>.Read("DataSources/Accounts.json");
        // foreach(Account acc in JsonFile<Account>.listOfObjects!){
        //     Console.WriteLine(acc+"\n");
        // }
        // List<Account> testing = JsonFile<Account>.listOfObjects!;
        // List<String> test = new List<String>();
        // test.Add(testing[0].);
        // OptionSelection.Start();
        // Console.WriteLine("\nPress any key to continue...");
        // Console.ReadKey();
        OptionSelection2<Account>.Start(JsonFile<Account>.listOfObjects!, OptionSelection2<Account>.GoBack);
    }
}