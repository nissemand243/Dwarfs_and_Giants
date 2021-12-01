namespace SE_training.Infrastructure;
public class Rating
{   
    public int UserId { get; set; } 
    [Range(1,6)]
    public int UserRating { get; set; }
}