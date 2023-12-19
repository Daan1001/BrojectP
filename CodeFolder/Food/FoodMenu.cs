using System.ComponentModel;
using System.Reflection;

public enum EconomyMenu{
    [Description("Grilled chicken with roasted vegetables and mashed potatoes")]
    Chicken=1,
    [Description("Pasta primavera with tomato and basil sauce")]
    Pasta,
    [Description("Beef stir-fry with steamed rice")]
    Beef,
    [Description("Mixed green salad with balsamic vinaigrette")]
    Salad
}
public enum BusinessMenu{
    [Description("Lemon herb grilled chicken with quinoa and sautéed vegetables")]
    Chicken,
    [Description("Seared beef tenderloin with red wine reduction, roasted potatoes, and asparagus")]
    Beef,
    [Description("Spinach and ricotta-stuffed ravioli in a sun-dried tomato cream sauce")]
    Ravioli,
    [Description("Caesar salad with grilled shrimp")]
    Salad
}
public enum FirstClassMenu{
    [Description("Pan-seared Chilean sea bass with saffron risotto and grilled asparagus")]
    Bass,
    [Description("Grilled filet mignon with truffle mashed potatoes, haricots verts, and red wine reduction")]
    Filet,
    [Description("Butternut squash and sage-stuffed roulade with port wine reduction")]
    Squash,
    [Description("Lobster bisque with herb-infused croutons")]
    Lobster
}

public class FoodMenu{
    public static void PrintMenuDescriptions<TEnum>(){
        int Count = 0;
        foreach (TEnum value in Enum.GetValues(typeof(TEnum))){
            Count++;
            FieldInfo field = value.GetType().GetField(value.ToString()!)!;
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
            Console.WriteLine($"{Count}. {value}: {attribute.Description}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}