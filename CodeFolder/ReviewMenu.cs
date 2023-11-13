public class ReviewMenu{
    private static List<String> options = new List<string>();
    public static void Start(){
        options.Clear();
        options.Add("Show reviews");
        if(MainMenu.user != null){
            options.Add("Leave a review");
        } 
        options.Add("<-- Go back");
        OptionSelection.Start(ReviewMenu.options);
    }
}