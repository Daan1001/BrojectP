﻿using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json; 

public class MainMenu{
    public static Account? currentUser;
    private static List<String> options = new List<string>();
    public static void Start(){
        OptionSelection<String>.selectedAccount = null;
        OptionSelection<Account>.selectedAccount = null;
        OptionSelection<Booking>.selectedAccount = null;
        options.Clear();
        options.Add("Airport contact details");
        options.Add("Book a flight");
        options.Add("Reviews");
        options.Add("See food selection");
        if(currentUser is null || currentUser.isSuperAdmin){
            options.Add("Sign up");
            if(currentUser is null){
                options.Add("Log in");
            }
        }
        if(currentUser is not null){

            options.Add("Account information");
            options.Add("My bookings");
            options.Add("Log out");
        }
        options.Add("Exit");
        OptionSelection<String>.Start(MainMenu.options);
    }
    public static void AirportName(){
        Console.WriteLine(@"__________        __    __                   .___                      _____  .__       .__  .__                      ");
        Console.WriteLine(@"\______   \ _____/  |__/  |_  ___________  __| _/____    _____        /  _  \ |__|______|  | |__| ____   ____   ______");
        Console.WriteLine(@" |       _//  _ \   __\   __\/ __ \_  __ \/ __ |\__  \  /     \      /  /_\  \|  \_  __ \  | |  |/    \_/ __ \ /  ___/");
        Console.WriteLine(@" |    |   (  <_> )  |  |  | \  ___/|  | \/ /_/ | / __ \|  Y Y  \    /    |    \  ||  | \/  |_|  |   |  \  ___/ \___ \ ");
        Console.WriteLine(@" |____|_  /\____/|__|  |__|  \___  >__|  \____ |(____  /__|_|  /    \____|__  /__||__|  |____/__|___|  /\___  >____  >");
        Console.WriteLine(@"        \/                       \/           \/     \/      \/             \/                       \/     \/     \/ ");
        Console.WriteLine();
    }
    public static void AirportDetails(){
        Console.Clear();
        MainMenu.AirportName();
        Console.WriteLine("For help:");
        Console.WriteLine("-Call: 112 or 911 (We advise 112 for quicker arrival of help)");
        Console.WriteLine("-Email: RotterdamAirport.ProjectB@gmail.com");
        Console.WriteLine();
        Console.WriteLine("Some tips for a more enjoyable stay:");
        Console.WriteLine("-Avoid the restaurants completely (they will empty your wallet before you can say 'where did my money go?')");
        Console.WriteLine("-Keep a 2m distance from everyone (to avoid any kind of bacteria from others)");
        Console.WriteLine("-Come prepared for your flight (melee weapons are recommended)");
        Console.WriteLine("-Pay for your ticket (if we find out you skipped this part, we know where you live)");
        Console.WriteLine();
    }
    public static void Return(ConsoleKeyInfo KeyPressed){
        if(KeyPressed.Key == ConsoleKey.Backspace || KeyPressed.Key == ConsoleKey.Escape){
            Console.WriteLine("This is just here so that pressing escape doesnt break the application. Don't remove this.");
            Start();
        }
    }
    public static void Return(String input){
        if(input.ToUpper() == "CANCEL" || input.ToUpper() == "QUIT"){
            Start();
        }
    }
}