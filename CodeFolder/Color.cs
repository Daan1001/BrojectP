public static class Color{
    public static void PrintAll(){
        Console.Write("dit moet ");
        Color.Black("zwart",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Black("zwart",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Blue("blauw",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Blue("blauw",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Cyan("cyaan",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Cyan("cyaan",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkBlue("darkblue",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkBlue("darkblue",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkCyan("donker cyaan",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkCyan("donker cyaan",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkGray("donker grijs",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkGray("donker grijs",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkGreen("donker groen",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkGreen("donker groen",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkMagenta("donker paars",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkMagenta("donker paars",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkRed("donker rood",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkRed("donker rood",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.DarkYellow("donker geel",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.DarkYellow("donker geel",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Gray("grijs",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Gray("grijs",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Green("groen",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Green("groen",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Magenta("paars",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Magenta("paars",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Red("rood",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Red("rood",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.White("wit",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.White("wit",false);
        Console.WriteLine(" zijn");

        Console.Write("dit moet ");
        Color.Yellow("geel",true);
        Console.WriteLine(" zijn");
        Console.Write("dit moet ");
        Color.Yellow("geel",false);
        Console.WriteLine(" zijn");
    }
    public static void Black(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Blue(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Blue;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Cyan(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Cyan;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkBlue(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkCyan(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkGray(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkGreen(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkMagenta(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkRed(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void DarkYellow(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Gray(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Gray;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Green(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Green;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Magenta(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Magenta;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Red(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Red;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void White(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.White;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static void Yellow(String text, bool background){
        if(background){
            Console.BackgroundColor = ConsoleColor.Yellow;
            Color.Black(text, false);
            Console.BackgroundColor = ConsoleColor.Black;
        } else {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}