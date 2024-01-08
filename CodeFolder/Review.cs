using Newtonsoft.Json;

public class Review{
    public String Title{get; set;}
    public String Body{get; set;}
    public Review(String Title, String Body){
        this.Title = Title;
        this.Body = Body;
    }
    public static void CreateNewReview(String Title, String Body){
        JsonFile<Review>.Read("DataSources/Reviews.json");
        JsonFile<Review>.Write("DataSources/Reviews.json", new Review(Title, Body));
    }
    public static void CreateNewReviewInput(){
        Console.Clear();
        MainMenu.AirportName();
        Console.CursorVisible = true;
        Console.WriteLine("Type \"Cancel\" or \"Quit\" anytime when asked for input to cancel.");
        Console.WriteLine("What is the subject of this review?");
        String? Subject = Console.ReadLine();
        MainMenu.Return(Subject!);
        Console.WriteLine("What is the content of this review?");
        String? Content = Console.ReadLine();
        MainMenu.Return(Content!);
        if(Subject == ""){
            Subject = "Reviewer didnt bother to fill this in.";
        }
        if(Content == ""){
            Content = "Reviewer didnt bother to fill this in.";
        }
        CreateNewReview(Subject!, Content!);
        Console.WriteLine("New Review created! (press any key to continue)");
        Console.ReadKey();
    }
    public static void ShowAllReviews(){
        Console.Clear();
        MainMenu.AirportName();
        JsonFile<Review>.Read("DataSources/Reviews.json");
        if(JsonFile<Review>.listOfObjects!.Count() > 0){
            for(int i = 0; i < JsonFile<Review>.listOfObjects!.Count(); i++){
                Console.WriteLine("Subject: "+JsonFile<Review>.listOfObjects![i].Title);
                Console.WriteLine("Contents: "+JsonFile<Review>.listOfObjects![i].Body);
                Console.WriteLine();
            }
        } else {
            Console.WriteLine("No reviews to show.");
        }
        Console.ReadKey();
    }

}