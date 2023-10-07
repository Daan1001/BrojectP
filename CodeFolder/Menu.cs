using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json; 

public class Menu{
    private User user;
    
    private static string[] options = { "Log in", "Sign in", "Account information","Book a flight", "Leave a review", "Exit"};
    public static void Start(){
        OptionSelection.Start(Menu.options);
    }

}


