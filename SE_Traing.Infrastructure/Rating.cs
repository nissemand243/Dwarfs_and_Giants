using System.ComponentModel.DataAnnotations;

namespace SE_Traing.Infrastructure;
public class Rating
{   
    public int UserId { get; set; } 
    public int UserRating { get; set; }
}