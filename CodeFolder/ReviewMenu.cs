public class ReviewMenu{
    private static List<String> options = new List<string>();
    public static void Start(){
        options.Clear();
        options.Add("Show reviews");
        if(MainMenu.currentUser != null){
            options.Add("Leave a review");
        } 
        options.Add("<-- Go back");
        OptionSelection<String>.Start(ReviewMenu.options);
    }
}