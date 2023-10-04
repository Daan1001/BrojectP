using System;

namespace CodeFolder
{
    public class Menu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("**Appetizers**");
            Console.WriteLine("1. Spinach and Artichoke Dip");
            Console.WriteLine("2. Garlic Parmesan Wings");
            Console.WriteLine("3. Caprese Salad");
            Console.WriteLine();

            Console.WriteLine("**Main Courses**");
            Console.WriteLine("4. Grilled Salmon");
            Console.WriteLine("5. Chicken Alfredo");
            Console.WriteLine("6. Vegetarian Stir-Fry");
            Console.WriteLine();

            Console.WriteLine("**Desserts**");
            Console.WriteLine("7. Chocolate Lava Cake");
            Console.WriteLine("8. New York Cheesecake");
            Console.WriteLine("9. Vanilla Bean Ice Cream");
            Console.WriteLine();

            Console.WriteLine("**Beverages**");
            Console.WriteLine("10. Red Wine - Merlot");
            Console.WriteLine("11. Classic Mojito");
            Console.WriteLine("12. Iced Tea");
            Console.WriteLine();

            Console.WriteLine("**Kids Menu**");
            Console.WriteLine("13. Kids Chicken Tenders");
            Console.WriteLine("14. Macaroni and Cheese");
            Console.WriteLine("15. Fruit Cup");
            Console.WriteLine();

            Console.ReadLine(); 
        }
    }
}
