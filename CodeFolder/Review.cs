public class Review{
    public static List<Review> reviews = new List<Review>();
    public String Title{get; set;}
    public String Body{get; set;}
    public Review(String Title, String Body){
        this.Title = Title;
        this.Body = Body;
    }
    public static void CreateNewReview(String Title, String Body){
        reviews.Add(new Review(Title, Body));
        Console.WriteLine("New Review created! (press any key to continue)");
        Console.ReadKey();
    }
    public static void CreateNewReviewInput(){
        Console.CursorVisible = true;
        Console.WriteLine("What is the subject of this review?");
        String? Subject = Console.ReadLine();
        Console.WriteLine("What is the content of this review?");
        String? Content = Console.ReadLine();
        if(Subject == ""){
            Subject = "Reviewer didnt bother to fill this in.";
        }
        if(Content == ""){
            Content = "Reviewer didnt bother to fill this in.";
        }
        CreateNewReview(Subject!, Content!);
    }
    public static void ShowAllReviews(){
        if(reviews.Count() > 0){
            for(int i = 0; i < reviews.Count(); i++){
                Console.WriteLine("Subject: "+reviews[i].Title);
                Console.WriteLine("Contents: "+reviews[i].Body);
                Console.WriteLine();
            }
        } else {
            Console.WriteLine("No reviews to show.");
        }
        Console.ReadKey();
    }

}