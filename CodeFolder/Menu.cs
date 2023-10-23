using System;
using System.Collections.Generic;

public class Menu
{
    public List<string> Appetizers { get; set; }
    public List<string> MainCourses { get; set; }
    public List<string> Desserts { get; set; }

    public Menu(string airplaneClass)
    {
        if (airplaneClass == "business")
        {
            LoadBusinessClassMenu();
        }
        else if (airplaneClass == "first")
        {
            LoadFirstClassMenu();
        }
        else if (airplaneClass == "economy")
        {
            LoadEconomyClassMenu();
        }
        else
        {
            Console.WriteLine("Invalid airplane class selection.");
        }
    }

    private void LoadBusinessClassMenu()
    {
        Appetizers = new List<string>
        {
            "1. Spinach and Artichoke Dip",
            "2. Garlic Parmesan Wings",
            "3. Caprese Salad"
        };

        MainCourses = new List<string>
        {
            "4. Grilled Salmon",
            "5. Chicken Alfredo",
            "6. Vegetarian Stir-Fry"
        };

        Desserts = new List<string>
        {
            "7. Chocolate Lava Cake",
            "8. New York Cheesecake",
            "9. Vanilla Bean Ice Cream"
        };

    }

    private void LoadFirstClassMenu()
    {
        Appetizers = new List<string>
        {
            "1. Spinach and Artichoke Dip",
            "2. Garlic Parmesan Wings",
            "3. Caprese Salad"
        };

        MainCourses = new List<string>
        {
            "4. Grilled Salmon",
            "5. Chicken Alfredo",
            "6. Lobster Bisque"
        };

        Desserts = new List<string>
        {
            "7. Chocolate Lava Cake",
            "8. New York Cheesecake",
            "9. Tiramisu"
        };
        
    }

    private void LoadEconomyClassMenu()
    {
        Appetizers = new List<string>
        {
            "1. Chips and Salsa",
            "2. Vegetable Spring Rolls",
            "3. Mixed Nuts"
        };

        MainCourses = new List<string>
        {
            "4. Chicken Stir-Fry",
            "5. Beef Stroganoff",
            "6. Veggie Burger"
        };

        Desserts = new List<string>
        {
            "7. Apple Pie",
            "8. Chocolate Chip Cookies",
            "9. Fruit Salad"
        };
    }

    public void DisplayMenu()
    {
        Console.WriteLine("**Appetizers**");
        foreach (string item in Appetizers)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n**Main Courses**");
        foreach (string item in MainCourses)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n**Desserts**");
        foreach (string item in Desserts)
        {
            Console.WriteLine(item);
        }
        
    }
}


