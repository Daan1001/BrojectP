using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json; 

public class MainMenu{
    public static User? user;
    private static List<String> options = new List<string>();
    public static void Start(){
        options.Clear();
        options.Add("Show flights");
        options.Add("Reviews");
        if(user == null){
            options.Add("Sign in");
            options.Add("Log in");
        } else {
            options.Add("Account information");
            options.Add("Log out");
        }
        options.Add("Airport contact details");
        options.Add("Exit");
        OptionSelection.Start(MainMenu.options);
    }

}