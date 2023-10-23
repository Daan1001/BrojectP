public class Program
{
    static void Main()
    {
        Console.WriteLine("Please select your airplane class (business, first, or economy):");
        string airplaneClass = Console.ReadLine().ToLower();

        Menu restaurantMenu = new Menu(airplaneClass);
        restaurantMenu.DisplayMenu();
    }
}